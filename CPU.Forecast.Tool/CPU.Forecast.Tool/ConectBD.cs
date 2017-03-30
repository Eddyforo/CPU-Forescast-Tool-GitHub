using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CPU.Forecast.Tool
{
    public partial class ConectBD : DevExpress.XtraEditors.XtraForm
    {
        public ConectBD()
        {
            InitializeComponent();
            txtDataBase.Text = DataConnect.SDataBase ;
            txtDataSource.Text = DataConnect.SDataSource ;
            txtUser.Text = DataConnect.SUser ;
            txtPassword.Text = DataConnect.SPassword;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConect_Click(object sender, EventArgs e)
        {
            DataConnect.SDataBase = txtDataBase.Text;
            DataConnect.SDataSource = txtDataSource.Text;
            DataConnect.SUser = txtUser.Text;
            DataConnect.SPassword = txtPassword.Text;

            if(DataConnect.ConnectToSql())
            {
                Close();
            }
            else
            {
                Error.ShowError(true);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
