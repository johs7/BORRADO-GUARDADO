using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace EXAMENN
{
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
        }

        List<ClaseCliente> Milista = new List<ClaseCliente>();
       
        int posicion; /*Posicion*/

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsj, int wparam, int lparam);



        ClassEmp obj = new ClassEmp();

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (ValidarNombre() == false)
            {
                return;
            }
            if (ValidarID() == false)
            {
                return;
            }
            if (ValidarCorreo() == false)
            {
                return;
            }
            if (ValidarTelefono() == false)
            {
                return;
            }
            if (ValidarGenero() == false)
            {
                return;
            }
            if (ValidarCedula() == false)
            {
                return;
            }
            if (ValidarDireccion() == false)
            {
                return;
            }
/*Esto no es del todo necesario pero para mi proyecto me lo exigian por instancias objetos y asi*/
            ClaseCliente MiCliente = new ClaseCliente();
            MiCliente.C_Nombre = TxbNomClteDgv1.Text;
            MiCliente.C_IdUsuario = (int.Parse(TxbIdClteDgv1.Text));
            MiCliente.C_CorreoClte = TxbCorreoClte.Text;
            MiCliente.C_CedulaClte = TxbCedulaClte.Text;
            MiCliente.C_SexoClte = CmbTipoSexo.Text;
            MiCliente.C_DirecCliente = TxbDireccionClte.Text;
            MiCliente.C_TelClte = (int.Parse(TxbTelClte.Text));
           

            GrabarDatos();
            DgvCliente.Rows.Add(TxbIdClteDgv1.Text,TxbNomClteDgv1.Text,TxbCedulaClte.Text,TxbCorreoClte.Text,TxbTelClte.Text,CmbTipoSexo.Text,TxbDireccionClte.Text);
            TxbNomClteDgv1.Text = "";
            TxbIdClteDgv1.Text = "";
            TxbCedulaClte.Text = "";
            TxbCorreoClte.Text = "";
            TxbTelClte.Text = "";
            TxbDireccionClte.Text = "";
            CmbTipoSexo.Text = "";


       



        }

    /*Aca es donde inicia lo bueno aca es donde tu haces que todos los datos sean guardados*/

        private void GrabarDatos()
        {
            StreamWriter archivo = new StreamWriter("InfoEmpleados.txt",true);
            archivo.WriteLine(TxbIdClteDgv1.Text);
            archivo.WriteLine(TxbNomClteDgv1.Text);
            archivo.WriteLine(TxbCedulaClte.Text);
            archivo.WriteLine(TxbCorreoClte.Text);
         archivo.WriteLine(TxbTelClte.Text);
            archivo.WriteLine(CmbTipoSexo.Text);
            archivo.WriteLine(TxbDireccionClte.Text);
            archivo.Close();
        }

        

        private void PbxCerrarAuto1_Click(object sender, EventArgs e)
        {
            DialogResult Respuesta = MessageBox.Show("¿Desea Cerrar El Formulario Clientes?",
    "RENT A CAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (Respuesta == DialogResult.Yes)
            {
                this.Close();
            }
        }
/*Con este load creas un documento esto se hara automatico si lo haces bien*/
        private void FormCliente_Load(object sender, EventArgs e)
        {
           
            CmbTipoSexo.DropDownStyle = ComboBoxStyle.DropDownList;
          

            if (!File.Exists("InfoEmpleados.txt"))
            {
                StreamWriter Archivo = new StreamWriter("InfoEmpleados.txt");
                Archivo.Close();
            }
            else
            {
                StreamReader archivo= new StreamReader("InfoEmpleados.txt");
                while(!archivo.EndOfStream)
                {
                    string IdClte = archivo.ReadLine();
                    string NomClte = archivo.ReadLine();
                    string CedulaClte = archivo.ReadLine();
                    string CorreoClte = archivo.ReadLine();
                    string TelClte = archivo.ReadLine();
                    string SexoClte = archivo.ReadLine();
                    string DireccionClte = archivo.ReadLine();
                    DgvCliente.Rows.Add(IdClte,NomClte,CedulaClte,CorreoClte,TelClte,SexoClte,DireccionClte);

                }
                archivo.Close();
            }
        }

        private void BtnEliminarClte_Click_1(object sender, EventArgs e)
        {
            TxbTelClte.Clear();
            TxbNomClteDgv1.Clear();
            TxbIdClteDgv1.Clear();
            TxbDireccionClte.Clear();
            TxbCorreoClte.Clear();
            TxbCedulaClte.Clear();
        }

        private void PnlArribaCliente_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void DgvCliente_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void LblDniCliente_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelIzqCliente_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

/*Esto borra de manera permanente los datos que tu selecciones en tu dgv,recuerda que tienes que utilizar la variable global posicion*/
        private void BtnEliminarClte_Click(object sender, EventArgs e)
        {
            DialogResult Respuesta = MessageBox.Show("¿BORRAR INFORMACIÓN DE MANERA PERMANENTE?",
"RENT A CAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Respuesta == DialogResult.Yes)
            {
                DgvCliente.Rows.RemoveAt(posicion);
                GrabarBorrado();
            MessageBox.Show("BORRADO CORRECTAMENTE");
            }
        }

        private void DgvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            posicion = DgvCliente.CurrentRow.Index;
            TxbIdClteDgv1.Text = DgvCliente[0, posicion].Value.ToString();
            TxbNomClteDgv1.Text = DgvCliente[1, posicion].Value.ToString();
          TxbCedulaClte.Text = DgvCliente[2, posicion].Value.ToString();
           TxbCorreoClte.Text = DgvCliente[3, posicion].Value.ToString();
           TxbTelClte.Text = DgvCliente[4, posicion].Value.ToString();
            TxbIdClteDgv1.Text = DgvCliente[5, posicion].Value.ToString();
            TxbDireccionClte.Text = DgvCliente[6, posicion].Value.ToString();
        }
        private void GrabarBorrado()
        {
            StreamWriter Archivo=new StreamWriter("InfoEmpleados.txt");
            for(int i=0;i<DgvCliente.Rows.Count;i++)
            {
                posicion = DgvCliente.CurrentRow.Index;
                Archivo.WriteLine(DgvCliente[0, posicion].Value.ToString());
                Archivo.WriteLine(DgvCliente[1, posicion].Value.ToString());
                Archivo.WriteLine(DgvCliente[2, posicion].Value.ToString());
                Archivo.WriteLine(DgvCliente[3, posicion].Value.ToString());
                Archivo.WriteLine(DgvCliente[4, posicion].Value.ToString());
                Archivo.WriteLine(DgvCliente[5, posicion].Value.ToString());
                Archivo.WriteLine(DgvCliente[6, posicion].Value.ToString());


            }
            Archivo.Close();
        }

        public static void SoloLetras(KeyPressEventArgs v)
        {
            if (char.IsLetter(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (char.IsSeparator(v.KeyChar))
            {
                v.Handled = false;
            }
            else
            {
                v.Handled = true;
                MessageBox.Show("Porfavor Digite solo Letras");
            }
        }

        public static void SoloNumeros(KeyPressEventArgs v)
        {
            
             if(char.IsDigit(v.KeyChar))
                {
                v.Handled = false;
                }
             else if(char.IsSeparator(v.KeyChar))
            {
                v.Handled = false;
            }
             else if(char.IsControl(v.KeyChar))
            {
                v.Handled = false;
            }
            else
            {
                v.Handled = true;
                MessageBox.Show("Digite solo números porfavor");
            }
            }
        
     

        private void TxbNomClteDgv1_KeyPress(object sender, KeyPressEventArgs v)
        {
            SoloLetras(v);
        }

        private void TxbTelClte_KeyPress(object sender, KeyPressEventArgs v)
        {
            SoloNumeros(v);
        }

        private bool ValidarNombre()
        {
            if (string.IsNullOrEmpty(TxbNomClteDgv1.Text))
            {
                Error.SetError(TxbNomClteDgv1, "Debe Escribir un Nombre");
                return false;
            }
            else
            {
                Error.SetError(TxbNomClteDgv1, "");
                return true;
            }
        }
        private bool ValidarCorreo()
        {
            if (string.IsNullOrEmpty(TxbCorreoClte.Text))
            {
                Error.SetError(TxbCorreoClte, "Debe Escribir un Correo");
                return false;
            }
            else
            {
                Error.SetError(TxbCorreoClte, "");
                return true;
            }

        }

        private bool ValidarID()
        {
            if (string.IsNullOrEmpty(TxbIdClteDgv1.Text))
            {
                Error.SetError(TxbIdClteDgv1, "Debe Escribir un ID");
                return false;
            }
            else
            {
                Error.SetError(TxbIdClteDgv1, "");
                return true;
            }
        }
        private bool ValidarDireccion()
        {
            if (string.IsNullOrEmpty(TxbDireccionClte.Text))
            {
                Error.SetError(TxbDireccionClte, "Debe Escribir una Dirección");
                return false;
            }
            else
            {
                Error.SetError(TxbDireccionClte, "");
                return true;
            }
        }

        private bool ValidarCedula()
        {
            if (string.IsNullOrEmpty(TxbCedulaClte.Text))
            {
                Error.SetError(TxbCedulaClte, "Debe Escribir Su Numero de Cedula");
                return false;
            }
            else
            {
                Error.SetError(TxbCedulaClte, "");
                return true;
            }
        }

        private bool ValidarGenero()
        {
            if (string.IsNullOrEmpty(CmbTipoSexo.Text))
            {
                Error.SetError(CmbTipoSexo, "Debe Seleccionar Un Genero");
                return false;
            }
            else
            {
                Error.SetError(CmbTipoSexo, "");
                return true;
            }
        }

        private bool ValidarTelefono()
        {
            if (string.IsNullOrEmpty(TxbTelClte.Text))
            {
                Error.SetError(TxbTelClte, "Debe Escribir Su Número Celular");
                return false;
            }
            else
            {
                Error.SetError(TxbTelClte, "");
                return true;
            }
        }


    }
}
