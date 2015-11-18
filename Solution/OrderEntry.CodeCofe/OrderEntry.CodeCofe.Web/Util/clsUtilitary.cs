using OrderEntry.CodeCofe.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace OrderEntry.CodeCofe.Web.Util
{
    public class clsUtilitary
    {
        #region Orders

        public List<OrdersModels> MaperOrders(DataTable pData)
        {
            List<OrdersModels> vListOrder = new List<OrdersModels>();

            if (pData != null)
            {
                if (pData.Rows.Count > 0)
                {
                    for (int i = 0; i < pData.Rows.Count; i++)
                    {
                        DataRow vRow = pData.Rows[i];

                        vListOrder.Add(new OrdersModels
                        {
                            ID = vRow["ID"] != null ? Convert.ToInt32(vRow["ID"].ToString()) : 0,
                            OrderDate = Convert.ToDateTime(vRow["OrderDate"]),
                            OrderNumber = vRow["OrderNumber"] != null ? vRow["OrderNumber"].ToString() : string.Empty,
                            CustomerName = vRow["CustomerName"] != null ? vRow["CustomerName"].ToString() : string.Empty,
                            DeliveryAddress = vRow["DeliveryAddress"] != null ? vRow["DeliveryAddress"].ToString() : string.Empty,
                            QuantityOrdered = Convert.ToDecimal(vRow["QuantityOrdered"])
                        });
                    }
                }
            }

            return vListOrder;
        }

        #endregion

        #region Order

        public OrderModels MaperOnlyOrder(DataTable pData)
        {
            OrderModels vOrder = null;

            if (pData != null)
            {
                if (pData.Rows.Count > 0)
                {
                    for (int i = 0; i < pData.Rows.Count; i++)
                    {
                        DataRow vRow = pData.Rows[i];

                        vOrder = new OrderModels
                        {
                            ID = vRow["ID"] != null ? Convert.ToInt32(vRow["ID"].ToString()) : 0,
                            OrderNumber = vRow["OrderNumber"] != null ? vRow["OrderNumber"].ToString() : string.Empty,
                            CustomerName = vRow["CustomerName"] != null ? vRow["CustomerName"].ToString() : string.Empty,
                            DeliveryAddress = vRow["DeliveryAddress"] != null ? vRow["DeliveryAddress"].ToString() : string.Empty,
                            OrderDate = Convert.ToDateTime(vRow["OrderDate"])
                        };
                    }
                }
            }

            return vOrder;
        }

        public bool CheckUpdateOrder(OrderModels pOrder)
        {
            bool vIsUpdate = false;

            if (pOrder.ID > 0)
            {
                OrderModels vOrderDB = MaperOnlyOrder(new Business.SQL.Admin.clsRMOrder().sp_Get_RMOrder_ID(pOrder.ID));

                if (Equals(pOrder.ID, vOrderDB.ID) &&
                    Equals(pOrder.CustomerName, vOrderDB.CustomerName) &&
                    Equals(pOrder.DeliveryAddress, vOrderDB.DeliveryAddress) &&
                    Equals(pOrder.OrderDate, vOrderDB.OrderDate) &&
                    Equals(pOrder.OrderNumber, vOrderDB.OrderNumber))
                    vIsUpdate = false;
                else
                    vIsUpdate = true;
            }

            return vIsUpdate;
        }

        #endregion

        #region User

        public UserModels MaperOnlyUser(DataTable pData)
        {
            UserModels vUser = null;

            if (pData != null)
            {
                if (pData.Rows.Count > 0)
                {
                    for (int i = 0; i < pData.Rows.Count; i++)
                    {
                        DataRow vRow = pData.Rows[i];

                        vUser = new UserModels
                        {
                            ID = vRow["ID"] != null ? Convert.ToInt32(vRow["ID"].ToString()) : 0,
                            Email = vRow["Email"] != null ? vRow["Email"].ToString() : string.Empty,
                            Password = vRow["Password"] != null ? vRow["Password"].ToString() : string.Empty
                        };
                    }
                }
            }

            return vUser;
        }

        #endregion

        #region Email

        public void SendEmail(string pSubject, string pDetall, string pEmailDestination)
        {
            System.Net.Mail.MailMessage vMmsg = new System.Net.Mail.MailMessage();

            string vEmailServer = System.Configuration.ConfigurationManager.AppSettings["EMAILSERVER"];
            string vEmailPsw = System.Configuration.ConfigurationManager.AppSettings["EMAILPSW"];
            string vHost = System.Configuration.ConfigurationManager.AppSettings["EMAILHOST"];

            vMmsg.To.Add(pEmailDestination);
            string vSubject = pSubject;
            vMmsg.Subject = vSubject;
            vMmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            vMmsg.Body = pDetall;
            vMmsg.BodyEncoding = System.Text.Encoding.UTF8;
            vMmsg.IsBodyHtml = false;

            vMmsg.From = new System.Net.Mail.MailAddress(vEmailServer);

            System.Net.Mail.SmtpClient vCliente = new System.Net.Mail.SmtpClient();

            vCliente.Credentials =
                new System.Net.NetworkCredential(vEmailServer, vEmailPsw);
            vCliente.Host = vHost;

            try
            {                 
                vCliente.Send(vMmsg);
                vCliente.Dispose();
            }
            catch (System.Net.Mail.SmtpException ex)
            {
            }
        }

        #endregion

        #region Security

        public string Encrypt(string pInput)
        {
            string vResult = string.Empty;
            byte[] vEncryted = System.Text.Encoding.Unicode.GetBytes(pInput);
            vResult = Convert.ToBase64String(vEncryted);
            return vResult;
        }

        public string Decrypt(string pInput)
        {
            string vResult = string.Empty;
            byte[] vDecryted = Convert.FromBase64String(pInput);
            vResult = System.Text.Encoding.Unicode.GetString(vDecryted);
            return vResult;
        }

        #endregion
    }
}