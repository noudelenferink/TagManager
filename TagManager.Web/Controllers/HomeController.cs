using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace TagManager.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : TagManagerControllerBase
    {
        public ActionResult Index()
        {
            return View("~/Index.cshtml"); //Layout of the angular application.
        }
	}
}