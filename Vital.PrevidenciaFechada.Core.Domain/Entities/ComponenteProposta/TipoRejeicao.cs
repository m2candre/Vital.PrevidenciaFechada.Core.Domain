using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
    /// <summary>
    /// Tipo de Rejeição
    /// </summary>
    public class TipoRejeicao : IAggregateRoot<Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

		/// <summary>
		/// ID do Convênio de Adesão
		/// </summary>
		public virtual Guid IdDoConvenioDeAdesao { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public virtual string Descricao { get; set; }
    }
}
