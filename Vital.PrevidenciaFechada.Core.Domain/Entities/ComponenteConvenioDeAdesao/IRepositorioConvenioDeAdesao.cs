using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteConvenioDeAdesao
{
    /// <summary>
    /// Repositório de Convênio de Adesão
    /// </summary>
    public interface IRepositorioConvenioDeAdesao : IRepositorio<ConvenioDeAdesao>
    {
        /// <summary>
        /// Obtém o último número de proposta para um Convênio de Adesão
        /// </summary>
        /// <param name="idDoConvenioDeAdesao">ID do convênio de adesão</param>
        /// <returns></returns>
        int UltimoNumeroDeProposta(Guid idDoConvenioDeAdesao);
    }
}