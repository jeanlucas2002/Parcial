using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_Paulino
{
    public partial class frmProducto : Form
    {
    
        string cadenaConexion = "server=localhost; database=Parcial; Integrated Security = true;";

        public frmProducto()
        {
            InitializeComponent();

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmProductoedit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var nombre = ((TextBox)frm.Controls["txtnombre"]).Text;
                var marca = ((TextBox)frm.Controls["txtmarca"]).Text;
                var precio = ((TextBox)frm.Controls["txtprecio"]).Text;
                var stock = ((TextBox)frm.Controls["txtstock"]).Text;
                var categoria = ((ComboBox)frm.Controls["cbocategoria"]).SelectedValue.ToString();




                using (var conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    var sql = "INSERT INTO Producto(Nombre, Marca, Precio, Stock, IdCategoria) VALUES(@nombre, @marca, @precio, @stock, @categoria)";


                    using (var comando = new SqlCommand(sql, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@marca", marca);
                        comando.Parameters.AddWithValue("@precio", precio);
                        comando.Parameters.AddWithValue("@stock", stock);
                        comando.Parameters.AddWithValue("@categoria", categoria);

                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            MessageBox.Show("El Producto ha sido Exitosamente", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cargarDatos();
                        }
                        else
                        {
                            MessageBox.Show("Error al Registrar el Producto.", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void cargarDatos()
        {
            dgvproducto.Rows.Clear();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var sql = "SELECT Nombre,Marca,Precio,Stock,IdCategoria FROM Producto";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                dgvproducto.Rows.Add(lector[0], lector[1], lector[2], lector[3], lector[4]
);
                            }
                        }
                    }
                }
            }
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            cargarDatos();

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                var frm = new frmProductoedit();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var nombre = ((TextBox)frm.Controls["txtnombre"]).Text;
                    var nom = ((TextBox)frm.Controls["txtnombre"]).Text;
                    var marca = ((TextBox)frm.Controls["txtmarca"]).Text;
                    var precio = ((TextBox)frm.Controls["txtprecio"]).Text;
                    var stock = ((TextBox)frm.Controls["txtstock"]).Text;
                    var categoria = ((ComboBox)frm.Controls["cbocategoria"]).SelectedValue.ToString();

                    using (var conexion = new SqlConnection(cadenaConexion))
                    {
                        conexion.Open();
                        var sql = "UPDATE Producto SET Nombre=@nombre, Marca=@marca, Precio=@precio, Stock=@stock, IdCategoria=@idcategoria WHERE Nombre=@nom ";

                        using (var comando = new SqlCommand(sql, conexion))
                        {
                            comando.Parameters.AddWithValue("@nombre", nombre);
                            comando.Parameters.AddWithValue("@marca", marca);
                            comando.Parameters.AddWithValue("@precio", precio);
                            comando.Parameters.AddWithValue("@stock", stock);
                            comando.Parameters.AddWithValue("@idcategoria", categoria);
                            comando.Parameters.AddWithValue("@nom", nom);
                            int resultado = comando.ExecuteNonQuery();
                            if (resultado > 0)
                            {
                                MessageBox.Show("El Producto ha sido Editado", "Sistemas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cargarDatos();
                            }
                            else
                            {
                                MessageBox.Show("El proceso de Edición del Prodcuto ha fallado.", "Sistemas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
    

      
        

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmProductoedit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var nombre = ((TextBox)frm.Controls["txtnombre"]).Text;
                var nom = ((TextBox)frm.Controls["txtnombre"]).Text;
                var marca = ((TextBox)frm.Controls["txtmarca"]).Text;
                var precio = ((TextBox)frm.Controls["txtprecio"]).Text;
                var stock = ((TextBox)frm.Controls["txtstock"]).Text;
                var categoria = ((ComboBox)frm.Controls["cbocategoria"]).SelectedValue.ToString();

                using (var conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    var sql = "DELETE FROM Producto WHERE Nombre=@nom ";

                    using (var comando = new SqlCommand(sql, conexion))
                    {
                        
                        comando.Parameters.AddWithValue("@nom", nom);
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            MessageBox.Show("El Producto ha sido eliminado", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cargarDatos();
                        }
                        else
                        {
                            MessageBox.Show("El proceso de eliminacion del Prodcuto ha fallado.", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}

