using System;
using System.ComponentModel.DataAnnotations;
namespace Models.Category
{
    public class MenuCategory
    {
        [Key]
        public int CategoryId { get; set; } = 0;
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? CategoryImg { get; set; }
    }
}