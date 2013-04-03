using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras
{
    /// <summary>
    /// Valida se o cpf do participante foi informado
    /// </summary>
    public class RegraCpfObrigatorio : IRegra
    {
        /// <summary>
        /// Valida se o cpf do participante foi informado
        /// </summary>
        /// <param name="proposta">dados da proposta</param>
        /// <returns>PropostaVO</returns>
        public virtual PropostaVO Validar(PropostaVO proposta)
        {
            if (string.IsNullOrWhiteSpace(proposta.CpfDoParticipante))
                proposta = proposta.InformarCritica("O cpf do participante é obrigatório", "CPF");

            return proposta;
        }
    }
}
