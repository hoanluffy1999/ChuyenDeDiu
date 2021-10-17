

using System;

using System.Web.Mvc;
using System.Web;
using System.Web.Mvc.Filters;

namespace ChuyenDeDiu.Models
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class CustomAuthenAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        private string _actionCode;
      
        public CustomAuthenAttribute(string actionCode = "")
        {
            this._actionCode = actionCode;
            
        }
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            NhanVien account = (NhanVien)HttpContext.Current.Session["Account"];


            //var permission = SessionExtension.Get<List<Role>>(session, Utils.SessionExtensions.SesscionPermission);
            if (account == null)
            {
                HttpContext.Current.Response.Redirect("/account/login");
            }

            else
            {
                

            }
            
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
          
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {


            NhanVien account = (NhanVien)HttpContext.Current.Session["Account"];


            //var permission = SessionExtension.Get<List<Role>>(session, Utils.SessionExtensions.SesscionPermission);
            if (account == null)
            {
                HttpContext.Current.Server.Transfer("Account/login");
            }

            else
            {
                if (_actionCode == "NoCheck")
                {

                }
                else
                {
                    //var path = context.HttpContext.Request.Path.Value.ToString();

                    ////var controller = new ControllerContext().ActionDescriptor.ControllerName;
                    //var controller = actionDescriptor.ControllerTypeInfo.CustomAttributes.FirstOrDefault().ConstructorArguments[0].Value.ToString();//lấy  tên controll
                    //var exist = permission.Where(c => c.MenuUrl.ToString().ToLower().Contains(controller)).ToList();

                    //if (exist.Count == 0)
                    //{
                    //    context.Result = new RedirectToRouteResult(
                    //new RouteValueDictionary(
                    //    new
                    //    {
                    //        controller = "Error",
                    //        action = "NoPermission"
                    //    }
                    //    ));
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(_actionCode))
                    //    {
                    //        var control = exist.Find(c => c.ActionCode == _actionCode);
                    //        if (control == null)
                    //        {
                    //            context.Result = new RedirectToRouteResult(
                    //        new RouteValueDictionary(
                    //            new
                    //            {
                    //                controller = "Error",
                    //                action = "NoPermission"
                    //            }
                    //            ));
                    //        }
                    //    }
                    //    else
                    //    {

                    //    }
                    //}
                }

            }
        }
    }
}

