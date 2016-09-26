using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using TagManager.EntityFramework;

namespace TagManager.Migrator
{
    [DependsOn(typeof(TagManagerDataModule))]
    public class TagManagerMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<TagManagerDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}