using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
    /// <summary>
    /// Valor de campo da proposta
    /// </summary>
    public class ValorDeCampo
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Campo do modelo de proposta
        /// </summary>
        public virtual CampoDeProposta Campo { get; set; }

        /// <summary>
        /// Valor do campo
        /// </summary>
        public virtual string Valor { get; set; }
    }
}
