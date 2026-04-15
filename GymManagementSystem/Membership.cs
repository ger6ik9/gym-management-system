using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagementSystem
{
    class Membership
    {
        // Fields
        private string type;
        private decimal price;
        private int durationMonths;

        // Constructor
        public Membership(string type, decimal price, int durationMonths)
        {
            this.type = type;
            this.price = price;
            this.durationMonths = durationMonths;
        }

        // Getters and Setters
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public int DurationMonths
        {
            get { return durationMonths; }
            set { durationMonths = value; }
        }

        // Methods
        public void GetDetails()
        {
            Console.WriteLine("--- Membership Details ---");
            Console.WriteLine("Type     : " + type);
            Console.WriteLine("Price    : $" + price + "/month");
            Console.WriteLine("Duration : " + durationMonths + " months");
        }

        public decimal GetPrice()
        {
            return price;
        }
    }
}