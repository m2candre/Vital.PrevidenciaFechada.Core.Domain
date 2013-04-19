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

        /// <summary>
        /// Mensagem de Erro
        /// </summary>
        public virtual string Mensagem { get; set; }

        /// <summary>
        /// Stack trace
        /// </summary>
        public virtual string StackTrace { get; set; }

        /// <summary>
        /// Data do registro de log
        /// </summary>
        public virtual DateTime? DataDoRegistro { get; set; }
    }
}
