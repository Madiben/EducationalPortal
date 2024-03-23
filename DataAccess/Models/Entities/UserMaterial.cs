using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public class UserMaterial
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public bool IsCompleted { get; set; }

        public UserMaterial() { }

        public override string ToString()
        {
            return $"Material Name: {Material.Name}, Material complation status: {IsCompleted}.\n";
        }
    }
}
