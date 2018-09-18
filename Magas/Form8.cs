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
    public partial class Form8 : Form
    {
        string command = "";
        BD dbase = new BD();
        public Form8()
        {
            InitializeComponent();
            // загрузить данные в гриды
            LoadDataGrid();
        }
        // загрузка данных в гриды
        private void LoadDataGrid()
        {
            // показать данные о телефоне кроме картинки
            command = "SELECT Sklad.id_sk, Sklad.name_sk, Sklad.collNaSklade_sk, Sklad.garantMont_sk, Sklad.collSim_sk,  Sklad.ekran_sk, Sklad.info_sk, Sklad.proisw_sk, Sklad.dateSosd_sk, Sklad.system_sk, Sklad.rasr_sk FROM Sklad ";
            dbase.SelectGridPlus(command, dataGridView1);
            // показать все заказы
            command = "SELECT * FROM Sakas ";
            dbase.SelectGridPlus(command, dataGridView2);
        }
        // кнопка продано
        private void button4_Click(object sender, EventArgs e)
        {
            // открыть форму продаж
            Form4 frm4 = new Form4();
            frm4.Show();
        }
        // кнопка создания
        private void button1_Click(object sender, EventArgs e)
        {
            // открыть форму создания, передаем 0
            Form7 frm7 = new Form7(0);
            frm7.Show();
        }
        // редактирование
        private void button2_Click(object sender, EventArgs e)
        {
            // смотрим какой телефон выбран
            int id = int.Parse(dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString());
            // открыть форму редактирования передает ид телефона который редактировать
            Form7 frm7 = new Form7(id);
            frm7.Show();
        }
        // кнопка удалить
        private void button3_Click(object sender, EventArgs e)
        {
            // если нажали ок
            if (MessageBox.Show("Действительно удалить ?", "Подтвердите удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // смотрим какой телефон удалить
                int id = int.Parse(dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString());
                // команду удалить
                command = "DELETE FROM Sklad WHERE id_sk = " + id + " ";
                // выполнить удаление
                dbase.Delete(command);
                // обновить гриды
                LoadDataGrid();
            }
        }
        // еще одна удалить, принцип тот же
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Действительно удалить ?", "Подтвердите удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = int.Parse(dataGridView2[0, dataGridView2.SelectedCells[0].RowIndex].Value.ToString());
                command = "DELETE FROM Sakas WHERE id_sak = " + id + " ";
                dbase.Delete(command);
                LoadDataGrid();
            }
        }
        // обновить, просто перезагрузка гридов
        private void button6_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }
        // подробнее
        private void button7_Click(object sender, EventArgs e)
        {
            // показать подробности о телефоне
            int id = int.Parse(dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString());
          
            Form6 frm6 = new Form6(id);
            frm6.Show();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
    }
}
