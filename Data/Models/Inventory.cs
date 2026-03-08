using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E4_Project.Data.Models
{
    internal class Inventory
    {
        [Key]
        [Required]
        public int ItemId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Column("Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Column("Description")]
        public string Description { get; set; }

        [Required]
        [Column("Type")]
        public string Type { get; set; }

        [Required]
        [Column("Rarity")]
        public string Rarity { get; set; }

        [Required]
        [Column("Power")]
        public string Power { get; set; }

        [Required]
        [Column("Speed")]
        public string Speed { get; set; }

        [Required]
        [Column("Durability")]
        public string Durability { get; set; }

        [Required]
        [Column("Magical Addons")]
        public string MagicalAddons { get; set; }
    }
}
