using OrderEntry.CodeCofe.Business.SQL.Admin;
using OrderEntry.CodeCofe.Web.Models;
using OrderEntry.CodeCofe.Web.Util;
using System.Data;
using System.Web.Mvc;

namespace OrderEntry.CodeCofe.Web.Controllers
{
    public class OrderController : Controller
    {
        clsRMOrder _clsOrder = new clsRMOrder();
        clsUtilitary _clsUtil = new clsUtilitary();

        public ActionResult Index(int id)
        {
            DataTable vData = _clsOrder.sp_Get_RMOrder_ID(id);
            OrderModels vModel = _clsUtil.MaperOnlyOrder(vData);

            return View(vModel);
        }

        [HttpPost]
        public ActionResult SaveOrder(OrderModels pModel)
        {
            _clsOrder.sp_Save_RMOrder(pModel.ID, pModel.OrderNumber, pModel.CustomerName, pModel.DeliveryAddress, pModel.OrderDate);

             return Json(pModel, JsonRequestBehavior.AllowGet);            
        }

        public ActionResult ReturnOrders(OrderModels pModel)
        {
            bool vIsUpdate = _clsUtil.CheckUpdateOrder(pModel);

            return Json(vIsUpdate.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}