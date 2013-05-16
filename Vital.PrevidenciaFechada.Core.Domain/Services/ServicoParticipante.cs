using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteParticipante;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para entidade Participante
    /// </summary>
    public class ServicoParticipante
    {
        public virtual List<string> ObterPropriedadesDoParticipante()
        {
            List<string> lista = new List<string>();
            foreach (var propriedade in typeof(Participante).GetProperties())
            {
                lista.Add(propriedade.Name);
            }
            return lista;
        }
    }
}
