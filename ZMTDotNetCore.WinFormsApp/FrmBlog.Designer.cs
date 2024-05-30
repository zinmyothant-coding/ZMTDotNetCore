namespace ZMTDotNetCore.WinFormsApp
{
    partial class FrmBlog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCanel = new Button();
            btnSave = new Button();
            lblTitle = new Label();
            lblAuthor = new Label();
            lblContent = new Label();
            txtTitle = new TextBox();
            txtAuthor = new TextBox();
            txtContent = new TextBox();
            btnUpdate = new Button();
            SuspendLayout();
            // 
            // btnCanel
            // 
            btnCanel.BackColor = Color.FromArgb(84, 110, 122);
            btnCanel.ForeColor = SystemColors.Window;
            btnCanel.Location = new Point(289, 254);
            btnCanel.Name = "btnCanel";
            btnCanel.Size = new Size(94, 29);
            btnCanel.TabIndex = 0;
            btnCanel.Text = "&Cancel";
            btnCanel.UseVisualStyleBackColor = false;
            btnCanel.Click += btnCanel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(76, 175, 80);
            btnSave.ForeColor = Color.Cornsilk;
            btnSave.Location = new Point(443, 254);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 1;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(158, 31);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(38, 20);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Title";
            lblTitle.Click += label1_Click;
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Location = new Point(158, 91);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(54, 20);
            lblAuthor.TabIndex = 3;
            lblAuthor.Text = "Author";
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new Point(146, 159);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(61, 20);
            lblContent.TabIndex = 4;
            lblContent.Text = "Content";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(289, 24);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(310, 27);
            txtTitle.TabIndex = 5;
            // 
            // txtAuthor
            // 
            txtAuthor.Location = new Point(289, 84);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(310, 27);
            txtAuthor.TabIndex = 6;
            // 
            // txtContent
            // 
            txtContent.Location = new Point(289, 152);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.Size = new Size(310, 71);
            txtContent.TabIndex = 7;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(76, 175, 80);
            btnUpdate.ForeColor = Color.Cornsilk;
            btnUpdate.Location = new Point(443, 254);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 29);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "&Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // FrmBlog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 381);
            Controls.Add(btnUpdate);
            Controls.Add(txtContent);
            Controls.Add(txtAuthor);
            Controls.Add(txtTitle);
            Controls.Add(lblContent);
            Controls.Add(lblAuthor);
            Controls.Add(lblTitle);
            Controls.Add(btnSave);
            Controls.Add(btnCanel);
            Name = "FrmBlog";
            Text = "Form1";
            Load += FrmBlog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCanel;
        private Button btnSave;
        private Label lblTitle;
        private Label lblAuthor;
        private Label lblContent;
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtContent;
        private Button btnUpdate;
    }
}
