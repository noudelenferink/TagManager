﻿namespace TagManager.MediaItems.Dto
{
    using Abp.AutoMapper;
    using System.Collections.Generic;

   [AutoMapFrom(typeof(ProcessedTag))]
   public class ProcessedTagOutput
   {
        public int Id { get; set; }
        public string Value { get; set; }
        public MediaTag MediaTag { get; set; }
        public int? DuplicateTagId { get; set; }
        public int? SynonymTagId { get; set; }
        public int? RelatedTagId { get; set; }
        public bool IsDuplicateReference { get; set; }
        public bool IsRelatedReference { get; set; }
        public bool IsSpelledCorrect { get; set; }
        public int Occurences { get; internal set; }
    }
}