using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace TagManager.MediaItems.Dto
{
    [AutoMapFrom(typeof(MediaItem))]
    public class MediaItemOutput : EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public MediaItemType TypeId { get; set; }
        public MediaItemStatus StatusId { get; set; }
        public string FileName { get; set; }
        public int ExternalId { get; set; }

        public List<TagDto> Tags { get; set; }
    }
}