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
    public partial class Departments : Form
    {
        DepartmentService departmentService;
        List<Department> departmentList;
        public Departments()
        {
            InitializeComponent();
            departmentService = new DepartmentService();
            departmentService.createConnection();
            var departmentList = departmentService.GetDepartments();

            listBox1.DataSource = departmentList;
            listBox1.DisplayMember = "description";

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Department selectedDepartment = (Department)listBox1.SelectedItem;

                textBox1.Text = selectedDepartment.description;
                textBox3.Text = Convert.ToString(selectedDepartment.managerId);            }
            else
            {
                textBox1.Clear();
                textBox3.Clear();
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Department selectedDepartment = (Department)listBox1.SelectedItem;
            departmentService.UpdateDepartment(selectedDepartment.departmentId, textBox1.Text);
            var departmentList = departmentService.GetDepartments();
            listBox1.DataSource = departmentList;
            listBox1.DisplayMember = "description";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            label8.Visible = true;

            textBox8.Visible = true;
            textBox7.Visible = true;
            
            button4.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            menu.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            departmentService.AddDepartment(textBox8.Text, Int32.Parse(textBox7.Text));
            var departmentList = departmentService.GetDepartments();
            listBox1.DataSource = departmentList;
            listBox1.DisplayMember = "description";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Department selectedDepartment = (Department)listBox1.SelectedItem;
            departmentService.DeleteDepartment(selectedDepartment.departmentId);
            var departmentList = departmentService.GetDepartments();
            listBox1.DataSource = departmentList;
            listBox1.DisplayMember = "description";
        }
    }
}
