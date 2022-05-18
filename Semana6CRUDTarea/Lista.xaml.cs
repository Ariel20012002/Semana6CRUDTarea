using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace Semana6CRUDTarea
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista : ContentPage
    {
        private const string Url = "http://192.168.0.108/Deber5POSTMAN/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Semana6CRUDTarea.Datos> _post; 
        public Lista()
        {
            InitializeComponent();
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista());
        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<Semana6CRUDTarea.Datos> posts = JsonConvert.DeserializeObject<List<Semana6CRUDTarea.Datos>>(content);
                _post = new ObservableCollection<Semana6CRUDTarea.Datos>(posts);
                MyListView.ItemsSource = _post;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }

        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VistaBorrar());
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VistaModificar());
        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}