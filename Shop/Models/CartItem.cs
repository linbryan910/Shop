﻿using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class CartItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
