using System;
using System.Collections.Generic;
using Vital.InfraStructure.DSL.DesignByContract;
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
					_estado = "Iniciada";

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
					_maquinaDeEstadoDaProposta = new MaquinaDeEstadoDaProposta("Iniciada", this);

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