using Abp.AutoMapper;

namespace TagManager.MediaItems.Dto
{
   [AutoMapFrom(typeof(Tag))]
   public class TagDto
   {
      public string TagValue { get; set; }
   }
}