using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User: IdentityUser<int>
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        //[EmailAddress]
        //public string Email { get; set; }
        //public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Description { get; set; }

        public List<Pet> Pets { get; set; }
        public List<IdentityUserRole<int>> UserRoles { get; set; }
    }
}
