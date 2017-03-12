using DevExpress.XtraEditors;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Card;

namespace CPU.Forecast.Tool
{
    public partial class MainForm : XtraForm
    {

        public static DataTable dtTypeDevices;
        public static DataTable dtMaximunCost;
        public static DataTable dtComponents;
        public static DataTable dtPlan;
        //public static DataTable dtMaestro;
        //public static DataTable dtDetalle;

        DataTable dtEstimate;
        Clases.Forecast clsTransacc;

        
        public MainForm()
        {
            InitializeComponent();
            InitializeComponentExtra();
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {

        }

        private void tileBar1_Click(object sender, System.EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click_1(object sender, System.EventArgs e)
        {
           // Login Conectar = new Login();

            ConectBD Conectar = new ConectBD();

            Conectar.ShowDialog(this);
        }

        private void PanelPrincipal_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_ItemClick(object sender, TileItemEventArgs e)
        {

            if (PanelPrincipal.SelectedPageIndex == 0)
            {
                this.gvTypeDevices.PostEditor();
                gvTypeDevices.AddNewRow();
            }
            else if (PanelPrincipal.SelectedPageIndex== 1)
            {
                this.gvMaximun.PostEditor();
                gvMaximun.AddNewRow();

            }

            else if (PanelPrincipal.SelectedPageIndex == 2)
            {
                this.gvMaintenceCompo.PostEditor();
                gvMaintenceCompo.AddNewRow();
            }
            else if (PanelPrincipal.SelectedPageIndex == 3)
            {
                this.gvPlan.PostEditor();
                gvPlan.AddNewRow();
            }

        }

        private void InitializeComponentExtra()
        {
            //se selecciona el primer tab
            PanelPrincipal.SelectedPageIndex = 0;

            DataConnect.InicializarVaribales();

            if (!DataConnect.ConnectToSql())
            {
                toolStripButton1_Click_1(this, new EventArgs());
            }

            // clase para hacer las transacciones
            clsTransacc = new Clases.Forecast();


            //se cargan los datagrid's
            CargarGrid();

            clsTransacc.LoadDatosTabla(dtTypeDevices, dtComponents, dtMaximunCost, dtPlan);
        }

        private void CargarGrid()
        {
             dgvTypeDevices.DataSource = CreatablaTipos();
            // Clases.TypeDevices TD = new Clases.TypeDevices();

            //dgvTypeDevices.DataSource = TD.CargaTypeDevices();

            dgvMaximun.DataSource = CreaMaximunCost();

            dgvMaintenceCompo.DataSource = CreaMaintenceCompo();

            dgvPlan.DataSource = CreaPlans();


            //DataSet dtSet = new DataSet("TYPE_DEVICE");
            //dtSet.Tables.Add(dtTypeDevices);
            //dtSet.Tables.Add(dtComponents);

            //DataRelation dtRelation;
            //DataColumn custCol = dtSet.Tables["TYPE_DEVICE"].Columns["PART"];
            //DataColumn orderCol = dtSet.Tables["COMPONENTS"].Columns["PART_CODE"];
            //dtRelation = new DataRelation("CustOrderRelation ", custCol, orderCol);
            //dtSet.Tables["COMPONENTS"].ParentRelations.Add(dtRelation);
            ////dgvLowerCostUnit. SetDataBinding(dtSet, "TYPE_DEVICE");


            //dgvLowerCostUnit.DataSource = dtSet.Tables["COMPONENTS"];
            
            
        }

        //private DataTable CreatablaMaestro()
        //{
            
        //    dtMaestro = new DataTable("PLAN");

        //    dtMaestro.Columns.Add(new DataColumn("PLAN", typeof(string)));
        //    dtMaestro.Columns.Add(new DataColumn("MODEL", typeof(string)));

        //    dtMaestro.Rows.Add("1", "2");
        //   // dtMaestro.Rows.Add("1", "2");
        //    dtMaestro.Rows.Add("1", "3");
        //    //dtMaestro.Rows.Add("1", "3");
        //    dtMaestro.Rows.Add("1", "4");



        //    return dtMaestro;
        //}

        //private DataTable CreatablaDetalle()
        //{
        //    dtDetalle = new DataTable("MODELO");

        //    dtDetalle.Columns.Add(new DataColumn("DESCRIPTION", typeof(string)));
        //    dtDetalle.Columns.Add(new DataColumn("MODEL", typeof(string)));
        //    dtDetalle.Columns.Add(new DataColumn("PART", typeof(string)));
        //    dtDetalle.Columns.Add(new DataColumn("QUANTITY", typeof(Int32)));

        //    dtDetalle.Rows.Add("Prueba","2","adaa",5);
        //    dtDetalle.Rows.Add("Prueba1", "2", "adaa1", 51);
        //    dtDetalle.Rows.Add("Prueba2", "3", "adaa2", 52);
        //    dtDetalle.Rows.Add("Prueba3", "3", "adaa3", 53);
        //    dtDetalle.Rows.Add("Prueba4", "4", "adaa4", 54);


        //    return dtDetalle;
        //}

        private DataTable CreatablaTipos()
        {
            dtTypeDevices = new DataTable("TYPE_DEVICE");

            dtTypeDevices.Columns.Add(new DataColumn("TYPE_DEVICE", typeof(string)));
            dtTypeDevices.Columns.Add(new DataColumn("DESCRIPTION", typeof(string)));
            dtTypeDevices.Columns.Add(new DataColumn("MODEL", typeof(string)));
            dtTypeDevices.Columns.Add(new DataColumn("PART", typeof(string)));
            dtTypeDevices.Columns.Add(new DataColumn("QUANTITY", typeof(Int32)));

            DataColumn[] keys = new DataColumn[3];

            keys[0] = dtTypeDevices.Columns["TYPE_DEVICE"];
            keys[1] = dtTypeDevices.Columns["MODEL"];
            keys[2] = dtTypeDevices.Columns["PART"];

            dtTypeDevices.PrimaryKey = keys;
            

            return dtTypeDevices;
        }

        private DataTable CreaMaintenceCompo()
        {
            dtComponents = new DataTable("COMPONENTS");

            dtComponents.Columns.Add(new DataColumn("PART_CODE", typeof(string)));
            dtComponents.Columns.Add(new DataColumn("DESCRIPTION", typeof(string)));
            dtComponents.Columns.Add(new DataColumn("COST", typeof(decimal)));
            dtComponents.Columns.Add(new DataColumn("STOCK", typeof(Int32)));

            dtComponents.Columns["PART_CODE"].Unique = true;

            return dtComponents;
        }


        private DataTable CreaPlans()
        {
            dtPlan = new DataTable("PLANS");

            dtPlan.Columns.Add(new DataColumn("MODEL", typeof(string)));
            dtPlan.Columns.Add(new DataColumn("PLANS", typeof(Int32)));

            return dtPlan;
        }

        private DataTable CreaMaximunCost()
        {
            dtMaximunCost = new DataTable("MAXIMUN_COST");

            dtMaximunCost.Columns.Add(new DataColumn("MODEL", typeof(string)));
            dtMaximunCost.Columns.Add(new DataColumn("MAX_COST", typeof(decimal)));

            return dtMaximunCost;
        }

        private void tbTypeDevices_ItemClick(object sender, TileItemEventArgs e)
        {
            PanelPrincipal.SelectedPageIndex = 0;
        }

        private void tbMaximunCosts_ItemClick(object sender, TileItemEventArgs e)
        {
            PanelPrincipal.SelectedPageIndex = 1;
        }

        private void tbMaintence_ItemClick(object sender, TileItemEventArgs e)
        {
            PanelPrincipal.SelectedPageIndex = 2;
        }

        private void tbPlans_ItemClick(object sender, TileItemEventArgs e)
        {
            PanelPrincipal.SelectedPageIndex = 3;
        }

        private void btnDelete_ItemClick(object sender, TileItemEventArgs e)
        {
            if (PanelPrincipal.SelectedPageIndex == 0)
            {
                this.gvTypeDevices.PostEditor();
                gvTypeDevices.DeleteRow(gvTypeDevices.FocusedRowHandle);
            }
            else if (PanelPrincipal.SelectedPageIndex == 1)
            {
                this.gvMaximun.PostEditor();
                gvMaximun.DeleteRow(gvMaximun.FocusedRowHandle);

            }

            else if (PanelPrincipal.SelectedPageIndex == 2)
            {
                this.gvMaintenceCompo.PostEditor();
                gvMaintenceCompo.DeleteRow(gvMaintenceCompo.FocusedRowHandle);
            }
            else if (PanelPrincipal.SelectedPageIndex == 3)
            {
                this.gvPlan.PostEditor();
                gvPlan.DeleteRow(gvPlan.FocusedRowHandle);
                
            }
        }

        private void btnUpdate_ItemClick(object sender, TileItemEventArgs e)
        {
            bool bOk = true;

            bOk = validarGrid();

            if (bOk)
            {
                bOk = DataConnect.ConnectToSql();
                
                //DataConnect.Conn.BeginTransaction();
            }

            if (bOk)
            {

                bOk = clsTransacc.UpdateDB(dtTypeDevices, dtComponents, dtMaximunCost, dtPlan);
            }

            if (!bOk)
            {
               
            }
            else
            {

            }

        }

        private bool validarGrid()
        {
            bool bOk = true;
            string sError = string.Empty;

            DataRow[] rowsInError;

            //foreach (DataTable table in dgvTypeDevices.DataSource)
            //{
            // Test if the table has errors. If not, skip it.
            if (dtTypeDevices.HasErrors)
            {
                // Get an array of all rows with errors.
                rowsInError = dtTypeDevices.GetErrors();
                // Print the error of each column in each row.
                for (int i = 0; i < rowsInError.Length; i++)
                {
                    foreach (DataColumn column in dtTypeDevices.Columns)
                    {
                        sError = column.ColumnName + " " + rowsInError[i].GetColumnError(column);
                        bOk = false;
                    }
                    // Clear the row errors
                    rowsInError[i].ClearErrors();
                }
            }

            return bOk;
           // }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void tbLoad_ItemClick(object sender, TileItemEventArgs e)
        {
           
            if (PanelPrincipal.SelectedPageIndex == 0)
            {
                Clases.sTablasLoad = Clases.EnumTablas.TYPE_DEVICES;
            }
            else if (PanelPrincipal.SelectedPageIndex == 1)
            {
               
            }

            else if (PanelPrincipal.SelectedPageIndex == 2)
            {
                Clases.sTablasLoad = Clases.EnumTablas.COMPONENTS;
            }
            else if (PanelPrincipal.SelectedPageIndex == 3)
            {
                
            }

            Form load = new CargaExcelWizard();
            
            load.ShowDialog();
            

            dgvTypeDevices.DataSource = dtTypeDevices;
            dgvMaintenceCompo.DataSource = dtComponents;
            
        }

     
    }
}