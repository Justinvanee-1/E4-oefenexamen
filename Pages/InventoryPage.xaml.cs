using E4_Project.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace E4_Project.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InventoryPage : Page
    {

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }

        private AppDbContext db = new AppDbContext();

        public InventoryPage()
        {
            this.InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            InventoryList.ItemsSource = db.Inventories.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = (int)btn.Tag;

            var item = db.Inventories.FirstOrDefault(i => i.ItemId == id);

            if (item != null)
            {
                db.Inventories.Remove(item);
                db.SaveChanges();
                LoadItems();
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddItemPage));
        }
    }
}
