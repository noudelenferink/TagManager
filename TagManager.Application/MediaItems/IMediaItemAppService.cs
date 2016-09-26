namespace TagManager.MediaItems
{
    using Abp.Application.Services;
    using Abp.Application.Services.Dto;
    using System.Collections.Generic;
    using global::TagManager.MediaItems.Dto;

    public interface IMediaItemAppService : IApplicationService
    {
        PagedResultOutput<MediaItemListOutput> GetMediaItemList(MediaItemListInput input);
        MediaItemOutput GetMediaItem(MediaItemInput input);
        List<PreprocessedTagOutput> GetPreprocessedTags(MediaItemInput input);
        List<ProcessedGroupOutput> ProcessTags(ProcessTagsInput input);
        bool SaveMediaTags(SaveMediaTagsInput input);
        List<RelevantMediaItem> GetRelevantMediaItems(MediaItemInput input);
    }

}
