using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace WcfService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public XmlDocument GetEmployees()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-16\" ?>\n";

            try
            {
                //string stConnectionOLEDB = @"Data Source=HSSSC1PCL01198\SQLSERVER2014;Initial Catalog=AdventureWorks2014;Provider=SQLNCLI11.0;Integrated Security=SSPI;";
                string stConnectionOLEDB = @"Data Source=.\SQLExpress2014;Initial Catalog=AdventureWorks2014;Integrated Security=SSPI;";
                string stQueryText = "SELECT NationalIDNumber,[LoginID],JobTitle,BirthDate,[MaritalStatus],[Gender],[HireDate] FROM HumanResources.Employee WHERE NationalIDNumber In(10708100) FOR XML RAW('Employee'),ROOT('Employees'),ELEMENTS;";
                using (SqlConnection sqlcon = new SqlConnection(stConnectionOLEDB))
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(stQueryText, sqlcon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        xml = reader[0].ToString();
                    }
                    sqlcon.Close();
                }
            }
            catch
            {
                // return dt;

            }

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            return xmldoc;
        }
    }
}
