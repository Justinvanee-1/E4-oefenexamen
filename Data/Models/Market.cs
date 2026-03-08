using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E4_Project.Data.Models
{
    internal class Market
    {
        [Key]
        public int MarketItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Rarity { get; set; }

        [Required]
        public string Power { get; set; }

        [Required]
        public string Speed { get; set; }

        [Required]
        public string Durability { get; set; }

        [Required]
        public string MagicalAddons { get; set; }

        [Required]
        public int Price { get; set; }

        // Geformatteerde string property
        public string PriceText => $"Price: {Price} gold";
    }
}
