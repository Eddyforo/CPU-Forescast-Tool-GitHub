using DevExpress.XtraEditors;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Card;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using static CPU.Forecast.Tool.Clases;
using DevExpress.XtraSplashScreen;
using System.IO;
using DevExpress.XtraEditors.Repository;

namespace CPU.Forecast.Tool
{
    public partial class MainForm : XtraForm
    {

        public static DataTable dtTypeDevices;
        public static DataTable dtMaximunCost;
        public static DataTable dtComponents;
        public static DataTable dtPlan;

        public static DataTable dtEventViewer;
        public static DataTable dtMissingPlan;

        //public static DataTable dtMaestro;
        //public static DataTable dtDetalle;

        DataSet dtSet;
        DataTable dtEstimate;
        DataTable dtEstimadoDetalle;
        Clases.Forecast clsTransacc;
        bool Cerrar = false;

        PersistentRepository PersistentRepository1;
        RepositoryItemLookUpEdit RepModel;

        //Lista versiones por plan
        List<VersionPlan> listVersionPlan;

        //se usa para el combo de modelos
        List<ModelLook> listaModel;

        public MainForm()
        {
            InitializeComponent();

            DataConnect.InicializarVaribales();

            if (!DataConnect.ConnectToSql())
            {
                toolStripButton1_Click_1(this, new EventArgs());

            }
 
        }
        

        #region Eventos

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
           
        }

