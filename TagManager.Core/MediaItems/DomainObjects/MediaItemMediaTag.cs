using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagManager.MediaItems
{
   public class MediaItemMediaTag : FullAuditedEntity
   {
      [Required]
      [ForeignKey("MediaItem")]
      public int MediaItemId { get; set; }

      [Required]
      [ForeignKey("MediaTag")]
      public int MediaTagId { get; set; }

      public string Position { get; set; }

      public virtual MediaItem MediaItem { get; set; }

      public virtual MediaTag MediaTag { get; set; }
   }
}