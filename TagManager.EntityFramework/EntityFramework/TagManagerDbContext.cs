using System.Data.Common;
using Abp.Zero.EntityFramework;
using TagManager.Authorization.Roles;
using TagManager.MultiTenancy;
using TagManager.Users;
using TagManager.MediaItems;
using System.Data.Entity;

namespace TagManager.EntityFramework
{
    public class TagManagerDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public virtual IDbSet<MediaItem> MediaItems { get; set; }
        public virtual IDbSet<MediaItemMediaTag> MediaItemMediaTag { get; set; }
        public virtual IDbSet<MediaTag> MediaTags { get; set; }
        public virtual IDbSet<Tag> Tags { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public TagManagerDbContext()
              : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in TagManagerDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of TagManagerDbContext since ABP automatically handles it.
         */
        public TagManagerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public TagManagerDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
