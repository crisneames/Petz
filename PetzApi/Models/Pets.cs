using System;
namespace PetzApi.Models
{
    public class Pets
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string KindOfPet { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public List<Users>User { get; set; }
       }
    
}

