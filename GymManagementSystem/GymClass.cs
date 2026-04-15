using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagementSystem
{
    class GymClass
    {
        // Fields
        private string className;
        private string instructorName;
        private int capacity;
        private List<Member> enrolledMembers;

        // Constructor
        public GymClass(string className, string instructorName, int capacity)
        {
            this.className = className;
            this.instructorName = instructorName;
            this.capacity = capacity;
            this.enrolledMembers = new List<Member>();
        }

        // Getters and Setters
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        public string InstructorName
        {
            get { return instructorName; }
            set { instructorName = value; }
        }

        public int Capacity
        {
            get { return capacity; }
        }

        // Methods
        public void Enroll(Member member)
        {
            if (enrolledMembers.Count >= capacity)
            {
                Console.WriteLine("Sorry, " + className + " is full.");
                return;
            }

            if (!member.IsActive)
            {
                Console.WriteLine(member.Name + " is not an active member.");
                return;
            }

            // Check if already enrolled
            foreach (Member m in enrolledMembers)
            {
                if (m.MemberId == member.MemberId)
                {
                    Console.WriteLine(member.Name + " is already enrolled in " + className + ".");
                    return;
                }
            }

            enrolledMembers.Add(member);
            Console.WriteLine(member.Name + " has been enrolled in " + className + ".");
        }

        public void RemoveMember(int memberId)
        {
            foreach (Member m in enrolledMembers)
            {
                if (m.MemberId == memberId)
                {
                    enrolledMembers.Remove(m);
                    Console.WriteLine(m.Name + " has been removed from " + className + ".");
                    return;
                }
            }
            Console.WriteLine("Member not found in " + className + ".");
        }

        public void DisplayRoster()
        {
            Console.WriteLine("--- " + className + " Roster ---");
            Console.WriteLine("Instructor : " + instructorName);
            Console.WriteLine("Capacity   : " + enrolledMembers.Count + "/" + capacity);

            if (enrolledMembers.Count == 0)
            {
                Console.WriteLine("No members enrolled.");
                return;
            }

            Console.WriteLine("Enrolled Members:");
            foreach (Member m in enrolledMembers)
            {
                Console.WriteLine("  - [" + m.MemberId + "] " + m.Name + " (" + m.MembershipType + ")");
            }
        }
    }
}
