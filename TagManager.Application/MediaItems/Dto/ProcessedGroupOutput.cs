namespace TagManager.MediaItems.Dto
{
    using Abp.AutoMapper;
    using System.Collections.Generic;

    [AutoMapFrom(typeof(ProcessedGroup))]
    public class ProcessedGroupOutput
    {
        public int GroupId { get; set; }
        public List<ProcessedTagOutput> Tags { get; set; }
    }
}
