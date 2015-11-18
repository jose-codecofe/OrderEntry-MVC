using OrderEntry.CodeCofe.Business.SQL.Admin;
using OrderEntry.CodeCofe.Web.Models;
using OrderEntry.CodeCofe.Web.Util;
using System.Data;
using System.Web.Mvc;

namespace OrderEntry.CodeCofe.Web.Controllers
{
    public class AutentificationController : Controller
    {
        clsRMUser _clsUser = new clsRMUser();
        clsUtilitary _clsUtil = new clsUtilitary();
   
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string pEmail, string pPassword)
        {
            string vMessage = string.Empty;

            DataTable vData = _clsUser.sp_Get_RMUser_Email(pEmail);
            UserModels vModel = _clsUtil.MaperOnlyUser(vData);           

            if(vModel != null)
            {
                if (_clsUtil.Decrypt(vModel.Password).Trim() != pPassword.Trim())                 
                    vMessage = "Password is incorrect.";
            }else
                vMessage = "No existing user.";

            return Json(vMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUser(string pEmail, string pPassword)
        {
            _clsUser.sp_Save_RMUser(pEmail,_clsUtil.Encrypt(pPassword));

            _clsUtil.SendEmail("Registration confirmation", "Congratulations your user is active.", pEmail);

            return Json("Saved Correctly", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgotPassword(string pEmail)
        {
            string vMessage = string.Empty;

            DataTable vData = _clsUser.sp_Get_RMUser_Email(pEmail);
            UserModels vModel = _clsUtil.MaperOnlyUser(vData);

            if (vModel != null)
            {                
                _clsUtil.SendEmail("Your password", string.Format("Mr. your password is '{0}' ", _clsUtil.Decrypt(vModel.Password)), pEmail);
                vMessage = "We send an email with your password";
            }
            else
                vMessage = "No existing user.";

            return Json(vMessage, JsonRequestBehavior.AllowGet);
        }
	}
}