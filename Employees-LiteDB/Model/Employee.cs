using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Employees_LiteDB.Model
{
    public class Employee
    {
        [BsonId]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Salary { get; set; }

        public Employee()
        { }

        public Employee(int Id, string FirstName, string LastName, int Age, string Email, string PhoneNumber, double Salary)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Age = Age;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Salary = Salary;
        }

        public override string ToString()
        {
            return Id + " " + FirstName + " " + LastName + " " + Age + " " + Email + " " + PhoneNumber + " " + Salary;
        }
    }
}
