using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    // Represents a gym membership plan with its type, price, and duration
    class Membership
    {
        // Fields that define the membership plan
        private string type;
        private decimal price;
        private int durationMonths;

        // Constructor - sets all plan details when a Membership object is created
        public Membership(string type, decimal price, int durationMonths)
        {
            this.type = type;
            this.price = price;
            this.durationMonths = durationMonths;
        }

        // Property - allows getting and setting the membership type
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        // Property - allows getting and setting the membership price
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        // Property - allows getting and setting the duration in months
        public int DurationMonths
        {
            get { return durationMonths; }
            set { durationMonths = value; }
        }

        // Prints the full membership plan details to the console
        public void GetDetails()
        {
            Console.WriteLine("--- Membership Details ---");
            Console.WriteLine("Type     : " + type);
            Console.WriteLine("Price    : $" + price + "/month");
            Console.WriteLine("Duration : " + durationMonths + " months");
        }

        // Returns just the price value for calculations
        public decimal GetPrice()
        {
            return price;
        }
    }
}