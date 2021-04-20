using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetaMart2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    this.barangTableAdapter.Fill(this.appData.barang);
                    barangBindingSource.DataSource = this.appData.barang;
                    // dataGridView.DataSource = barangBindingSource;
                }
                else
                {
                    var query = from o in this.appData.barang
                                where o.NamaBarang.Contains(txtSearch.Text) || o.KodeBarang.Contains(txtSearch.Text) || o.Satuan.Contains(txtSearch.Text)
                                select o;
                    barangBindingSource.DataSource = query.ToList();
                    // dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;
                barangBindingSource.RemoveCurrent();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtKodeBarang.Focus();
                this.appData.barang.AddbarangRow(this.appData.barang.NewbarangRow());
                barangBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtKodeBarang.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            barangBindingSource.ResetBindings(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                barangBindingSource.EndEdit();
                barangTableAdapter.Update(this.appData.barang);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.barangTableAdapter.Fill(this.appData.barang);
            barangBindingSource.DataSource = this.appData.barang;
        }
    }
}
