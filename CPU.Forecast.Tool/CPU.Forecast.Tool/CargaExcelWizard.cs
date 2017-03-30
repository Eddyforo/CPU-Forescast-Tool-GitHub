using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraWizard;
using DevExpress.XtraSplashScreen;

namespace CPU.Forecast.Tool
{

    public partial class CargaExcelWizard : DevExpress.XtraEditors.XtraForm
    {

        List<string> NombresHojas;
        List<string> ListaNombreTypeDevices;
        List<string> ListaNombreComponents;

        List<string> ListaNombrePlans;
        List<string> ListaNombreMaximun;


        public DataTable dtCargarExcel = null;

        List<Clases.AsociacionImportacion> ListaAsociacion;

       
        public CargaExcelWizard()
        {
            InitializeComponent();
        }
        

        #region Abrir Excel

        private void LlenarNombreshojas(string psRuta)
        {

            Excel.Application App = new Excel.Application();

            try
            {

                Excel.Workbook wBook = App.Workbooks.Open(psRuta);

                object misValue = System.Reflection.Missing.Value;

                NombresHojas = new List<string>();


                foreach (Excel.Worksheet hoja in wBook.Sheets)
                {
                    NombresHojas.Add(hoja.Name);

                }

                wBook.Close(false, misValue, misValue);
                App.Workbooks.Close();
                App.Quit();

            }
            catch (Exception ex)
            {

                Error.addMensaje("Error opening file", ex.Message, true);
            }

        }

        private void llenarColumnasBDArchivo()
        {
            if (Clases.sTablasLoad == Clases.EnumTablas.TYPE_DEVICES)
            {
                //llenar
                imglstbxColCargador.Items.Clear();
                imglstbxColCargador.Items.Add(Clases.constantes.TYPE_DEVICE);
                imglstbxColCargador.Items.Add(Clases.constantes.DESCRIPTION);
                imglstbxColCargador.Items.Add(Clases.constantes.PART);
                imglstbxColCargador.Items.Add(Clases.constantes.QUANTITY);
                imglstbxColCargador.Items.Add(Clases.constantes.MODEL);

                imglstbxColArchivo.Items.Clear();
                for (int i = 0; i < ListaNombreTypeDevices.Count; i++)
                {
                    imglstbxColArchivo.Items.Add(ListaNombreTypeDevices[i]);
                }
            }
            else if (Clases.sTablasLoad == Clases.EnumTablas.COMPONENTS)
            {
                //llenar
                imglstbxColCargador.Items.Clear();
                imglstbxColCargador.Items.Add(Clases.constantes.PART_CODE);
                imglstbxColCargador.Items.Add(Clases.constantes.DESCRIPTION);
                imglstbxColCargador.Items.Add(Clases.constantes.COST);
                imglstbxColCargador.Items.Add(Clases.constantes.STOCK);

                imglstbxColArchivo.Items.Clear();
                for (int i = 0; i < ListaNombreComponents.Count; i++)
                {
                    imglstbxColArchivo.Items.Add(ListaNombreComponents[i]);
                }
            }


            ListaAsociacion = new List<Clases.AsociacionImportacion>();

        }

        private void Abrirarchivo()
        {
            try
            {
                OpenFileDialog openfile1 = new OpenFileDialog();
                openfile1.Filter = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx"; //le indicamos el tipo de filtro en este caso que busque

                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SplashScreenManager.ShowForm(typeof(WaitForm1));
                    this.txtRuta.Text = openfile1.FileName;

                    LlenarNombreshojas(txtRuta.Text);

                    //llenar el grid 
                    LLenarGrid(openfile1.FileName, NombresHojas[0].ToString());
                    
                    llenarColumnasBDArchivo();

                    SplashScreenManager.CloseForm();
                   

                }

            }
            catch (Exception ex)
            {

                //en caso de haber una excepcion que nos mande un mensaje de error
                Error.addMensaje("Error opening file", ex.Message, true);

            }
        }

