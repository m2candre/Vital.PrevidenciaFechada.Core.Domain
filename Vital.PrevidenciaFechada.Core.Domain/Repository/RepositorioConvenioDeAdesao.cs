using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteConvenioDeAdesao;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    /// <summary>
    /// Repositório de Convênio de Adesão
    /// </summary>
    public class RepositorioConvenioDeAdesao : Repositorio<ConvenioDeAdesao>, IRepositorioConvenioDeAdesao
    {
		/// <summary>
		/// Obtém o último número de proposta para um Convênio de Adesão
		/// </summary>
		/// <param name="idDoConvenioDeAdesao">ID do convênio de adesão</param>
		/// <returns></returns>
		public int UltimoNumeroDeProposta(Guid idDoConvenioDeAdesao)
        {
			ConvenioDeAdesao convenio = ObterPorId(idDoConvenioDeAdesao);
            int ultimoNumeroDaProposta = convenio.Propostas.Max(x=> x.Numero);

            return ultimoNumeroDaProposta;
        }
    }
}