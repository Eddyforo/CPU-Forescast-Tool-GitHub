using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CPU.Forecast.Tool
{
    class Clases
    {
        public static EnumTablas sTablasLoad;

        public class TypeDevices
        {
            private string type_devices;
            private string description;
            private string model;
            private string part;
            private int quantity;

            public string Type_devices
            {
                get
                {
                    return type_devices;
                }

                set
                {
                    type_devices = value;
                }
            }

            public string Description
            {
                get
                {
                    return description;
                }

                set
                {
                    description = value;
                }
            }

            public string Model
            {
                get
                {
                    return model;
                }

                set
                {
                    model = value;
                }
            }

            public string Part
            {
                get
                {
                    return part;
                }

                set
                {
                    part = value;
                }
            }

            public int Quantity
            {
                get
                {
                    return quantity;
                }

                set
                {
                    quantity = value;
                }
            }
 
        }

        public class MaintenceComponents
        {
            private string sPartCode;
            private string sDescription;
            private decimal nCost;
            private int nStock;

            public string SPartCode
            {
                get
                {
                    return sPartCode;
                }

                set
                {
                    sPartCode = value;
                }
            }

            public string SDescription
            {
                get
                {
                    return sDescription;
                }

                set
                {
                    sDescription = value;
                }
            }

            public decimal NCost
            {
                get
                {
                    return nCost;
                }

                set
                {
                    nCost = value;
                }
            }

            public int NStock
            {
                get
                {
                    return nStock;
                }

                set
                {
                    nStock = value;
                }
            }
        }

        public class MaximunCost
        {
            private string sModel;
            private decimal sMaximunCost;

            public string SModel
            {
                get
                {
                    return sModel;
                }

                set
                {
                    sModel = value;
                }
            }

            public decimal SMaximunCost
            {
                get
                {
                    return sMaximunCost;
                }

                set
                {
                    sMaximunCost = value;
                }
            }
        }

        public class Plan
        {
            private string sModel;
            private int nPlan;


            public string SModel
            {
                get
                {
                    return sModel;
                }

                set
                {
                    sModel = value;
                }
            }

            public int NPlan
            {
                get
                {
                    return nPlan;
                }

                set
                {
                    nPlan = value;
                }
            }
        }

        public class DetalleVersionPlan
        {
            private int nVersion;
            private string sPart;
            private string sModel;
            private int nCantidadPart;


            public string SPart
            {
                get
                {
                    return sPart;
                }

                set
                {
                    sPart = value;
                }
            }

            public string SModel
            {
                get
                {
                    return sModel;
                }

                set
                {
                    sModel = value;
                }
            }

            public int NVersion
            {
                get
                {
                    return nVersion;
                }

                set
                {
                    nVersion = value;
                }
            }

            public int NCantidadPart
            {
                get
                {
                    return nCantidadPart;
                }

                set
                {
                    nCantidadPart = value;
                }
            }
        }
        public class VersionPlan
        {
            private string sType;
            private string sModel;
            private int nVersion = 0;
            private int nCantidadPlan;
            private decimal nCOG;
            private decimal nTotalCOG;
            private List<DetalleVersionPlan> listaDetalle;

            public VersionPlan()
            {
                nVersion += 1;
            }

            public string SType
            {
                get
                {
                    return sType;
                }

                set
                {
                    sType = value;
                }
            }

            public string SModel
            {
                get
                {
                    return sModel;
                }

                set
                {
                    sModel = value;
                }
            }

            public int NVersion
            {
                get
                {
                    return nVersion;
                }
            }

            public int NCantidadPlan
            {
                get
                {
                    return nCantidadPlan;
                }

                set
                {
                    nCantidadPlan = value;
                }
            }

            public decimal NCOG
            {
                get
                {
                    return nCOG;
                }

                set
                {
                    nCOG = value;
                }
            }

            public decimal NTotalCOG
            {
                get
                {
                    return nTotalCOG;
                }

                set
                {
                    nTotalCOG = value;
                }
            }

            internal List<DetalleVersionPlan> ListaDetalle
            {
                get
                {
                    return listaDetalle;
                }

                set
                {
                    listaDetalle = value;
                }
            }

            public void addDetalle(string sPrmPart, int nPrmCantidadPart)
            {
                DetalleVersionPlan nuevo = new DetalleVersionPlan();

                nuevo.NVersion = NVersion;
                nuevo.SModel = SModel;
                nuevo.SPart = sPrmPart;
                nuevo.NCantidadPart = nPrmCantidadPart;

                ListaDetalle.Add(nuevo);
            }
        }

        public class Forecast
        {
            public bool UpdateDB(DataTable dtTypeDevice, DataTable dtMaintenceCompo, DataTable dtMaximCost, DataTable dtPlan )
            {
                bool bOk = true; 

                if (dtTypeDevice.Rows.Count > 0 && bOk == true)
                {
                    bOk = DeleteDatosTabla("TYPE_DEVICES");

                    bOk = bOk && InsertarTablas(dtTypeDevice, "TYPE_DEVICES");
                }

                if (dtMaintenceCompo.Rows.Count > 0 && bOk == true)
                {
                    bOk = DeleteDatosTabla("COMPONENTS");

                    bOk = bOk && InsertarTablas(dtMaintenceCompo, "COMPONENTS");
                }

                if (dtMaximCost.Rows.Count > 0 && bOk == true)
                {
                    bOk = DeleteDatosTabla("MAXIMUN_COST");

                    bOk = bOk && InsertarTablas(dtMaximCost, "MAXIMUN_COST");
                }

                if (dtPlan.Rows.Count > 0 && bOk == true)
                {
                    bOk = DeleteDatosTabla("PLANS");

                    bOk = bOk && InsertarTablas(dtPlan, "PLANS");
                }

                return bOk;

            }

            /// <summary>
            /// Inserta todo el datatable a la tabla especificada
            /// </summary>
            /// <param name="dtCarga"></param>
            /// <param name="sTabla"></param>
            /// <returns></returns>
            public bool InsertarTablas(DataTable dtCarga, string sTabla)
            {
                bool bOk = true;


                if (DataConnect.Conn.State == ConnectionState.Closed)
                {
                    DataConnect.Conn.Open();
                }

                SqlBulkCopy bulkTypeDevice =
                    new SqlBulkCopy
                    (
                    DataConnect.Conn,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );

                bulkTypeDevice.DestinationTableName = sTabla;

                try
                {
                    bulkTypeDevice.WriteToServer(dtCarga);
                }
                catch (Exception ex)
                {
                    Error.addError("Error inserting row in a : " + sTabla + " ", ex.Message);
                    bOk = false;
                }
                return bOk;

            }

            public bool DeleteDatosTabla( string sTabla)
            {
                bool bOk = true;

                try
                {
                    using (var cmd = DataConnect.Conn.CreateCommand())
                    {
                        if (DataConnect.Conn.State == ConnectionState.Closed)
                        {
                            DataConnect.Conn.Open();
                        }
                        cmd.CommandText = " DELETE FROM " + sTabla;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Error.addError("Error deleting row in a table" + sTabla, e.Message);
                    bOk = false;
                }

                return bOk;

            }

            public bool LoadDatosTabla(DataTable dtTypeDevice, DataTable dtMaintenceCompo, DataTable dtMaximCost, DataTable dtPlan)
            {
                bool bOk = true;

                #region  Type Device

                try
                {
                    dtTypeDevice.Clear();
                    using (var cmd = DataConnect.Conn.CreateCommand())
                    {
                        cmd.CommandText = " SELECT TYPE_DEVICE, DESCRIPTION, MODEL, PART, QUANTITY FROM TYPE_DEVICES ";
                        //cmd.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        // this will query your database and return the result to your datatable
                        da.Fill(dtTypeDevice);
                        
                        da.Dispose();

                    }
                }
                catch (Exception e)
                {
                    Error.addError("Error loading rows in a table type_devices " , e.Message);
                    bOk = false;
                }

                #endregion


                #region  Components

                try
                {
                    dtMaintenceCompo.Clear();
                    using (var cmd = DataConnect.Conn.CreateCommand())
                    {
                        cmd.CommandText = " SELECT PART_CODE, DESCRIPTION, COST, STOCK FROM COMPONENTS ";
                        //cmd.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        // this will query your database and return the result to your datatable
                        da.Fill(dtMaintenceCompo);

                        da.Dispose();

                    }
                }
                catch (Exception e)
                {
                    Error.addError("Error loading rows in a table COMPONENTS ", e.Message);
                    bOk = false;
                }

                #endregion

                #region  MAXIMUN_COST

                try
                {
                    dtMaximCost.Clear();
                    using (var cmd = DataConnect.Conn.CreateCommand())
                    {
                        cmd.CommandText = " SELECT MODEL, MAX_COST FROM MAXIMUN_COST ";
                        //cmd.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        // this will query your database and return the result to your datatable
                        da.Fill(dtMaximCost);

                        da.Dispose();

                    }
                }
                catch (Exception e)
                {
                    Error.addError("Error loading rows in a table MAXIMUN_COST ", e.Message);
                    bOk = false;
                }

                #endregion

                #region  PLANS

                try
                {
                    dtPlan.Clear();
                    using (var cmd = DataConnect.Conn.CreateCommand())
                    {
                        cmd.CommandText = " SELECT MODEL, PLANS FROM PLANS ";
                        //cmd.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        // this will query your database and return the result to your datatable
                        da.Fill(dtPlan);

                        da.Dispose();

                    }
                }
                catch (Exception e)
                {
                    Error.addError("Error loading rows in a table PLANS ", e.Message);
                    bOk = false;
                }

                #endregion

                return bOk;

            }

        }

        public struct AsociacionImportacion
        {

            private string nombreCargador;
            /// <summary>
            /// Nombre de la columna del cargador
            /// </summary>
            public string NombreCargador
            {
                get { return nombreCargador; }
                set
                {
                    nombreCargador = value;
                    SetOrdinario(nombreCargador);
                }
            }

            private string nombreArchivo;
            /// <summary>
            /// Nombre de la columna del Archivo
            /// </summary>
            public string NombreArchivo
            {
                get { return nombreArchivo; }
                set { nombreArchivo = value; }
            }

            private string descripcionAsoc;
            /// <summary>
            /// Nombre de la columna del Archivo
            /// </summary>
            public string DescripcionAsoc
            {
                get { return descripcionAsoc; }
                set
                {
                    descripcionAsoc = value;

                }
            }

            private string tabla;
            /// <summary>
            /// Nombre de la tabla del Archivo
            /// </summary>
            public string Tabla
            {
                get { return tabla; }
                set { tabla = value; }
            }

            private int ordinal;

            public int Ordinal
            {
                get { return ordinal; }
                set { ordinal = value; }
            }

            private void SetOrdinario(string nombre)
            {
                if (sTablasLoad == EnumTablas.TYPE_DEVICES)
                {
                    if (nombre == constantes.TYPE_DEVICE)
                        Ordinal = 0;
                    else if (nombre == constantes.DESCRIPTION)
                        Ordinal = 1;
                    else if (nombre == constantes.MODEL)
                        Ordinal = 2;
                    else if (nombre == constantes.PART)
                        Ordinal = 3;
                    else if (nombre == constantes.QUANTITY)
                        Ordinal = 4;
                }
                else if (sTablasLoad == EnumTablas.COMPONENTS)
                {
                    if (nombre == constantes.PART_CODE)
                        Ordinal = 0;
                    else if (nombre == constantes.DESCRIPTION)
                        Ordinal = 1;
                    else if (nombre == constantes.COST)
                        Ordinal = 2;
                    else if (nombre == constantes.STOCK)
                        Ordinal = 3;
                }


            }
        }


        public class constantes
        {
            public const string TYPE_DEVICE = "TYPE_DEVICE";
            public const string DESCRIPTION = "DESCRIPTION";
            public const string MODEL = "MODEL";
            public const string PART = "PART";
            public const string QUANTITY = "QUANTITY";


            public const string PART_CODE = "PART_CODE";
            public const string COST = "COST";
            public const string STOCK = "STOCK";


            public const string PLANS = "PLANS";
            public const string MAX_COST = "MAX_COST";


        }

        public enum EnumTablas
        {
            TYPE_DEVICES,
            COMPONENTS,
            PLANS,
            MAXIMUN_COST
        };

    }
}
