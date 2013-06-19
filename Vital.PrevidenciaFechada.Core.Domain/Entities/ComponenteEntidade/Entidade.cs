using System;
using System.Collections.Generic;
using System.Linq;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade
{
    /// <summary>
    /// Entidade Fechada de Previdência Complementar , Possui de (1..N) Plano(s) vinculado(s)
    /// </summary>
    public class Entidade : IAggregateRoot<Guid>
    {
        /// <summary>
        /// Id da Entidade
        /// </summary>
        public virtual Guid Id { get; set; }
        
        /// <summary>
        /// Nome da Entidade
        /// </summary>
        public virtual string Nome { get; set; }
    }
}