using System.Linq;
using TagManager.EntityFramework;
using TagManager.MultiTenancy;

namespace TagManager.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly TagManagerDbContext _context;

        public DefaultTenantCreator(TagManagerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
