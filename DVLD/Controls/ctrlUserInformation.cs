using DataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Controls
{
    public partial class ctrlUserInformation : UserControl
    {
        private int _PersonID = -1;

        public ctrlUserInformation()
        {
            InitializeComponent();
        }

        public void LoadctrlUserInformation(int PersonID)
        {
            ctrlPersonInformation1.LoadctrlPersonInformation(PersonID);
            _PersonID = PersonID;

            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = clsUser.GetLoginInformation(_PersonID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblUserID.Text = row["UserID"].ToString();
                lblUserName.Text = row["UserName"].ToString();
                lblIsActive.Text = (Convert.ToInt16(row["IsActive"]) == 1) ? "Yes" : "NO";
            }
        }
    }
}
