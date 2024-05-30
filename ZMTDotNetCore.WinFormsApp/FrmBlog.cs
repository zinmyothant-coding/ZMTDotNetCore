using System.Data;
using ZMTDotNetCore.Shared;
using ZMTDotNetCore.WinFormsApp.Model;

namespace ZMTDotNetCore.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DrapperService _drapperService;
        private readonly int _blogId;
        public FrmBlog()
        {
            InitializeComponent();
            _drapperService = new DrapperService(ConnectionStrings.connectionStrings.ConnectionString);
        }
        public FrmBlog(int id)
        {
            InitializeComponent();
            _blogId = id;
            _drapperService = new DrapperService(ConnectionStrings.connectionStrings.ConnectionString);
            var model = _drapperService.QueryFirstOrDefault<BlogModel>("select * from Tbl_Blog where BlogId=@BlogId;", new { BlogId = id });
            txtTitle.Text = model.BlogTitle;
            txtContent.Text = model.BlogContent;
            txtAuthor.Text = model.BlogAuthor;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            ControlBtn();
        }
        public void ControlBtn()
        {
            txtAuthor.Clear();
            txtContent.Clear();
            txtTitle.Clear();
            txtTitle.Focus();
        }
        private void FrmBlog_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                BlogModel blog = new BlogModel()
                {
                    BlogAuthor = txtAuthor.Text.Trim(),
                    BlogContent = txtContent.Text.Trim(),
                    BlogTitle = txtTitle.Text.Trim()
                };

                int result = _drapperService.Excute<BlogModel>(Queries.CreateBlog, blog);
                string message = result > 0 ? "Save Successfully" : "Save Failed";
                var btn = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, btn);
                ControlBtn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var item = new BlogModel
                {
                    BlogId = _blogId,
                    BlogTitle = txtTitle.Text.Trim(),
                    BlogAuthor = txtAuthor.Text.Trim(),
                    BlogContent = txtContent.Text.Trim()
                };
                string query = @"Update Tbl_Blog set [BlogTitle]=@BlogTitle,[BlogAuthor]=@BlogAuthor,[BlogContent]=@BlogContent where BlogId=@BlogId;";
          int result = _drapperService.Excute<BlogModel>(query, item);
                string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";
                MessageBox.Show(message);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
