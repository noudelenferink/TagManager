namespace TagManager.MediaItems
{
    using System.Collections.Generic;

    public class ProcessedGroup
    {
        public int GroupId { get; set; }
        public List<ProcessedTag> Tags { get; set; }
    }
}
