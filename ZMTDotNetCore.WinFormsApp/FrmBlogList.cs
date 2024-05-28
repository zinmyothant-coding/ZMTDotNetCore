using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
           List<BlogModel> lst= _drapperService.Query<BlogModel>("select * from Tbl_Blog;");
            dgv.DataSource = lst;
        }
    }
}
