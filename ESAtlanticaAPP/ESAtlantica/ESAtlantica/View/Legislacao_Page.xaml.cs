using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESAtlantica.Dal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESAtlantica.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Legislacao_Page : ContentPage
    {
        
        public Legislacao_Page()
        {
            InitializeComponent();
        }

        private void PckLegislacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker == null)
            {
                return;
            }

            switch (picker.SelectedIndex)
            {
                case 0:
                    IsVisibleFalse();
                    stLtAPP.IsVisible = true;
                    break;
                case 1:
                    IsVisibleFalse();
                    stLtFauna.IsVisible = true;
                    break;
                case 2:
                    IsVisibleFalse();
                    stLtFlora.IsVisible = true;
                    break;
                case 3:
                    IsVisibleFalse();
                    stLtPoluicao.IsVisible = true;
                    break;
                case 4:
                    IsVisibleFalse();
                    stLtPatrimonio.IsVisible = true;
                    break;
                default:
                    IsVisibleFalse();
                    return;
            }
        }

        private void IsVisibleFalse()
        {
            stLtAPP.IsVisible = false;
            stLtFauna.IsVisible = false;
            stLtFlora.IsVisible = false;
            stLtPoluicao.IsVisible = false;
            stLtPatrimonio.IsVisible = false;
        }
    }
}