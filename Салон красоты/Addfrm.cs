using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Салон_красоты
{
    public partial class Addfrm : Form
    {
        Model1 db = new Model1();
        public Addfrm()
        {
            InitializeComponent();
            productBindingSource.DataSource = db.Product.ToList();
            manufacturerBindingSource.DataSource = db.Manufacturer.ToList();
        }

        private void Addfrm_Load(object sender, EventArgs e)
        {
            
        }
        private void nameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            iDTextBox.ReadOnly = true;
            if (titleTextBox.Text == "" || descriptionTextBox.Text == "" ||
            costTextBox.Text == "" /*|| nameComboBox.Text == ""*/)
            {
                MessageBox.Show("Одно из важных полей не заполнено!");
                return;
            }
            Product lich = new Product();
            lich.Title = titleTextBox.Text;
            lich.Description = descriptionTextBox.Text;
            lich.Cost = Convert.ToDecimal(costTextBox.Text);
            lich.IsActive = isActiveCheckBox.Checked;
            if (nameComboBox.Text == " ")
            {
                MessageBox.Show(" Не задан производитель продукта!");
                return;
            }
           
            //lich.ID = Convert.ToInt32(iDTextBox.Text);
            //int id = int.Parse(nameComboBox.Text);
            //// ищем сотрудника с заданным ID – первичный ключ
            //Product std = db.Product.Find(id);
            //if (std == null)
            //{
            //    MessageBox.Show(" Продукта с таким ID нет! ");
            //    return;
            //}
            //lich.Manufacturer = Convert.ToString( nameComboBox.Text);
            db.Product.Add(lich);
            try
            {
                // сохраняем сделанные изменения в БД
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
            DialogResult = DialogResult.OK;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = " Выберите фото сотрудника ";
            ofd.InitialDirectory = @" C:\Users\micha\OneDrive\Изображения\Students_Photos";
            ofd.Filter = " Файлы JPG, GIF, PNG| *.jpg; *.gif; *.png | Все файлы(*.*) | *.* ";
            DialogResult rc = ofd.ShowDialog();
            if (rc == DialogResult.OK)
            {
                // показываем выбранный файл в PictureBox
                mainImagePathPictureBox.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

