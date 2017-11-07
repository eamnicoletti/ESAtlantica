using System;
using ESAtlantica.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESAtlantica.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pagina_Principal : TabbedPage
    {
        public Pagina_Principal()
        {
            InitializeComponent();

            this.BarBackgroundColor = Color.FromHex("#cfea59");
            this.BackgroundColor = Color.FromHex("#e5e5e5");

            var pag1 = new Denunciar_Page(new Denuncia());
            var pag2 = new Pesquisar_Page();
            var pag3 = new Legislacao_Page();
            var pag4 = new Listar_Denuncias_Salvas_Page();

            this.Children.Add(pag1);
            this.Children.Add(pag2);
            this.Children.Add(pag3);
            this.Children.Add(pag4);

            pag1.Icon = "denunciar.png";
            pag2.Icon = "pesquisar.png";
            pag3.Icon = "legislacao.png";
            pag4.Icon = "denuncias_salvas.png";

            if (Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.UWP)
            {
                pag1.Title = "Denunciar";
                pag2.Title = "Pesquisar";
                pag3.Title = "Legislação";
                pag4.Title = "Salvo";                
                Title = "ESAtlântica";                
            }


            /* var navigationPageDenunciar = new NavigationPage(new Denunciar_Page());
            navigationPageDenunciar.Icon = "button_denunciar.png";            

            //NavigationPage.SetTitleIcon(this, "icon.png");

            var navigationPagePesquisar = new NavigationPage(new Pesquisar_Page());
            navigationPagePesquisar.Icon = "pesquisar.png";

            var navigationPageLegislacao = new NavigationPage(new Legislacao_Page());
            navigationPageLegislacao.Icon = "legislacao.png";

            var navigationPageDenuncaias_Salvas = new NavigationPage(new Denuncias_Salvas_Page());
            navigationPageDenuncaias_Salvas.Icon = "denuncias_salvas.png";
            */

        }

        private void GenerateAction()
        {
            throw new NotImplementedException();
        }
    }
}