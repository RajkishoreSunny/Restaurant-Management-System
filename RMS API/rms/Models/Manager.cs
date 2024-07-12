using System;
namespace Models.Manager
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string? ManagerName { get; set;}
        public string? ManagerEmail { get; set; }
        public string? ManagerPassword { get; set;}
        public string? Branch { get; set; }
    }
}