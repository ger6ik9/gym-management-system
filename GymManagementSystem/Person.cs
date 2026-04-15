using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    // Base class that holds shared information for all people in the system
    class Person
    {
        // Private fields - can only be accessed through properties
        private string name;
        private string email;
        private string phone;

        // Constructor - runs when a Person object is created
        public Person(string name, string email, string phone)
        {
            this.name = name;
            this.email = email;
            this.phone = phone;
        }

        // Property - allows controlled access to the name field
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Property - allows controlled access to the email field
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        // Property - allows controlled access to the phone field
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        // Virtual method - can be overridden by derived classes
        public virtual void GetInfo()
        {
            Console.WriteLine("Name  : " + name);
            Console.WriteLine("Email : " + email);
            Console.WriteLine("Phone : " + phone);
        }
    }
}