using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace my_cam
{
    public partial class Form2 : Form
    {
        public bool can_drag = true;
        bool dragging = false;
        int min_x = 200;
        int min_y = 200;
        public void UI_Init()
        {
            c.Left = this.Width / 2 - 25;
            c.Top = this.Height / 2 - 25;

            lt.Left = 0;
            lt.Top = 0;



            t.Left = this.Width / 2 - 25;
            t.Top = 0;

            rt.Left = this.Width - 50 - 1;
            rt.Top = 0;

            l.Left = 0;
            l.Top = this.Height / 2 - 25;

            r.Left = this.Width - 50 - 1;
            r.Top = this.Height / 2 - 25;

            lb.Left = 0;
            lb.Top = this.Height - 50 - 1;

            b.Left = this.Width / 2 - 25;
            b.Top = this.Height - 50 - 1;

            rb.Left = this.Width - 50 - 1;
            rb.Top = this.Height - 50 - 1;
            C_txt.Left = this.Width / 2 - 25 + 60;
            C_txt.Top = this.Height / 2 - 25 + 60;
            C_txt.Text = "( " + this.Width + " , " + this.Height + ")";
            this.Refresh();
        }
        public Form2()
        {
            InitializeComponent();
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia;
            this.TopMost = true;
            c.Size = new Size(50, 50);
            lt.Size = new Size(50, 50);
            t.Size = new Size(50, 50);
            rt.Size = new Size(50, 50);
            l.Size = new Size(50, 50);
            r.Size = new Size(50, 50);
            lb.Size = new Size(50, 50);
            b.Size = new Size(50, 50);
            rb.Size = new Size(50, 50);

            c.Cursor = System.Windows.Forms.Cursors.SizeAll;



            lt.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            t.Cursor = System.Windows.Forms.Cursors.SizeNS;
            rt.Cursor = System.Windows.Forms.Cursors.SizeNESW;

            l.Cursor = System.Windows.Forms.Cursors.SizeWE;
            r.Cursor = System.Windows.Forms.Cursors.SizeWE;

            lb.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            b.Cursor = System.Windows.Forms.Cursors.SizeNS;
            rb.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
        }

        private void Lt_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void Lt_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {                
                if (this.Width - e.X < min_x) return;
                if (this.Height - e.Y < min_y) return;

                this.Left += e.X;
                this.Width -= e.X;
                this.Top += e.Y;
                this.Height -= e.Y;
                UI_Init();
            }
        }

        private void Lt_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void T_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void T_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {
                if (this.Height - e.Y < min_y) return;

                this.Top += e.Y;
                this.Height -= e.Y;
                UI_Init();
            }
        }

        private void T_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }


        private void Rt_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void Rt_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {
                if (this.Width + e.X < min_x) return;
                if (this.Height - e.Y < min_y) return;

                this.Top += e.Y;
                this.Width += e.X;
                this.Height -= e.Y;
                UI_Init();
            }
        }

        private void Rt_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void L_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void L_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {
                if (this.Width - e.X < min_x) return;

                this.Left += e.X;
                this.Width -= e.X;

                UI_Init();
            }
        }

        private void L_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void R_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void R_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {
                if (this.Width + e.X < min_x) return;

                this.Width += e.X;
                UI_Init();
            }
        }

        private void R_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Lb_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void Lb_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {
                if (this.Width - e.X < min_x) return;
                if (this.Height + e.Y < min_y) return;

                this.Left += e.X;
                this.Width -= e.X;
                this.Height += e.Y;
                UI_Init();
            }
        }

        private void Lb_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void B_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void B_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {
                if (this.Height + e.Y < min_y) return;

                this.Height += e.Y;
                UI_Init();
            }
        }

        private void B_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Rb_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void Rb_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {
                if (this.Width + e.X < min_x) return;
                if (this.Height + e.Y < min_y) return;

                this.Width += e.X;
                this.Height += e.Y;
                UI_Init();
            }
        }

        private void Rb_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void C_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
        }

        private void C_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && can_drag)
            {

                this.Left += e.X - 25;
                this.Top += e.Y - 25;

                if (this.Left + e.X - 25 < 0)
                {
                    this.Left = 0;
                }
                if (this.Top + e.Y - 25 < 0)
                { 
                    this.Top = 0;
                }
                /*if (this.Left + this.Width > Screen.PrimaryScreen.Bounds.Width)
                {
                    this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
                }
                if (this.Top + this.Height > Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Top = Screen.PrimaryScreen.Bounds.Height - this.Height;
                }*/
               
                UI_Init();
            }
        }

        private void C_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Back_panel_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle
                     ,Color.FromArgb(50,255,0,0),1, ButtonBorderStyle.Dotted
                     ,Color.FromArgb(50,255,0,0),1, ButtonBorderStyle.Dotted
                     ,Color.FromArgb(50,255,0,0),1, ButtonBorderStyle.Dotted
                     ,Color.FromArgb(50,255,0,0),1, ButtonBorderStyle.Dotted
                    );
            //back_panel.BackColor = Color.FromArgb(50, 88, 44, 55);
        }
    }
}
