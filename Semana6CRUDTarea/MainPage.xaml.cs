using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;

namespace Semana6CRUDTarea
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try{
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("idcliente", txtIdCliente.Text);
                parametros.Add("nombrecliente", txtNombreCliente.Text);
                parametros.Add("direccioncliente", txtDireccionCliente.Text);
                parametros.Add("celularcliente", txtCelularCliente.Text);
                parametros.Add("correocliente", txtCorreoCliente.Text);
                parametros.Add("estadocliente", txtEstadoCliente.Text);
                cliente.UploadValues("http://192.168.0.108/Deber5POSTMAN/post.php", "POST", parametros);
                await DisplayAlert("Mensaje de alerta", "Ingreso correcto", "ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista());
        }
    }
}
