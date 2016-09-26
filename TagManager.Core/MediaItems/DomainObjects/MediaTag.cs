using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace TagManager.MediaItems
{
   public class MediaTag : FullAuditedEntity
   {
      [Required]
      public MediaTagType Type { get; set; }

      [Required]
      public string Value { get; set; }
   }
}