using ESAtlantica.Dal;
using ESAtlantica.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using Plugin.Media;
using ESAtlantica.Service;
using Plugin.Geolocator;
using System.Text;
using ESAtlantica.View;
using System.Threading.Tasks;

namespace ESAtlantica.ViewModel
{
    public class DenunciaViewModel : INotifyPropertyChanged
    {
        private long? _denuncia_Id;
        private DateTime _data_denuncia;
        private string _nome_acusado;
        private string _tipo_fato;
        private string _endereco_fato;
        private string _cidade_fato;
        private string _historico_fato;
        private byte[] _foto_fato = Encoding.UTF8.GetBytes("0");
        private double _latitude = 0;
        private double _longitude = 0;
        private bool _situacao = false;
        private string _numero_formulario = "Não Registrada";

        private long? _dispositivoId = 0;

        DenunciaDAL denunciaDAL;
        PhoneNumberServicePCL phoneNumberService;
        ConfiguracaoDispositivoREST confDispositivoREST;
        DenunciaREST denunciaREST;
        ConfiguracaoDAL configuracaoDAL;
        Denunciar_Page denunciar_page;

        //private TabbedPage _tabbedPage;

        public DenunciaViewModel(Denuncia denuncia, Denunciar_Page denunciar_page)
        {
            denunciaDAL = new DenunciaDAL();
            phoneNumberService = new PhoneNumberServicePCL();
            configuracaoDAL = new ConfiguracaoDAL();

            this.denunciar_page = denunciar_page;

            this._denuncia_Id = denuncia.Denuncia_Id;
            this._data_denuncia = denuncia.Data_denuncia;
            this._nome_acusado = denuncia.Nome_acusado;
            this._tipo_fato = denuncia.Tipo_fato;
            this._endereco_fato = denuncia.Endereco_fato;
            this._cidade_fato = denuncia.Cidade_fato;
            this._historico_fato = denuncia.Historico_fato;
            //this._tabbedPage = tabbedPage;
        }

        public long? Denuncia_Id
        {
            get { return _denuncia_Id; }
            set { _denuncia_Id = value; OnPropertyChanged(); }
        }

        public DateTime Data_denuncia
        {
            get { return _data_denuncia; }
            set { _data_denuncia = value; OnPropertyChanged(); }
        }

        public string Nome_acusado
        {
            get { return _nome_acusado; }
            set { _nome_acusado = value; OnPropertyChanged(); }
        }

        public string Tipo_fato
        {
            get { return _tipo_fato; }
            set { _tipo_fato = value; OnPropertyChanged(); }
        }

        public string Endereco_fato
        {
            get { return _endereco_fato; }
            set { _endereco_fato = value; OnPropertyChanged(); }
        }

        public string Cidade_fato
        {
            get { return _cidade_fato; }
            set { _cidade_fato = value; OnPropertyChanged(); }
        }

        public string Historico_fato
        {
            get { return _historico_fato; }
            set { _historico_fato = value; OnPropertyChanged(); }
        }

        public byte[] Foto_fato
        {
            get { return _foto_fato; }
            set { _foto_fato = value; OnPropertyChanged(); }
        }

        public ICommand RegistrarDenuncia
        {
            get
            {
                return new Command(async () =>
                {
                    await VerificarConfiguracaoDispositivoAsync();
                    denunciaREST = new DenunciaREST();

                    if (this._denuncia_Id != null) //Denuncia já persiste no aparelho
                    {
                        if (Valida())
                        {
                            denunciar_page.IsBusy = true;
                            try
                            {
                                Denuncia denuncia = await denunciaREST.AddDenunciaAsync(GetObjectFromView());

                                this._situacao = denuncia.Situacao;
                                this._numero_formulario = denuncia.Numero_formulario;

                                denunciaDAL.Update(GetObjectFromView());

                                await App.Current.MainPage.DisplayAlert("Denúncia", "Denúncia registrada com sucesso!", "OK");
                                await App.Current.MainPage.DisplayAlert("Denúncia", "Denúncia atualizada no aparelho!", "OK");
                                ZeraValores();
                            }
                            catch(Exception ex)
                            {
                                denunciaDAL.Update(GetObjectFromView());
                                await App.Current.MainPage.DisplayAlert("Denúncia", "Não foi possível registrar a denúncia. \n" +
                                    "Verifique sua conexão com a internet! \n"+ ex.Message, "OK");
                                await App.Current.MainPage.DisplayAlert("Denúncia", "Denúncia atualizada no aparelho!", "OK");
                            }
                            denunciar_page.IsBusy = false;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Atenção", "Todos os campos com * devem ser preenchidos!", "OK");
                        }
                    }
                    else //Nova denúncia
                    {
                        if (Valida())
                        {
                            denunciar_page.IsBusy = true;
                            try
                            {
                                Denuncia denuncia = await denunciaREST.AddDenunciaAsync(GetObjectFromView());
                                denunciaDAL.Update(denuncia);

                                await App.Current.MainPage.DisplayAlert("Denúncia", "Denúncia registrada com sucesso!", "OK");
                                await App.Current.MainPage.DisplayAlert("Denúncia", "Denúncia salva no aparelho!", "OK");
                                ZeraValores();
                            }
                            catch (Exception ex)
                            {
                                denunciaDAL.Add(GetObjectFromView());
                                await App.Current.MainPage.DisplayAlert("Denúncia", "Não foi possível registrar a denúncia. \n" +
                                    "Verifique sua conexão com a internet! \n" + ex.Message, "OK");
                                await App.Current.MainPage.DisplayAlert("Denúncia", "Denúncia salva no aparelho!", "OK");
                            }
                            denunciar_page.IsBusy = false;
                        }
                        else
                        {  
                            await App.Current.MainPage.DisplayAlert("Atenção", "Todos os campos com * devem ser preenchidos!", "OK");
                        }
                    }
                });
            }
        }

