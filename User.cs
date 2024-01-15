using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTEST
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(64), Required]
        public string Password { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [MaxLength(255), Required]
        public string FullName { get; set; }
    }
}
