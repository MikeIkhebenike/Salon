﻿using System;
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (productDataGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("Title like '{2}%'", textBox1.Text);
        }


        /// <summary>
        /// Check if a given text exists in the given DataGridView at a given column index
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="dataGridView"></param>
        /// <param name="columnIndex"></param>
        /// <returns>The cell in which the searchText was found</returns>
        private DataGridViewCell GetCellWhereTextExistsInGridView(string searchText, DataGridView dataGridView, int columnIndex)
        {
            DataGridViewCell cellWhereTextIsMet = null;

            // For every row in the grid (obviously)
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // I did not test this case, but cell.Value is an object, and objects can be null
                // So check if the cell is null before using .ToString()
                if (row.Cells[1].Value != null && searchText == row.Cells[1].Value.ToString())
                {
                    // the searchText is equals to the text in this cell.
                    cellWhereTextIsMet = row.Cells[columnIndex];
                    break;
                }
            }

            return cellWhereTextIsMet;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DataGridViewCell cell = GetCellWhereTextExistsInGridView(textBox1.Text, productDataGridView, 2);
            if (cell != null)
            {
                // Value exists in the grid
                // you can do extra stuff on the cell
                cell.Style = new DataGridViewCellStyle { ForeColor = Color.Red };
            }
            else
            {
                // Value does not exist in the grid
            }
        }







        private void button5_Click(object sender, EventArgs e)
        {
            Plitka form = new Plitka();
            DialogResult = form.ShowDialog();
        }
    }
}
