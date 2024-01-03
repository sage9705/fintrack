using System;

namespace FinTrack.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
    }
}