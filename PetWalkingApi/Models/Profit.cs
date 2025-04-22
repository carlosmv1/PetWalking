using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetWalkingApi.Models
{
    public class Profit
    {
        [Column("profit_id")]
        [Key]
        public int ProfitId { get; set; }
        
        [Column("booking_id")]
        public int BookingId { get; set; }
        [Column("client_amount")]
        public decimal ClientAmount { get; set; }
        [Column("walker_amount")]
        public decimal WalkerAmount { get; set; }
        [Column("platform_profit")]
        public decimal PlatformProfit { get; set; }
        [Column("calculate_at")]
        public DateTime CalculatedAt { get; set; }
    }
}