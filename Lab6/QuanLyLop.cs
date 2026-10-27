using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Lab6
{
    public partial class frmLop : Form
    {
        private string chuoiketnoi =
    @"Server=(localdb)\MSSQLLocalDB;Database=QuanLyHocSinh;Integrated Security=True;";

        private void KiemTraKetNoi()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(chuoiketnoi))
                {
                    con.Open();
                    MessageBox.Show("Ket noi thanh cong!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }
        }
        public frmLop()
        {
            InitializeComponent();

            lvLop.View = View.Details;
            lvLop.FullRowSelect = true;
            lvLop.GridLines = true;

            if (lvLop.Columns.Count == 0)
            {
                lvLop.Columns.Add("Mã lớp", 100);
                lvLop.Columns.Add("Tên lớp", 180);
                lvLop.Columns.Add("Sĩ số", 80);
            }

            loadDanhSach();
        }

        private void QuanLyLop_Load(object sender, EventArgs e)
        {
            try
            {
                loadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        private void loadDanhSach()
        {
            lvLop.Items.Clear();

            using (SqlConnection con = new SqlConnection(chuoiketnoi))
            {
                con.Open();

                string sql = "SELECT MaLop, TenLop, SiSo FROM dbo.Lop";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    int dem = 0;

                    while (dr.Read())
                    {
                        string ma = dr["MaLop"].ToString();
                        string ten = dr["TenLop"].ToString();
                        string siSo = dr["SiSo"].ToString();

                        ListViewItem item = new ListViewItem(ma);
                        item.SubItems.Add(ten);
                        item.SubItems.Add(siSo);

                        lvLop.Items.Add(item);
                        dem++;
                    }

                    MessageBox.Show("Số lớp đọc được: " + dem);
                }
            }
        }
        private void cboListType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
