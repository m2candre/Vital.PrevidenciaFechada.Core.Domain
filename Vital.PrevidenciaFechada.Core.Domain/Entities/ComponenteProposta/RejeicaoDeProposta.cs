using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
    public class RejeicaoDeProposta : IAggregateRoot<Guid>
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
        /// Tipo de rejeicao
        /// </summary>
        public virtual TipoRejeicao TipoRejeicao { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public virtual string Motivo { get; set; }


    }
}
