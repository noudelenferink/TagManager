using Abp.Domain.Services;
using System.Collections.Generic;

namespace TagManager.MediaItems
{
   public interface ITagManager : IDomainService
   {
      List<PreprocessedTag> PreprocessTags(List<string> tags);
      List<ProcessedGroup> ProcessTags(List<string> tags);
    }
}
