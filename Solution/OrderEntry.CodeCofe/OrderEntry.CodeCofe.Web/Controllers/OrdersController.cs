using OrderEntry.CodeCofe.Business.SQL.Admin;
using OrderEntry.CodeCofe.Web.Models;
using OrderEntry.CodeCofe.Web.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace OrderEntry.CodeCofe.Web.Controllers
{
    public class OrdersController : Controller
    {
        clsRMOrderLineItem _clsOrdenLine = new clsRMOrderLineItem();
        clsRMOrder _clsOrder = new clsRMOrder();
        clsUtilitary _clsUtil = new clsUtilitary();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrders()
        {
            DataTable vData = _clsOrdenLine.sp_Get_RMOrderLineItem();
            List<OrdersModels> vModel = _clsUtil.MaperOrders(vData);

            return Json(vModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrdersByDate(DateTime pDate)
        {
            DataTable vData = _clsOrdenLine.sp_Get_RMOrderLineItem_Date(pDate);
            List<OrdersModels> vModel = _clsUtil.MaperOrders(vData);

            return Json(vModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteOrder(int pId)
        {
            _clsOrder.sp_Delete_Order(pId);

            return Content("OK", "text/plain");
        }
    }
}