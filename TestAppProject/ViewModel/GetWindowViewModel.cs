using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Newtonsoft.Json;
using Prism.Commands;
using TestAppProject.Model;

namespace TestAppProject.ViewModel
{
    public class GetWindowViewModel:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private readonly DelegateCommand _goToBack;
        public ICommand CloseCommand => _goToBack;


        private List<Client> _clients = new List<Client>();
        public List<Client> Clients
        {
            get => _clients;
            set { _clients = value;
                OnPropertyChanged("Clients");
            }
        }
        private Window _page;

        private bool  _isFromApi;
        public GetWindowViewModel(bool isFromApi, Window page)
        {
            _isFromApi = isFromApi;
            _page=page;
            _goToBack = new DelegateCommand(GoToBack);
            GetClient(_isFromApi);          
        }

        public void GoToBack()
        {
            _page.Close();
        }
        public async void GetClient(bool _isFromApi)
        {

            if (!_isFromApi)
            {
                DbContext dbContext = new DbContext();
                Clients = dbContext.GetAllClinets();
            }
            else
            {
                ApiContext apiContext = new ApiContext();
                Clients = await apiContext.GetAllClinets();
            }
        }
    }   
}
