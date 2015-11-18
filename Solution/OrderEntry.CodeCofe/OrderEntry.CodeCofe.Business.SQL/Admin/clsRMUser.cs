using OrderEntry.CodeCofe.DataAccess;
using System.Data;

namespace OrderEntry.CodeCofe.Business.SQL.Admin
{
    public class clsRMUser
    {
        clsConfigurationManager _ConfigurationManager = new clsConfigurationManager();

        public DataTable sp_Get_RMUser_Email(string pEmail)
        {
            ParamStruct[] vParam = new ParamStruct[1];

            vParam[0].ParamName = "@Email";
            vParam[0].DataType = DbType.String;
            vParam[0].value = pEmail;

            return _ConfigurationManager.ExecDataSet("sp_Get_RMUser_Email", vParam);
        }

        public void sp_Save_RMUser(string pEmail, string pPassword)
        {
            ParamStruct[] vParam = new ParamStruct[2];

            vParam[0].ParamName = "@Email";
            vParam[0].DataType = DbType.String;
            vParam[0].value = pEmail;

            vParam[1].ParamName = "@Password";
            vParam[1].DataType = DbType.String;
            vParam[1].value = pPassword;

            _ConfigurationManager.ExecSql("sp_Save_RMUser", vParam);
        }


    }
}
