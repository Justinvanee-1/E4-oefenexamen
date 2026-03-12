using E4_Project.Data;
using E4_Project.Data.Models;
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
        private List<Inventory> allItems;
        private void LoadItems()
        {
            allItems = db.Inventories.ToList();
            InventoryList.ItemsSource = allItems;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchBox.Text.ToLower();

            var filtered = allItems
                .Where(i =>
                    i.ItemName.ToLower().Contains(search) ||
                    i.MagicalAddons.ToLower().Contains(search))
                .ToList();

            InventoryList.ItemsSource = filtered;
        }
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
        private async void Details_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = (int)btn.Tag;

            var item = db.Inventories.FirstOrDefault(i => i.ItemId == id);

            if (item != null)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = $"Details van {item.ItemName}",
                    Content =
                        $"Name: {item.ItemName}\n" +
                        $"Description: {item.Description}\n" +
                        $"Type: {item.Type}\n" +
                        $"Rarity: {item.Rarity}\n" +
                        $"Power: {item.Power}\n" +
                        $"Speed: {item.Speed}\n" +
                        $"Durability: {item.Durability}\n" +
                        $"Magical Addons: {item.MagicalAddons}",
                    CloseButtonText = "Close",
                    XamlRoot = this.XamlRoot
                };

                await dialog.ShowAsync();
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddItemPage));
        }
    }
}
