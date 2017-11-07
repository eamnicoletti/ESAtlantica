using System;
using System.Threading.Tasks;
using ESAtlantica.Model;
using ESAtlantica.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.Media;

namespace ESAtlantica.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Denunciar_Page : ContentPage
    {
        private DenunciaViewModel denunciaViewModel;

        public Denunciar_Page(Denuncia denuncia)
        {
            InitializeComponent();

            //var tabbedPage = this.ParentTabbedPage() as TabbedPage;

            denunciaViewModel = new DenunciaViewModel(denuncia, this);
            BindingContext = denunciaViewModel;
            dpkData_denuncia.Date = DateTime.Now;             
        }
        /* private async void OnTappedDenunciar(object sender, EventArgs e)
         {

             var imageSender = (Image)sender;

             var b = imageSender.Bounds;
             b.X = b.X + 5;
             b.Y = b.Y + 5;
             await imageSender.LayoutTo(b, 100);

             b.X = b.X - 5;
             b.Y = b.Y - 5;
             await imageSender.LayoutTo(b, 100);

         }

         private async void OnTappedSalvarDenuncia(object sender, EventArgs e)
         {

         }*/
    }
}

//logoEsAtlanticaTabbar.png | NavigationPage.SetHasNavigationBar(this, false);