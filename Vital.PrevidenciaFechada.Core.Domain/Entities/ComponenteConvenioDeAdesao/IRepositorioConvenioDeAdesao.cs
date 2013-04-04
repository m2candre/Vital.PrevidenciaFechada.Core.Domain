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
        /// Último número de proposta
        /// </summary>
        /// <param name="PlanoId">PlanoId</param>
        /// <param name="PessoaJuridicaId">PessoaJuridicaId</param>
        /// <returns>int</returns>
        int UltimoNumeroDeProposta(Guid PlanoId, Guid PessoaJuridicaId);

		/// <summary>
		/// Obter convênio por plano e pessoa jurídica do contexto do usuário
		/// </summary>
		/// <param name="PlanoId"></param>
		/// <param name="PessoaJuridicaId"></param>
		/// <returns></returns>
		ConvenioDeAdesao ObterPor(Guid PlanoId, Guid PessoaJuridicaId);
    }
}