using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Салон_красоты
{
    public partial class Addfrm : Form
    {
        Model1 db = new Model1();
        
        string folder;
        public Addfrm()
        {
            InitializeComponent();
            productBindingSource.DataSource = db.Product.ToList();
            manufacturerBindingSource.DataSource = db.Manufacturer.ToList();
        }

        private void Addfrm_Load(object sender, EventArgs e)
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
            lich.ManufacturerID = Convert.ToInt32(manufacturerIDComboBox.Text);
            // создаем конвертер для преобразования изображения

            ImageConverter conv = new ImageConverter();
            // создаем массив байтов, из свойства Image ЭУ PictureBox
            byte[] bImg = (byte[])conv.ConvertTo(mainImagePathPictureBox.Image, typeof(byte[]));
            // присваиваем свойству изображение в формате byte[]
            lich.MainImagePath = folder;
            // добавляем новый объект к коллекции

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
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите фото";
            ofd.InitialDirectory = @"C:\Users\315\source\repos\Салон красоты\Товары салона красоты";
            ofd.Filter = "Файлы JPG, GIF, PNG|*.jpg;*.gif;*.png|Все файлы (*.*)|*.*";
            DialogResult rc = ofd.ShowDialog();
            if (rc == DialogResult.OK)
            {
                mainImagePathPictureBox.Image = Image.FromFile(ofd.FileName);
                folder = ofd.FileName;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

