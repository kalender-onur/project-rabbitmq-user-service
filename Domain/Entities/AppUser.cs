using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Password { get; set; }

    }
}
