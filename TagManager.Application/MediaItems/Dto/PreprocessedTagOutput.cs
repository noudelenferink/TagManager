using Abp.AutoMapper;
using System.Collections.Generic;

namespace TagManager.MediaItems.Dto
{
   [AutoMapFrom(typeof(PreprocessedTag))]
   public class PreprocessedTagOutput
   {
      public string TagValue { get; set; }
      public List<string> Suggestions { get; set; }
      public bool IsSpelledCorrect { get; set; }
   }
}