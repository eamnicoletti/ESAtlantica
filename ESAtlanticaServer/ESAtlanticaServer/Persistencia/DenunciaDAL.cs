using ESAtlantica.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESAtlanticaServer.Persistencia
{
    public class DenunciaDAL
    {
        private ESAtlanticaContexts contexto = new ESAtlanticaContexts();

        public IEnumerable<Denuncia> GetAll()
        {
            return contexto.Denuncias;
        }

        public Denuncia Insert(Denuncia denuncia)
        {
            denuncia.Numero_formulario = IncrementarNumFormulario();
            denuncia.Situacao = true;
            contexto.Denuncias.Add(denuncia);
            contexto.SaveChanges();

            return denuncia;
        }

        public string IncrementarNumFormulario()
        {
            Denuncia ultimaDenuncia = contexto.Denuncias.ToArray().LastOrDefault();
            long num_form_int;
            try
            {
                string num_formulario = ultimaDenuncia.Numero_formulario;
                num_formulario = num_formulario.Substring(4, 4);
                num_form_int = Convert.ToInt32(num_formulario);
            }
            catch
            {
                num_form_int = 0000;
            }            

            if (num_form_int == 9999) num_form_int = 0000;

            string num_form_increment = (num_form_int + 1).ToString("0000");

            return DateTime.Now.Year + num_form_increment;
        }
    }
}