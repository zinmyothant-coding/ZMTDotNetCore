using ZMTDotNetCore.Shared;
using ZMTDotNetCore.WinFormsApp.Model;

namespace ZMTDotNetCore.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DrapperService _drapperService;
        public FrmBlog()
        {
            InitializeComponent();
            _drapperService = new DrapperService(ConnectionStrings.connectionStrings.ConnectionString);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
