﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGIMTProyecto
{
    public partial class F_MenuEditar : Form
    {
        public F_MenuEditar()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            AddUserControl(new F_EditarVehiculo());
            BTN_DatosVehiculo.BackColor = Color.FromArgb(80, 14, 95);
            BTN_DatosPropietario.BackColor = Color.FromArgb(103, 24, 122);
        }
        private void AddUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            PNL_Editar.Controls.Clear();
            PNL_Editar.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void BTN_DatosVehiculo_Click(object sender, EventArgs e)
        {
            AddUserControl(new F_EditarVehiculo());
            BTN_DatosVehiculo.BackColor = Color.FromArgb(80, 14, 95);
            BTN_DatosPropietario.BackColor = Color.FromArgb(103, 24, 122);
        }

        private void BTN_Inicio_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_DatosPropietario_Click(object sender, EventArgs e)
        {
            AddUserControl(new F_EditarPropietario());
            BTN_DatosVehiculo.BackColor = Color.FromArgb(103, 24, 122);
            BTN_DatosPropietario.BackColor = Color.FromArgb(80, 14, 95);
        }
    }
}
