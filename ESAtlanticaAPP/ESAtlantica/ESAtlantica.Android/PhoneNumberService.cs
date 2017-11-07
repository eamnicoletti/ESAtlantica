using Android.App;
using Android.Content;
using Android.Telephony;
using ESAtlantica.Droid;
using ESAtlantica.Infraestructure;

[assembly: Xamarin.Forms.Dependency(typeof(PhoneNumberService))]
namespace ESAtlantica.Droid
{
    public class PhoneNumberService : IPhoneNumberService
    {
        public string GetMyPhoneNumber()
        {
            TelephonyManager mgr = Application.Context.GetSystemService(Context.TelephonyService) as TelephonyManager;
            return mgr.Line1Number;
        }
    }
}