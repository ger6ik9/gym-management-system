using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagementSystem
{
    class GymManager
    {
        // Fields
        private List<Member> members;
        private List<Staff> staff;
        private List<GymClass> gymClasses;
        private int nextMemberId;
        private int nextStaffId;

        // Constructor
        public GymManager()
        {
            members = new List<Member>();
            staff = new List<Staff>();
            gymClasses = new List<GymClass>();
            nextMemberId = 1;
            nextStaffId = 1;
        }

        // ── Member Methods ──────────────────────────────

        public void AddMember()
        {
            Console.WriteLine("\n--- Add New Member ---");
            Console.Write("Name            : ");
            string name = Console.ReadLine();

            Console.Write("Email           : ");
            string email = Console.ReadLine();

            Console.Write("Phone           : ");
            string phone = Console.ReadLine();

            Console.WriteLine("Membership Type : ");
            Console.WriteLine("  1. Basic    - $29.99/month");
            Console.WriteLine("  2. Premium  - $49.99/month");
            Console.WriteLine("  3. VIP      - $79.99/month");
            Console.Write("Choose (1-3)    : ");
            string choice = Console.ReadLine();

            string membershipType;
            switch (choice)
            {
                case "1": membershipType = "Basic"; break;
                case "2": membershipType = "Premium"; break;
                case "3": membershipType = "VIP"; break;
                default: membershipType = "Basic"; break;
            }

            Member newMember = new Member(nextMemberId, name, email, phone, membershipType);
            members.Add(newMember);
            nextMemberId++;

            Console.WriteLine("\n✔ Member added successfully! Member ID: " + (nextMemberId - 1));
        }

        public void DisplayAllMembers()
        {
            Console.WriteLine("\n--- All Members ---");
            if (members.Count == 0)
            {
                Console.WriteLine("No members registered yet.");
                return;
            }

            foreach (Member m in members)
            {
                m.GetInfo();
                Console.WriteLine();
            }
        }

        public void DeactivateMember()
        {
            Console.WriteLine("\n--- Deactivate Member ---");
            Console.Write("Enter Member ID : ");
            int id;

            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID entered.");
                return;
            }

            foreach (Member m in members)
            {
                if (m.MemberId == id)
                {
                    m.Deactivate();
                    return;
                }
            }
            Console.WriteLine("Member ID " + id + " not found.");
        }

        // ── Staff Methods ───────────────────────────────

        public void AddStaff()
        {
            Console.WriteLine("\n--- Add New Staff ---");
            Console.Write("Name  : ");
            string name = Console.ReadLine();

            Console.Write("Email : ");
            string email = Console.ReadLine();

            Console.Write("Phone : ");
            string phone = Console.ReadLine();

            Console.Write("Role  : ");
            string role = Console.ReadLine();

            Staff newStaff = new Staff(nextStaffId, name, email, phone, role);
            staff.Add(newStaff);
            nextStaffId++;

            Console.WriteLine("\n✔ Staff added successfully! Staff ID: " + (nextStaffId - 1));
        }

        public void DisplayAllStaff()
        {
            Console.WriteLine("\n--- All Staff ---");
            if (staff.Count == 0)
            {
                Console.WriteLine("No staff registered yet.");
                return;
            }

            foreach (Staff s in staff)
            {
                s.GetInfo();
                Console.WriteLine();
            }
        }

        // ── GymClass Methods ────────────────────────────

        public void AddGymClass()
        {
            Console.WriteLine("\n--- Add New Gym Class ---");
            Console.Write("Class Name       : ");
            string className = Console.ReadLine();

            Console.Write("Instructor Name  : ");
            string instructor = Console.ReadLine();

            Console.Write("Capacity         : ");
            int capacity;
            if (!int.TryParse(Console.ReadLine(), out capacity))
            {
                capacity = 10;
            }

            GymClass newClass = new GymClass(className, instructor, capacity);
            gymClasses.Add(newClass);

            Console.WriteLine("\n✔ Gym class added successfully!");
        }

        public void EnrollMemberInClass()
        {
            Console.WriteLine("\n--- Enroll Member in Class ---");

            if (members.Count == 0)
            {
                Console.WriteLine("No members registered yet.");
                return;
            }

            if (gymClasses.Count == 0)
            {
                Console.WriteLine("No gym classes available yet.");
                return;
            }

            // Show available members
            Console.WriteLine("Members:");
            foreach (Member m in members)
            {
                Console.WriteLine("  [" + m.MemberId + "] " + m.Name + " - " + m.GetStatus());
            }
            Console.Write("Enter Member ID  : ");
            int memberId;
            if (!int.TryParse(Console.ReadLine(), out memberId))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            // Show available classes
            Console.WriteLine("\nGym Classes:");
            for (int i = 0; i < gymClasses.Count; i++)
            {
                Console.WriteLine("  [" + (i + 1) + "] " + gymClasses[i].ClassName + " - " + gymClasses[i].InstructorName);
            }
            Console.Write("Choose Class     : ");
            int classChoice;
            if (!int.TryParse(Console.ReadLine(), out classChoice) || classChoice < 1 || classChoice > gymClasses.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            // Find the member and enroll
            Member selectedMember = null;
            foreach (Member m in members)
            {
                if (m.MemberId == memberId)
                {
                    selectedMember = m;
                    break;
                }
            }

            if (selectedMember == null)
            {
                Console.WriteLine("Member not found.");
                return;
            }

            gymClasses[classChoice - 1].Enroll(selectedMember);
        }

        public void DisplayClassRoster()
        {
            Console.WriteLine("\n--- View Class Roster ---");

            if (gymClasses.Count == 0)
            {
                Console.WriteLine("No gym classes available yet.");
                return;
            }

            for (int i = 0; i < gymClasses.Count; i++)
            {
                Console.WriteLine("  [" + (i + 1) + "] " + gymClasses[i].ClassName);
            }
            Console.Write("Choose Class : ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > gymClasses.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            Console.WriteLine();
            gymClasses[choice - 1].DisplayRoster();
        }

        // ── Main Menu ───────────────────────────────────

        public void Run()
        {
            bool running = true;

            while (running)
            {
          
                Console.WriteLine("    GYM MANAGEMENT SYSTEM     ");
                Console.WriteLine("══════════════════════════════");
                Console.WriteLine("  1. Add Member               ");
                Console.WriteLine("  2. View All Members         ");
                Console.WriteLine("  3. Deactivate Member        ");
                Console.WriteLine("  4. Add Staff                ");
                Console.WriteLine("  5. View All Staff           ");
                Console.WriteLine("  6. Add Gym Class            ");
                Console.WriteLine("  7. Enroll Member in Class   ");
                Console.WriteLine("  8. View Class Roster        ");
                Console.WriteLine("  9. Exit                     ");
                Console.WriteLine("══════════════════════════════");
                Console.Write("Choose an option : ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": AddMember(); break;
                    case "2": DisplayAllMembers(); break;
                    case "3": DeactivateMember(); break;
                    case "4": AddStaff(); break;
                    case "5": DisplayAllStaff(); break;
                    case "6": AddGymClass(); break;
                    case "7": EnrollMemberInClass(); break;
                    case "8": DisplayClassRoster(); break;
                    case "9": running = false; break;
                    default:
                        Console.WriteLine("Invalid option. Please choose 1-9.");
                        break;
                }
            }

            Console.WriteLine("\nGoodbye!");
        }
    }
}
