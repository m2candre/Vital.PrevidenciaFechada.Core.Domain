using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica
{
    /// <summary>
    /// Tipo de documento
    /// </summary>
    public class TipoDeDocumento
    {
        /// <summary>
        /// Id do tipo de documento
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Id do arquivo persistido
        /// </summary>
        public virtual Guid IdDoArquivo { get; set; }
    }
}
