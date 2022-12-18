using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestAppProject.Model;

namespace TestAppProject.ViewModel
{
    public class ImportFromXMLViewModel
    {
        private readonly DelegateCommand _goToBack;
        public ICommand CloseCommand => _goToBack;

        private List<Client> _clients=new List<Client>();
        public List<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }
        private Window _page;
        public ImportFromXMLViewModel(Window page)
        {
            ImportFromXmlMethod();
            _page=page;
            _goToBack = new DelegateCommand(GoToBack);
        }
        public void GoToBack()
        {
            _page.Close();
        }

        public void ImportFromXmlMethod()
        {
            var ds = new DataSet();
            ds.ReadXml(@"C:/Users/PC/source/repos/TestAppProject/TestAppProject/Clients.xml");
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
              
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Client c = new Client();
                    string add = "";
                    
                    c.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    c.BirthDate = ds.Tables[0].Rows[i]["BirthDate"].ToString();


                    for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                    {

                        if (int.Parse(ds.Tables[0].Rows[i]["Client_Id"].ToString()) == int.Parse(ds.Tables[2].Rows[j]["Addresses_Id"].ToString()))
                        {
                            add += (ds.Tables[2].Rows[j]["Address_Text"].ToString()) + "\n";
                        }

                    }
                    c.Address = add;
                    Clients.Add(c);
                }               
            }
          

        }
    }
}
