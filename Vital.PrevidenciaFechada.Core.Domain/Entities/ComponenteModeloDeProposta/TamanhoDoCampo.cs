using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    public class TamanhoDoCampo
    {
        /// <summary>
        /// Identificador do tamanho do campo
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Classe CSS para o campo
        /// </summary>
        public virtual string Classe { get; set; }
    }
}
