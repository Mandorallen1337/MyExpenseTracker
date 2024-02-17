using MyExpenseTracker.ExpenseTracker.DataAccess;
using MyExpenseTracker.ExpenseTracker.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExpenseTracker.Forms
{
    public partial class LogInForm : Form
    {
        
        public LogInForm()
        {
            InitializeComponent();

            this.Paint += LogInForm_Paint;
            registerPanel.Paint += registerPanel_Paint;
        }

        private void LogInForm_Paint(object sender, PaintEventArgs e)
        {
            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(this.Width, this.Height);
            
            Color color1 = Color.FromArgb(255, 255, 128, 128);
            Color color2 = Color.FromArgb(255, 128, 128, 255);

            LinearGradientBrush gradientBrush = new LinearGradientBrush(startPoint, endPoint, color1, color2);
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }

        private void registerPanel_Paint(object sender, PaintEventArgs e)
        {
            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(this.Width, this.Height);

            Color color1 = Color.FromArgb(255, 153, 102, 204);
            Color color2 = Color.FromArgb(255, 204, 153, 255);

            LinearGradientBrush gradientBrush = new LinearGradientBrush(startPoint, endPoint, color1, color2);
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
           if (string.IsNullOrEmpty(usernameBox.Text) || string.IsNullOrEmpty(passwordBox.Text))
            {
                MessageBox.Show("Username and Password are required");
                return;
            }
            UserRepository userRepository = new UserRepository();

            try
            {
                userRepository.LoginCheck(usernameBox.Text, passwordBox.Text);
                MessageBox.Show("Login Successful");
                ExpenseTrackerForm expenseTrackerForm = new ExpenseTrackerForm();
                expenseTrackerForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Failed: " + ex.Message);
            }

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(regUserBox.Text) || string.IsNullOrEmpty(regPasswordBox.Text) || string.IsNullOrEmpty(regEmailBox.Text))
            {
                MessageBox.Show("Username, Password and Email are required");
                return;
            }
            UserRepository userRepository = new UserRepository();
            try
            {
                userRepository.RegisterUser(regUserBox.Text, regPasswordBox.Text, regEmailBox.Text);
                MessageBox.Show("Registration Successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration Failed: " + ex.Message);
            }
        }
    }
}
