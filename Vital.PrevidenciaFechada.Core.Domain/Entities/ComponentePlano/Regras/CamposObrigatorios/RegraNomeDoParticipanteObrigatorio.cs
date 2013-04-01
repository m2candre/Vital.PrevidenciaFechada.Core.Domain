using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras
{
    /// <summary>
    /// Valida se o nome do participante foi informado
    /// </summary>
    public class RegraNomeDoParticipanteObrigatorio : IRegra
    {
        /// <summary>
        /// Validar se o nome do participante foi informado
        /// </summary>
        /// <param name="proposta">dados da proposta</param>
        /// <returns>PropostaVO</returns>
        public virtual PropostaVO Validar(PropostaVO proposta)
        {
            if (string.IsNullOrWhiteSpace(proposta.NomeDoParticipante))
                proposta = proposta.InformarCritica("Nome do participante é obrigatório");

            return proposta;
        }
    }
}
