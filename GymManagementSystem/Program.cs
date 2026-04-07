using System;

namespace GymManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test Person inheritance with Member
            Member m1 = new Member(1, "Gerik Lopez", "gerik@email.com", "123-4567", "Premium");
            m1.GetInfo();
            Console.WriteLine();

            // Test deactivate
            m1.Deactivate();
            Console.WriteLine("Status: " + m1.GetStatus());
            Console.WriteLine();

            // Test Person inheritance with Staff
            Staff s1 = new Staff(1, "Evan", "evan@email.com", "987-6543", "Trainer");
            s1.GetInfo();
            Console.WriteLine();

            // Test assign role
            s1.AssignRole("Manager");
            Console.WriteLine("New Role: " + s1.GetRole());

            Console.ReadLine();
        }
    }
}