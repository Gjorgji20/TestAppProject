using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestAppProject.ViewModel;

namespace TestAppProject
{
    /// <summary>
    /// Interaction logic for GetWindow.xaml
    /// </summary>
    public partial class GetWindow : Window
    {
        public GetWindow(bool isFromApi)
        {
            InitializeComponent();
            DataContext = new GetWindowViewModel(isFromApi, ImportPg);
        }
        public Window ImportPg => this.importPg;
    }
}
