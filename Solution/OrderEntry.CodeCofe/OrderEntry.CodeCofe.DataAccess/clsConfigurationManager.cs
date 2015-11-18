using System;
using System.Data;
using System.Data.SqlClient;

namespace OrderEntry.CodeCofe.DataAccess
{
   public class clsConfigurationManager
    {
        public static string _glCadenaConexion;
        public static string _glProveedor;

        public clsConfigurationManager()
        {
            SetStringConnection();
        }

        private static void SetStringConnection()
        {
            string vLlave, vServer, vDataBase, vUser, vPassword, vSeguridad, vProveedor;

            vLlave = System.Configuration.ConfigurationManager.AppSettings["APLICACION"];           

            vServer = System.Configuration.ConfigurationManager.AppSettings["SERVER"];
            if (vServer == "") vServer = String.Empty;

            vDataBase = System.Configuration.ConfigurationManager.AppSettings["DATABASE"];
            if (vDataBase == "") vDataBase = String.Empty;

            vUser = System.Configuration.ConfigurationManager.AppSettings["USER"];
            if (vUser == "") vUser = String.Empty;

            vPassword = System.Configuration.ConfigurationManager.AppSettings["PASSWORD"];
            if (vPassword == "") vPassword = String.Empty;

            vSeguridad = System.Configuration.ConfigurationManager.AppSettings["SEGURIDAD"];
            if (vSeguridad == "") vSeguridad = String.Empty;

            vProveedor = System.Configuration.ConfigurationManager.AppSettings["PROVEEDOR"];
            if (vProveedor == "") _glProveedor = String.Empty;

            if (vProveedor == "SQL")
            {

                if (vSeguridad == "1")
                {  
                    _glCadenaConexion = "Data Source=" + vServer + "; " + "Initial Catalog=" + vDataBase + "; " + "User Id=" + vUser + "; " + "Password=" + vPassword + ";";
                }
                else if (vSeguridad == "2")
                {
                    _glCadenaConexion = "Data Source=" + vServer + "; " + "Initial Catalog=" + vDataBase + "; " + "Integrated Security=SSPI;";
                }
            }

            _glProveedor = vProveedor;
        }

        public void Dispose()
        {
            _glCadenaConexion = String.Empty; 
            _glProveedor = String.Empty;
        }

        public static IDbDataParameter GetParameter(string pParamName, ParameterDirection pParamDirection, object pParamValue, DbType pParamtype, string pSourceColumn, Int16 pSize)
        {
            IDbDataParameter param = new SqlParameter();
            param.ParameterName = pParamName;
            param.DbType = pParamtype;
            if (pSize > 0)
            {
                param.Size = pSize;
            }
            if (pParamValue != null)
            {
                param.Value = pParamValue;
            }
            param.Direction = pParamDirection;
            if (pSourceColumn != "")
            {
                param.SourceColumn = pSourceColumn;
            }
            return param;
        }
       
        public DataTable ExecDataSet(string pSql, ParamStruct[] pParameterArray)
        {
            SqlConnection vCon = new SqlConnection(_glCadenaConexion);
            SqlCommand vCmd = new SqlCommand(pSql, vCon);
            vCmd.CommandType = CommandType.StoredProcedure;
            
            if (pParameterArray != null)
            {
                for (int i = 0; i < pParameterArray.Length; i++)
                {
                    ParamStruct vParam = pParameterArray[i];
                    IDbDataParameter vPm = GetParameter(vParam.ParamName, vParam.direction, vParam.value, vParam.DataType, vParam.sourceColumn, vParam.size);                   
                    vCmd.Parameters.Add(vPm);
                }
            }

            vCon.Open();

            DataSet vDs = new DataSet();
            SqlDataAdapter vDa = new SqlDataAdapter();
            vDa.SelectCommand = vCmd;          
            vDa.Fill(vDs);
            vCon.Close();

            if (vDs != null & vDs.Tables.Count > 0)            
                return vDs.Tables[0];            
            else            
                return null;            

        }
        
        public void ExecSql(string pSql, ParamStruct[] pParameterArray)
        {
            SqlConnection pCon = new SqlConnection(_glCadenaConexion);
            SqlCommand pCmd = new SqlCommand(pSql, pCon);
            pCmd.CommandType = CommandType.StoredProcedure;

            if (pParameterArray != null)
            {
                for (int i = 0; i < pParameterArray.Length; i++)
                {
                    ParamStruct vPs = pParameterArray[i];
                    IDbDataParameter vPm = GetParameter(vPs.ParamName, vPs.direction, vPs.value, vPs.DataType, vPs.sourceColumn, vPs.size);                  
                    pCmd.Parameters.Add(vPm);
                }
            }

            pCon.Open();
            pCmd.ExecuteNonQuery();
            pCon.Close();
        }
    }

   public struct ParamStruct
   {
       public string ParamName;
       public DbType DataType;
       public object value;
       public ParameterDirection direction;
       public string sourceColumn;
       public Int16 size;
   }
}
