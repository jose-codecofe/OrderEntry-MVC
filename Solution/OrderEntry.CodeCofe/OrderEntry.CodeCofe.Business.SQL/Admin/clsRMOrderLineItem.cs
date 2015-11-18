using OrderEntry.CodeCofe.DataAccess;
using System;
using System.Data;

namespace OrderEntry.CodeCofe.Business.SQL.Admin
{
    public class clsRMOrderLineItem
    {
        clsConfigurationManager _ConfigurationManager = new clsConfigurationManager();

        public DataTable sp_Get_RMOrderLineItem()
        {
            return _ConfigurationManager.ExecDataSet("sp_Get_RMOrderLineItem", null);
        }

        public DataTable sp_Get_RMOrderLineItem_Date(DateTime pDate)
        {
            ParamStruct[] vParam = new ParamStruct[1];

            vParam[0].ParamName = "@Date";
            vParam[0].DataType = DbType.DateTime;
            vParam[0].value = pDate;

            return _ConfigurationManager.ExecDataSet("sp_Get_RMOrderLineItem_Date", vParam);
        }
    }
}
