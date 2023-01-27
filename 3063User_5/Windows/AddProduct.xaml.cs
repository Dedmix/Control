using _3063User_5.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Microsoft.Win32;

namespace _3063User_5.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public static BD.user6Entities connection = Class.Class2.GetUser6Entities();

        public BD.Product newProduct { get; set; }

        public List<BD.Category> categories { get; set; }
        public List<BD.Manufacturer> manufacturer { get; set; }
        public List<BD.Unit> units { get; set; }
        public List<BD.Provider> providers { get; set; }
        public AddProduct()
        {
            InitializeComponent();
            newProduct = new BD.Product();  
            categories = new List<BD.Category>(connection.Category.ToList());
            CategoryCB.SetBinding(ComboBox.ItemsSourceProperty, new Binding() {Source = categories});
            manufacturer = new List<BD.Manufacturer>(connection.Manufacturer.ToList());
            ManufacturerCB.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source=manufacturer});
            providers = new List<BD.Provider>(connection.Provider.ToList());
            SupplierCB.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = providers});
            units = new List<BD.Unit>(connection.Unit.ToList());
            UnitCombo.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = units});
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminCatalog.connection.Product.Add(newProduct);
            if (AdminCatalog.connection.SaveChanges() == 1)
            {
                AdminCatalog.Products.Add(newProduct);
                newProduct= new BD.Product();
                Add.GetBindingExpression(DataContextProperty).UpdateTarget();
                MessageBox.Show("Товар добавлен");
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            if(openFile.ShowDialog().Value == true )
            {
                newProduct.ProductPhoto = "\\Materials\\" + openFile.SafeFileName;
            }
        }
    }
}
