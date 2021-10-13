using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Models.Models
{
    public class Auth
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public Account Account { get; set; }
    }
}
