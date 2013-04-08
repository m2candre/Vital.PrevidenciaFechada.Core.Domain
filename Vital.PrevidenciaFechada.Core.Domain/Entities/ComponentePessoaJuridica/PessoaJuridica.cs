﻿using System;
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

		/// <summary>
		/// Convênios de Adesão com Patrocinadores e/ou Instituidores
		/// </summary>
		public virtual IList<ConvenioDeAdesao> ConveniosDeAdesao { get; set; }

		/// <summary>
		/// Construtor inicializando os Convênios de Adesão
		/// </summary>
		public PessoaJuridica()
		{
			ConveniosDeAdesao = new List<ConvenioDeAdesao>();
		}
    }
}
