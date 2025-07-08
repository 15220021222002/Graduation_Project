using DCare.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Data.Entites
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public UserRole Role { get; set; } = UserRole.Admin;
    }
}
