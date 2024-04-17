using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Magas
{
    // название класса для работы с бд
    class BD
    {
        // создаем в нем подключение
        OleDbConnection connection = new OleDbConnection();
        // конструктор по умолчанию
        public BD()
        {
            // при создании экземпляра класса сразу прописываем путь к бд
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=bd.accdb;Persist Security Info = False; ";
        }
        // метод входа
        public int LogIn(string com)
        {
            // try и cath ловит ошибки, код, в котором может быть ошибка пишется в первом блоке,  если ошибка происходит, то сделать все, что написано в втором блоке
            try
            {
                // открываем подключение
                connection.Open();
                // создаем новую команду, в которую будем писать наш запрос
                OleDbCommand command = new OleDbCommand();
                // говорим,что команду будет использовать подключение конекшн
                command.Connection = connection;
                // строку которую приняли, присваиваем тексту команды
                command.CommandText = com;
                // создаем ридер, элемент для считываения всей информации
                OleDbDataReader reader = command.ExecuteReader();
                // создаем счетчик
                int count = 0;
                // создаем переменную в которой смотрим админ ли
                bool admin = false;
                // цикл: пока читает, если есть строки, то счетчик++
                while (reader.Read())
                {
                    count++;
                    admin = (bool)reader["admin_us"];
                }
                reader.Close();
                connection.Close();
                // если нашло строку, вывести успешно, закрыть подключение и вернуть 1
                if (count == 1) {
                    MessageBox.Show("Вход выполнен успешно");
                    // если админ вернуть 2, если не админ вернуть 1
                    if (admin == false)
                        return 1;
                    if (admin == true)
                        return 2;
                }
                // если не нашло, вывести сообщение не успешно, закрыть подключение, вернуть 0
                else {
                    MessageBox.Show("Неправильный. Повторите еще раз");
                    return 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                // если произошла ошибка, вывеси ее в сообщении
                MessageBox.Show("FAIL" + ex);
                // закрыть подключение и вернуть0
                connection.Close();
                return 0;
            }
        }

        //вывести данные в грид, принимает команду и грид, в который все это выводить
        public void SelectGridPlus(string com, DataGridView dg)
        {
            try
            {
                connection.Open();
                OleDbCommand mes = new OleDbCommand();
                mes.Connection = connection;
                mes.CommandText = com;
                // резервируем место и создаем новый адаптер, в который считываются значения из таблицы в зависимости от команды
                OleDbDataAdapter da = new OleDbDataAdapter(mes.CommandText, connection);
                // создаем новую таблицу, в которую мы запишем все, что принял адаптер
                DataTable dt = new DataTable();
                // сейчас записываем это
                da.Fill(dt);
                // выводим данные из таблиц в грил
                dg.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // загрузить в выпадающие списки
        // принимаем команду, какой столбез загрузить и в какой комбобокс
        public void LoadBox(string com,string collums, ComboBox cb)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                // присваиваем команду нашему подключению
                command.CommandText = com;
                OleDbDataReader reader = command.ExecuteReader();
                // считываем базу              
                while (reader.Read())
                {
                    // добавляем в выпадающий список
                    cb.Items.Add (reader[collums].ToString());
                }
                reader.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // метод для записи
        // его, метод для удаления и обновления можно бы сделаь одним, все равно строки идентичны
        // но я решил что чтоб не путаться в названиях методов, пусть будут три с одинаковой реализацией
        // но с разными именами
        // все работают по принципу: открыть подключение, использовать команду какую получили ранее
        // и выполнить ее, закрыть подключение и все
        public void Insert(string com)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = com;
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Успешно");

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // метод загрузки лейблов
        // получает команду, колонку которую записать и лэйбл в который записыяваем
        public void LoadLabel(string com, string collums, Label lb)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = com;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // аписать в выбранный лейбл
                    lb.Text += reader[collums].ToString();
                }
                reader.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // то же что и выше, только для текстбоксов
        public void LoadTexbox(string com, string collums, TextBox tb)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = com;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tb.Text += reader[collums].ToString();
                }
                reader.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // метод обновления, реализация как и добавления
        public void Update(string com)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = com;
                command.ExecuteNonQuery();
                connection.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // метод удаления, реализация как ив  добавлении
        public void Delete(string com)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = com;
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // загрузка нужных данных в тект комбобокса
        public void LoadComboBox(string com, string collums, ComboBox cb)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = com;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cb.Text += reader[collums].ToString();
                }
                reader.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // запись и редактирование телефона
        // получаем команду и пикчербокс
        public void InsertTelephone(string com, PictureBox pictureBox1)
        {
            try
            {
                connection.Open();
                // создаем изображение, которое будем использовать пр записи
                var bmp = new Bitmap(100, 100);
                // если есть изображение в пикчербоксе, то используем его          
                if (pictureBox1.Image != null) { bmp = (Bitmap)pictureBox1.Image; }


                // для преобразования рисунка в byte[]
                // создаем поток данных
                var mss = new MemoryStream();
                // пишем в него нашу картинку
                bmp.Save(mss, ImageFormat.Bmp);
                // заходим на начало потока
                mss.Position = 0;

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = com;

                // записать 'рисунок' и другие данные в базу
                command.Parameters.AddWithValue("@image_sk", mss.ToArray());
                command.ExecuteNonQuery();
                
                connection.Close();
                MessageBox.Show("Успешно");

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }
        }
        // метод загрузки изображения
        public void LoadImage(string com, PictureBox pictureBox1)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = com;
                // преобразоввываем изображение из двоичного кода в бмп
                var res = (byte[])command.ExecuteScalar();
                MemoryStream ms = new MemoryStream(res);
                Image img = Bitmap.FromStream(ms);
                // присваиваем его пикчербоксу
                pictureBox1.Image = img;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("FAIL" + ex);
                connection.Close();
            }

        }

    }
}
