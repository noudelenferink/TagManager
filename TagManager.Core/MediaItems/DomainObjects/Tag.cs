using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagManager.MediaItems
{
   public class Tag : FullAuditedEntity
   {
      [Required]
      [ForeignKey("MediaItem")]
      public int MediaItemId { get; set; }

      [Required]
      public string TagValue { get; set; }

      public virtual MediaItem MediaItem { get; set; }
   }
}