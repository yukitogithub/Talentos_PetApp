using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Pet
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime Birthday { get; set; }
        public string Breed { get; set; }
        public string ImageUrl { get; set; }

        //[ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
