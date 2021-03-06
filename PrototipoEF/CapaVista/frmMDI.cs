﻿using CapaVista.Transaccion;
using CapaVistaSeguridad;
using CapaVistaSeguridad.Formularios.Mantenimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista
{
    public partial class frmMDI : Form
    {
        private int childFormNumber = 0;
        clsFuncionesSeguridad seguridad = new clsFuncionesSeguridad();//instancia para los permisos por aplicacion
        clsVistaBitacora bit = new clsVistaBitacora();//instancia para la bitacora.
        public frmMDI()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void frmMDI_Load(object sender, EventArgs e)
        {
            //Este codigo es para el login.
            frmLogin frm = new frmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtUsuario.Text = frm.usuario();
            }
        }

        private void mantenimientoCategoriaProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seguridad.PermisosAcceso("3303", txtUsuario.Text) == 1)
            {
                bit.user(txtUsuario.Text);
                bit.insert("Ingreso Mantenimiento Categoria Productos", 3302);
                frmCategoriaProducto ventana = new frmCategoriaProducto(txtUsuario.Text);
                ventana.MdiParent = this;
                ventana.Show();
            }
            else
            {
                MessageBox.Show("El Usuario No Cuenta Con Permisos De Acceso A La Aplicación");
                bit.user(txtUsuario.Text);
                bit.insert("Trato de Ingresar a Mantenimiento Categoria Productos", 3302);
            }
        }

        private void categoriaPrudcotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteCategoriaProducto ventana = new ReporteCategoriaProducto();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void reportesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBitacora ventana = new frmBitacora();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void procesoOrdenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seguridad.PermisosAcceso("3310", txtUsuario.Text) == 1)
            {
                bit.user(txtUsuario.Text);
                bit.insert("Ingreso Generar Orden de Compra", 3310);
                frmOrdenDeCompra perfil = new frmOrdenDeCompra(txtUsuario.Text);
                perfil.MdiParent = this;
                perfil.Show();
            }
            else
            {
                bit.user(txtUsuario.Text);
                bit.insert("Trato de Ingresar a Generar Orden de Compra", 3310);
                MessageBox.Show("El Usuario No Cuenta Con Permisos De Acceso A La Aplicación");
            }
        }

        private void ordenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteOrdenCompra ventana = new ReporteOrdenCompra();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            frmLogin frm = new frmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtUsuario.Text = frm.usuario();
            }
        }

        private void cambioDeContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo los usuarios pueden acceder a esta ventana para cambiar su contraseña.
            frmCambioContraseña cambioContraseña = new frmCambioContraseña(txtUsuario.Text);
            cambioContraseña.MdiParent = this;
            cambioContraseña.Show();
            bit.user(txtUsuario.Text);
            bit.insert("Ingreso A Cambio de Contraseña", 11);
        }

        private void mantenimientoUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seguridad.PermisosAcceso("2", txtUsuario.Text) == 1)
            {
                bit.user(txtUsuario.Text);
                bit.insert("Ingreso A Mantenimiento Usuario", 2);
                frmMantenimientoUsuario mantenimientoUsuario = new frmMantenimientoUsuario(txtUsuario.Text);
                mantenimientoUsuario.MdiParent = this;
                mantenimientoUsuario.Show();
            }
            else
            {
                bit.user(txtUsuario.Text);
                bit.insert("Trato de ingresar a Mantenimiento Usuario", 2);
                MessageBox.Show("El Usuario No Cuenta Con Permisos De Acceso A La Aplicación");
            }
        }
    }
}
