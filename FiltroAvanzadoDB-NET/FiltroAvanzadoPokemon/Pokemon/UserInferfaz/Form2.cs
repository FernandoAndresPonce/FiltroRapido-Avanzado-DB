using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using BusinessLogic;
using BussinesLogic;

namespace UserInferfaz
{

    public partial class frmAltaPokemon : Form
    {
        private Pokemon pokemon = null;

        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        public frmAltaPokemon(Pokemon modificarPokemon)
        {
            InitializeComponent();

            this.pokemon = modificarPokemon;
            lblAlta.Text = "Modificar Pokemon";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            ElementoBusi elementoLista = new ElementoBusi();
            
            

            cmbTipo.DataSource = elementoLista.ListarE();
            cmbTipo.ValueMember = "Id";
            cmbTipo.DisplayMember = "Descripcion";

            
            cmbDebilidad.DataSource = elementoLista.ListarE();
            cmbDebilidad.ValueMember = "Id";
            cmbDebilidad.DisplayMember = "descripcion";

            if (pokemon != null)
            {
                txtNumero.Text = pokemon.Numero.ToString();
                txtNombre.Text = pokemon.Nombre;

                txtDescripcion.Text = pokemon.Descripcion;

                cargarImagenAP(pokemon.UrlImagen);
                txtUrlImagen.Text = pokemon.UrlImagen;

                cmbTipo.SelectedValue = pokemon.Tipo.Id;
                cmbDebilidad.SelectedValue = pokemon.Debilidad.Id;
            }
        }
            



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            PokemonBusiness negocio = new PokemonBusiness();
            

            try
            {
                if (pokemon == null)
                    pokemon = new Pokemon();

                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;


                pokemon.Tipo = (Elemento)cmbTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cmbDebilidad.SelectedItem;

                if (pokemon.Id != 0)
                {
                    negocio.modificar(pokemon);
                    MessageBox.Show("Modificado Exitosament");
                }
                else
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("Agregado Exitosamente");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
            finally
            {
                Close();
            }
           

            
            
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagenAP(txtUrlImagen.Text);
        }

        public void cargarImagenAP( string imagen)
        {
            try
            {
                pictureBox1.Load(imagen);
            }
            catch (Exception ex)
            {
                pictureBox1.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSkq9bHJ3gt0lMcFAlhsCbumDb0fYgvpP0HNQ&s");


            }

        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
