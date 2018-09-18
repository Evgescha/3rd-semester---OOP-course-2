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
    public partial class Form2 : Form
    {
        string command = "";
        BD dbase = new BD();
        // с предыдущей формы получаем имя продавца,будем хранить его в этой переменной
        string prodavec;
        public Form2(string prod)
        {           
            InitializeComponent();
            // сохраняем имя
            prodavec = prod;
        }
        // при загрузке формы
        private void Form2_Load(object sender, EventArgs e)
        {
            // даем команду показать определенные столбцы в грид
            command = "SELECT Sklad.id_sk, Sklad.[name_sk], Sklad.[collNaSklade_sk], Sklad.[collSim_sk], Sklad.[proisw_sk] FROM Sklad WHERE   Sklad.[collNaSklade_sk] > 0 ";
            // обращаемся к классу передавая эту команду и грид, в который вывести все
            dbase.SelectGridPlus(command, dataGridView1);
            // загружаем выпадающие списки
            LoadBox();
        }
        // при нажатии на кнопку поиска
        private void button1_Click(object sender, EventArgs e)
        {
            // даем команду показать определенные столбцы
            command = "SELECT Sklad.id_sk, Sklad.[name_sk], Sklad.[collNaSklade_sk], Sklad.[collSim_sk], Sklad.[proisw_sk] FROM Sklad WHERE Sklad.[id_sk] > 0  AND Sklad.[collNaSklade_sk] > 0 ";
            // если что-то вводится в критерии поиска, добавляем это в общую команду
            if (textBox1.Text != "") { command += " AND Sklad.[collSim_sk] = '" + textBox1.Text + "' ";  }
            if (textBox2.Text != "") { command += " AND Sklad.[name_sk] LIKE ('%" + textBox2.Text + "%') "; }
            if (comboBox1.Text != "") { command += " AND Sklad.[ekran_sk] = '" + comboBox1.Text + "' "; }
            if (comboBox2.Text != "") { command += " AND Sklad.[proisw_sk] = '" + comboBox2.Text + "' "; }
            if (comboBox3.Text != "") { command += " AND Sklad.[system_sk] = '" + comboBox3.Text + "' "; }
            if (comboBox4.Text != "") { command += " AND Sklad.[rasr_sk] = '" + comboBox4.Text + "' "; }
            // показать все в гриде с критериями поиска
            dbase.SelectGridPlus(command, dataGridView1);
        }
        // метод загрузки выпадающих списков
        private void LoadBox()
        {
            // даем команду показать значения из определенной таблицы
            command = "SELECT * FROM Ekran";
            // заносим эти значения в определенный комбобокс, который с коммандой передаем в класс
            dbase.LoadBox(command, "rasmEkran_ekr", comboBox1);
            command = "SELECT * FROM Proiswod";
            dbase.LoadBox(command, "proisw_prois", comboBox2);
            command = "SELECT * FROM Systema";
            dbase.LoadBox(command, "system_sys", comboBox3);
            command = "SELECT * FROM Rasreschenie";
            dbase.LoadBox(command, "rasresch_ras", comboBox4);
        }
        // при нажатии на кнопку сброса
        private void button2_Click(object sender, EventArgs e)
        {
            // даем первоначальную команду показать определенные столбы в гриде
            command = "SELECT Sklad.id_sk, Sklad.[name_sk], Sklad.[collNaSklade_sk], Sklad.[collSim_sk], Sklad.[proisw_sk] FROM Sklad WHERE Sklad.[id_sk] > 0 AND Sklad.[collNaSklade_sk] > 0  ";
            // обнуляем значения всех критериев поиска
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            // обновляем грид
            dbase.SelectGridPlus(command, dataGridView1);
        }
        // при нажатии на кнопку оформить
        private void button3_Click(object sender, EventArgs e)
        {
            // смотрим какой телефон выбран в гриде
            int id = int.Parse(dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString());
            // открывает форму заказа, передаем туда имя продавца и ид телефона
            Form5 frm5 = new Form5(prodavec, id);
            frm5.Show();
        }
        // при нажатии кнопки заказа
        private void button4_Click(object sender, EventArgs e)
        {
            // просто открываем форму для заказа
            Form3 frm3 = new Form3();
            frm3.Show();
        }
        // при нажатии кнопки подробнее
        private void button5_Click(object sender, EventArgs e)
        {
            // смотрим какой телефон выбран
            int id = int.Parse(dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString()) ;
            // открываем форму с подробностями телефона и передаем в нее ид выбранного телефона
            Form6 frm6 = new Form6(id);
            frm6.Show();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[comboBox5.Text], ListSortDirection.Ascending);
        }
    }
}
