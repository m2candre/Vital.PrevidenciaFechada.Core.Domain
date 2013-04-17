using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    public class ValoresDoCampo
    {
        /// <summary>
        /// Identificador dos valores do campo
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Exibe o valor
        /// </summary>
        public virtual string Valor { get; set; }

        /// <summary>
        /// Exibe o rotulo
        /// </summary>
        public virtual string Rotulo { get; set; }

        /// <summary>
        /// Exibe a ordem dos dados na lista
        /// </summary>
        public virtual int Ordem { get; set; }
    }
}
