using Abp.Web.Mvc.Views;

namespace TagManager.Web.Views
{
    public abstract class TagManagerWebViewPageBase : TagManagerWebViewPageBase<dynamic>
    {

    }

    public abstract class TagManagerWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected TagManagerWebViewPageBase()
        {
            LocalizationSourceName = TagManagerConsts.LocalizationSourceName;
        }
    }
}