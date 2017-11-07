using System;
using ESAtlantica.Dal;
using ESAtlantica.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESAtlantica.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listar_Denuncias_Salvas_Page : ContentPage
    {
        private DenunciaDAL denunciaDAL = new DenunciaDAL();

        public Listar_Denuncias_Salvas_Page()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lvDenuncias.ItemsSource = denunciaDAL.GetAll();
        }

        public async void OnAlterarClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var denuncia = mi.CommandParameter as Denuncia;
            //var tabbedPage = this.Parent as TabbedPage;
            //tabbedPage.CurrentPage = new Denunciar_Page(denuncia);
            await Navigation.PushAsync(new Denunciar_Page(denuncia));
        }

        public async void OnRemoverClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var denuncia = mi.CommandParameter as Denuncia;
            var opcao = await DisplayAlert("Confirmação de exclusão",
                "Confirma excluir a Denúncia " + denuncia.Denuncia_Id + "?", "Sim", "Não");
            if (opcao)
            {
                denunciaDAL.DeleteById((long)denuncia.Denuncia_Id);
                this.lvDenuncias.ItemsSource = denunciaDAL.GetAll();
            }
        }
    }
}