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
    public partial class Form3 : Form
    {
        string command = "";
        BD dbase = new BD();
        public Form3()
        {
            InitializeComponent();
        }
        // при нажатии на кнопку заказать
        private void button1_Click(object sender, EventArgs e)
        {
            // пишем в команду записать значение текстбокса в таблицу заказы
            command = "INSERT INTO Sakas(dan_sak) VALUES ('"+textBox1.Text+"')";
            // отправляем команду в класс
            dbase.Insert(command);
            // закрываем форму
            this.Close();
        }
    }
}
