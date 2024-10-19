using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_incom
{
    public partial class Main_Form : Form
    {
        private bool menuExpand = false;
        private Form activeForm;

        public Main_Form()
        {
            InitializeComponent();
            this.MouseDown += MouseMoveForm;
            //label1.MouseDown += MouseMoveForm;
            //button1.MouseDown += MouseMoveForm;
            //pictureBox1.MouseDown += MouseMoveForm;
        }

        

        private void MouseMoveForm(object sender, MouseEventArgs e)
        {
            // Развернуть и Свернуть в окно при двойном клике мышки
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                }
                else if (WindowState == FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Maximized;
                }
            }
            base.Capture = false; // <- Основной объект "Окно формы"
            // указываем все объекты за которые можно перетаскивать форму
            // label1.Capture = false;
            // button1.Capture = false;
            // pictureBox1.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {

        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();    
        }

        private void MaxSized_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        private void Minimum_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.MouseDown += MouseMoveForm;
        }

        private void Panel_Size_Button_Click(object sender, EventArgs e)
        {
            if(menuExpand == false)
            {
                panel6.Size = new Size(50, 630);
            }
        }

        private void Reports_Button_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Reports());
        }

        
        private void OpenChildForm(Form childForm) //Метод для открытия Дочерных форм в главном экране
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.Child_Form_Panel.Controls.Add(childForm);
            this.Child_Form_Panel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Information_Buttom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Information());
        }

        private void Support_Button_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Supports());
        }
    }
    
}
