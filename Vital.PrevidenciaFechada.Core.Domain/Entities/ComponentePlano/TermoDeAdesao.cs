using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    /// <summary>
    /// termo de adesão de um plano com um (Patrocinador/Instituidor)
    /// </summary>
    public class TermoDeAdesao
    {
        /// <summary>
        /// Id do contrato
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Pessoa jurídica ( Patrocinador/ Instituidor )
        /// </summary>
        public virtual PessoaJuridica PessoaJuridica { get; set; }
    }
}
