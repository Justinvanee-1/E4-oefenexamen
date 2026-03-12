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
    public sealed partial class MarketPage : Page
    {
        private AppDbContext db = new AppDbContext();

        private List<Market> allItems;

        private void LoadItems()
        {
            allItems = db.MarketItems.ToList();
            MarketList.ItemsSource = allItems;
        }

        private void FilterChanged(object sender, object e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (allItems == null)
                return;

            var filtered = allItems.AsQueryable();

            string search = SearchBox.Text?.ToLower() ?? "";

            if (!string.IsNullOrWhiteSpace(search))
            {
                filtered = filtered.Where(i =>
                    i.ItemName.ToLower().Contains(search) ||
                    i.MagicalAddons.ToLower().Contains(search));
            }

            string rarity = (RarityFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (rarity != null && rarity != "All Rarity")
            {
                filtered = filtered.Where(i => i.Rarity == rarity);
            }

            string type = (TypeFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (type != null && type != "All Types")
            {
                filtered = filtered.Where(i => i.Type == type);
            }

            string sort = (SortPrice.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (sort == "Price ↑")
                filtered = filtered.OrderBy(i => i.Price);

            if (sort == "Price ↓")
                filtered = filtered.OrderByDescending(i => i.Price);

            MarketList.ItemsSource = filtered.ToList();
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allItems == null)
                return;

            string search = SearchBox.Text.ToLower();

            var filtered = allItems
                .Where(i =>
                    i.ItemName.ToLower().Contains(search) ||
                    i.MagicalAddons.ToLower().Contains(search))
                .ToList();

            MarketList.ItemsSource = filtered;
        }

        private async void Trade_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int marketItemId = (int)btn.Tag;

            var marketItem = db.MarketItems.FirstOrDefault(i => i.MarketItemId == marketItemId);

            if (marketItem == null)
                return;

            var inventoryItems = db.Inventories.Where(i => i.UserId == 1).ToList();

            ComboBox tradeItems = new ComboBox
            {
                ItemsSource = inventoryItems,
                DisplayMemberPath = "ItemName"
            };

            ContentDialog tradeDialog = new ContentDialog
            {
                Title = $"Trade for {marketItem.ItemName}",
                Content = tradeItems,
                PrimaryButtonText = "Send Trade",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await tradeDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var selectedItem = tradeItems.SelectedItem as Inventory;

                if (selectedItem == null)
                    return;

                TradeRequest trade = new TradeRequest
                {
                    OfferedItemId = selectedItem.ItemId,
                    RequestedMarketItemId = marketItem.MarketItemId,
                    Status = "Pending"
                };

                db.TradeRequests.Add(trade);
                db.SaveChanges();

                ContentDialog confirm = new ContentDialog
                {
                    Title = "Trade Sent",
                    Content = "Trade request sent. Waiting for seller response.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                await confirm.ShowAsync();
            }
        }

        public MarketPage()
        {
            this.InitializeComponent();
            LoadItems();
        }

        private async void Details_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = (int)btn.Tag;

            var item = db.MarketItems.FirstOrDefault(i => i.MarketItemId == id);
            if (item != null)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = $"Details van {item.ItemName}",
                    Content = $"Naam: {item.ItemName}\n" +
                              $"Beschrijving: {item.Description}\n" +
                              $"Type: {item.Type}\n" +
                              $"Rarity: {item.Rarity}\n" +
                              $"Power: {item.Power}\n" +
                              $"Speed: {item.Speed}\n" +
                              $"Durability: {item.Durability}\n" +
                              $"Magical Addons: {item.MagicalAddons}\n" +
                              $"Prijs: {item.PriceText}",
                    CloseButtonText = "Sluit",
                    XamlRoot = this.XamlRoot  // ← Zet dit erbij!
                };

                await dialog.ShowAsync();
            }
        }

        private async void Buy_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = (int)btn.Tag;

            var item = db.MarketItems.FirstOrDefault(i => i.MarketItemId == id);

            if (item != null)
            {
                // Vraag bevestiging
                ContentDialog confirmDialog = new ContentDialog
                {
                    Title = "Bevestig aankoop",
                    Content = $"Wil je {item.ItemName} kopen voor {item.PriceText}?",
                    PrimaryButtonText = "Ja",
                    CloseButtonText = "Nee",
                    XamlRoot = this.XamlRoot  // ← Zet dit erbij!
                };

                var result = await confirmDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    // Voeg toe aan inventory
                    Inventory newItem = new Inventory
                    {
                        UserId = 1,
                        ItemName = item.ItemName,
                        Description = item.Description,
                        Type = item.Type,
                        Rarity = item.Rarity,
                        Power = item.Power,
                        Speed = item.Speed,
                        Durability = item.Durability,
                        MagicalAddons = item.MagicalAddons
                    };

                    db.Inventories.Add(newItem);

                    // Verwijder item uit de markt als je wilt
                    db.MarketItems.Remove(item);

                    db.SaveChanges();

                    // Refresh lijst
                    LoadItems();
                }
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }
    }
}
