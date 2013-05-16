using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteParticipante
{
    /// <summary>
    /// Entidade Participante
    /// </summary>
    public class Participante
    {
        /// <summary>
        /// ID do participante
        /// </summary>
        public virtual Guid Id { get; set; }
        
        /// <summary>
        /// Nome do participante
        /// </summary>
        public virtual string Nome { get; set; }

        /// <summary>
        /// CPF do participante
        /// </summary>
        public virtual string CPF { get; set; }
    }
}