        public ICommand Gravar
        {
            get
            {
                var denunciaDAL = new DenunciaDAL();
                return new Command(() =>
                {                    
                    if (this._denuncia_Id != null) //Denúncia já persiste no aparelho
                    {
                        if (Valida()) 
                        {
                            denunciaDAL.Update(GetObjectFromView());
                            App.Current.MainPage.DisplayAlert("Denúncia", "Denúncia alterada com sucesso!", "OK");
                            ZeraValores();
                        }
                        else
                        {
                            App.Current.MainPage.DisplayAlert("Atenção", "Todos os campos com * devem ser preenchidos!", "OK");
                        }

                        //this._tabbedPage.CurrentPage = new Listar_Denuncias_Salvas_Page();
                    }
                    else //Nova denúncia
                    { 
                        if (Valida())
                        {
                            denunciaDAL.Add(GetObjectFromView());
                            App.Current.MainPage.DisplayAlert("Denúncia", "Denúncia salva com sucesso!", "OK");
                            ZeraValores();
                        }
                        else
                        {
                            App.Current.MainPage.DisplayAlert("Atenção", "Todos os campos com * devem ser preenchidos!", "OK");
                        }      

                       // this._tabbedPage.CurrentPage = new Listar_Denuncias_Salvas_Page();
                    }                    
                });
            }
        }
        
        public ICommand SelecionarFoto
        {
            get
            {
                return new Command(async () =>
                {
                    denunciar_page.IsBusy = true;
                    try
                    {
                        await CrossMedia.Current.Initialize();
                        if (!CrossMedia.Current.IsPickPhotoSupported)
                        {
                            await App.Current.MainPage.DisplayAlert("Álbum não suportado", "Não existe permissão para acessar o álbum de fotos", "OK");
                            return;
                        }
                        var file = await CrossMedia.Current.PickPhotoAsync();

                        if (file == null)
                            return;

                        var stream = file.GetStream();
                        file.Dispose();
                        var memoryStream = new MemoryStream();
                        stream.CopyTo(memoryStream);
                        this._foto_fato = memoryStream.ToArray();
                        await App.Current.MainPage.DisplayAlert("Imagem", "Imagem carregada com sucesso!", "OK");
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Erro :", "Não foi possível carregar a imagem: \n"+ex.Message, "OK");
                    }
                    denunciar_page.IsBusy = false;
                });
            }
        }

        public ICommand ObterCoordenadas
        {
            get
            {
                return new Command(async () =>
                {
                    denunciar_page.IsBusy = true;
                    try
                    {
                        var locator = CrossGeolocator.Current;
                        locator.DesiredAccuracy = 50;

                        var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                        if (position == null)
                            return;

                        await App.Current.MainPage.DisplayAlert("Localização Obtida: ", "Latitude: "+ position.Latitude +"\n"+ "Longitude: "+ position.Longitude, "OK");
                        //position.Timestamp + "\n"; OBS.: Obter hora
                        this._latitude = position.Latitude;
                        this._longitude = position.Longitude;

                    }
                    catch (Exception)
                    {
                        await App.Current.MainPage.DisplayAlert("Erro: ", "Tempo de espera ultrapassado \n Verifique se o GPS está ligado!", "OK");
                    }
                    denunciar_page.IsBusy = false;
                });
            }
        }

        private Denuncia GetObjectFromView()
        {
            return new Denuncia()
            {
                Denuncia_Id = this.Denuncia_Id,
                Data_denuncia = this.Data_denuncia,
                Nome_acusado = this.Nome_acusado,
                Tipo_fato = this.Tipo_fato,
                Endereco_fato = this.Endereco_fato,
                Cidade_fato = this.Cidade_fato,
                Historico_fato = this.Historico_fato,
                Foto_fato = this.Foto_fato,
                Latitude = this._latitude,
                Longitude = this._longitude,
                Situacao = this._situacao,
                Numero_formulario = this._numero_formulario,
                DispositivoId = this._dispositivoId                
            };
        }

        private bool Valida()
        {
            if(string.IsNullOrEmpty(this.Tipo_fato) || string.IsNullOrEmpty(this.Endereco_fato) || string.IsNullOrEmpty(this.Cidade_fato) || string.IsNullOrEmpty(Historico_fato))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task VerificarConfiguracaoDispositivoAsync()
        {
            var cd = configuracaoDAL.GetConfiguracao();
            if (cd == null)
            {
                confDispositivoREST = new ConfiguracaoDispositivoREST();
                denunciar_page.IsBusy = true;
                try
                {
                    var pn = phoneNumberService.GetNumTelefone();
                    var id = await confDispositivoREST.GetDispositivoIdAsync(pn);
                    var cdNew = new ConfiguracaoDispositivo()
                    {
                        Num_tel = pn,
                        ConfiguracaoDispositivoId = id
                    };
                    configuracaoDAL.Add(cdNew);
                    this._dispositivoId = cdNew.ConfiguracaoDispositivoId;
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Erro", "Não foi possível obter ID para o aparelho. \n" +
                        ex.Message, "OK");
                }
                denunciar_page.IsBusy = false;
            }
            else
            {
                this._dispositivoId = cd.ConfiguracaoDispositivoId;
            }
            return;
        }

        private void ZeraValores()
        {
            this.Data_denuncia = DateTime.Now;
            this.Nome_acusado = "";
            this.Tipo_fato = "";
            this.Endereco_fato = "";
            this.Cidade_fato = "";
            this.Historico_fato = "";
            this.Foto_fato = Encoding.UTF8.GetBytes("0");
            this._latitude = 0;
            this._longitude = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
