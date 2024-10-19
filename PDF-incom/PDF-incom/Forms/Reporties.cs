using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using System.Drawing.Printing;
public delegate void MyEventHandler();

namespace PDF_incom
{
    public partial class Reports : Form
    {
        string folderPath = @"C:\Users\Yaznur.sadykov\Desktop\Губкинский"; //Путь, где лежат архивы
        public Reports()
        {
            InitializeComponent();
            //Start_Open_datagrid();
        }

        public void MouseHookWeb() //Вызывает события
        {
            this.FormClosed += new FormClosedEventHandler(frmMain_FormClosed);
            MouseHook.MouseDown += new MouseEventHandler(MouseHook_MouseUp);
            MouseHook.LocalHook = false;
            MouseHook.InstallHook();
        }

        /*public void Start_Open_datagrid()
        {
            string[] files = Directory.GetFiles(folderPath);

            dataGridView1.Rows.Clear();

            foreach (string file in files)
            {
                DateTime modifiedDate = File.GetLastWriteTime(file);

                string fileName = Path.GetFileName(file);

                dataGridView1.Rows.Add(fileName);
                
            }
        }*/

        private void Open_Button_Click(object sender, EventArgs e) //Кнопка открывает выбранный архивный файл
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedFileName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // Получаем выбранное имя файла из DataGridView

                string filePath = Path.Combine(folderPath, selectedFileName); // Формируем путь к выбранному файлу

                if (File.Exists(filePath))
                {
                    webBrowser1.Navigate(filePath);
                }
                else
                {
                    MessageBox.Show("Файл не найден");
                }
            }
            else
            {
                MessageBox.Show("Выберите файл для открытия");
            }

            MouseHookWeb();
        }
        
        private void Search_Button_Click(object sender, EventArgs e) //Кнопка поиска архивных документов по выбранной дате
        {
            DateTime selectedDate = dateTimePicker1.Value;

            string[] files = Directory.GetFiles(folderPath);

            dataGridView1.Rows.Clear();

            foreach (string file in files)
            {
                DateTime modifiedDate = File.GetLastWriteTime(file);

                if (modifiedDate.Date == selectedDate.Date)
                {
                    string fileName = Path.GetFileName(file);

                    dataGridView1.Rows.Add(fileName);
                }
            }
        }

        private void PrintButton_Click(object sender, EventArgs e) //Отправляет документ на печать
        {
            

            webBrowser1.Print();            
        }

        //Отлавливает нажатие кнопки, если кнопка нажата в пределах WebBrowser - то оно сварачивается.
        void MouseHook_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Location.X > (ActiveForm.Location.X + webBrowser1.Location.X) && //Левый предел
                    e.Location.Y > (ActiveForm.Location.Y + webBrowser1.Location.Y) && //Верхний предел
                    e.Location.X < (ActiveForm.Location.X + webBrowser1.Location.X+ webBrowser1.Size.Width) &&  //Правый предел
                    e.Location.Y < (ActiveForm.Location.Y + webBrowser1.Location.Y + webBrowser1.Size.Height))  //Нижний предел
                {
                    webBrowser1.Hide();
                }
            }

            else if (e.Button == MouseButtons.Left)
            {
                if (e.Location.X > (ActiveForm.Location.X + webBrowser1.Location.X) &&
                    e.Location.Y > (ActiveForm.Location.Y + webBrowser1.Location.Y) &&
                    e.Location.X < (ActiveForm.Location.X + webBrowser1.Location.X + webBrowser1.Size.Width) &&
                    e.Location.Y < (ActiveForm.Location.Y + webBrowser1.Location.Y + webBrowser1.Size.Height))
                {
                    webBrowser1.Hide();
                }

            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e) //Надо
        {
            MouseHook.UnInstallHook(); // Обязательно !!!
        }

        
    }
}
