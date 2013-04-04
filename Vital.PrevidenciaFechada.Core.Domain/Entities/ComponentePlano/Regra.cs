using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    /// <summary>
    /// Regra do Plano
    /// </summary>
    public abstract class Regra
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Id do campo da proposta que a regra está validando
        /// </summary>
        public virtual Guid CampoId { get; set; }

        /// <summary>
        /// Valida a Regra
        /// </summary>
        public abstract PropostaVO Validar(PropostaVO proposta);
    }
}
