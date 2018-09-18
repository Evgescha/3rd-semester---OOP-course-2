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
    public partial class Form6 : Form
    {
        string command = "";
        BD dbase = new BD();
        // получаем ид телефона
        int id;
        public Form6(int id)
        {
            InitializeComponent();
            // сохраняем в нашу переменную
            this.id = id;
        }
        // при загрузке
        private void Form6_Load(object sender, EventArgs e)
        {
            // команда получить все данные по выбранному телефону
            command = "SELECT * FROM Sklad WHERE id_sk = "+id+" ";
            // записать эти данные в лейблы
            dbase.LoadLabel(command, "name_sk", label1);
            dbase.LoadLabel(command, "garantMont_sk", label2);
            dbase.LoadLabel(command, "collSim_sk", label3);
            dbase.LoadLabel(command, "ekran_sk", label4);
            dbase.LoadLabel(command, "proisw_sk", label6);
            dbase.LoadLabel(command, "dateSosd_sk", label7);
            dbase.LoadLabel(command, "system_sk", label8);
            dbase.LoadLabel(command, "rasr_sk", label9);
            dbase.LoadTexbox(command, "info_sk", textBox1);
            // показать рисунок телефона
            command = "SELECT image_sk FROM Sklad WHERE id_sk = " + id + " ";
            // поместить рисунок в пикчербокс
            dbase.LoadImage(command, pictureBox1);
        }
    }
}
