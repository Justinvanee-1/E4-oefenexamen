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

        public MarketPage()
        {
            this.InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            // Zorg dat PriceText in je model aanwezig is
            MarketList.ItemsSource = db.MarketItems.ToList();
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
