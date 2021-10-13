using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Models.Models
{
    public class AccountDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required,MaxLength(24),MinLength(8)]
        public string Password { get; set; }

        [Required,MinLength(3),MaxLength(30)]
        public string UserName { get; set; }
    }
}
