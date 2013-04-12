using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.Comun
{
    /// <summary>
    /// Erro
    /// </summary>
    public class Erro : IAggregateRoot<Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public virtual int Numero { get; set; }
    }
}
