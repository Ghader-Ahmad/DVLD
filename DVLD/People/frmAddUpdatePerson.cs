﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public frmAddUpdatePerson(int personID)
        {
            InitializeComponent();
            ctrlAdd_EditPerson1.LoadctrlAddEditPerson(personID);
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            
        }
    }
}
