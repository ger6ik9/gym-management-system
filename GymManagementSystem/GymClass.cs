using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagementSystem
{
    // Represents a scheduled fitness class (e.g. Yoga, Spin, Pilates)
    class GymClass
    {
        // Fields that define the gym class
        private string className;
        private string instructorName;
        private int capacity;
        private List<Member> enrolledMembers; // Tracks who is signed up

        // Constructor - initializes all fields and creates an empty members list
        public GymClass(string className, string instructorName, int capacity)
        {
            this.className = className;
            this.instructorName = instructorName;
            this.capacity = capacity;
            this.enrolledMembers = new List<Member>(); // Start with no members
        }

        // Property - allows getting and setting the class name
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        // Property - allows getting and setting the instructor name
        public string InstructorName
        {
            get { return instructorName; }
            set { instructorName = value; }
        }

        // Read-only property - capacity should not change after creation
        public int Capacity
        {
            get { return capacity; }
        }

        // Adds a member to the class after passing all validation checks
        public void Enroll(Member member)
        {
            // Check 1: Make sure the class is not already full
            if (enrolledMembers.Count >= capacity)
            {
                Console.WriteLine("Sorry, " + className + " is full.");
                return;
            }

            // Check 2: Make sure the member's account is active
            if (!member.IsActive)
            {
                Console.WriteLine(member.Name + " is not an active member.");
                return;
            }

            // Check 3: Make sure the member is not already enrolled
            foreach (Member m in enrolledMembers)
            {
                if (m.MemberId == member.MemberId)
                {
                    Console.WriteLine(member.Name + " is already enrolled in " + className + ".");
                    return;
                }
            }

            // All checks passed - add the member to the class
            enrolledMembers.Add(member);
            Console.WriteLine(member.Name + " has been enrolled in " + className + ".");
        }

        // Removes a member from the class using their ID
        public void RemoveMember(int memberId)
        {
            // Loop through enrolled members to find the matching ID
            foreach (Member m in enrolledMembers)
            {
                if (m.MemberId == memberId)
                {
                    enrolledMembers.Remove(m);
                    Console.WriteLine(m.Name + " has been removed from " + className + ".");
                    return;
                }
            }
            // If we get here, the member was not found
            Console.WriteLine("Member not found in " + className + ".");
        }

        // Prints the full list of enrolled members for this class
        public void DisplayRoster()
        {
            Console.WriteLine("--- " + className + " Roster ---");
            Console.WriteLine("Instructor : " + instructorName);
            Console.WriteLine("Capacity   : " + enrolledMembers.Count + "/" + capacity);

            // Handle the case where no one is enrolled yet
            if (enrolledMembers.Count == 0)
            {
                Console.WriteLine("No members enrolled.");
                return;
            }

            // Print each enrolled member's ID, name, and membership type
            Console.WriteLine("Enrolled Members:");
            foreach (Member m in enrolledMembers)
            {
                Console.WriteLine("  - [" + m.MemberId + "] " + m.Name + " (" + m.MembershipType + ")");
            }
        }
    }
}