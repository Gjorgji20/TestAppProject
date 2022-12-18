using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestAppProject.Model
{
    public class DbContext
    {
       
        private SqlConnection cnn;
        private string connetionString = "Data Source=DESKTOP-09Q6SHE\\SQLEXPRESS;Initial Catalog=Project_Client;User ID=sa;Password=Gjorgji1@";
        private List<Client> _clients = new List<Client>();
        public List<Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;              
            }
        }
        public DbContext()
        {

        }

        public List<Client> GetAllClinets()
        {
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                DataSet dt = new DataSet();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "GetClient";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(dt, "test");


                if (dt.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        Client client = new Client();
                        client.Name = dt.Tables[0].Rows[i]["Name"].ToString();
                        client.Address = dt.Tables[0].Rows[i]["Address"].ToString();
                        client.BirthDate = dt.Tables[0].Rows[i]["BirthDate"].ToString();
                        Clients.Add(client);
                    }
                }
                return Clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't open connection!");
                return null;

            }
            finally
            {
                cnn.Close();
            }
        }

        public int InserClients(Client client)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-09Q6SHE\\SQLEXPRESS;Initial Catalog=Project_Client;User ID=sa;Password=Gjorgji1@";
            cnn = new SqlConnection(connetionString);
            int returnvalue =0;
            try
            {

                cnn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "InsertClient";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar, 256)).Value = client.Name;
                cmd.Parameters.Add(new SqlParameter("@ClientAddress", SqlDbType.NVarChar, 256)).Value = client.Address;
                cmd.Parameters.Add(new SqlParameter("@ClientBirthDate", SqlDbType.NVarChar, 256)).Value = client.BirthDate;
                cmd.Parameters.Add("@return_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                returnvalue = int.Parse(cmd.Parameters["@return_value"].Value.ToString());

                return returnvalue;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
                return returnvalue;
            }
            finally
            {
                cnn.Close();
            }
        }

    }
}
