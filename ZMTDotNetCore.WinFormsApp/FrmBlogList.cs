using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZMTDotNetCore.Shared;
using ZMTDotNetCore.WinFormsApp.Model;

namespace ZMTDotNetCore.WinFormsApp
{
    public partial class FrmBlogList : Form
    {
        private readonly DrapperService _drapperService;

        public FrmBlogList()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
            _drapperService = new DrapperService(ConnectionStrings.connectionStrings.ConnectionString);

        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            List<BlogModel> lst = _drapperService.Query<BlogModel>("select * from Tbl_Blog;");
            dgv.DataSource = lst;
        }
        private void DataLoad()
        {
            List<BlogModel> lst = _drapperService.Query<BlogModel>("select * from Tbl_Blog;");
            dgv.DataSource = lst;

        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var blogId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["colId"].Value);
            
            int columnIndex = e.ColumnIndex;
            if (e.RowIndex == -1) return;
            #region if case
            if (columnIndex==(int)EnumFormType.Delete)
            {
                var diaLog = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo);
                if (diaLog != DialogResult.Yes) return;
                Delete(blogId);
                DataLoad();
            }
            else if(columnIndex==(int)EnumFormType.Edit)
            {
                FrmBlog frm = new FrmBlog(blogId);
                frm.ShowDialog();
                DataLoad();
            }
            #endregion 
        }
        private void Delete(int id)
        {
            string query = "Delete From Tbl_Blog where BlogId=@BlogId;";
           var result= _drapperService.Excute<BlogModel>(query, new { BlogId=id});
            string message = result > 0 ? "Delete Successful" : "Delete Fail";
            MessageBox.Show(message);
        }
    }
}
