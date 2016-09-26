namespace TagManager.MediaItems
{
    using Abp.Application.Services.Dto;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using global::TagManager.MediaItems.Dto;
    using System;

    public class MediaItemAppService : IMediaItemAppService
    {
        private readonly IRepository<MediaItem> mediaItemRepository;
        private readonly IRepository<MediaTag> mediaTagRepository;
        private readonly IRepository<MediaItemMediaTag> mediaItemMediaTagRepository;
        private readonly ITagManager tagManager;

        private const int PageSize = 20;

        public MediaItemAppService(IRepository<MediaItem> mediaItemRepository, IRepository<MediaTag> mediaTagRepository, IRepository<MediaItemMediaTag> mediaItemMediaTagRepository,ITagManager tagManager)
        {
            this.mediaItemRepository = mediaItemRepository;
            this.mediaTagRepository = mediaTagRepository;
            this.mediaItemMediaTagRepository = mediaItemMediaTagRepository;
            this.tagManager = tagManager;
        }

        public PagedResultOutput<MediaItemListOutput> GetMediaItemList(MediaItemListInput input)
        {
            if (input.Page < 1)
            {
                input.Page = 1;
            }

            var pageIndex = input.Page - 1;
            var query = this.mediaItemRepository.GetAll().OrderBy(mi => mi.CreationTime);

            var result = new PagedResultOutput<MediaItemListOutput>();
            result.TotalCount = query.Count();

            var mediaItems = query.Skip(pageIndex * PageSize).Take(PageSize).ToList();
            result.Items = mediaItems.MapTo<List<MediaItemListOutput>>();

            return result;
        }

        public MediaItemOutput GetMediaItem(MediaItemInput input)
        {
            var mediaItem = this.mediaItemRepository.Get(input.Id);
            return mediaItem.MapTo<MediaItemOutput>();
        }

        public List<PreprocessedTagOutput> GetPreprocessedTags(MediaItemInput input)
        {
            var mediaItem = this.mediaItemRepository.Get(input.Id);
            var tags = mediaItem.Tags.Select(t => t.TagValue).ToList();
            var processedTags = this.tagManager.PreprocessTags(tags);
            return processedTags.MapTo<List<PreprocessedTagOutput>>();
        }

        public List<ProcessedGroupOutput> ProcessTags(ProcessTagsInput input)
        {
            var processedGroups = this.tagManager.ProcessTags(input.Tags);
            return processedGroups.MapTo<List<ProcessedGroupOutput>>();
        }

        public bool SaveMediaTags(SaveMediaTagsInput input)
        {
            var mediaItem = this.mediaItemRepository.Get(input.MediaItemId);

            if (mediaItem == null)
            {
                return false;
            }

            foreach (var mediaTag in input.MediaTags)
            {
                if (!mediaTag.Id.HasValue)
                {
                    mediaTag.Id = this.mediaTagRepository.InsertAndGetId(new MediaTag { Type = mediaTag.Type, Value = mediaTag.Value });
                }

                this.mediaItemMediaTagRepository.Insert(new MediaItemMediaTag { MediaItemId = input.MediaItemId, MediaTagId = mediaTag.Id.Value });
            }

            mediaItem.StatusId = MediaItemStatus.Processed;

            return true;
        }

        public List<RelevantMediaItem> GetRelevantMediaItems(MediaItemInput input)
        {
            // Get all the media tags that are associated with the given media item.
            var mediaTagIds = this.mediaItemMediaTagRepository.GetAll().Where(mimt => mimt.MediaItemId == input.Id).Select(mimt => mimt.MediaTag.Id);

            // Find all the media items that have multiple matching media tags.
            var matchingItemIds = this.mediaItemMediaTagRepository.GetAll()
                .Where(mimt => mimt.MediaItemId != input.Id && mediaTagIds.Contains(mimt.MediaTagId))
                .Select(mimt => mimt.MediaItemId)
                .GroupBy(mi => mi, mi => mi, (key, group) => new { MediaItemId = key, Occurences = group.Count() })
                .Where(mi => mi.Occurences > 1)
                .Select(mi => mi.MediaItemId);

            var list = (from mi in this.mediaItemRepository.GetAll()
                        where matchingItemIds.Contains(mi.Id)
                        select new RelevantMediaItem
                        {
                            Id = mi.Id,
                            Title = mi.Title,
                            Description = mi.Description,
                            StatusId = mi.StatusId,
                            TypeId = mi.TypeId
                        }).ToList();

            list.ForEach(mi => mi.MediaTags = 
                this.mediaItemMediaTagRepository.GetAll()
                    .Where(mimt => mimt.MediaItemId == mi.Id && mediaTagIds.Contains(mimt.MediaTagId))
                    .Select(mimt => new MediaTagDto {
                        Id=  mimt.MediaTag.Id,
                        Type = mimt.MediaTag.Type,
                        Value = mimt.MediaTag.Value
                    }).ToList());

            return list;
        }
    }
}
