using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E4_Project.Data.Models
{
    public class TradeRequest
    {
        [Key]
        public int TradeRequestId { get; set; }

        public int OfferedItemId { get; set; }

        public int RequestedMarketItemId { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
