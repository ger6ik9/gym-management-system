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

        // ── Validation Helpers ──────────────────────────

        // Keeps prompting until the user enters a non-empty string
        private string ReadRequiredString(string prompt)
        {
            string input = "";
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("  This field cannot be empty. Please try again.");
            }
            return input.Trim();
        }

        // Keeps prompting until the user enters a valid email (must contain @ and .)
        private string ReadEmail(string prompt)
        {
            string input = "";
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("  Email cannot be empty. Please try again.");
                }
                else if (!input.Contains("@") || !input.Contains("."))
                {
                    Console.WriteLine("  Invalid email format. Must contain @ and a dot.");
                }
                else
                {
                    break;
                }
            }
            return input;
        }

        // Keeps prompting until the user enters a valid phone (digits only, 7-15 chars)
        private string ReadPhone(string prompt)
        {
            string input = "";
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine().Trim();
                bool allDigitsOrDash = true;

                // Check each character is a digit or dash
                foreach (char c in input)
                {
                    if (!char.IsDigit(c) && c != '-')
                    {
                        allDigitsOrDash = false;
                        break;
                    }
                }

                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("  Phone cannot be empty. Please try again.");
                else if (!allDigitsOrDash)
                    Console.WriteLine("  Phone can only contain numbers and dashes.");
                else if (input.Length < 7 || input.Length > 15)
                    Console.WriteLine("  Phone must be between 7 and 15 characters.");
                else
                    break;
            }
            return input;
        }

        // Keeps prompting until the user enters a positive whole number
        private int ReadPositiveInt(string prompt)
        {
            int value = 0;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out value) && value > 0)
                    break;
                Console.WriteLine("  Please enter a valid positive number.");
            }
            return value;
        }

        // Checks if an email is already used by an existing member
        private bool EmailExistsInMembers(string email)
        {
            foreach (Member m in members)
            {
                if (m.Email.ToLower() == email.ToLower())
                    return true;
            }
            return false;
        }

        // Checks if an email is already used by an existing staff member
        private bool EmailExistsInStaff(string email)
        {
            foreach (Staff s in staff)
            {
                if (s.Email.ToLower() == email.ToLower())
                    return true;
            }
            return false;
        }

        // ── Member Methods ──────────────────────────────

        // Prompts the user for details and adds a new member to the list
        public void AddMember()
        {
            Console.WriteLine("\n--- Add New Member ---");

            // Validate name
            string name = ReadRequiredString("Name            : ");

            // Validate email - must be valid format and not already registered
            string email;
            while (true)
            {
                email = ReadEmail("Email           : ");
                if (EmailExistsInMembers(email))
                    Console.WriteLine("  That email is already registered to another member.");
                else
                    break;
            }

            // Validate phone
            string phone = ReadPhone("Phone           : ");

            // Show membership options and get user's choice
            string membershipType;
            while (true)
            {
                Console.WriteLine("Membership Type :");
                Console.WriteLine("  1. Basic    - $29.99/month");
                Console.WriteLine("  2. Premium  - $49.99/month");
                Console.WriteLine("  3. VIP      - $79.99/month");
                Console.Write("Choose (1-3)    : ");
                string choice = Console.ReadLine();

                if (choice == "1") { membershipType = "Basic"; break; }
                if (choice == "2") { membershipType = "Premium"; break; }
                if (choice == "3") { membershipType = "VIP"; break; }

                Console.WriteLine("  Invalid choice. Please enter 1, 2, or 3.");
            }

            // Create the new member object and add it to the list
            Member newMember = new Member(nextMemberId, name, email, phone, membershipType);
            members.Add(newMember);
            nextMemberId++;

            Console.WriteLine("\n✔ Member added! Member ID: " + (nextMemberId - 1));
        }

        // Loops through all members and prints their info
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

        // Finds a member by ID and deactivates their membership
        public void DeactivateMember()
        {
            Console.WriteLine("\n--- Deactivate Member ---");

            if (members.Count == 0)
            {
                Console.WriteLine("No members registered yet.");
                return;
            }

            // Show all members first so the user knows which IDs exist
            Console.WriteLine("Current Members:");
            foreach (Member m in members)
                Console.WriteLine("  [" + m.MemberId + "] " + m.Name + " - " + m.GetStatus());

            int id = ReadPositiveInt("\nEnter Member ID : ");

            foreach (Member m in members)
            {
                if (m.MemberId == id)
                {
                    // Check if already inactive
                    if (!m.IsActive)
                    {
                        Console.WriteLine(m.Name + " is already inactive.");
                        return;
                    }
                    m.Deactivate();
                    return;
                }
            }
            Console.WriteLine("Member ID " + id + " not found.");
        }

        // Finds a member by ID and reactivates their membership
        public void ReactivateMember()
        {
            Console.WriteLine("\n--- Reactivate Member ---");

            if (members.Count == 0)
            {
                Console.WriteLine("No members registered yet.");
                return;
            }

            // Show all inactive members only
            bool hasInactive = false;
            Console.WriteLine("Inactive Members:");
            foreach (Member m in members)
            {
                if (!m.IsActive)
                {
                    Console.WriteLine("  [" + m.MemberId + "] " + m.Name);
                    hasInactive = true;
                }
            }

            if (!hasInactive)
            {
                Console.WriteLine("No inactive members found.");
                return;
            }

            int id = ReadPositiveInt("\nEnter Member ID : ");

            foreach (Member m in members)
            {
                if (m.MemberId == id)
                {
                    // Check if already active
                    if (m.IsActive)
                    {
                        Console.WriteLine(m.Name + " is already active.");
                        return;
                    }
                    m.Reactivate();
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

            // Validate name
            string name = ReadRequiredString("Name  : ");

            // Validate email - must be valid and not already registered
            string email;
            while (true)
            {
                email = ReadEmail("Email : ");
                if (EmailExistsInStaff(email))
                    Console.WriteLine("  That email is already registered to another staff member.");
                else
                    break;
            }

            // Validate phone
            string phone = ReadPhone("Phone : ");

            // Validate role
            string role = ReadRequiredString("Role  : ");

            // Create the new staff object and add it to the list
            Staff newStaff = new Staff(nextStaffId, name, email, phone, role);
            staff.Add(newStaff);
            nextStaffId++;

            Console.WriteLine("\n✔ Staff added! Staff ID: " + (nextStaffId - 1));
        }

        // Loops through all staff and prints their info
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

        // Prompts the user for details and adds a new gym class to the list
        public void AddGymClass()
        {
            Console.WriteLine("\n--- Add New Gym Class ---");

            // Validate class name - check for duplicates
            string className;
            while (true)
            {
                className = ReadRequiredString("Class Name       : ");
                bool duplicate = false;
                foreach (GymClass gc in gymClasses)
                {
                    if (gc.ClassName.ToLower() == className.ToLower())
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (duplicate)
                    Console.WriteLine("  A class with that name already exists.");
                else
                    break;
            }

            // Validate instructor name
            string instructor = ReadRequiredString("Instructor Name  : ");

            // Validate capacity - must be between 1 and 100
            int capacity;
            while (true)
            {
                capacity = ReadPositiveInt("Capacity (1-100) : ");
                if (capacity <= 100)
                    break;
                Console.WriteLine("  Capacity cannot exceed 100.");
            }

            // Create and add the new gym class
            GymClass newClass = new GymClass(className, instructor, capacity);
            gymClasses.Add(newClass);

            Console.WriteLine("\n✔ Gym class added successfully!");
        }

        // Lets the user pick a member and a class then enrolls them
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

            // Display all members
            Console.WriteLine("Members:");
            foreach (Member m in members)
                Console.WriteLine("  [" + m.MemberId + "] " + m.Name + " - " + m.GetStatus());

            int memberId = ReadPositiveInt("Enter Member ID  : ");

            // Find the selected member
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
                Console.WriteLine("Member ID " + memberId + " not found.");
                return;
            }

            // Display all classes
            Console.WriteLine("\nGym Classes:");
            for (int i = 0; i < gymClasses.Count; i++)
                Console.WriteLine("  [" + (i + 1) + "] " + gymClasses[i].ClassName + " - " + gymClasses[i].InstructorName);

            // Validate class selection
            int classChoice;
            while (true)
            {
                classChoice = ReadPositiveInt("Choose Class     : ");
                if (classChoice >= 1 && classChoice <= gymClasses.Count)
                    break;
                Console.WriteLine("  Please choose a number between 1 and " + gymClasses.Count + ".");
            }

            // Call Enroll() on the selected class - it handles capacity and duplicate checks
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
                Console.WriteLine("  [" + (i + 1) + "] " + gymClasses[i].ClassName);

            // Validate class selection
            int choice;
            while (true)
            {
                choice = ReadPositiveInt("Choose Class : ");
                if (choice >= 1 && choice <= gymClasses.Count)
                    break;
                Console.WriteLine("  Please choose a number between 1 and " + gymClasses.Count + ".");
            }

            Console.WriteLine();
            gymClasses[choice - 1].DisplayRoster();
        }

        // ── Main Menu ───────────────────────────────────

        // Main loop - keeps the program running until the user chooses Exit
        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("    GYM MANAGEMENT SYSTEM       ");
                Console.WriteLine("=================================");
                Console.WriteLine("  1.  Add Member                ");
                Console.WriteLine("  2.  View All Members          ");
                Console.WriteLine("  3.  Deactivate Member         ");
                Console.WriteLine("  4.  Reactivate Member         ");
                Console.WriteLine("  5.  Add Staff                 ");
                Console.WriteLine("  6.  View All Staff            ");
                Console.WriteLine("  7.  Add Gym Class             ");
                Console.WriteLine("  8.  Enroll Member in Class    ");
                Console.WriteLine("  9.  View Class Roster         ");
                Console.WriteLine("  10. Exit                      ");
                Console.WriteLine("================================");
                Console.Write("Choose an option : ");

                string input = Console.ReadLine();

                // Call the matching method based on user input
                switch (input)
                {
                    case "1": AddMember(); break;
                    case "2": DisplayAllMembers(); break;
                    case "3": DeactivateMember(); break;
                    case "4": ReactivateMember(); break;
                    case "5": AddStaff(); break;
                    case "6": DisplayAllStaff(); break;
                    case "7": AddGymClass(); break;
                    case "8": EnrollMemberInClass(); break;
                    case "9": DisplayClassRoster(); break;
                    case "10": running = false; break;
                    default:
                        Console.WriteLine("Invalid option. Please choose 1-10.");
                        break;
                }
            }

            Console.WriteLine("\nGoodbye!");
        }
    }
}