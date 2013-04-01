using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
    /// <summary>
    /// Value Object de proposta para transitar dados de validação
    /// </summary>
    public class PropostaVO
    {
        /// <summary>
        /// Nome do participante
        /// </summary>
        public virtual string NomeDoParticipante { get; private set; }

        /// <summary>
        /// Informar o nome do Participante
        /// </summary>
        /// <param name="nome">nome do participante</param>
        /// <returns>PropostaVO</returns>
        public virtual PropostaVO InformarNome(string nome)
        {
            return new PropostaVO { NomeDoParticipante = nome };
        }
    }
}
