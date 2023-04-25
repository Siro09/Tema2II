using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Tema3II
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
        SqlConnection myCon = new SqlConnection();
        
        
        [WebMethod(Description="This method creates an account")]
        public void CreateAccount(string name,string pass)
        {
            // connection string
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\costi\OneDrive\Desktop\Projects\An3\Sem2\II\lab4\Tema3II\Tema3II\App_Data\Database1.mdf;Integrated Security=True";
            myCon.Open();

            // Account account = new Account(name, pass);
            // make query
            string query = "INSERT INTO Credentials(Name,Password)";
            query += " VALUES (@Name,@Password)";
            // add params 
            SqlCommand myCommand = new SqlCommand(query, myCon);
            myCommand.Parameters.AddWithValue("@Name", name);
            myCommand.Parameters.AddWithValue("@Password", pass);
            myCommand.ExecuteNonQuery();
            myCon.Close();
        }
        [WebMethod(Description = "This method change an account's password")]
        public bool ChangeCredentials(string name,string passOld,string passNew)
        {
            // connection string
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\costi\OneDrive\Desktop\Projects\An3\Sem2\II\lab4\Tema3II\Tema3II\App_Data\Database1.mdf;Integrated Security=True";
            // make query
            string query = "SELECT * FROM Credentials WHERE Name=@Name";
            // add params 
            SqlCommand myCommand1 = new SqlCommand(query, myCon);
            myCommand1.Parameters.AddWithValue("@Name", name);
            myCon.Open();
            SqlDataReader reader= myCommand1.ExecuteReader();
            int idAcc;
            string nameAcc;
            string passAcc="";
            if (reader.Read())
            {
                 idAcc = reader.GetInt32(0);
                 nameAcc = reader.GetString(1);
                 passAcc = reader.GetString(2);
            }
            reader.Close();
            if(passOld.Equals(passAcc))
            {
                // make query
                string query2 = "UPDATE Credentials SET Password=@Pass WHERE Name=@Name";
                // add params 
                SqlCommand myCommand2 = new SqlCommand(query2, myCon);
                myCommand2.Parameters.AddWithValue("@Pass", passNew);
                myCommand2.Parameters.AddWithValue("@Name", name);
                myCommand2.ExecuteNonQuery();
                myCon.Close();
                return true;
            }
            myCon.Close();
            return false;
        }

        [WebMethod(Description = "This method checks the login credentials")]
        public bool Login(string name, string pass)
        {
            // connection string
            myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\costi\OneDrive\Desktop\Projects\An3\Sem2\II\lab4\Tema3II\Tema3II\App_Data\Database1.mdf;Integrated Security=True";
            // make query
            string query = "SELECT * FROM Credentials WHERE Name=@Name";
            // add params 
            SqlCommand myCommand1 = new SqlCommand(query, myCon);
            myCommand1.Parameters.AddWithValue("@Name", name);
            myCon.Open();
            SqlDataReader reader = myCommand1.ExecuteReader();
            int idAcc;
            string nameAcc;
            string passAcc = "";
            if (reader.Read())
            {
                idAcc = reader.GetInt32(0);
                nameAcc = reader.GetString(1);
                passAcc = reader.GetString(2);
                if(passAcc.Equals(pass))
                {
                    reader.Close();
                    myCon.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    myCon.Close();
                    return false;
                }
            }
            else
            {
                reader.Close();
                myCon.Close();
                return false;
            }
            
        }
    }
}
