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
    public sealed partial class AddItemPage : Page
    {
        private AppDbContext db = new AppDbContext();

        public AddItemPage()
        {
            this.InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Inventory item = new Inventory
            {
                UserId = 1,
                ItemName = ItemNameBox.Text,
                Description = DescriptionBox.Text,
                Type = TypeBox.Text,
                Rarity = RarityBox.Text,
                Power = PowerBox.Text,
                Speed = SpeedBox.Text,
                Durability = DurabilityBox.Text,
                MagicalAddons = MagicalAddonsBox.Text
            };

            db.Inventories.Add(item);
            db.SaveChanges();

            Frame.GoBack();
        }
    }
}
