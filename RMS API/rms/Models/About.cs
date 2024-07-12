using System;
namespace Models.About
{
    public class About
    {
        public int AboutId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime OpeningTime{ get; set; }
    }
}