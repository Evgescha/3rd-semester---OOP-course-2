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
    public partial class Form1 : Form
    {
        //строковая перменная для хранения комманд
        string command = "";
        //создаем экземпляр класса для работы с бд
        BD dbase = new BD();
        public Form1()
        {
            InitializeComponent();
            
        }
        // при нажатии на кнопку входа
        private void button1_Click(object sender, EventArgs e)
        {
            // даем команду показать всех с введенными логином и паролем
            command = "SELECT * FROM [User] WHERE User.[login_us] = '"+textBox1.Text+"' AND User.[pass_us] = '"+textBox2.Text+"'";
            // интовой переменной присваиваем то, что нам вернул запрос класса
            // 0 если таких нет, 1 если это продавец, 2 если адми
            int result = dbase.LogIn(command);
            // если продавец, открываем форму для продавца
            if (result == 1)
            {
                Form2 frm2 = new Form2(textBox1.Text);
                frm2.Show();
            }
            // если админ, открываем форму для админа
            if (result == 2)
            {
                Form8 frm8 = new Form8();
                frm8.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
