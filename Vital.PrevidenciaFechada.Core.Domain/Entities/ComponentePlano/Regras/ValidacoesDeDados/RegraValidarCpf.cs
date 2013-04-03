using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.Extensions.Texto;
using Vital.InfraStructure.DSL.DesignByContract;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras.ValidacoesDeDados
{
    /// <summary>
    /// Valida um número de CPF
    /// </summary>
    public class RegraValidarCpf : IRegra
    {
        /// <summary>
        /// Valida um número de cpf
        /// </summary>
        /// <param name="proposta">Dados da proposta</param>
        /// <returns>PropostaVO</returns>
        public PropostaVO Validar(PropostaVO proposta)
        {
            if (!proposta.CpfDoParticipante.CpfValido())
                proposta = proposta.InformarCritica("Cpf está inválido", "Cpf");

            return proposta;
        }
    }
}
