using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magas
{
    public partial class Form7 : Form
    {
        string command = "";
        BD dbase = new BD();
        // получаем ид телефона или просто число
        int id;
        public Form7(int id)
        {
            // сохраняем в нашу переменную
            this.id = id;
            InitializeComponent();
        }
        // при загрузке формы
        private void Form7_Load(object sender, EventArgs e)
        {
            // загружаем выпадающие списки
            LoadBox();
            // если полученное ид не равно 0, по есть если получаем ид телефона
            if (id!= 0) {
                // то переносим все его данные в поля
                LoadData();
            }
        }
        // загрузка выпадающих списков была описана ранее
        private void LoadBox()
        {
            command = "SELECT * FROM Ekran";
            dbase.LoadBox(command, "rasmEkran_ekr", comboBox1);
            command = "SELECT * FROM Proiswod";
            dbase.LoadBox(command, "proisw_prois", comboBox2);
            command = "SELECT * FROM Systema";
            dbase.LoadBox(command, "system_sys", comboBox3);
            command = "SELECT * FROM Rasreschenie";
            dbase.LoadBox(command, "rasresch_ras", comboBox4);
        }
        // загрузка данных телефона, если приши редактировать
        private void LoadData()
        {
            // получаем все данные о телефоне
            command = "SELECT * FROM Sklad WHERE id_sk = "+id+"";
            // заносим все данные в текстбоксы и комбобоксы
            dbase.LoadTexbox(command, "name_sk", textBox1);
            dbase.LoadTexbox(command, "collNaSklade_sk", textBox2);
            dbase.LoadTexbox(command, "garantMont_sk", textBox3);
            dbase.LoadTexbox(command, "collSim_sk", textBox4);
            dbase.LoadTexbox(command, "dateSosd_sk", textBox5);
            dbase.LoadTexbox(command, "info_sk", textBox6);
            dbase.LoadComboBox(command, "ekran_sk", comboBox1);
            dbase.LoadComboBox(command, "proisw_sk", comboBox2);
            dbase.LoadComboBox(command, "system_sk", comboBox3);
            dbase.LoadComboBox(command, "rasr_sk", comboBox4);
            // заносим картинку
            command = "SELECT image_sk FROM Sklad WHERE id_sk = " + id + "";
            dbase.LoadImage(command, pictureBox1);
        }
        // если клинкули загрузить картинку
        private void button1_Click(object sender, EventArgs e)
        {
            // открываем загрузку
            openFileDialog1.ShowDialog();
        }
        // если выбрали фотот и нажали ок
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // присвоить картинку 
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            // растянуть по размеру
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        // нажата зобновить, редактировать
        private void button2_Click(object sender, EventArgs e)
        {
            // команда записать в таблицу это телефон как новый
            command = "INSERT INTO Sklad(name_sk, collNaSklade_sk, garantMont_sk, collSim_sk, image_sk, ekran_sk, info_sk, proisw_sk, dateSosd_sk, system_sk, rasr_sk) VALUES ('" + textBox1.Text+"','"+ textBox2.Text + "','"+ textBox3.Text + "','"+ textBox4.Text + "', @image_sk, '"+ comboBox1.Text + "', '"+ textBox6.Text + "', '"+ comboBox2.Text + "', '"+ textBox5.Text + "', '"+ comboBox3.Text + "', '"+ comboBox4.Text +"') ";
            // записать
            dbase.InsertTelephone(command, pictureBox1);
            //если пришли редактировать
            if (id != 0)
            {
                // удаляем старый телефон,все равно когда писали обновить, до добавляем этот же телефон по новому
                command = "DELETE * FROM Sklad WHERE id_sk = "+id+" ";
                dbase.Delete(command);
            }
            this.Close();
        }
    }
}
