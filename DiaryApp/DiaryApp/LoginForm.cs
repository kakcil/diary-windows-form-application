using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLibrary;
using CommonLibrary;


namespace DiaryApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public User user;
        UserContent userOperation;

        private void button3_Click(object sender, EventArgs e)
        {
            //Cancel
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Login
            userOperation = new UserContent();
            user = userOperation.GetUser(txtName.Text, txtPass.Text);
            if (user != null)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("You need to register..");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Register
            userOperation = new UserContent();
            user = new User();
            user.Name = txtName.Text;
            user.Password = txtPass.Text;
            userOperation.AddUser(user);
        }

    }
}
