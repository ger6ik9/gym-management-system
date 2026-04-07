using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    class Person
    {
        // Fields
        private string name;
        private string email;
        private string phone;

        // Constructor
        public Person(string name, string email, string phone)
        {
            this.name = name;
            this.email = email;
            this.phone = phone;
        }

        // Getters and Setters
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        // Method
        public virtual void GetInfo()
        {
            Console.WriteLine("Name  : " + name);
            Console.WriteLine("Email : " + email);
            Console.WriteLine("Phone : " + phone);
        }
    }
}
