using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestAppProject.Model;

namespace TestAppProject.ViewModel
{
    public class MainWidnowViewModel:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly DelegateCommand _navigationToInsertWindow;
        public ICommand InsertCommand => _navigationToInsertWindow;

        private readonly DelegateCommand _navigationToInsertWindowApi;
        public ICommand InsertApiCommand => _navigationToInsertWindowApi;

        
        private readonly DelegateCommand _navigationToGetWindow;
        public ICommand LoadCommand => _navigationToGetWindow;
        

        private readonly DelegateCommand _navigationToGetWindowApi;
        public ICommand LoadApiCommand => _navigationToGetWindowApi;

        private readonly DelegateCommand _exportClientJSON;
        public ICommand ExportClientsCommand => _exportClientJSON;

        private readonly DelegateCommand _importClientsXml;
        public ICommand ImportClientsCommand => _importClientsXml;     

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public MainWidnowViewModel()
        {
            _navigationToInsertWindow = new DelegateCommand(()=>ToInsertWindow(false));
            _navigationToInsertWindowApi = new DelegateCommand(() => ToInsertWindow(true));

            _exportClientJSON = new DelegateCommand(ExportToJSON);
            _importClientsXml = new DelegateCommand(ImportFromXML);


            _navigationToGetWindow = new DelegateCommand(()=>ToGetWindow(false));
            _navigationToGetWindowApi = new DelegateCommand(() => ToGetWindow(true));


        }
        public void ToInsertWindow(bool isFromApi)
        {
            
            InsertWindow insertWindow = new InsertWindow(isFromApi);
            insertWindow.Show();           
        }
        public void ToGetWindow(bool isFromApi)
        {
            GetWindow getWindow = new GetWindow(isFromApi);
            getWindow.Show();
        }

        public void ExportToJSON()
        {
            try
            {
                DbContext dbContext = new DbContext();
                List<Client> clients = dbContext.GetAllClinets();
            //    clients = clients.OrderBy(x => x.Name).ToList();
                Client p = null;
              
                for(int i=0; i < clients.Count()-1; i++)
                {
                    for(int j=i+1;j<clients.Count();j++)
                    {
                        if (clients[i].Name.CompareTo(clients[j].Name)==1)
                        {
                            p = clients[i];
                            clients[i]=clients[j];
                            clients[j] = p;
                        }
                    }
                }
                var json = JsonConvert.SerializeObject(clients);
                File.WriteAllText(@"C:/Users/PC/source/repos/TestAppProject/TestAppProject/Clients.json", json);
                MessageBox.Show("Successful import in JSON file");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with import in JSON file");
            }

        }

        public void ImportFromXML()
        {
            ImportFromXMLWindow importFromXMLWindow = new ImportFromXMLWindow();
            importFromXMLWindow.Show();
        }
    }
}
