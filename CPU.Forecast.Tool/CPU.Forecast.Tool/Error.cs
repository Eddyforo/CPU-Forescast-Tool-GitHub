using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CPU.Forecast.Tool
{
    public static class Error
    {
        private static string sError;
        private static string sDescripcionError;
        private static List<string> lErroCargarExcel;


        public static string SError
        {
            get
            {
                return sError;
            }

            set
            {
                sError = value;
            }
        }

        public static string SDescripcionError
        {
            get
            {
                return sDescripcionError;
            }

            set
            {
                sDescripcionError = value;
            }
        }

        public static List<string> LErroCargarExcel
        {
            get
            {
                return lErroCargarExcel;
            }

            set
            {
                lErroCargarExcel = value;
            }
        }

        public static void addError( string sPrmError, string sPrmDecripcion) {
            SError = sPrmError;
            SDescripcionError = sPrmDecripcion;
        }

        public static void ShowError()
        {
            MessageBox.Show(SError, SDescripcionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
