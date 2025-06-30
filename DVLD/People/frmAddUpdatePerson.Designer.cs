namespace DVLD.People
{
    partial class frmAddUpdatePerson
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlAdd_EditPerson1 = new DVLD.ctrlAdd_EditPerson();
            this.SuspendLayout();
            // 
            // ctrlAdd_EditPerson1
            // 
            this.ctrlAdd_EditPerson1.BackColor = System.Drawing.Color.White;
            this.ctrlAdd_EditPerson1.Location = new System.Drawing.Point(-1, 0);
            this.ctrlAdd_EditPerson1.Name = "ctrlAdd_EditPerson1";
            this.ctrlAdd_EditPerson1.Size = new System.Drawing.Size(839, 458);
            this.ctrlAdd_EditPerson1.TabIndex = 0;
            // 
            // frmAddUpdatePerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 455);
            this.Controls.Add(this.ctrlAdd_EditPerson1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUpdatePerson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add / Edit Person";
            this.Load += new System.EventHandler(this.frmAddUpdatePerson_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public ctrlAdd_EditPerson ctrlAdd_EditPerson1;
    }
}