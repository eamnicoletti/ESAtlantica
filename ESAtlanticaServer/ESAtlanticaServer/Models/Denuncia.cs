using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESAtlantica.Model
{
    public class Denuncia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Denuncia_Id { get; set; }
        public DateTime Data_denuncia { get; set; }
        public string Nome_acusado { get; set; }
        public string Tipo_fato { get; set; }
        public string Endereco_fato { get; set; }
        public string Cidade_fato { get; set; }
        public string Historico_fato { get; set; }
        public byte[] Foto_fato { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Situacao { get; set; }
        public string Numero_formulario { get; set; }

        public long DispositivoId { get; set; }
    }
}