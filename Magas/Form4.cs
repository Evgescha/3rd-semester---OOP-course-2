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
    public partial class Form4 : Form
    {
        string command = "";
        BD dbase = new BD();
        public Form4()
        {
            InitializeComponent();
            // даем команду показать все проданые товары
            command = "SELECT * FROM Prodano ";
            // заносим это в грид
            dbase.SelectGridPlus(command, dataGridView1);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        // при нажатии кнопки сортировки
        private void button1_Click(object sender, EventArgs e)
        {
            // даем команду показать все товары
            command = "SELECT * FROM Prodano WHERE EMAI_pr > 0  ";
            // если  есть критерии, добавляем в общую команду
            if (textBox1.Text != "")
                command += " AND prodawec_pr = '"+textBox1.Text+"' ";
            if (textBox2.Text != "")
                command += " AND telName_pr LIKE ('%"+textBox2.Text+"%') ";
            if (dateTimePicker1.Enabled == true)
                command += " AND dataProd_pr LIKE ('%" + dateTimePicker1.Text + "%') ";
            // обновляем грид
            dbase.SelectGridPlus(command, dataGridView1);
           
        }
        // если включаем или выключаем чекбокс
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // то включаем или выключаем сортировку по дате соответственно
            dateTimePicker1.Enabled = checkBox1.Checked;
        }
    }
}
