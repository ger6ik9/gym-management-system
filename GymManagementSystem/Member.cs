using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    class Member : Person
    {
        // Fields
        private int memberId;
        private string membershipType;
        private DateTime joinDate;
        private bool isActive;

        // Constructor
        public Member(int memberId, string name, string email, string phone, string membershipType)
            : base(name, email, phone)
        {
            this.memberId = memberId;
            this.membershipType = membershipType;
            this.joinDate = DateTime.Now;
            this.isActive = true;
        }

        // Getters and Setters
        public int MemberId
        {
            get { return memberId; }
        }

        public string MembershipType
        {
            get { return membershipType; }
            set { membershipType = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
        }

        // Methods
        public void Deactivate()
        {
            isActive = false;
            Console.WriteLine(Name + "'s membership has been deactivated.");
        }

        public string GetStatus()
        {
            return isActive ? "Active" : "Inactive";
        }

        public override void GetInfo()
        {
            Console.WriteLine("--- Member Info ---");
            base.GetInfo();
            Console.WriteLine("Member ID       : " + memberId);
            Console.WriteLine("Membership Type : " + membershipType);
            Console.WriteLine("Join Date       : " + joinDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("Status          : " + GetStatus());
        }
    }
}
