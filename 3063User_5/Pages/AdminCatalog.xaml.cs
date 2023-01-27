using _3063User_5.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3063User_5.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminCatalog.xaml
    /// </summary>
    public partial class AdminCatalog : Page
    {
        Windows.AddProduct addProduct;
        Windows.editProduct editPr;
        public class CostSort
        {
            public string DisplayNeme { get; set; }
            public string PropertyNeme { get; set; }
            public bool Ascending { get; set; }
        }
        public static List<CostSort> CostSorts { get; set; } = new List<CostSort>()
        {
            new CostSort(){DisplayNeme="Без соритировки", PropertyNeme=null, Ascending=true},
            new CostSort(){DisplayNeme="По возрастанию цены", PropertyNeme="ProductCost", Ascending=true},
            new CostSort(){DisplayNeme="По убыванию цены", PropertyNeme="ProductCost",Ascending=false},
        };
        public static BD.user6Entities connection = Class.Class2.GetUser6Entities();
        public static ObservableCollection<BD.Product> Products;
        public static ObservableCollection<BD.Manufacturer> Manufacturers;
        int viewcount = 0;
        int totalcount = 0;

        
        public AdminCatalog()
        {
            InitializeComponent();
            Loci();
            Products = new ObservableCollection<BD.Product>(connection.Product.ToList());
            Manufacturers = new ObservableCollection<BD.Manufacturer>(connection.Manufacturer.ToList());
            Manufacturers.Insert(0, new BD.Manufacturer() { NameManufacturer = "Все производители" });
            ProductList.ItemsSource = Products; 
            A3.ItemsSource = Manufacturers;
            A2.SelectedIndex= 0;
            A3.SelectedIndex= 0;
            viewcount = Products.Count;
            totalcount = Products.Count;
            LabelCount.Content = $"{viewcount}/{totalcount}";
            DataContext = this;
        }
        public void Loci() //показывает кто авторизался
        {
            var log = Class.Class2.Login;
            var user1 = Class.Class2.GetUser6Entities().User.Where(u => u.UserLogin == log).ToList();
            listUser.ItemsSource = user1;
        }
        private void A1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search(A1.Text.Trim());
        }
        private void Search(string sub)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource);
            if (view == null) return;
            viewcount = 0;
            view.Filter = new Predicate<object>(obj =>
            {
                bool IsView = ((BD.Product)obj).ProductDescription.ToLower().Contains(sub.ToLower());
                if (IsView) viewcount++;
                return IsView;
            });
            LabelCount.Content = $"{viewcount}/{totalcount}";
        }

        private void A2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CostSort costSort = A2.SelectedItem as CostSort;
            Sort(costSort.PropertyNeme, costSort.Ascending);
        }

        private void Sort(string prop, bool asc)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource); 
            if (view == null) return;
            view.SortDescriptions.Clear();
            if (prop!= null) 
            {
                view.SortDescriptions.Add(new SortDescription(prop,asc ? ListSortDirection.Ascending : ListSortDirection.Descending));
            }
        }
        private void Filter(string manufacturer)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource); 
            if (view == null) return;
            viewcount = 0;
            view.Filter = new Predicate<object>(obj =>
            {
                if (manufacturer == "Все производители")
                {
                    viewcount = Products.Count;
                    return true;
                }  
                bool IsView = ((BD.Product)obj).ProductManufacturer == manufacturer;
                if (IsView) viewcount++;
                return IsView;
            });
            LabelCount.Content = $"{viewcount}/{totalcount}";
        }

        private void A3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BD.Manufacturer manufacturer = A3.SelectedItem as BD.Manufacturer;
            Filter(manufacturer.NameManufacturer);
        }

        private void A4_Click(object sender, RoutedEventArgs e)
        {
            if (addProduct == null)
            {
                addProduct = new Windows.AddProduct();
            }
            addProduct.Closed += addProduct_Closed;
            addProduct.ShowDialog();
        }
        private void addProduct_Closed(object sender, EventArgs e)
        {
            addProduct = null;
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (editPr != null) return;
            editPr = new editProduct(ProductList.SelectedItem as BD.Product);
            editPr.Closed += editPr_Closed;
            editPr.Topmost= true;
            editPr.Show();
            ProductList.SelectedItem = -1;
        }
        private void editPr_Closed(object sender, EventArgs e)
        { editPr = null; }

        private void Button_Click(object sender, RoutedEventArgs e)//кнопка назад
        {
            NavigationService.Navigate(Class.Class1.GetAuthorization());
        }
    }
}
