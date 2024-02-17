using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExpenseTracker.Forms
{
    public partial class ExpenseTrackerForm : Form
    {
        private const int ExpandedWidth = 200; // Width of the side panel when expanded
        private const int CollapsedWidth = 50; // Width of the side panel when collapsed
        private const int AnimationInterval = 10; // Interval for animation (milliseconds)
        private const int ResizeIncrement = 5; // Width increment/decrement during animation
        private Timer timer; // Timer for animation
        private bool isExpanded; // Flag to track the state of the side panel

        public ExpenseTrackerForm()
        {
            InitializeComponent();

            this.Paint += ExpenseTrackerForm_Paint;
            timer = new Timer();
            timer.Interval = AnimationInterval;
            timer.Tick += Timer_Tick;

            sidePanel.MouseEnter += SidePanel_MouseEnter;
            sidePanel.MouseLeave += SidePanel_MouseLeave;
        }

        private void SidePanel_MouseLeave(object sender, EventArgs e)
        {
            isExpanded = false;
            timer.Start();
        }

        private void SidePanel_MouseEnter(object sender, EventArgs e)
        {
            isExpanded = true;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                if (sidePanel.Width < ExpandedWidth)
                {
                    sidePanel.Width += ResizeIncrement;
                }
                else
                {
                    timer.Stop();
                }
            }
            else
            {
                if (sidePanel.Width > CollapsedWidth)
                {
                    sidePanel.Width -= ResizeIncrement;
                }
                else
                {
                    timer.Stop();
                }
            }
        }

        private void ExpenseTrackerForm_Paint(object sender, PaintEventArgs e)
        {
            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(this.Width, this.Height);

            Color color1 = Color.FromArgb(255, 255, 128, 128);
            Color color2 = Color.FromArgb(255, 128, 128, 255);

            LinearGradientBrush gradientBrush = new LinearGradientBrush(startPoint, endPoint, color1, color2);
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }
    }
}
