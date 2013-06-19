using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica
{
    /// <summary>
    /// Pessoa Jurídica
    /// </summary>
    public abstract class PessoaJuridica : IAggregateRoot<Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Razão Social
        /// </summary>
        public virtual string RazaoSocial { get; set; }
    }
}