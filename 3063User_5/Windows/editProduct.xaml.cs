using _3063User_5.BD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Microsoft.Win32;
using _3063User_5.Pages;

namespace _3063User_5.Windows
{
    /// <summary>
    /// Логика взаимодействия для editPro.xaml
    /// </summary>
    public partial class editProduct : Window
    {
        public static BD.user6Entities connection = Class.Class2.GetUser6Entities();

        public BD.Product SelectedProduct { get; set; }
        public List<BD.Category> category { get; set; }
        public List<BD.Manufacturer> manufacturers { get; set; }
        public List<BD.Provider> providers { get; set; }
        public List<BD.Unit> units { get; set; }
        public editProduct(BD.Product product)
        {
            InitializeComponent();
            SelectedProduct = product;
            category = new List<BD.Category>(connection.Category.ToList());
            CategoryCB.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = category });
            manufacturers = new List<BD.Manufacturer>(connection.Manufacturer.ToList());
            ManufacturerCombo.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = manufacturers });
            providers = new List<BD.Provider>(connection.Provider.ToList());
            ProviderCB.SetBinding(ComboBox.ItemsSourceProperty, new Binding() {Source= providers});
            units = new List<BD.Unit>(connection.Unit.ToList());
            UnitCB.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = units });
            DataContext = this;
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.SaveChanges();
                MessageBox.Show("Данные обновлены");
                EditGrid.GetBindingExpression(DataContextProperty).UpdateTarget();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog addImage = new OpenFileDialog();
            addImage.Multiselect = false;
            if (addImage.ShowDialog().Value== true) 
            {
                SelectedProduct.ProductPhoto= "\\Materials\\" +addImage.SafeFileName;
                Img.GetBindingExpression(Image.SourceProperty).UpdateTarget();
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            foreach (BD.OrderProduct order in connection.OrderProduct) 
            {
                if (order.Product.ProductArticleNumber == SelectedProduct.ProductArticleNumber) 
                {
                    MessageBox.Show("Товар заказан и присутствует в списке заказанных товаров!");
                    return;
                }
            }
            connection.Product.Remove(SelectedProduct);
            AdminCatalog.Products.Remove(SelectedProduct);
            MessageBox.Show("Данные удалены");
            connection.SaveChanges();
            this.Close();
        }
    }
}
