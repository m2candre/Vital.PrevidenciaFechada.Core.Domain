using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    public class ClasseDeTamanhoDoCampo : IAggregateRoot<Guid>
    {
        public ClasseDeTamanhoDoCampo()
        {

        }

        public ClasseDeTamanhoDoCampo(int tamanho)
        {
            this.Tamanho = tamanho;
        }

        /// <summary>
        /// Identificador do tamanho do campo
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Classe CSS para o campo
        /// </summary>
        public virtual int Tamanho { get; set; }

        /// <summary>
        /// Obtém classe CSS
        /// </summary>
        /// <returns></returns>
        public virtual string ObterClasse()
        {
            return "field" + Tamanho;
        }
    }
}
