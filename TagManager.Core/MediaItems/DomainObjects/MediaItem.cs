using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TagManager.MediaItems
{
   public class MediaItem : FullAuditedEntity
   {
      [Required]
      public string Title { get; set; }

      public string Description { get; set; }

      public int ExternalId { get; set; }

      public MediaItemType TypeId { get; set; }

      public string FileName { get; set; }

      [Required]
      public MediaItemStatus StatusId { get; set; }

      public virtual ICollection<Tag> Tags { get; set; }
   }
}