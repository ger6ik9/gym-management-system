using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    // Member inherits from Person - gets name, email, phone automatically
    class Member : Person
    {
        // Fields specific to a gym member
        private int memberId;
        private string membershipType;
        private DateTime joinDate;
        private bool isActive;

        // Constructor - calls base() to pass shared fields up to Person
        public Member(int memberId, string name, string email, string phone, string membershipType)
            : base(name, email, phone)
        {
            this.memberId = memberId;
            this.membershipType = membershipType;
            this.joinDate = DateTime.Now; // Automatically set to today's date
            this.isActive = true;         // New members are active by default
        }

        // Read-only property - memberId should never change after creation
        public int MemberId
        {
            get { return memberId; }
        }

        // Property - membership type can be updated if needed
        public string MembershipType
        {
            get { return membershipType; }
            set { membershipType = value; }
        }

        // Read-only property - isActive is changed only through Deactivate()
        public bool IsActive
        {
            get { return isActive; }
        }

        // Sets the member's status to inactive
        public void Deactivate()
        {
            isActive = false;
            Console.WriteLine(Name + "'s membership has been deactivated.");
        }

        // Reactivates a previously deactivated member
        public void Reactivate()
        {
            isActive = true;
            Console.WriteLine(Name + "'s membership has been reactivated.");
        }

        // Returns a readable status string instead of true/false
        public string GetStatus()
        {
            return isActive ? "Active" : "Inactive";
        }

        // Overrides Person's GetInfo() to include member-specific fields
        public override void GetInfo()
        {
            Console.WriteLine("--- Member Info ---");
            base.GetInfo(); // Calls Person's GetInfo() to print name, email, phone
            Console.WriteLine("Member ID       : " + memberId);
            Console.WriteLine("Membership Type : " + membershipType);
            Console.WriteLine("Join Date       : " + joinDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("Status          : " + GetStatus());
        }
    }
}