using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestAppProject.Model;

namespace TestAppProject.ViewModel
{
    internal class InsertWindowViewModel : INotifyPropertyChanged
    {    

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private readonly DelegateCommand _insertClient;
        public ICommand SaveCommand => _insertClient;

        private readonly DelegateCommand _goToBack;
        public ICommand CloseCommand => _goToBack;

        private bool _isInsertFromApi;


        private string _msgName;
        public string MsgName
        {
            get => _msgName;
            set
            {
                _msgName = value;
                OnPropertyChanged("MsgName");
            }
        }
        private string _msgAddress;
        public string MsgAddress
        {
            get => _msgAddress;
            set
            {
                _msgAddress = value;
                OnPropertyChanged("MsgAddress");
            }
        }
        private string _msgBirthDate;
        public string MsgBirthDate
        {
            get => _msgBirthDate;
            set
            {
                _msgBirthDate = value;
                OnPropertyChanged("MsgBirthDate");
            }
        }


        private bool _enableBtn = false;
        public bool EnableBtn
        {
            get => _enableBtn;
            set
            {
                _enableBtn = value;
                OnPropertyChanged("EnableBtn");
            }
        }
        private bool _enableAddress = false;
        public bool EnableAddress
        {
            get => _enableAddress;
            set
            {
                _enableAddress = value;
                OnPropertyChanged("EnableAddress");
            }
        }
        private bool _enableBDate = false;
        public bool EnableBDate
        {
            get => _enableBDate;
            set
            {
                _enableBDate = value;
                OnPropertyChanged("EnableBDate");
            }
        }



        private Window _page;
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;

                if (value.Length < 6 || value.Length > 50)
                {
                    MsgName = "Short Name! The name have to contains between 6 and 50 charachters ";
                    EnableAddress = false;
                    EnableBDate = false;
                    EnableBtn = false;
                }
                else
                {
                    MsgName = "";
                    EnableAddress = true;
                }
                OnPropertyChanged("Name");

            }
        }
        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                if (value.Length < 15 || value.Length > 150)
                {
                    MsgAddress = "Short Name! Invalid length of Address!";
                    EnableBDate = false;
                    EnableBtn = false;
                }
                else
                {
                    MsgAddress = "";
                    EnableBDate = true;
                    EnableBtn = true;
                        

                }
                OnPropertyChanged("Address");
            }
        }
        private DateTime _dateBirth = DateTime.Now;
        public DateTime DateBirth
        {
            get => _dateBirth;
            set
            {
                _dateBirth = value;
                if (value.ToString("dd.MM.yyyy").Length != 10)
                {
                    MsgBirthDate = "Select date";
                    EnableBtn = false;
                }
                else
                {
                    MsgBirthDate = "";
                    EnableBtn = true;
                }
                OnPropertyChanged("DateBirth");
            }
        }

        public InsertWindowViewModel(bool isInsertFromApi, Window page)
        {
            _isInsertFromApi = isInsertFromApi;
            _page = page;
            _goToBack = new DelegateCommand(GoToBack);
            _insertClient = new DelegateCommand(async () => await SaveClient(isInsertFromApi));
            
        }

        public void GoToBack()
        {
            _page.Close();
        }
        public async Task SaveClient(bool isInsertFromApi)
        {
            Client client = new Client();
            client.Name = Name;
            client.Address = Address;
            client.BirthDate = DateBirth.ToString("dd.MM.yyyy");
            if (!isInsertFromApi)
            {
                DbContext db = new DbContext();
                int res = db.InserClients(client);
                if (res == 1)
                {                   
                    
                    MessageBox.Show("Success added");
                }
                else if (res == -1)
                {
                    MessageBox.Show("This client exist");
                }
            }
            else
            {
                ApiContext apiContext = new ApiContext();
                int res = await apiContext.InsetClients(client);
                if (res == 1){

                    MessageBox.Show("Success added");
                }else if(res==-1)
                {
                    MessageBox.Show("This client exist");

                }

            }
        }

    }
}
