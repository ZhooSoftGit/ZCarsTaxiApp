using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class UserRoleDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserRoles RoleId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
