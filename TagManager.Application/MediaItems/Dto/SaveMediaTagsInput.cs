using System.Collections.Generic;

namespace TagManager.MediaItems.Dto
{
    public class SaveMediaTagsInput
    {
        public int MediaItemId { get; set; }
        public List<MediaTagDto> MediaTags { get; set; }
    }
}