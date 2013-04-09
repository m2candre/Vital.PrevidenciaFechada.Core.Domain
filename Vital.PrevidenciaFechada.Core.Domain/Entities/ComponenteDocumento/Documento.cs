using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteDocumento
{
    /// <summary>
    /// Documento 
    /// </summary>
    public class Documento : IAggregateRoot<Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Token deste documento em um sistema de busca e indexação de arquivos
        /// </summary>
        public virtual Guid Token { get; set; }

        /// <summary>
        /// Nome do documento
        /// </summary>
        public virtual string Nome { get; set; }
    }
}
