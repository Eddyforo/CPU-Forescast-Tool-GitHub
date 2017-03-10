namespace CPU.Forecast.Tool
{
    partial class CargaExcelWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CargaExcelWizard));
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.WelcomPage = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAbrir = new DevExpress.XtraEditors.SimpleButton();
            this.txtRuta = new DevExpress.XtraEditors.TextEdit();
            this.FinalPage = new DevExpress.XtraWizard.CompletionWizardPage();
            this.label2 = new System.Windows.Forms.Label();
            this.AsociarPage = new DevExpress.XtraWizard.WizardPage();
            this.btnSepararAsociacion = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnirSeleccionados = new DevExpress.XtraEditors.SimpleButton();
            this.imglstbxColAsociacion = new DevExpress.XtraEditors.ImageListBoxControl();
            this.imglstbxColArchivo = new DevExpress.XtraEditors.ImageListBoxControl();
            this.imglstbxColCargador = new DevExpress.XtraEditors.ImageListBoxControl();
            this.lblColAsociacion = new DevExpress.XtraEditors.LabelControl();
            this.lblColArchivo = new DevExpress.XtraEditors.LabelControl();
            this.lblColCargador = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.WelcomPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuta.Properties)).BeginInit();
            this.FinalPage.SuspendLayout();
            this.AsociarPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imglstbxColAsociacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imglstbxColArchivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imglstbxColCargador)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.WelcomPage);
            this.wizardControl1.Controls.Add(this.FinalPage);
            this.wizardControl1.Controls.Add(this.AsociarPage);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishText = "&Finish and load";
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.WelcomPage,
            this.AsociarPage,
            this.FinalPage});
            this.wizardControl1.Size = new System.Drawing.Size(734, 415);
            this.wizardControl1.Text = "CPU forecast charge excel";
            this.wizardControl1.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            this.wizardControl1.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.wizardControl1_SelectedPageChanging);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            // 
            // WelcomPage
            // 
            this.WelcomPage.Controls.Add(this.label1);
            this.WelcomPage.Controls.Add(this.groupBox1);
            this.WelcomPage.Name = "WelcomPage";
            this.WelcomPage.Size = new System.Drawing.Size(674, 247);
            this.WelcomPage.Text = "Welcome to the data loader wizard!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(39, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(580, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "This wizard will help you, step by step, to upload an Excel spreadsheet into a da" +
    "tabase table.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAbrir);
            this.groupBox1.Controls.Add(this.txtRuta);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBox1.Location = new System.Drawing.Point(23, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 109);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose a file";
            // 
            // btnAbrir
            // 
            this.btnAbrir.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnAbrir.Location = new System.Drawing.Point(560, 41);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(82, 22);
            this.btnAbrir.TabIndex = 1;
            this.btnAbrir.Text = "Open";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(19, 42);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Properties.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(535, 20);
            this.txtRuta.TabIndex = 0;
            // 
            // FinalPage
            // 
            this.FinalPage.Controls.Add(this.label2);
            this.FinalPage.Name = "FinalPage";
            this.FinalPage.Size = new System.Drawing.Size(674, 247);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(49, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(563, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "If you press the button \"Finish and Load\" all the rows of the Excel are going to " +
    "drop on the old data.";
            // 
            // AsociarPage
            // 
            this.AsociarPage.Controls.Add(this.btnSepararAsociacion);
            this.AsociarPage.Controls.Add(this.btnUnirSeleccionados);
            this.AsociarPage.Controls.Add(this.imglstbxColAsociacion);
            this.AsociarPage.Controls.Add(this.imglstbxColArchivo);
            this.AsociarPage.Controls.Add(this.imglstbxColCargador);
            this.AsociarPage.Controls.Add(this.lblColAsociacion);
            this.AsociarPage.Controls.Add(this.lblColArchivo);
            this.AsociarPage.Controls.Add(this.lblColCargador);
            this.AsociarPage.Name = "AsociarPage";
            this.AsociarPage.Size = new System.Drawing.Size(674, 247);
            this.AsociarPage.Text = "Associate the columns";
            // 
            // btnSepararAsociacion
            // 
            this.btnSepararAsociacion.Image = ((System.Drawing.Image)(resources.GetObject("btnSepararAsociacion.Image")));
            this.btnSepararAsociacion.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSepararAsociacion.Location = new System.Drawing.Point(401, 146);
            this.btnSepararAsociacion.Name = "btnSepararAsociacion";
            this.btnSepararAsociacion.Size = new System.Drawing.Size(31, 26);
            this.btnSepararAsociacion.TabIndex = 54;
            this.btnSepararAsociacion.ToolTip = "Separar las asociaciones seleccionadas";
            this.btnSepararAsociacion.Click += new System.EventHandler(this.btnSepararAsociacion_Click);
            // 
            // btnUnirSeleccionados
            // 
            this.btnUnirSeleccionados.Image = ((System.Drawing.Image)(resources.GetObject("btnUnirSeleccionados.Image")));
            this.btnUnirSeleccionados.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnUnirSeleccionados.Location = new System.Drawing.Point(401, 51);
            this.btnUnirSeleccionados.Name = "btnUnirSeleccionados";
            this.btnUnirSeleccionados.Size = new System.Drawing.Size(31, 28);
            this.btnUnirSeleccionados.TabIndex = 53;
            this.btnUnirSeleccionados.ToolTip = "Relacionar columnas seleccionadas";
            this.btnUnirSeleccionados.Click += new System.EventHandler(this.btnUnirSeleccionados_Click);
            // 
            // imglstbxColAsociacion
            // 
            this.imglstbxColAsociacion.HorizontalScrollbar = true;
            this.imglstbxColAsociacion.Location = new System.Drawing.Point(438, 37);
            this.imglstbxColAsociacion.Name = "imglstbxColAsociacion";
            this.imglstbxColAsociacion.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.imglstbxColAsociacion.Size = new System.Drawing.Size(215, 190);
            this.imglstbxColAsociacion.TabIndex = 52;
            // 
            // imglstbxColArchivo
            // 
            this.imglstbxColArchivo.HorizontalScrollbar = true;
            this.imglstbxColArchivo.Location = new System.Drawing.Point(211, 38);
            this.imglstbxColArchivo.Name = "imglstbxColArchivo";
            this.imglstbxColArchivo.Size = new System.Drawing.Size(184, 190);
            this.imglstbxColArchivo.TabIndex = 51;
            // 
            // imglstbxColCargador
            // 
            this.imglstbxColCargador.Cursor = System.Windows.Forms.Cursors.Default;
            this.imglstbxColCargador.HorizontalScrollbar = true;
            this.imglstbxColCargador.Location = new System.Drawing.Point(21, 37);
            this.imglstbxColCargador.Name = "imglstbxColCargador";
            this.imglstbxColCargador.Size = new System.Drawing.Size(184, 190);
            this.imglstbxColCargador.TabIndex = 50;
            // 
            // lblColAsociacion
            // 
            this.lblColAsociacion.Location = new System.Drawing.Point(514, 19);
            this.lblColAsociacion.Name = "lblColAsociacion";
            this.lblColAsociacion.Size = new System.Drawing.Size(54, 13);
            this.lblColAsociacion.TabIndex = 49;
            this.lblColAsociacion.Text = "Association";
            // 
            // lblColArchivo
            // 
            this.lblColArchivo.Location = new System.Drawing.Point(246, 19);
            this.lblColArchivo.Name = "lblColArchivo";
            this.lblColArchivo.Size = new System.Drawing.Size(59, 13);
            this.lblColArchivo.TabIndex = 48;
            this.lblColArchivo.Text = "File Columns";
            // 
            // lblColCargador
            // 
            this.lblColCargador.Location = new System.Drawing.Point(52, 19);
            this.lblColCargador.Name = "lblColCargador";
            this.lblColCargador.Size = new System.Drawing.Size(89, 13);
            this.lblColCargador.TabIndex = 47;
            this.lblColCargador.Text = "Database Columns";
            // 
            // CargaExcelWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 415);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "CargaExcelWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CPU Forecast Tool";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.WelcomPage.ResumeLayout(false);
            this.WelcomPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRuta.Properties)).EndInit();
            this.FinalPage.ResumeLayout(false);
            this.FinalPage.PerformLayout();
            this.AsociarPage.ResumeLayout(false);
            this.AsociarPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imglstbxColAsociacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imglstbxColArchivo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imglstbxColCargador)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage WelcomPage;
        private DevExpress.XtraWizard.CompletionWizardPage FinalPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnAbrir;
        private DevExpress.XtraEditors.TextEdit txtRuta;
        private DevExpress.XtraWizard.WizardPage AsociarPage;
        private DevExpress.XtraEditors.SimpleButton btnSepararAsociacion;
        private DevExpress.XtraEditors.SimpleButton btnUnirSeleccionados;
        private DevExpress.XtraEditors.ImageListBoxControl imglstbxColAsociacion;
        private DevExpress.XtraEditors.ImageListBoxControl imglstbxColArchivo;
        private DevExpress.XtraEditors.ImageListBoxControl imglstbxColCargador;
        private DevExpress.XtraEditors.LabelControl lblColAsociacion;
        private DevExpress.XtraEditors.LabelControl lblColArchivo;
        private DevExpress.XtraEditors.LabelControl lblColCargador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}