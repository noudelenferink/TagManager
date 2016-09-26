using System.Collections.Generic;

namespace TagManager.MediaItems.Dto
{
    public class RelevantMediaItem
    {
        public string Title { get; internal set; }
        public MediaItemType TypeId { get; set; }
        public MediaItemStatus StatusId { get; set; }
        public string Description { get; set; }
        public List<MediaTagDto> MediaTags { get; set; }
        public int Id { get; internal set; }
    }
}