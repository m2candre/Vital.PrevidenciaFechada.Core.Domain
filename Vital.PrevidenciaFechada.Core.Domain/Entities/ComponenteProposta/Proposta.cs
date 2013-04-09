using System;
using System.Collections.Generic;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteDocumento;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
	/// <summary>
	/// Proposta
	/// </summary>
	public class Proposta : IAggregateRoot<Guid>
	{
		private MaquinaDeEstadoDaProposta _maquinaDeEstadoDaProposta;
		private string _estado;

        #region Propriedades

        /// <summary>
		/// Id
		/// </summary>
		public virtual Guid Id { get; set; }

        /// <summary>
        /// Tipo de Rejeição
        /// </summary>
        public virtual TipoRejeicao TipoRejeicao { get; set; }

        /// <summary>
        /// Documentos
        /// </summary>
        public virtual IList<Documento> Documentos { get; set; }

		/// <summary>
		/// Nome do prospect
		/// </summary>
		public virtual string Nome { get; set; }

		/// <summary>
		/// Número da Proposta
		/// </summary>
		public virtual int Numero { get; set; }

		/// <summary>
		/// Data da proposta
		/// </summary>
		public virtual DateTime Data { get; set; }

		/// <summary>
		/// Nome do estado atual
		/// </summary>
		public virtual string Estado
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_estado))
					_estado = "EmRascunho";

				return _estado;
			}
			set { _estado = value; }
		}

		/// <summary>
		/// Objeto responsável por controlar a transição de estados da proposta
		/// </summary>
		public virtual MaquinaDeEstadoDaProposta MaquinaDeEstado
		{
			get
			{
				if (_maquinaDeEstadoDaProposta == null)
					_maquinaDeEstadoDaProposta = new MaquinaDeEstadoDaProposta("EmRascunho", this);

				return _maquinaDeEstadoDaProposta;
			}
			set { _maquinaDeEstadoDaProposta = value; }
		}

        /// <summary>
        /// Modelo de Proposta
        /// </summary>
        public virtual ModeloDeProposta ModeloDeProposta { get; set; }

        /// <summary>
        /// Valores dos campos da proposta
        /// </summary>
        public virtual IList<ValorDeCampo> Valores { get; set; }

        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        public Proposta()
        {
            Valores = new List<ValorDeCampo>();
            Documentos = new List<Documento>();
        }

        /// <summary>
		/// Autoriza a proposta
		/// </summary>
		public virtual void Autorizar()
		{
            AlterarEstadoPelaAcao("Autorizar");
		}

        /// <summary>
        /// Rejeita a a proposta
        /// </summary>
        public virtual void Rejeitar()
        {
            AlterarEstadoPelaAcao("Rejeitar");
        }

		/// <summary>
		/// Recusa a proposta
		/// </summary>
		public virtual void Recusar()
		{
            AlterarEstadoPelaAcao("Recusar");
		}

        /// <summary>
        /// Adiciona um documento na proposta
        /// </summary>
        /// <param name="documento">documento</param>
        public virtual void AdicionarDocumento(Documento documento)
        {
            #region Pré-Condições

            IAssertion aListaDeDocumentoFoiInicializada = Assertion.NotNull(Documentos, "A lista de documentos não foi inicializada");
            IAssertion oDocumentoFoiInformado = Assertion.NotNull(documento, "O documento não foi informado");
            IAssertion oTokenDoDocumentoFoiInformado = Assertion.LambdaAssertion(() => documento.Token != Guid.Empty, "O Token do documento não foi informado");
            IAssertion oDocumentoExisteNaBaseDeDados = Assertion.LambdaAssertion(() => documento.Id != Guid.Empty, "Não é possível anexar um documento inexistente na base na proposta");
            IAssertion oDocumentoPossuiNome = Assertion.IsFalse(string.IsNullOrWhiteSpace(documento.Nome), "O Nome do documento não foi informado");

            #endregion

            aListaDeDocumentoFoiInicializada.and(oDocumentoFoiInformado).
                AND(oTokenDoDocumentoFoiInformado.and(oDocumentoExisteNaBaseDeDados).and(oDocumentoPossuiNome)).Validate();

            int quantidadeDeDocumentosAtual = Documentos.Count;

            Documentos.Add(documento);

            #region Pós-Condições

            IAssertion oDocumentoFoiAnexadoAPropostaComSucesso = Assertion.GreaterThan(Documentos.Count, quantidadeDeDocumentosAtual, "Não foi possível anexar o documento na proposta");

            #endregion

            oDocumentoFoiAnexadoAPropostaComSucesso.Validate();
        }

        /// <summary>
        /// Altera o Estado na máquina de estado da proposta pela ação
        /// </summary>
        /// <param name="acao">acao</param>
        private void AlterarEstadoPelaAcao(string acao)
        {
            #region Pré-condições

            IAssertion maquinaDeEstadoEstaInicializada = Assertion.NotNull(MaquinaDeEstado, "A máquina de estados da proposta deve ser inicializada");

            #endregion

            maquinaDeEstadoEstaInicializada.Validate();

            MaquinaDeEstado.AlterarEstadoPelaAcao(acao);
        }

		/// <summary>
		/// Altera o estado da proposta
		/// </summary>
		/// <param name="estado">estado</param>
		public virtual void AlterarEstado(string estado)
		{
			#region Pré-condições

			IAssertion oEstadoFoiInformado = Assertion.IsFalse(string.IsNullOrWhiteSpace(estado), "O estado não foi informado");

			#endregion

			oEstadoFoiInformado.Validate();

			Estado = estado;

			#region Pós-condições

			IAssertion oEstadoDaPropostaFoiAlterado = Assertion.Equals(Estado, estado, "O estado não foi alterado corretamente");

			#endregion

			oEstadoDaPropostaFoiAlterado.Validate();
		}
	}
}