using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    // Controller class - manages all lists and drives the console menu
    class GymManager
    {
        // Master lists that store all objects in the system
        private List<Member> members;
        private List<Staff> staff;
        private List<GymClass> gymClasses;

        // Auto-incrementing counters to assign unique IDs
        private int nextMemberId;
        private int nextStaffId;

        // Constructor - initializes all lists and ID counters
        public GymManager()
        {
            members = new List<Member>();
            staff = new List<Staff>();
            gymClasses = new List<GymClass>();
            nextMemberId = 1;
            nextStaffId = 1;
        }

        // ── Member Methods ──────────────────────────────

        // Prompts the user for details and adds a new member to the list
        public void AddMember()
        {
            Console.WriteLine("\n--- Add New Member ---");
            Console.Write("Name            : ");
            string name = Console.ReadLine();

            Console.Write("Email           : ");
            string email = Console.ReadLine();

            Console.Write("Phone           : ");
            string phone = Console.ReadLine();

            // Show membership options and get user's choice
            Console.WriteLine("Membership Type : ");
            Console.WriteLine("  1. Basic    - $29.99/month");
            Console.WriteLine("  2. Premium  - $49.99/month");
            Console.WriteLine("  3. VIP      - $79.99/month");
            Console.Write("Choose (1-3)    : ");
            string choice = Console.ReadLine();

            // Convert number choice to membership type string
            string membershipType;
            switch (choice)
            {
                case "1": membershipType = "Basic"; break;
                case "2": membershipType = "Premium"; break;
                case "3": membershipType = "VIP"; break;
                default: membershipType = "Basic"; break; // Default to Basic if invalid
            }

            // Create the new member object and add it to the list
            Member newMember = new Member(nextMemberId, name, email, phone, membershipType);
            members.Add(newMember);
            nextMemberId++; // Increment so the next member gets a unique ID

            Console.WriteLine("\n✔ Member added successfully! Member ID: " + (nextMemberId - 1));
        }

        // Loops through all members and prints their info
        public void DisplayAllMembers()
        {
            Console.WriteLine("\n--- All Members ---");

            // Handle the case where no members have been added yet
            if (members.Count == 0)
            {
                Console.WriteLine("No members registered yet.");
                return;
            }

            foreach (Member m in members)
            {
                m.GetInfo(); // Calls the overridden GetInfo() in Member
                Console.WriteLine();
            }
        }

        // Finds a member by ID and deactivates their membership
        public void DeactivateMember()
        {
            Console.WriteLine("\n--- Deactivate Member ---");
            Console.Write("Enter Member ID : ");
            int id;

            // Validate that the input is actually a number
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID entered.");
                return;
            }

            // Search for the member with the matching ID
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

        // Prompts the user for details and adds a new staff member to the list
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

            // Create the new staff object and add it to the list
            Staff newStaff = new Staff(nextStaffId, name, email, phone, role);
            staff.Add(newStaff);
            nextStaffId++; // Increment so the next staff gets a unique ID

            Console.WriteLine("\n✔ Staff added successfully! Staff ID: " + (nextStaffId - 1));
        }

        // Loops through all staff and prints their info
        public void DisplayAllStaff()
        {
            Console.WriteLine("\n--- All Staff ---");

            // Handle the case where no staff have been added yet
            if (staff.Count == 0)
            {
                Console.WriteLine("No staff registered yet.");
                return;
            }

            foreach (Staff s in staff)
            {
                s.GetInfo(); // Calls the overridden GetInfo() in Staff
                Console.WriteLine();
            }
        }

        // ── GymClass Methods ────────────────────────────

        // Prompts the user for details and adds a new gym class to the list
        public void AddGymClass()
        {
            Console.WriteLine("\n--- Add New Gym Class ---");
            Console.Write("Class Name       : ");
            string className = Console.ReadLine();

            Console.Write("Instructor Name  : ");
            string instructor = Console.ReadLine();

            Console.Write("Capacity         : ");
            int capacity;

            // If capacity input is invalid, default to 10
            if (!int.TryParse(Console.ReadLine(), out capacity))
            {
                capacity = 10;
            }

            // Create the new gym class and add it to the list
            GymClass newClass = new GymClass(className, instructor, capacity);
            gymClasses.Add(newClass);

            Console.WriteLine("\n✔ Gym class added successfully!");
        }

        // Lets the user pick a member and a class, then enrolls them
        public void EnrollMemberInClass()
        {
            Console.WriteLine("\n--- Enroll Member in Class ---");

            // Can't enroll if there are no members or no classes yet
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

            // Display all members so the user can pick one
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

            // Display all classes so the user can pick one
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

            // Find the selected member object by their ID
            Member selectedMember = null;
            foreach (Member m in members)
            {
                if (m.MemberId == memberId)
                {
                    selectedMember = m;
                    break;
                }
            }

            // Make sure the member ID actually exists
            if (selectedMember == null)
            {
                Console.WriteLine("Member not found.");
                return;
            }

            // Call Enroll() on the selected class - it handles all validation
            gymClasses[classChoice - 1].Enroll(selectedMember);
        }

        // Lets the user pick a class and view its full roster
        public void DisplayClassRoster()
        {
            Console.WriteLine("\n--- View Class Roster ---");

            if (gymClasses.Count == 0)
            {
                Console.WriteLine("No gym classes available yet.");
                return;
            }

            // Show all classes for the user to choose from
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
            gymClasses[choice - 1].DisplayRoster(); // Display the selected class roster
        }

        // ── Main Menu ───────────────────────────────────

        // Main loop - keeps the program running until the user chooses Exit
        public void Run()
        {
            bool running = true;

            while (running)
            {
                // Display the menu options
       
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

                // Call the matching method based on user input
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
                    case "9": running = false; break; // Exit the loop
                    default:
                        Console.WriteLine("Invalid option. Please choose 1-9.");
                        break;
                }
            }

            Console.WriteLine("\nGoodbye!");
        }
    }
}