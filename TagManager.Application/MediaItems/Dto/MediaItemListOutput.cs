using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TagManager.MediaItems.Dto
{
    [AutoMapFrom(typeof(MediaItem))]
    public class MediaItemListOutput : EntityDto
    {
        public string Title { get; set; }

        public MediaItemStatus StatusId { get; set; }
        public MediaItemType TypeId { get; set; }
    }
}