namespace WindowsAsyncMore
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnmore = new System.Windows.Forms.Button();
            this.btnold = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnmore
            // 
            this.btnmore.Location = new System.Drawing.Point(82, 103);
            this.btnmore.Name = "btnmore";
            this.btnmore.Size = new System.Drawing.Size(131, 61);
            this.btnmore.TabIndex = 0;
            this.btnmore.Text = "同时执行多个任务";
            this.btnmore.UseVisualStyleBackColor = true;
            this.btnmore.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnold
            // 
            this.btnold.Location = new System.Drawing.Point(344, 103);
            this.btnold.Name = "btnold";
            this.btnold.Size = new System.Drawing.Size(169, 61);
            this.btnold.TabIndex = 1;
            this.btnold.Text = "传统方法执行多个任务";
            this.btnold.UseVisualStyleBackColor = true;
            this.btnold.Click += new System.EventHandler(this.btnold_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 273);
            this.Controls.Add(this.btnold);
            this.Controls.Add(this.btnmore);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnmore;
        private System.Windows.Forms.Button btnold;
    }
}

