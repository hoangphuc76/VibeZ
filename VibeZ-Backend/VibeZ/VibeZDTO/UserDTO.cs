using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class UserDTO : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public DateTime DOB { get; set; }
        public string premium { get; set; }
    }
}