using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Category;
namespace Models.MenuRepo
{
    public class Menu
    {
        public int MenuId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; } = 0;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } = 0;
        public byte[]? ItemImg { get; set; }
        public int Rating { get; set; } = 0;
        public MenuCategory? Category { get; set; }
    }
}