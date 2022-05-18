using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana6CRUDTarea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VistaBorrar : ContentPage
    {
        private const string Url = "http://192.168.0.108/Deber5POSTMAN/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Semana6CRUDTarea.Datos> _post;

        public VistaBorrar()
        {
            InitializeComponent();
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            try
            {
            if (!string.IsNullOrEmpty(txtIdCliente.Text))
            {
                int idcliente = int.Parse(txtIdCliente.Text.ToString());
                var content = await client.GetStringAsync(Url + "?idcliente=" + idcliente);
                content = "[" + content + "]";
                List<Semana6CRUDTarea.Datos> posts = JsonConvert.DeserializeObject<List<Semana6CRUDTarea.Datos>>(content);
                _post = new ObservableCollection<Semana6CRUDTarea.Datos>(posts);


                if (_post.Count > 0)
                {

                    Datos data = new Datos();

                    data = posts.FirstOrDefault();

                    txtNombreCliente.Text = data.nombrecliente.ToString();
                    txtDireccionCliente.Text = data.direccioncliente.ToString();
                    txtCelularCliente.Text = data.celularcliente.ToString();
                    txtCorreoCliente.Text = data.correocliente.ToString();
                    txtEstadoCliente.Text = data.estadocliente.ToString();
                }
            }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Mensaje de alerta " + ex.Message, "Ok");
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdCliente.Text))
                    DisplayAlert("Alerta", "No ha seleccionado ningun cliente.", "Ok");
                else
                {
                    WebClient cliente = new WebClient();

                    string parametros = "";

                    parametros += "?idcliente=" + txtIdCliente.Text;

                    var urlCompleta = new Uri(Url + parametros);

                    cliente.UploadString(urlCompleta, "DELETE", "");

                    DisplayAlert("Alerta", "Registro eliminado correctamente.", "Ok");

                    txtIdCliente.Text = "";
                    txtNombreCliente.Text = "";
                    txtDireccionCliente.Text = "";
                    txtCelularCliente.Text = "";
                    txtCorreoCliente.Text = "";
                    txtEstadoCliente.Text = "";
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "Mensaje de alerta " + ex.Message, "Ok");
            }
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista());
        }
    }
}