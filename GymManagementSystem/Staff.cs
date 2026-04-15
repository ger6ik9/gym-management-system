using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    // Staff inherits from Person - gets name, email, phone automatically
    class Staff : Person
    {
        // Fields specific to a staff member
        private int staffId;
        private string role;

        // Constructor - calls base() to pass shared fields up to Person
        public Staff(int staffId, string name, string email, string phone, string role)
            : base(name, email, phone)
        {
            this.staffId = staffId;
            this.role = role;
        }

        // Read-only property - staffId should never change after creation
        public int StaffId
        {
            get { return staffId; }
        }

        // Property - role can be updated using AssignRole()
        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        // Updates the staff member's role and confirms the change
        public void AssignRole(string newRole)
        {
            role = newRole;
            Console.WriteLine(Name + " has been assigned the role: " + role);
        }

        // Returns the staff member's current role
        public string GetRole()
        {
            return role;
        }

        // Overrides Person's GetInfo() to include staff-specific fields
        public override void GetInfo()
        {
            Console.WriteLine("--- Staff Info ---");
            base.GetInfo(); // Calls Person's GetInfo() to print name, email, phone
            Console.WriteLine("Staff ID : " + staffId);
            Console.WriteLine("Role     : " + role);
        }
    }
}