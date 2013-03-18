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
        /// Descrição do arquivo
        /// </summary>
        public virtual string Descricao { get; set; }
    }
}
