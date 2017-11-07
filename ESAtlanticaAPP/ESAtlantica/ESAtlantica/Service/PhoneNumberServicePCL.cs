using Xamarin.Forms;
using ESAtlantica.Infraestructure;

namespace ESAtlantica.Service
{
    public class PhoneNumberServicePCL
    {
        public string GetNumTelefone()
        {
            return DependencyService.Get<IPhoneNumberService>().GetMyPhoneNumber();
        }
    }
}
