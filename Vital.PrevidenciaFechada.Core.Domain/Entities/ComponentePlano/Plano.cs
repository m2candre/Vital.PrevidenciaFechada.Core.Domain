using System;
using System.Collections.Generic;
using System.Linq;
using Vital.InfraStructure.AOP;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;

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
        /// Termos de Adesão com Patrocinadores e/ou Instituidores
        /// </summary>
        public virtual IList<TermoDeAdesao> TermosDeAdesao { get; set; }

        /// <summary>
        /// Tipos de documento
        /// </summary>
        public virtual IList<TipoDeDocumento> TiposDeDocumento { get; set; }

        /// <summary>
        /// Regulamento do Plano
        /// </summary>
        public virtual Regulamento Regulamento { get; set; }

		/// <summary>
		/// Construtor - inicialização dos agregados de planoB
        /// Cria por padrão um modelo de proposta vazio em modo Rascunho
		/// </summary>
        public Plano()
        {
            ModeloDePropostaEmRascunho = new ModeloDeProposta();
            TermosDeAdesao = new List<TermoDeAdesao>();
            TiposDeDocumento = new List<TipoDeDocumento>();
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

        /// <summary>
        /// Adiciona um novo tipo de documento a pessoa jurídica
        /// </summary>
        /// <param name="tipoDeDocumento">Tipo de documento</param>
        public virtual void AdicionarTipoDeDocumento(TipoDeDocumento tipoDeDocumento)
        {
            if (TiposDeDocumento == null)
                TiposDeDocumento = new List<TipoDeDocumento>();

            IAssertion naoExisteDocumento = Assertion.IsFalse(ExisteTipoDeDocumentoComONome(tipoDeDocumento.Descricao), "Nome do tipo de documento já existe!");

            TiposDeDocumento.Add(tipoDeDocumento);
        }

        public virtual bool ExisteTipoDeDocumentoComONome(string nomeDoTipoDeDocumento)
        {
            return TiposDeDocumento.Any(x => x.Descricao == nomeDoTipoDeDocumento);
        }
    }
}