        private void tileBar1_Click(object sender, System.EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {
            DataConnect.DisConnectSql();
            this.Close();
        }

        private void toolStripButton1_Click_1(object sender, System.EventArgs e)
        {
            // Login Conectar = new Login();

            ConectBD Conectar = new ConectBD();

            Conectar.ShowDialog(this);

            //si no le dieron cancelar se inicializa los datos
            if (!Conectar.seCerro)
            {
               InitializeComponentExtra();
            }
        }

        private void PanelPrincipal_Click(object sender, EventArgs e)
        {

        }


        public void InitializeComponentExtra()
        {
            //se selecciona el primer tab
            PanelPrincipal.SelectedPageIndex = 0;
            
            // clase para hacer las transacciones
            clsTransacc = new Clases.Forecast();

            tpMaintenceCompo.PageVisible = false;
            tpTypeDevice.PageVisible = false;
            tpPlans.PageVisible = false;
            tpMaximunCost.PageVisible = false;
            tpMenorCosto.PageVisible = false;

            //se cargan los datagrid's
            CargarGrid();

            clsTransacc.LoadDatosTabla(dtTypeDevices, dtComponents, dtMaximunCost, dtPlan);

            // se inicializa el lookupedit de modelos
            listaModel = new List<ModelLook>();
            RepModel = new RepositoryItemLookUpEdit();

            //foreach (DataRow item in dtTypeDevices.Rows)
            //{
            //    if (item.RowState == DataRowState.Deleted)
            //    {

            //    }
            //}

            listaModel = (from DataRow row in dtTypeDevices.Rows

                                          select new ModelLook
                                          {
                                              Type = row[Clases.constantes.TYPE_DEVICE].ToString(),
                                              Model = row[Clases.constantes.MODEL].ToString(),
                                              Description = row[Clases.constantes.DESCRIPTION].ToString()
                                          }
                                 ).ToList();

            listaModel = listaModel.GroupBy(x => x.Model).Select(h => h.First()).ToList();

            RepModel.DataSource = listaModel;
            RepModel.ValueMember = "Model";
            RepModel.DisplayMember = "Model";

            RepModel.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            RepModel.DropDownRows = listaModel.Count();

            RepModel.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            // Specify the column against which an incremental search is performed in SearchMode.AutoComplete and SearchMode.OnlyInPopup modes
            RepModel.AutoSearchColumnIndex = 1;

            PersistentRepository1 = new PersistentRepository();

            PersistentRepository1.Items.Add(RepModel);

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

        private void tbEstimate_ItemClick(object sender, TileItemEventArgs e)
        {
            PanelPrincipal.SelectedPageIndex = 4;
        }

        private void btnNew_ItemClick(object sender, TileItemEventArgs e)
        {

            if (PanelPrincipal.SelectedPageIndex == 0)
            {
                this.gvTypeDevices.PostEditor();
                gvTypeDevices.AddNewRow();
            }
            else if (PanelPrincipal.SelectedPageIndex == 1)
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
           // AcceptChanges();
        }

        private void btnUpdate_ItemClick(object sender, TileItemEventArgs e)
        {
            bool bOk = true;

            SplashScreenManager.ShowForm(typeof(WaitForm1));

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

            SplashScreenManager.CloseForm();

            if (bOk)
            {
                Error.addMensaje("Successfully updated ", "The database is updated with all rows", false); 
            }

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

        private void PanelPrincipal_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (e.Page.Caption == "Lower cost per unit")
            {
                tbCalculate.Enabled = true;
            }
            else
            {
                tbCalculate.Enabled = false;
            }
        }

        private void tbCalculate_ItemClick(object sender, TileItemEventArgs e)
        {
            bool bOk = true;

            SplashScreenManager.ShowForm(typeof(WaitForm1));

            bOk = validarGridLlenos();

            if (bOk)
            {
                AcceptChanges();
                bOk = calcularCosto();
            }

            SplashScreenManager.CloseForm();
        }
        
        private void tileBarItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            exportar();
        }

        private void gvMaximun_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "MODEL")
            {

                MostrarComboModel(e);
            }
        }

        private void gvPlan_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "MODEL")
            {

                MostrarComboModel(e);
            }
        }

        #endregion Eventos

        #region Funciones


        private void AcceptChanges()
        {
            dtTypeDevices.AcceptChanges();
            dtMaximunCost.AcceptChanges();
            dtComponents.AcceptChanges();
            dtPlan.AcceptChanges();
        }

        private void MostrarComboModel(CustomRowCellEditEventArgs e)
        {
            List<ModelLook> listaModel = (from DataRow row in dtTypeDevices.Rows

                                          select new ModelLook
                                          {
                                              Type = row[Clases.constantes.TYPE_DEVICE].ToString(),
                                              Model = row[Clases.constantes.MODEL].ToString(),
                                              Description = row[Clases.constantes.DESCRIPTION].ToString()
                                          }
                                 ).ToList();

            listaModel = listaModel.GroupBy(x => x.Model).Select(h => h.First()).ToList();

            RepModel.DataSource = listaModel;
            RepModel.ValueMember = "Model";
            RepModel.DisplayMember = "Model";
            RepModel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            RepModel.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            RepModel.DropDownRows = listaModel.Count();

            RepModel.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            // Specify the column against which an incremental search is performed in SearchMode.AutoComplete and SearchMode.OnlyInPopup modes
            RepModel.AutoSearchColumnIndex = 1;


            e.RepositoryItem = RepModel;
        }

        private void CargarGrid()
        {
            dgvTypeDevices.DataSource = CreatablaTipos();

            dgvMaximun.DataSource = CreaMaximunCost();

            dgvMaintenceCompo.DataSource = CreaMaintenceCompo();

            dgvPlan.DataSource = CreaPlans();

            dgvMissing.DataSource = CrearTablaMissing();

            dgvEventViewe.DataSource = CrearTablaEventViewer();

        }

        private DataTable CrearTablaEventViewer()
        {
            dtEventViewer = new DataTable("EVENT_VIEWER");

            dtEventViewer.Columns.Add(new DataColumn("DATE", typeof(DateTime)));
            dtEventViewer.Columns.Add(new DataColumn("EVENT", typeof(string)));
            dtEventViewer.Columns.Add(new DataColumn("DESCRIPTION", typeof(string)));

            return dtEventViewer;
        }


        private DataTable CrearTablaMissing()
        {
            dtMissingPlan = new DataTable("MISSING_PLAN");

            dtMissingPlan.Columns.Add(new DataColumn("DATE", typeof(DateTime)));
            dtMissingPlan.Columns.Add(new DataColumn("MODEL", typeof(string)));
            dtMissingPlan.Columns.Add(new DataColumn("PLAN", typeof(int)));
            dtMissingPlan.Columns.Add(new DataColumn("MISSING", typeof(int)));

            return dtMissingPlan;
        }

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
            dtPlan.Columns.Add(new DataColumn("OEM", typeof(Decimal)));

            return dtPlan;
        }

        private DataTable CreaMaximunCost()
        {
            dtMaximunCost = new DataTable("MAXIMUN_COST");

            dtMaximunCost.Columns.Add(new DataColumn("MODEL", typeof(string)));
            dtMaximunCost.Columns.Add(new DataColumn("MAX_COST", typeof(decimal)));

            return dtMaximunCost;
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

        
        private bool validarGridLlenos()
        {
            bool bOk = true;

            if (dtTypeDevices.Rows.Count < 1) 
            {
                bOk = false;
                Error.addMensaje("Types of devices is empty","In order to calculate, we needs almost one row in the table: types of devices", true);
            }

            if (bOk && dtComponents.Rows.Count < 1)
            {
                bOk = false;

                Error.addMensaje("Components is empty", "In order to calculate, we needs almost one row in the table of components", true);
            }

            if (bOk && dtPlan.Rows.Count < 1)
            {
                bOk = false;

                Error.addMensaje("Plan is empty", "In order to calculate, we needs almost one row in the table of plans", true);
            }

            return bOk;
        }

        private bool calcularCosto()
        {
            bool bOk = true;
            //si ya encontramos el menor costo para ese modelo.
            bool bHayUnCostMenor= false;
            //si no hay suficientes componenetes para ese model del todo
            bool bNoHaySufComponentes = false;
            // si se estimo una parte de un componente para la estimacion ya que no alcanzaba y hay que buscar otra
            bool bEstimaCompParcial = false;
            //saber si hay que buscar una version nueva
            bool bVersionNueva = false;

            ///
            decimal nPercent = 0;// Convert.ToDecimal( txtPercent.EditValue);
            decimal nCantTotalPart = 0;
            decimal nCantPartPorPorcen = 0; // la cantidad de partes necesarias para cumplir con el porcentaje definido
            int nVersion = 0;

            //lista de datos en la tablas
            List<Clases.TypeDevices> listaTypeDevice = new List<Clases.TypeDevices>();

            ///Lista de componentesss
            List<Clases.MaintenceComponents> listaComponents = new List<Clases.MaintenceComponents>();
            List<Clases.Plan> listaPlan = new List<Clases.Plan>();

            //lista de tipos por modelo , se usa para saber como estan conformados los modelos
            List<Clases.TypeDevices> lTypePerModel = new List<Clases.TypeDevices>();


            // se usa para seleccionar todas las partes del mismo tipo para luego sugerirlos
            List<Clases.MaintenceComponents> lCompoPerParts = new List<Clases.MaintenceComponents>();


            //Lista versiones por plan
            listVersionPlan = new List<Clases.VersionPlan>() ;
            //versiones por plan para agregar uno a uno a la lista
            Clases.VersionPlan unaVersionPlan;

            // paso de datatables a listas
            listaTypeDevice = (from DataRow row in dtTypeDevices.Rows
                                    select new Clases.TypeDevices
                                    {
                                        Type_devices = row[Clases.constantes.TYPE_DEVICE].ToString(),
                                        Description = row[Clases.constantes.DESCRIPTION].ToString(),
                                        Model = row[Clases.constantes.MODEL].ToString(),
                                        Part = row[Clases.constantes.PART].ToString(),
                                        Quantity = (int)row[Clases.constantes.QUANTITY]
                                    }).ToList();

            listaComponents = (from DataRow row in dtComponents.Rows
                               select new Clases.MaintenceComponents
                               {
                                   SPartCode = row[Clases.constantes.PART_CODE].ToString(),
                                   SDescription = row[Clases.constantes.DESCRIPTION].ToString(),
                                   NCost = (decimal)row[Clases.constantes.COST],
                                   NStock = (int)row[Clases.constantes.STOCK]
                               }).ToList();

            listaPlan = (from DataRow row in dtPlan.Rows
                               select new Clases.Plan
                               {
                                   SModel = row[Clases.constantes.MODEL].ToString(),
                                   NPlan = (int)row[Clases.constantes.PLANS],
                                   NOEM = (decimal)row[Clases.constantes.OEM]
                               }).ToList();


            //Comienza el proceso
            foreach (Clases.Plan lPlanActual in listaPlan)
            {
                //Agregar una version nueva al plan
                unaVersionPlan = new Clases.VersionPlan(nVersion + 1);
                nVersion = 0;

                bHayUnCostMenor = false;
                // for por cada cantidad que se ocupa por plan
                for (int cont = 0; cont < lPlanActual.NPlan; cont++)
                {
                    if (bHayUnCostMenor)
                    {
                        int nCantidaFalta = lPlanActual.NPlan - cont;

                        ///Para saber si tener que restar uno a la cantidad faltante por que el que esta no alcanza
                        bool bBuscaMenorF = false;
                        bool bHayStockSuf = false;

                        for (int i = nCantidaFalta; i > 0; i--)
                        {
                            foreach (Clases.DetalleVersionPlan item in unaVersionPlan.ListaDetalle)
                            {
                                // es la cantidad que hace falta estimar
                                int cantidadNecesario = i * item.NumberPart;

                                //si la cantidad necesaria es menor o igual entonces la podemos usar 
                                if (cantidadNecesario <= listaComponents.Find(j => j.SPartCode == item.Part).NStock)
                                {
                                    bHayStockSuf = true;
                                    bBuscaMenorF = false;

                                }
                                else
                                {
                                    bBuscaMenorF = true;
                                    bHayStockSuf = false;

                                    break;
                                }

                            }

                            //hay stock suficiente y ya no hay que ir a restarle uno al faltante para ver si cabe
                            if (bHayStockSuf && !bBuscaMenorF)
                            {
                                unaVersionPlan.PlanAmount += i;

                                cont += i;

                                foreach (Clases.DetalleVersionPlan item in unaVersionPlan.ListaDetalle)
                                {

                                    //Resta el la cantidad de componentes que se ocupan para esta cantidad 
                                    listaComponents.Where(w => w.SPartCode == item.Part).ToList()
                                            .ForEach(k => k.NStock -= (item.NumberPart * i));
                                }



                                break;
                            }
                        }

                    }

                    //si ya hay un costo menor y si en cont del for de planes es igual a la cantidad de planes quiere decir que ya esta todo
                    //ese plan estimado , hay que pasar al otro
                    if (bHayUnCostMenor && cont == lPlanActual.NPlan)
                    {
                        dtEventViewer.Rows.Add(DateTime.Now, "Estimate completed", "We estimated the plan for the model " + lPlanActual.SModel);
                        break;
                    }
                    else
                    {
                        nVersion += 1;
                        unaVersionPlan = new Clases.VersionPlan(nVersion);
                    }
                    //filtro por modelo
                    lTypePerModel = (from Clases.TypeDevices x in listaTypeDevice
                                     where x.Model == lPlanActual.SModel
                                     orderby x.Model, x.Part
                                     select new Clases.TypeDevices
                                     {
                                         Type_devices = x.Type_devices.ToString(),
                                         Description = x.Description.ToString(),
                                         Model = x.Model.ToString(),
                                         Part = x.Part.ToString(),
                                         Quantity = x.Quantity
                                     }).ToList();

                    //Verificamos que el modelo exista en types de dispositivos
                    if (lTypePerModel.Count > 0)
                    {
                        //la cantidad de componentes totales para este modelo
                        nCantTotalPart = (from Clases.TypeDevices i in lTypePerModel
                                          select i.Quantity).Sum();

                        //Es la cantidad que tenemos  que cumplir de componentes OEM por modelo
                        nCantPartPorPorcen = nCantTotalPart * lPlanActual.NOEM;

                        nCantPartPorPorcen = nCantPartPorPorcen == 0 ? 1 : nCantPartPorPorcen;

                        //recorrer la lista de partes que ocupa ese modelo
                        foreach (Clases.TypeDevices lPartPerModel in lTypePerModel)
                        {
                            //filtramos los componentes que sean de la misma parte y que el stock sea mayor que cero

                            lCompoPerParts = (from MaintenceComponents z in listaComponents
                                              where z.SPartCode.StartsWith(lPartPerModel.Part) &&
                                              z.NStock > 0
                                              orderby z.NCost ascending
                                              select new Clases.MaintenceComponents
                                              {
                                                  SPartCode = z.SPartCode,
                                                  SDescription = z.SDescription,
                                                  NCost = z.NCost,
                                                  NStock = z.NStock
                                              }).ToList();


                            if (lCompoPerParts.Count > 0)
                            {
                                int nContEstimada = lPartPerModel.Quantity;
                                List<Clases.DetalleVersionPlanParcial> tempParcial = new List<Clases.DetalleVersionPlanParcial>();
                                //Se recorren todas las partes pensando en el caso que la primera no cumpla con el 
                                for (int i = 0; i < lCompoPerParts.Count; i++)
                                {
                                    if ((lCompoPerParts[i].NStock - nContEstimada) >= 0 && !bEstimaCompParcial)
                                    {

                                        //Se agrega el componente al detalle de la version
                                        unaVersionPlan.Model = lPartPerModel.Model;
                                        unaVersionPlan.Type = lPartPerModel.Type_devices;
                                        unaVersionPlan.nTotalPlan = lPlanActual.NPlan;
                                        unaVersionPlan.addDetalle(lCompoPerParts[i].SPartCode, nContEstimada, lCompoPerParts[i].NCost);
                                         
                                        //Resta el componente que acaba de agregar a la lista de componentes padre
                                        listaComponents.Where(w => w.SPartCode == lCompoPerParts[i].SPartCode).ToList()
                                                    .ForEach(k => k.NStock -= nContEstimada);

                                        bNoHaySufComponentes = false;
                                        //En el caso que ya cantidad estimada sea igual que la necesaria, ya no hay que calcular nada y se sale.
                                        break;
                                    }
                                    else
                                    {
                                        //se igual al stock ya que se va a agregar esa parte del stock para luego buscar las restantes
                                        // nContEstimada = lCompoPerParts[i].NStock;

                                        //se crea una lista temporal para el detalle ya que no se sabe si se van a cumplir con el total de componentes
                                        DetalleVersionPlanParcial pTemp = new DetalleVersionPlanParcial();

                                        pTemp.sParte = lCompoPerParts[i].SPartCode;
                                        pTemp.iCant = nContEstimada > lCompoPerParts[i].NStock ? lCompoPerParts[i].NStock : nContEstimada ;
                                        pTemp.dCosto = lCompoPerParts[i].NCost;

                                        nContEstimada = nContEstimada - pTemp.iCant;

                                        tempParcial.Add( pTemp);
                                        
                                        //se comienza a estimar componentes parciales por que no hay en el stock
                                        bEstimaCompParcial = true;

                                    }
                                }

                                if (bEstimaCompParcial && nContEstimada == 0 )
                                {
                                    foreach (Clases.DetalleVersionPlanParcial detalle in tempParcial)
                                    {
                                        //Se agrega el componente al detalle de la version
                                        unaVersionPlan.Model = lPartPerModel.Model;
                                        unaVersionPlan.Type = lPartPerModel.Type_devices;
                                        unaVersionPlan.addDetalle(detalle.sParte, detalle.iCant, detalle.dCosto);

                                        //Resta el componente que acaba de agregar a la lista de componentes padre
                                        listaComponents.Where(w => w.SPartCode == detalle.sParte).ToList()
                                                    .ForEach(k => k.NStock -= detalle.iCant);
                                    }

                                    bNoHaySufComponentes = false;

                                    bEstimaCompParcial = false;
                                }
                                else if (bEstimaCompParcial)
                                {
                                    bNoHaySufComponentes = true;
                                    break;
                                }

                            }
                            else
                            {
                                bNoHaySufComponentes = true;

                                dtEventViewer.Rows.Add(DateTime.Now, "Component doesn't exist", "The component " + lPartPerModel.Part + " doesn't exist Component doesn't exist or has too few to estimate the plan.");
                                //hacer si no existe el componente en el stock
                               // break;  
                            }
                        }// fin del foreach de las parts que ocupa por modelo

                        
                    }
                    else
                    {
                        bOk = false;

                        dtEventViewer.Rows.Add(DateTime.Now, "The model doesn't exist", "The model " + lPlanActual.SModel + " doesn't exist in the table types of devices.");
                       // Error.addMensaje("The model doesn't exist ", "The model " + lPlanActual.SModel + " doesn't exist in the table types of devices.", true);
                    }

                    
                    if (unaVersionPlan.ListaDetalle.Count > 0 && !bNoHaySufComponentes)
                    {
                        
                        //lista de todos los componentes que no son R en la version actual
                        List<DetalleVersionPlan> lversionDetalleNoR = (from DetalleVersionPlan h in unaVersionPlan.ListaDetalle
                                                                    where !h.Part.EndsWith("R")
                                                                    select new DetalleVersionPlan(h.Version)
                                                                    {
                                                                        Part = h.Part,
                                                                        Model = h.Model,
                                                                        Version = h.Version,
                                                                        NumberPart = h.NumberPart,
                                                                        Cost = h.Cost

                                                                    }).ToList();

                        //contamos la cantidad de componentes que no son R
                        decimal nCantTotalPartOEM = (from DetalleVersionPlan i in lversionDetalleNoR
                                          select i.NumberPart).Sum();

                        //verificamos si cumplimos con el porcentaje requerido
                        if (nCantTotalPartOEM < nCantPartPorPorcen)
                        {
                            decimal nCantidadfaltaOEM = nCantTotalPartOEM - nCantPartPorPorcen;
                            //DetalleVersionPlan itemTemp ;

                            //se sacan todos los compoenentes que son R para luego quitarlos y cumplir con el porcentaje
                            List<DetalleVersionPlan> lVersionDetalleR = unaVersionPlan.ListaDetalle.Except(lversionDetalleNoR).ToList();

                            //recorremos cada uno de ellos
                            foreach (DetalleVersionPlan item in lVersionDetalleR)
                            {
                                if (nCantidadfaltaOEM < 0)
                                {
                                    //comenzamos con el primero
                                    // itemTemp = unaVersionPlan.ListaDetalle.Find(x => x.Part == item.Part);

                                    //Quitamos el componente de la version antes de buscar el nuevo componente
                                    unaVersionPlan.ListaDetalle.Remove(item); 

                                    TypeDevices sMainTypeDevice = lTypePerModel.Find( x => x.Part.StartsWith(item.Part.Substring(0, item.Part.Length - 2)) 
                                                        && x.Model == item.Model);

                                    
                                    #region Codigo de estimación usado arriba con variantes

                                    //filtramos los componentes que sean de la misma parte y que el stock sea mayor que cero y en este caso que no sean R

                                    lCompoPerParts = (from MaintenceComponents z in listaComponents
                                                      where z.SPartCode.StartsWith(sMainTypeDevice.Part) &&
                                                      !z.SPartCode.EndsWith("R") &&
                                                      z.NStock > 0
                                                      orderby z.NCost ascending
                                                      select new Clases.MaintenceComponents
                                                      {
                                                          SPartCode = z.SPartCode,
                                                          SDescription = z.SDescription,
                                                          NCost = z.NCost,
                                                          NStock = z.NStock
                                                      }).ToList();


                                    if (lCompoPerParts.Count > 0)
                                    {
                                        int nContEstimada = sMainTypeDevice.Quantity;
                                        List<Clases.DetalleVersionPlanParcial> tempParcial = new List<Clases.DetalleVersionPlanParcial>();
                                        //Se recorren todas las partes pensando en el caso que la primera no cumpla con el 
                                        for (int i = 0; i < lCompoPerParts.Count; i++)
                                        {
                                            if ((lCompoPerParts[i].NStock - nContEstimada) >= 0 && !bEstimaCompParcial)
                                            {

                                                //Se agrega el componente al detalle de la version
                                                unaVersionPlan.Model = sMainTypeDevice.Model;
                                                unaVersionPlan.Type =  sMainTypeDevice.Type_devices;
                                                unaVersionPlan.nTotalPlan = lPlanActual.NPlan;
                                                unaVersionPlan.addDetalle(lCompoPerParts[i].SPartCode, nContEstimada, lCompoPerParts[i].NCost);

                                                //primero sumamos el componente que estamos sustituyendo
                                                listaComponents.Where(w => w.SPartCode == item.Part).ToList()
                                                            .ForEach(k => k.NStock += nContEstimada);

                                                //Resta el componente que acaba de agregar a la lista de componentes padre
                                                listaComponents.Where(w => w.SPartCode == lCompoPerParts[i].SPartCode).ToList()
                                                            .ForEach(k => k.NStock -= nContEstimada);

                                                //se suma la cantidad que falta para llegar al porcentaje de OEM
                                                nCantidadfaltaOEM += nContEstimada;
                                                nCantTotalPartOEM += nContEstimada;

                                                bNoHaySufComponentes = false;
                                                //En el caso que ya cantidad estimada sea igual que la necesaria, ya no hay que calcular nada y se sale.
                                                break;
                                            }
                                            else
                                            {
                                                //se igual al stock ya que se va a agregar esa parte del stock para luego buscar las restantes
                                                // nContEstimada = lCompoPerParts[i].NStock;

                                                //se crea una lista temporal para el detalle ya que no se sabe si se van a cumplir con el total de componentes
                                                DetalleVersionPlanParcial pTemp = new DetalleVersionPlanParcial();

                                                pTemp.sParte = lCompoPerParts[i].SPartCode;
                                                pTemp.iCant = nContEstimada > lCompoPerParts[i].NStock ? lCompoPerParts[i].NStock : nContEstimada;
                                                pTemp.dCosto = lCompoPerParts[i].NCost;

                                                nContEstimada = nContEstimada - pTemp.iCant;

                                                tempParcial.Add(pTemp);

                                                //se comienza a estimar componentes parciales por que no hay en el stock
                                                bEstimaCompParcial = true;
                                                

                                            }
                                        }

                                        if (bEstimaCompParcial && nContEstimada == 0)
                                        {
                                            foreach (Clases.DetalleVersionPlanParcial detalle in tempParcial)
                                            {
                                                //Se agrega el componente al detalle de la version
                                                unaVersionPlan.Model = sMainTypeDevice.Model;
                                                unaVersionPlan.Type = sMainTypeDevice.Type_devices;
                                                unaVersionPlan.addDetalle(detalle.sParte, detalle.iCant, detalle.dCosto);
                                                unaVersionPlan.nTotalPlan = lPlanActual.NPlan;

                                                //primero sumamos el componente que estamos sustituyendo
                                                listaComponents.Where(w => w.SPartCode == item.Part).ToList()
                                                            .ForEach(k => k.NStock += nContEstimada);

                                                //Resta el componente que acaba de agregar a la lista de componentes padre
                                                listaComponents.Where(w => w.SPartCode == detalle.sParte).ToList()
                                                            .ForEach(k => k.NStock -= detalle.iCant);


                                                //se Suma la cantidad que falta para llegar al porcentaje de OEM
                                                nCantidadfaltaOEM += nContEstimada;
                                            }
                                            bNoHaySufComponentes = false;

                                            bEstimaCompParcial = false;
                                        }
                                        else if (bEstimaCompParcial)
                                        {
                                            bNoHaySufComponentes = true;
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        bNoHaySufComponentes = true;
                                        dtEventViewer.Rows.Add(DateTime.Now, "OEM Percent Part", "There isn't enough components" + sMainTypeDevice.Part + " in order to reach la OEM percent");

                                        unaVersionPlan.ListaDetalle.Add(item);
                                        //hacer si no existe el componente en el stock
                                       // break;
                                    }

                                    #endregion

                                }//Fin if nCantidadfaltaOEM
                                else
                                {
                                    //salimos del recorrido por que el porcentaje de OEM esta bien 
                                    break;
                                }

                            }// fin del recorrido lVersionDetalleR
                            if (nCantidadfaltaOEM < 0)
                            {
                                dtEventViewer.Rows.Add(DateTime.Now, "OEM Percent", "There isn't enough components in order to reach la OEM percent");

                            }

                        } //if para saber si cumplimos con el OEM


                        decimal PorcentaOEM = Math.Round( (nCantTotalPartOEM * 100 / nCantTotalPart ),2);

                        dtEventViewer.Rows.Add(DateTime.Now, "Percentage OEM", "Percentage OEM for model " + unaVersionPlan.Model + " and version " + unaVersionPlan.Version + " is: " + PorcentaOEM.ToString() +" %." );
                        

                        unaVersionPlan.PlanAmount += 1;
                        
                        //bandera para saber si ya hay uno estimado
                        bHayUnCostMenor = true;

                        //agrego la version a la lista principal
                        listVersionPlan.Add(unaVersionPlan);

                        try
                        {
                            //validar el costo maximo
                            decimal costMaximo = Convert.ToDecimal((from DataRow row in dtMaximunCost.Rows
                                                                    where row[constantes.MODEL].ToString() == unaVersionPlan.Model
                                                                    select row[constantes.MAX_COST]).First());

                            if (costMaximo < unaVersionPlan.COG)
                            {
                                dtEventViewer.Rows.Add(DateTime.Now, "Maximum cost exceeded", "Maximum cost exceeded for model " + unaVersionPlan.Model + " and version " + unaVersionPlan.Version + " Was exceeded by : $" + (Math.Round(unaVersionPlan.COG - costMaximo, 2)).ToString() + " .");

                            }
                        }
                        catch (Exception)
                        {

                            dtEventViewer.Rows.Add(DateTime.Now, "Maximum cost", "Maximum cost for model " + unaVersionPlan.Model + " and version " + unaVersionPlan.Version + " It has not been found.");
                        }

                    }

                    if (bNoHaySufComponentes)
                    {
                        int nFaltane = lPlanActual.NPlan - cont;
                        cont = lPlanActual.NPlan;
                        //Hay que hacer el faltante y guardarlo en una lista o datatable
                        dtMissingPlan.Rows.Add(DateTime.Now, lPlanActual.SModel, lPlanActual.NPlan, nFaltane );

                    }

                }// fin del for cada plan por modelo uno a uno-- cont es la variable.
            }// fin del foreach de planes

            if (listVersionPlan.Count > 0)
            {
                dtEstimate = convertir.ToDataTable(listVersionPlan);

                //dgvEstimado.DataSource = dtEstimate;

                dtEstimadoDetalle = new DataTable();

                foreach (VersionPlan item in listVersionPlan)
                {
                    dtEstimadoDetalle.Merge(convertir.ToDataTable(item.ListaDetalle));
                }

                try
                {
                    List<COGModel> listCOGModel = new List<COGModel>();

                    listCOGModel = listVersionPlan.GroupBy(l => l.Model).Select(cl => new COGModel {
                        Model = cl.First().Model,
                        COG = cl.Sum( z => z.TotalCOG / z.PlanAmount),
                        Cantidad = cl.First().nTotalPlan
                    }).ToList();

                    //llemos el grid de cog por modelos
                    DataTable dtCOG = convertir.ToDataTable(listCOGModel);
                    dgvCOG.DataSource = dtCOG;

                    dgvEstimadoDetail.DataSource = dtEstimadoDetalle;

                    dtEstimate.TableName = "ESTIMATED";
                    dtEstimadoDetalle.TableName = "ESTIMATEDDETAILS";
                    dtSet = new DataSet("ESTIMATED");
                    dtSet.Tables.Add(dtEstimate);
                    dtSet.Tables.Add(dtEstimadoDetalle);

                    DataRelation dtRelation;
                    DataColumn custCol = dtSet.Tables["ESTIMATED"].Columns["Version"];
                    DataColumn orderCol = dtSet.Tables["ESTIMATEDDETAILS"].Columns["Version"];
                    dtRelation = new DataRelation("Version", custCol, orderCol);
                    dtSet.Tables["ESTIMATEDDETAILS"].ParentRelations.Add(dtRelation);

                    dgvEstimado.DataSource = dtSet.Tables["ESTIMATED"];


                }
                catch (Exception ex)
                {
                    dgvEstimado.DataSource = dtEstimate;
                }
                tbExport.Enabled = true;
                
            }
            else
            {
                dtEventViewer.Rows.Add( DateTime.Now, "No plan could be estimated", "There are not enough components to estimate the plan");
            }
            
            return bOk;
            
        }

        private void exportar()
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    switch (fileExtenstion)
                    {
                        case ".xls":
                            dgvEstimadoDetail.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            dgvEstimadoDetail.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            dgvEstimadoDetail.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            dgvEstimadoDetail.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            dgvEstimadoDetail.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            dgvEstimadoDetail.ExportToMht(exportFilePath);
                            break;
                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }





        #endregion Funciones

       
    }
}