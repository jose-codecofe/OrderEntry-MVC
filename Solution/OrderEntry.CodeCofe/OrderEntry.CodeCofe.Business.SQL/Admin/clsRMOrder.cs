using OrderEntry.CodeCofe.DataAccess;
using System;
using System.Data;

namespace OrderEntry.CodeCofe.Business.SQL.Admin
{
    public class clsRMOrder
    {
        clsConfigurationManager _ConfigurationManager = new clsConfigurationManager();

        public DataTable sp_Get_RMOrder_ID(int pId)
        {
            ParamStruct[] vParam = new ParamStruct[1];

            vParam[0].ParamName = "@Id";
            vParam[0].DataType = DbType.Int32;
            vParam[0].value = pId;

            return _ConfigurationManager.ExecDataSet("sp_Get_RMOrder_ID", vParam);
        }

        public void sp_Save_RMOrder(int pID, string pOrderNumber, string pCustomerName, string pDeliveryAddress, DateTime pOrderDate)
        {
            ParamStruct[] vParam = new ParamStruct[5];

            vParam[0].ParamName = "@Id";
            vParam[0].DataType = DbType.Int32;
            vParam[0].value = pID;

            vParam[1].ParamName = "@OrderNumber";
            vParam[1].DataType = DbType.String;
            vParam[1].value = pOrderNumber;

            vParam[2].ParamName = "@CustomerName";
            vParam[2].DataType = DbType.String;
            vParam[2].value = pCustomerName;

            vParam[3].ParamName = "@DeliveryAddress";
            vParam[3].DataType = DbType.String;
            vParam[3].value = pDeliveryAddress;

            vParam[4].ParamName = "@OrderDate";
            vParam[4].DataType = DbType.DateTime;
            vParam[4].value = pOrderDate;

            _ConfigurationManager.ExecSql("sp_Save_RMOrder", vParam);
        }

        public void sp_Delete_Order(int pID)
        {
            ParamStruct[] vParam = new ParamStruct[1];

            vParam[0].ParamName = "@OrderId";
            vParam[0].DataType = DbType.Int32;
            vParam[0].value = pID;

            _ConfigurationManager.ExecDataSet("sp_Delete_Order", vParam);
        }
    }
}
