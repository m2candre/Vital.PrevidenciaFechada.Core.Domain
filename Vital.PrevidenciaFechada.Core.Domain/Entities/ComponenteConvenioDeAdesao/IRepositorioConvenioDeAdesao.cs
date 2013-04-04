using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteConvenioDeAdesao
{
    /// <summary>
    /// Repositório de Convênio de Adesão
    /// </summary>
    public interface IRepositorioConvenioDeAdesao
    {
        /// <summary>
        /// Último número de proposta
        /// </summary>
        /// <param name="PlanoId">PlanoId</param>
        /// <param name="PessoaJuridicaId">PessoaJuridicaId</param>
        /// <returns>int</returns>
        int UltimoNumeroDeProposta(Guid PlanoId, Guid PessoaJuridicaId);
    }
}
