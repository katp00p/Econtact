using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        contactClass c = new contactClass();

        private void btn_add_Click(object sender, EventArgs e)
        {
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNumber = txtContactNumber.Text;
            c.Address = txtAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New contact successfully inserted.");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to insert new contact.");
            }

            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtContactID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text =  "";
            txtContactNumber.Text  = "";
            txtAddress.Text =  "";
            cmbGender.Text =  "";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(txtContactID.Text);
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNumber = txtContactNumber.Text;
            c.Address = txtAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact successfully updated.");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact.");
            }

            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;

        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(txtContactID.Text);

            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Contact successfully deleted.");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete contact.");
            }

        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%'  OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
