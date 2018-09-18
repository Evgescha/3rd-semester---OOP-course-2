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
    public partial class Form5 : Form
    {
        string command = "";
        BD dbase = new BD();
        // переменные для хранения имени продавца и ид телефона
        int id;
        string prodavec;
        // при создании формы получаем имя продавца и ид телефона
        public Form5(string prod, int id)
        {
            InitializeComponent();
            // сохраняем данные в наши переменные
            this.id = id;
            prodavec = prod;
        }
        
        private void label6_Click(object sender, EventArgs e)
        {

        }
        // при нажатии кнопки заказать
        private void button1_Click(object sender, EventArgs e)
        {
            // команду делаем записать что кому и т.д. в таблицу продано
            command = "INSERT INTO Prodano(prodawec_pr, pocupName_pr, pocupPass_pr, pocepData_pr, telName_pr, garantMont_pr, dataProd_pr ) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + textBox6.Text + "','" + dateTimePicker1.Text + "' )";
            dbase.Insert(command);
            // со склада отнимаем у выбранного телефона его количество минус 1
            command = "UPDATE Sklad SET collNaSklade_sk=collNaSklade_sk-1 WHERE id_sk = "+id+" ";
            dbase.Update(command);
            // закрываем форму
            this.Close();
        }
        // при загрузке формы
        private void Form5_Load(object sender, EventArgs e)
        {
            // присваиваем значение имени продавца и названия в текстбоксе
            textBox1.Text = prodavec;
            command = "SELECT * FROM Sklad WHERE id_sk = " + id + "";
            // загрузить данные телефона в соответствующие поля
            dbase.LoadTexbox(command, "name_sk", textBox3);
            dbase.LoadTexbox(command, "garantMont_sk", textBox6);
        }
    }
}
