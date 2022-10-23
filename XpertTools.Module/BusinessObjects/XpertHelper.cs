using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XpertTools.Module.BusinessObjects
{
    public class XpertHelper
    {
        public static string connectionString = "";
        public static string APP_NAME_CODE = "XERP";
        //
        //this.parameters = Parameters.GetInstance(base.Session);
        //public static Parameters GetInstance(DevExpress.Xpo.Session session)
        //{
        //    XPCollection<Parameters> parameters = new XPCollection<Parameters>(PersistentCriteriaEvaluationBehavior.InTransaction, session, null);
        //    Parameters parameter = null;
        //    if (parameters.Count != 0)
        //    {
        //        parameter = parameters.FirstOrDefault<Parameters>();
        //    }
        //    else
        //    {
        //        parameter = new Parameters(session);
        //        parameter.Save();
        //    }
        //    return parameter;
        //}
        public static bool IsNullOrEmpty(object value)
        {
            if (value is System.DBNull) return true;
            else if (value == null) return true;
            else if (value is IList)
            {
                return (value as IList).Count == 0;
            }
            else if (string.IsNullOrEmpty(value.ToString())) return true;
            return false;
        }

        public static bool CheckIsMachineServer()
        {
            string LocalMachineName = Get_LocalMachineName().ToString();
            string ServerName = Get_ServerName().ToString();
            if (LocalMachineName.Equals(ServerName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetServeurIP(string strHostName)
        {
            //try to set server host in XpertApplication variable
            var host = Dns.GetHostEntry(strHostName);
            string ipLocalNetwork = string.Empty;

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipLocalNetwork = ip.ToString();
                    break;
                }
            }

            return ipLocalNetwork;
        }

        public static string PeriodePaie { get; set; }
        public static object Get_ServerName()
        {
            string serverName = ServerName.Split(new Char[] { '\\' })[0].Split(new Char[] { ',' })[0];
            serverName = (serverName == "." || serverName == "(local)" || serverName == "localhost") ? Environment.MachineName : serverName;
            return serverName.ToLower();
        }
        public static string ServerName
        {
            get
            {
                System.Data.SqlClient.SqlConnectionStringBuilder decoder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                return decoder.DataSource;
            }
        }
        public static string DBName
        {
            get
            {
                System.Data.SqlClient.SqlConnectionStringBuilder decoder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                return decoder.InitialCatalog;
            }
        }

        public static object Get_LocalMachineName()
        {
            return Environment.MachineName.ToLower();
        }

        public static Version Get_AssemblyInfo()
        {
            return Assembly.GetEntryAssembly().GetName().Version;
        }
        //public static string Get_FileVersionInfo() 
        //{
        //    return FileVersionInfo.FileVersion;
        //}
        public static string GetMD5Hash(object row, string[] keys)
        {
            if (keys == null) return "";
            string value = XpertHelper.Concat(row, keys);
            return XpertHelper.GetMD5Hash(value);
        }
        public static string GetMD5Hash(string value)
        {
            // step 1, calculate MD5 hash from input
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(value);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }




        public static string Concat(object row, string[] keys)
        {
            if (keys == null) return "";
            string key, value = "";
            for (int i = 0; i < keys.Length; i++)
            {
                key = keys[i];
                value += XpertHelper.GetString(XpertHelper.GetValue(row, key));
            }
            return value;
        }

        public static string GetString(object value)
        {
            if (value == null) return "";
            if ((value is decimal) || (value is float) || (value is double))
            {
                decimal val = Math.Round(Convert.ToDecimal(value), 2);
                return val.ToString("n2");
            }
            return Convert.ToString(value);
        }
        public static object GetValue(object obj, string field)
        {
            if (obj == null) return null;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (pi.Name.Equals(field))
                {
                    object V = pi.GetValue(obj, null);
                    return V;
                }
            }
            return null;
        }


        public static bool IsNotNullAndNotEmpty(object value)
        {
            return !IsNullOrEmpty(value);
        }
        public static decimal GetValuePourcentage(decimal value, decimal p)
        {
            return value * p / 100;
        }
        public static decimal GetPourcentageFromValue(decimal value, decimal mt)
        {
            decimal res = 0;
            if (mt != 0)
            {
                res = value * 100 / mt;

            }
            return res;
        }
        public static decimal RoundMontant(decimal montant)
        {
            return Math.Round(montant, 2);
        }
        public static DateTime GetDate()
        {
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] == null)
            {
                return DateTime.Now;
            }
            return DateTime.Now;
            //    string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //    IDataLayer DataLayer = XpoDefault.GetDataLayer(ConnectionString, AutoCreateOption.DatabaseAndSchema);
            //    using (UnitOfWork unitOfWork = new UnitOfWork(DataLayer)
            //    {
            //          try
            //            {
            //                DateTime date = (DateTime)unitOfWork.ExecuteScalar("SELECT GETDATE() AS Result");
            //                return date;
            //            }
            //            catch (Exception)
            //            {
            //                return DateTime.Now;
            //            }
            //}
        }
        public static DateTime? GetDateTime(object date)
        {
            return GetDateTime(date, "yyyyMMdd");
        }
        public static DateTime? GetDateTime(object date, string format)
        {
            try
            {
                if (date == null) return null;

                if ((date is DateTime?) || (date is DateTime)) return Convert.ToDateTime(date);
                if (date is string)
                {
                    string ff = date.ToString().Trim();
                    if (string.IsNullOrWhiteSpace(ff)) return null;
                    try
                    {
                        return Convert.ToDateTime(date);
                    }
                    catch
                    {
                        try
                        {
                            return DateTime.ParseExact(ff, format, CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            return null;
                        }
                    }


                }
                return null;
            }
            catch
            {
                //logger.Error(String.Format("Impossible de convertir la date {0} avec le format {1}", date.ToString(), format), ex);
                return null;
            }
        }



        public enum typeCompareVersion { UnknownSav, UnknownApp, Larger, Equal, Smaller };



    }

}
