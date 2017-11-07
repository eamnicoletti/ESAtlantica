using ESAtlantica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using ESAtlantica.Service;

namespace ESAtlantica.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pesquisar_Page : ContentPage
    {
        //private string endereco;
        private DenunciaREST denunciaREST;
        private List<Denuncia> listaDenuncia;
        private Map map;

        public Pesquisar_Page()
        {
            InitializeComponent();
            denunciaREST = new DenunciaREST();
            AtualizaDados();                      
        }

        async void AtualizaDados()
        {
            this.map = new Map(MapSpan.FromCenterAndRadius(
                new Position(-19.53908414, -40.90759277),
                Distance.FromMiles(85)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            IsBusy = true;
            try
            {
                this.listaDenuncia = await denunciaREST.GetDenunciasAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Não foi possível estabelecer conexão com a servidor. \n"+ex.Message, "OK");
            }
            IsBusy = false;
        }

        protected async override void OnAppearing()
        {            
            base.OnAppearing();
            AtualizaDados();
            if(listaDenuncia != null)
            {

                var geoCoder = new Geocoder();
                foreach (Denuncia denuncia in listaDenuncia)
                {
                    var latitudeteste = denuncia.Latitude;
                    if (denuncia.Latitude == 0)
                    {
                        var position = await geoCoder.GetPositionsForAddressAsync(denuncia.Endereco_fato + ", " + denuncia.Cidade_fato + ", Espírito Santo");
                        map.Pins.Add(
                        new Pin
                        {
                            Position = position.First(),
                            Label = denuncia.Tipo_fato,
                            Address = denuncia.Endereco_fato + " " + denuncia.Cidade_fato
                        });
                    }
                    else
                    {
                        map.Pins.Add(
                        new Pin
                        {
                            Type = PinType.Place,
                            Position = new Position(denuncia.Latitude, denuncia.Longitude),
                            Label = denuncia.Tipo_fato,
                            Address = denuncia.Endereco_fato + " " + denuncia.Cidade_fato
                        });
                    }
                }
                Content = map;
            }
        }
    }
}