using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    class Staff : Person
    {
        // Fields
        private int staffId;
        private string role;

        // Constructor
        public Staff(int staffId, string name, string email, string phone, string role)
            : base(name, email, phone)
        {
            this.staffId = staffId;
            this.role = role;
        }

        // Getters and Setters
        public int StaffId
        {
            get { return staffId; }
        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        // Methods
        public void AssignRole(string newRole)
        {
            role = newRole;
            Console.WriteLine(Name + " has been assigned the role: " + role);
        }

        public string GetRole()
        {
            return role;
        }

        public override void GetInfo()
        {
            Console.WriteLine("--- Staff Info ---");
            base.GetInfo();
            Console.WriteLine("Staff ID : " + staffId);
            Console.WriteLine("Role     : " + role);
        }
    }
}
