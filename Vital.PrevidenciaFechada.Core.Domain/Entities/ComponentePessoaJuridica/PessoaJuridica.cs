using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Tipos de documento
        /// </summary>
        public virtual IList<TipoDeDocumento> TiposDeDocumento { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public PessoaJuridica()
        {
            TiposDeDocumento = new List<TipoDeDocumento>();
        }

        /// <summary>
        /// Adiciona um novo tipo de documento a pessoa jurídica
        /// </summary>
        /// <param name="tipoDeDocumento">Tipo de documento</param>
        public virtual void AdicionarTipoDeDocumento(TipoDeDocumento tipoDeDocumento)
        {
            TiposDeDocumento.Add(tipoDeDocumento);
        }
    }
}
