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
            comboBox1.Items.AddRange(uniq.Select(n => n.ToString()).ToArray());
        }

        private void Basefrm_Load(object sender, EventArgs e)
        {
            DataGridViewRow[] array;
            array = new DataGridViewRow[productDataGridView.Rows.Count];
            productDataGridView.Rows.CopyTo(array, 0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                return;
            }
            string n = (string)comboBox1.SelectedItem;
            var colOrder = db.Product.Where(x => x.Title == n);
            productBindingSource.DataSource = colOrder.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Addfrm form = new Addfrm();
            DialogResult = form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Changefrm form = new Changefrm();
            Product lich = (Product)productBindingSource.Current;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                productBindingSource.DataSource = db.Product.ToList();
                manufacturerBindingSource.DataSource = db.Manufacturer.ToList();
            }
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
        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    (productDataGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("Title like '{2}%'", textBox1.Text);
        //}

       
        private void button4_Click_1(object sender, EventArgs e)
        {

            for (int i = 0; i < productDataGridView.RowCount; i++)
            {
                productDataGridView.Rows[i].Selected = false;
                for (int j = 0; j < productDataGridView.ColumnCount; j++)
                    if (productDataGridView.Rows[i].Cells[j].Value != null)
                        if (productDataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            productDataGridView.Rows[i].Selected = true;
                            break;
                        }
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Plitka form = new Plitka();
            DialogResult = form.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                var uniq = customeIdList.Distinct();
                comboBox1.Items.AddRange(uniq.Select(n => n.ToString()).ToArray());
                comboBox1.Enabled = true;
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.Items.Clear();
                productBindingSource.DataSource = db.Product.ToList();
            }
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    DataGridViewRow[] newRows = new DataGridViewRow[0];
        //    foreach (DataGridViewRow row in array)
        //    {
        //        if (row..IndexOf(textBox1.Text) >= 0)
        //        {
        //            Array.Resize(ref newRows, newRows.Length + 1);
        //            newRows[newRows.Length - 1] = row;
        //        }
        //        productDataGridView.Rows.Clear();
        //        productDataGridView.Rows.AddRange(newRows);
        //    }
        //}
    }
}
