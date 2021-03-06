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
    public partial class Changefrm : Form
    {
        Model1 db = new Model1();
        //public Model1 db { get; set; }
        //public Product lich { get; set; }
        //public Manufacturer mn {get; set;}
        public Changefrm()
        {
            InitializeComponent();
            productBindingSource.DataSource = db.Product.ToList();
            manufacturerBindingSource.DataSource = db.Manufacturer.ToList();
        }

        private void Changefrm_Load(object sender, EventArgs e)
        {
            //iDTextBox.Text = lich.ID.ToString();
            //titleTextBox.Text = lich.Title;
            //descriptionTextBox.Text = lich.Description;
            //costTextBox.Text = lich.Cost.ToString();
            //isActiveCheckBox.Checked = lich.IsActive;
            //nameComboBox.Text = lich.Manufacturer.ToString();
            //manufacturerIDComboBox.Text = lich.ManufacturerID.ToString();
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
            lich.ManufacturerID = Convert.ToInt32(manufacturerIDComboBox.SelectedIndex);
            // создаем конвертер для преобразования изображения

            ImageConverter conv = new ImageConverter();
            // создаем массив байтов, из свойства Image ЭУ PictureBox
            byte[] bImg = (byte[])conv.ConvertTo(mainImagePathPictureBox.Image, typeof(byte[]));
            // присваиваем свойству изображение в формате byte[]
            lich.MainImagePath = Convert.ToString(bImg);
            // добавляем новый объект к коллекции

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
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DialogResult = DialogResult.OK;



            //lich.Title = titleTextBox.Text;
            //try
            //{
            //    db.SaveChanges();
            //    DialogResult = DialogResult.OK;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.InnerException.InnerException.Message);
            //}
            //lich.Description = descriptionTextBox.Text;
            //try
            //{
            //    db.SaveChanges();
            //    DialogResult = DialogResult.OK;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.InnerException.InnerException.Message);
            //}
            //lich.Cost = Convert.ToDecimal(costTextBox.Text);
            //try
            //{
            //    db.SaveChanges();
            //    DialogResult = DialogResult.OK;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.InnerException.InnerException.Message);
            //}
            //lich.IsActive = isActiveCheckBox.Checked;
            //try
            //{
            //    db.SaveChanges();
            //    DialogResult = DialogResult.OK;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.InnerException.InnerException.Message);
            //}
            //if (nameComboBox.Text == " ")
            //{
            //    MessageBox.Show(" Не задан производитель продукта!");
            //    return;
            //}
            //lich.ManufacturerID = Convert.ToInt32(manufacturerIDComboBox.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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
    }
}
