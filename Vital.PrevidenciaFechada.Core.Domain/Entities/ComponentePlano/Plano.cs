using System;
using System.Collections.Generic;
using System.Linq;
using Vital.InfraStructure.AOP;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    /// <summary>
    /// Plano de Benefícios de uma Entidade
    /// </summary>
    public class Plano : IAggregateRoot<Guid>
    {
        /// <summary>
        /// Identificador do Plano
        /// </summary>
        public virtual Guid Id { get;  set; }

        /// <summary>
        /// Nome do Plano
        /// </summary>
        public virtual string Nome { get; set; }

        /// <summary>
        /// Modelo de proposta em HTML
        /// </summary>
        public virtual string ModeloDePropostaHTML { get; set; }

        /// <summary>
        /// Modelo de Proposta do Plano Atual (publicada)
        /// </summary>
		public virtual ModeloDeProposta ModeloDePropostaAtual { get; protected set; }

        /// <summary>
        /// Modelo de Proposta do Plano Atual (publicada)
        /// </summary>
        public virtual ModeloDeProposta ModeloDePropostaEmRascunho { get; protected set; }

		/// <summary>
		/// Construtor - inicialização dos agregados de planoB
        /// Cria por padrão um modelo de proposta vazio em modo Rascunho
		/// </summary>
        public Plano()
        {
            ModeloDePropostaEmRascunho = new ModeloDeProposta();
        }

        /// <summary>
        /// Publica o modelo de Proposta mais atual (rascunho atual)
        /// </summary>
		public virtual void PublicarModeloDeProposta()
		{
			//TODO: Pensar em algo como uma máquina de estado para usar aqui. Proposta vai ter mais do que rascunho e publicada como estado
            this.ModeloDePropostaEmRascunho.Publicar();
            this.ModeloDePropostaAtual = ModeloDePropostaEmRascunho;
            this.ModeloDePropostaEmRascunho = ModeloDePropostaAtual.CopiarParaRascunho();
		}		
    }
}