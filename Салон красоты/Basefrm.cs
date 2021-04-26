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
    public partial class Basefrm : Form
    {
        Model1 db = new Model1();
        List<string> customeIdList = new List<string>();
        public Basefrm()
        {
            InitializeComponent();
            productBindingSource.DataSource = db.Product.ToList();
            foreach (Product o in db.Product)
            {
                customeIdList.Add(o.Title);
            }
            customeIdList.Sort();
            var uniq = customeIdList.Distinct();
            foreach (Product o in db.Product)
            {
                customeIdList.Add(o.Title);
            }
            customeIdList.Sort();
            var i = customeIdList.Distinct();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Addfrm form = new Addfrm();
            DialogResult = form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Changefrm form = new Changefrm();
            DialogResult = form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product lich = (Product)productBindingSource.Current;
            DialogResult dr = MessageBox.Show(
                "Вы действительно хотите удалить данные товара? " + lich.ID.ToString(),
                "Удаление данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                db.Product.Remove(lich);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                productBindingSource.DataSource = db.Product.ToList();
            }
        }
    }
}
