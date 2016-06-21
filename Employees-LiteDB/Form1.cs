using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiteDB;
using Employees_LiteDB.Model;

namespace Employees_LiteDB
{
    public partial class Form1 : Form
    {
        private LiteDatabase db;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int counter = 0;
            string line;

            //Initializes db and clears it
            db = new LiteDatabase(@"C:\Users\ffiaux\Desktop\Employees.db");
            LiteCollection<Employee> employees = db.GetCollection<Employee>("employees");
            //employees.Drop();
            //db.Commit();

            if (employees.Count() == 0)
            {
                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader("IN_200_funcionarios.txt");
                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    counter++;

                    string[] employeeLine = line.Split(' ');
                    Employee employee = new Employee
                    {
                        Id = Int32.Parse(employeeLine[0]),
                        FirstName = employeeLine[1],
                        LastName = employeeLine[2],
                        Age = Int32.Parse(employeeLine[3]),
                        Email = employeeLine[4],
                        PhoneNumber = employeeLine[5],
                        Salary = Double.Parse(employeeLine[6].Replace(".", ","))
                    };

                    employees.Insert(employee);
                }

                file.Close();
            }

            employees.EnsureIndex(x => x.Id);

            IEnumerable<Employee> results = employees.Find(Query.GTE("Age", 35));
            Console.WriteLine("Age GTE 35 = " + results.Count<Employee>());

            Employee one = employees.FindById(2);
            Console.WriteLine(one.ToString());

            List<Employee> employeeList = employees.FindAll().ToList<Employee>();
            foreach(Employee emp in employeeList)
            {
                employeeListBox.Items.Add(emp);
            }

            //employeeListBox.DataSource = employeeList;
            //employeeListBox.DisplayMember = "FirstName";
            //employeeListBox.ValueMember = "Id";
        }

        private void employeeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employee emp = (Employee)employeeListBox.SelectedItem;
            if (emp != null)
            {
                textBoxID.Text = emp.Id.ToString();
                textBoxFirstName.Text = emp.FirstName.ToString();
                textBoxLastName.Text = emp.LastName.ToString();
                textBoxAge.Text = emp.Age.ToString();
                textBoxEmail.Text = emp.Email.ToString();
                textBoxSalary.Text = emp.Salary.ToString();
                textBoxPhoneNumber.Text = emp.PhoneNumber.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBoxID.Text);
            LiteCollection<Employee> employees = db.GetCollection<Employee>("employees");
            Employee employee = employees.FindById(id);

            employee.FirstName = textBoxFirstName.Text;
            employee.LastName = textBoxLastName.Text;
            employee.Age = int.Parse(textBoxAge.Text);
            employee.Email = textBoxEmail.Text;
            employee.Salary = double.Parse(textBoxSalary.Text);
            employee.PhoneNumber = textBoxPhoneNumber.Text;

            employees.Update(employee);
            db.Commit();

            employeeListBox.Items[id - 1] = employee;
            employeeListBox.SetSelected(id - 1, true);

            employeeListBox.Refresh();
            employeeListBox.Update();
        }
    }
}
