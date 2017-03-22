using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CPU.Forecast.Tool
{
    public static class Error
    {
        private static string sMensaje;
        private static string sDescripcionMensaje;
        private static List<string> lErroCargarExcel;


        public static string SMensaje
        {
            get
            {
                return sMensaje;
            }

            set
            {
                sMensaje = value;
            }
        }

        public static string SDescripcionMensaje
        {
            get
            {
                return sDescripcionMensaje;
            }

            set
            {
                sDescripcionMensaje = value;
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

        public static void addMensaje( string sPrmMensaje, string sPrmDecripcion, bool EsError) {
            SMensaje = sPrmMensaje;
            SDescripcionMensaje = sPrmDecripcion;
            ShowError(EsError);
        }

        public static void ShowError(bool bEsError)
        {
            if (bEsError)
            {
                MessageBox.Show(SMensaje, SDescripcionMensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show(SMensaje, SDescripcionMensaje, MessageBoxButtons.OK, MessageBoxIcon.None);

            }
        }
    }
}
