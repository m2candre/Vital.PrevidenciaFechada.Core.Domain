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
    public interface IRegra
    {
        /// <summary>
        /// Valida a Regra
        /// </summary>
        PropostaVO Validar(PropostaVO proposta);
    }
}
