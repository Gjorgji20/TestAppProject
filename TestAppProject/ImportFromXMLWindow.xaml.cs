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
    /// Interaction logic for ImportFromXMLWindow.xaml
    /// </summary>
    public partial class ImportFromXMLWindow : Window
    {
        public ImportFromXMLWindow()
        {
            InitializeComponent();
            DataContext = new ImportFromXMLViewModel(ImportPg);
        }
        public Window ImportPg => this.importPg;
    }
}
