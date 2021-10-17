﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Models.Models
{
    public class LoginModelDto
    {
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(24), MinLength(8)]
        public string Password { get; set; }
    }
}
