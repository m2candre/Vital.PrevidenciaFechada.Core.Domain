using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    public class TipoDoCampo
    {
        /// <summary>
        /// Identificador do tipo do campo
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Tipo do campo
        /// </summary>
        public virtual string Tipo { get; set; }
    }
}
