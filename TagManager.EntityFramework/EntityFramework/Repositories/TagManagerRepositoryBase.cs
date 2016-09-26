using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace TagManager.EntityFramework.Repositories
{
    public abstract class TagManagerRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<TagManagerDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TagManagerRepositoryBase(IDbContextProvider<TagManagerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class TagManagerRepositoryBase<TEntity> : TagManagerRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TagManagerRepositoryBase(IDbContextProvider<TagManagerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
