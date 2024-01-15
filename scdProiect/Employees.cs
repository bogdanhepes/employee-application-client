using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace scdProiect
{
    public partial class Employees : Form
    {
        EmployeeService employeeService;
        List<Employee> employeeList;
        public Employees()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            employeeService.createConnection();
            var employeeList = employeeService.GetEmployees();

            listBox1.DataSource = employeeList;
            listBox1.DisplayMember = "name";

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            menu.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            label8.Visible = true;
            label7.Visible = true;
            label6.Visible = true;

            textBox8.Visible = true;
            textBox7.Visible = true;
            textBox6.Visible = true;
            textBox5.Visible = true;

            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            employeeService.AddEmployee( textBox8.Text, textBox7.Text, textBox6.Text, textBox5.Text);
            var employeeList = employeeService.GetEmployees();
            listBox1.DataSource = employeeList;
            listBox1.DisplayMember = "name";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Employee selectedEmployee = (Employee)listBox1.SelectedItem;

                textBox1.Text = selectedEmployee.name;
                textBox2.Text = selectedEmployee.username;
                textBox3.Text = selectedEmployee.email;
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Employee selectedEmployee = (Employee)listBox1.SelectedItem;
            employeeService.UpdateEmployee(selectedEmployee.id, textBox1.Text, textBox2.Text, textBox3.Text);
            var employeeList = employeeService.GetEmployees();
            listBox1.DataSource = employeeList;
            listBox1.DisplayMember = "name";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Employee selectedEmployee = (Employee)listBox1.SelectedItem;
            employeeService.DeleteEmployee(selectedEmployee.id);
            var employeeList = employeeService.GetEmployees();
            listBox1.DataSource = employeeList;
            listBox1.DisplayMember = "name";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Employee selectedEmployee = (Employee)listBox1.SelectedItem;
            employeeService.AssignEmployeeToDepartment(selectedEmployee.id, Int32.Parse(textBox4.Text));
            var employeeList = employeeService.GetEmployees();
            listBox1.DataSource = employeeList;
            listBox1.DisplayMember = "name";
        }
    }
}
