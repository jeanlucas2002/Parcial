using System.Data.SqlClient;

namespace Parcial_Paulino
{
    public partial class frmProductoedit : Form
    {
        string cadenaConexion = "server=localhost; database=Parcial; Integrated Security = true;";
        public frmProductoedit()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

        }

        private void frmProductoedit_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                // CARGAR DATOS DE CATEGORIA
                var sql = "SELECT * FROM Categoria";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            Dictionary<string, string> categoriaSource = new Dictionary<string, string>();
                            while (lector.Read())
                            {
                                categoriaSource.Add(lector[0].ToString(), lector[2].ToString());
                            }
                            cbocategoria.DataSource = new BindingSource(categoriaSource, null);
                            cbocategoria.DisplayMember = "Value";
                            cbocategoria.ValueMember = "Key";
                        }
                    }
                }
            }
        }
    }
}