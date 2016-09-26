using System.Collections.Generic;

namespace TagManager.MediaItems
{
   public class PreprocessedTag
   {
      public bool IsSpelledCorrect { get; internal set; }
      public List<string> Suggestions { get; internal set; }
      public string TagValue { get; internal set; }
   }
}