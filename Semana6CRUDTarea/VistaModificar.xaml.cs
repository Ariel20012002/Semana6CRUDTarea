using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Semana6CRUDTarea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VistaModificar : ContentPage
    {
        private const string Url = "http://192.168.0.108/Deber5POSTMAN/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Semana6CRUDTarea.Datos> _post;

        public VistaModificar()
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

        private void btnModificar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdCliente.Text))
                    DisplayAlert("Alerta", "No ha seleccionado ningun cliente.", "Ok");
                else
                {
                    WebClient cliente = new WebClient();

                    string parametros = "";
                    Datos data = new Datos();
                    data.idcliente = int.Parse(txtIdCliente.Text);

                    if (!string.IsNullOrEmpty(txtNombreCliente.Text))
                        data.nombrecliente = txtNombreCliente.Text;

                    if (!string.IsNullOrEmpty(txtDireccionCliente.Text))
                        data.direccioncliente = txtDireccionCliente.Text;

                    if (!string.IsNullOrEmpty(txtCelularCliente.Text))
                        data.celularcliente = txtCelularCliente.Text;

                    if (!string.IsNullOrEmpty(txtCorreoCliente.Text))
                        data.correocliente = txtCorreoCliente.Text;

                    if (!string.IsNullOrEmpty(txtEstadoCliente.Text))
                        data.estadocliente = int.Parse(txtEstadoCliente.Text);

                    var content = JsonConvert.SerializeObject(data);

                    parametros += "?idcliente=" + txtIdCliente.Text;

                    if (!string.IsNullOrEmpty(txtNombreCliente.Text))
                        parametros += "&nombrecliente=" + txtNombreCliente.Text;

                    if (!string.IsNullOrEmpty(txtDireccionCliente.Text))
                        parametros += "&direccioncliente=" + txtDireccionCliente.Text;

                    if (!string.IsNullOrEmpty(txtCelularCliente.Text))
                        parametros += "&celularcliente=" + txtCelularCliente.Text;

                    if (!string.IsNullOrEmpty(txtCorreoCliente.Text))
                        parametros += "&correocliente=" + txtCorreoCliente.Text;

                    if (!string.IsNullOrEmpty(txtEstadoCliente.Text))
                        parametros += "&estadocliente=" + txtEstadoCliente.Text;

                    var urlCompleta = new Uri(Url + parametros);

                    cliente.UploadString(urlCompleta, "PUT", content);

                    DisplayAlert("Alerta", "Registro editado correctamente.", "Ok");

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