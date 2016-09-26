using TagManager.EntityFramework;
using EntityFramework.DynamicFilters;

namespace TagManager.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly TagManagerDbContext _context;

        public InitialHostDbBuilder(TagManagerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