        private void LLenarGrid(string archivo, string hoja)
        {
            //declaramos las variables         
            OleDbConnection conexion = null;
            //DataSet dataSet = null;
            OleDbDataAdapter dataAdapter = null;
            string consultaHojaExcel = "Select * from [" + hoja + "$]";

            //esta cadena es para archivos excel 2007 y 2010
            string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + archivo + "';Extended Properties=Excel 12.0;";

            //para archivos de 97-2003 usar la siguiente cadena
            //string cadenaConexionArchivoExcel = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + archivo + "';Extended Properties=Excel 8.0;";

            //Validamos que el usuario ingrese el nombre de la hoja del archivo de excel a leer
            if (string.IsNullOrEmpty(hoja))
            {
                MessageBox.Show("No hay una hoja para leer");
            }
            else
            {
                try
                {
                    //Si el usuario escribio el nombre de la hoja se procedera con la busqueda
                    conexion = new OleDbConnection(cadenaConexionArchivoExcel);//creamos la conexion con la hoja de excel
                    conexion.Open(); //abrimos la conexion
                    dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion); //traemos los datos de la hoja y las guardamos en un dataSdapter
                    //dataSet = new DataSet(); // creamos la instancia del objeto DataSet
                    dtCargarExcel = new DataTable();
                    //dataAdapter.Fill(dataSet, hoja);//llenamos el dataset
                    dataAdapter.Fill(dtCargarExcel);
                    //gridControlVersionNivel.DataSource = null;

                    //MainForm.dtTypeDevice = dtCargarExcel;
                    
                   //.DataSource = dtCargarExcel;
                    //gvVersionNivel.RefreshData();
                    //gvVersionNivel.PopulateColumns();
                    ObtenerNombreColumnasArchivo(dtCargarExcel);


                    conexion.Close();//cerramos la conexion
                    //gridControl1.AllowUserToAddRows = false;       //eliminamos la ultima fila del datagridview que se autoagrega
                }
                catch (Exception ex)
                {
                    //en caso de haber una excepcion que nos mande un mensaje de error
                    Error.addMensaje("Error reading file", ex.Message, true);
                }
            }
        }


        private void ObtenerNombreColumnasArchivo(DataTable tablaImport)
        {
            try
            {
                if (Clases.sTablasLoad == Clases.EnumTablas.TYPE_DEVICES)
                {
                    ListaNombreTypeDevices = new List<string>(tablaImport.Columns.Count);
                    for (int col = 0; col < tablaImport.Columns.Count; col++)
                    {
                        ListaNombreTypeDevices.Add(tablaImport.Columns[col].Caption.Trim());
                    }
                }
                else if (Clases.sTablasLoad == Clases.EnumTablas.COMPONENTS)
                {
                    ListaNombreComponents = new List<string>(tablaImport.Columns.Count);
                    for (int col = 0; col < tablaImport.Columns.Count; col++)
                    {
                        ListaNombreComponents.Add(tablaImport.Columns[col].Caption.Trim());
                    }
                }

            }
            catch (Exception ex)
            {
                Error.addMensaje("Error reading columns of the excel", ex.Message, true);
            }

        }

        #endregion

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            Abrirarchivo();
        }

        private void btnUnirSeleccionados_Click(object sender, EventArgs e)
        {
            int indiceSelec = 0;
            if (imglstbxColCargador.ItemCount > 0 && imglstbxColArchivo.ItemCount > 0)
            {
               Clases.AsociacionImportacion Asociacion = new Clases.AsociacionImportacion();

                indiceSelec = this.imglstbxColCargador.SelectedIndices[0];
               // Asociacion.Tabla = Clases.constantes.TABLA_VERSION_NIVEL;
                Asociacion.NombreCargador = imglstbxColCargador.Items[indiceSelec].Value.ToString();
                imglstbxColCargador.Items.RemoveAt(indiceSelec);

                indiceSelec = this.imglstbxColArchivo.SelectedIndices[0];
                Asociacion.NombreArchivo = imglstbxColArchivo.Items[indiceSelec].Value.ToString();
                imglstbxColArchivo.Items.RemoveAt(indiceSelec);

                Asociacion.DescripcionAsoc = Asociacion.NombreCargador + " <> " + Asociacion.NombreArchivo;

                ListaAsociacion.Add(Asociacion);
                imglstbxColAsociacion.Items.Add(Asociacion.DescripcionAsoc);


            }
        }

        private void btnSepararAsociacion_Click(object sender, EventArgs e)
        {
            if (this.imglstbxColAsociacion.ItemCount > 0)
            {

                int indiceSelec = 0;
                Clases.AsociacionImportacion Asociacion = new Clases.AsociacionImportacion();

                indiceSelec = this.imglstbxColAsociacion.SelectedIndices[0];

                Asociacion = ListaAsociacion.Find(x => x.DescripcionAsoc == imglstbxColAsociacion.Items[indiceSelec].Value.ToString());

                imglstbxColArchivo.Items.Add(Asociacion.NombreArchivo);
                imglstbxColCargador.Items.Add(Asociacion.NombreCargador);

                ListaAsociacion.Remove(Asociacion);
                imglstbxColAsociacion.Items.RemoveAt(indiceSelec);

            }
        }

        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));

            Error.LErroCargarExcel = new List<string>();
            for (int i = 0; i < ListaAsociacion.Count; i++)
            {

                // se cambia el nombre de las columnas 
                dtCargarExcel.Columns[ListaAsociacion[i].NombreArchivo].ColumnName = ListaAsociacion[i].NombreCargador;
                dtCargarExcel.Columns[ListaAsociacion[i].NombreCargador].SetOrdinal(ListaAsociacion[i].Ordinal);
            }

            foreach (DataRow item in dtCargarExcel.Rows)
            {
                

                try
                {
                    if (Clases.EnumTablas.TYPE_DEVICES == Clases.sTablasLoad)
                    {
                        string type = item[Clases.constantes.TYPE_DEVICE].ToString();
                        string description = item[Clases.constantes.DESCRIPTION].ToString();
                        string part = item[Clases.constantes.PART].ToString();
                        int quantity = (int)item[Clases.constantes.QUANTITY];
                        string model = item[Clases.constantes.MODEL].ToString();
                        MainForm.dtTypeDevices.Rows.Add(type, description, model, part, quantity);
                    }
                    else if (Clases.EnumTablas.COMPONENTS == Clases.sTablasLoad)
                    {
                        string partCode = item[Clases.constantes.PART_CODE].ToString();
                        string description = item[Clases.constantes.DESCRIPTION].ToString();
                        decimal cost = Convert.ToDecimal(item[Clases.constantes.COST]);
                        string stock = item[Clases.constantes.STOCK].ToString();

                        MainForm.dtComponents.Rows.Add(partCode, description, cost, stock);
                    }
                }
                catch (ConstraintException ee)
                {
                    Error.LErroCargarExcel.Add(ee.Message);
                }
                catch (Exception ex)
                {
                    Error.addMensaje("Error loading data", ex.Message, true);
                }

            }

            SplashScreenManager.CloseForm();
        }

        private void wizardControl1_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            if (e.Page == this.AsociarPage && e.Direction == Direction.Forward)
            {

                if (txtRuta.EditValue == null)
                {

                    e.Cancel = true;

                    Abrirarchivo();
                }

                

            }
            else if (e.Page == this.FinalPage && e.Direction == Direction.Forward)
            {
                if (imglstbxColAsociacion.ItemCount == 0)
                {
                    Error.addMensaje("Validating association", "You must associate the columns of the file with the columns of the database", true);
                    e.Cancel = true;
                }
            }

        }
    }
}