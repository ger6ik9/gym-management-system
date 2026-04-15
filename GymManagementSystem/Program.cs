using System;

namespace GymManagementSystem
{
    class Program
    {
        // Entry point - this is where the program starts
        static void Main(string[] args)
        {
            // Create a GymManager object and start the menu loop
            GymManager manager = new GymManager();
            manager.Run();
        }
    }
}