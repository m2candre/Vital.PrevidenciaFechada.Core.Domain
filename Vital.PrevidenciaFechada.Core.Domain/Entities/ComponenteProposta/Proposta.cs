using System;
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

		/// <summary>
		/// Id
		/// </summary>
		public virtual Guid Id { get; set; }

		/// <summary>
		/// Nome do prospect
		/// </summary>
		public virtual string Nome { get; set; }

		/// <summary>
		/// CPF
		/// </summary>
		public virtual string CPF { get; set; }

		/// <summary>
		/// Nome do Instituidor/Patrocinador
		/// </summary>
		public virtual string InstituidorPatrocinador { get; set; }

		/// <summary>
		/// Número da Proposta
		/// </summary>
		public virtual string Numero { get; set; }

		/// <summary>
		/// Modelo HTML da proposta preenchida
		/// </summary>
		public virtual string ModeloHTML { get; set; }

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
					_maquinaDeEstadoDaProposta = new MaquinaDeEstadoDaProposta(Estado, this);

				return _maquinaDeEstadoDaProposta;
			}
			set { _maquinaDeEstadoDaProposta = value; }
		}

		/// <summary>
		/// Plano
		/// </summary>
		public virtual Plano Plano { get; set; }

		/// <summary>
		/// Coloca a proposta em modo Rascunho
		/// </summary>
		public virtual void SalvarRascunho()
		{
			#region Pré-condições

			IAssertion maquinaDeEstadoEstaInicializada = Assertion.NotNull(MaquinaDeEstado, "A máquina de estados da proposta deve ser inicializada");

			#endregion

			maquinaDeEstadoEstaInicializada.Validate();

			MaquinaDeEstado.AlterarEstadoPelaAcao("SalvarRascunho");

			#region Pós-condições

			IAssertion oEstadoDaPropostaFoiAlterado = Assertion.Equals(Estado, "Pendente", "O estado na proposta não foi alterado para 'Pendente'");

			#endregion

			oEstadoDaPropostaFoiAlterado.Validate();
		}

		/// <summary>
		/// Altera o estado da proposta para Registrada
		/// </summary>
		public virtual void Registrar()
		{
			#region Pré-condições

			IAssertion maquinaDeEstadoEstaInicializada = Assertion.NotNull(MaquinaDeEstado, "A máquina de estados da proposta deve ser inicializada");

			#endregion

			maquinaDeEstadoEstaInicializada.Validate();

			MaquinaDeEstado.AlterarEstadoPelaAcao("Registrar");

			#region Pós-condições

			IAssertion oEstadoDaPropostaFoiAlterado = Assertion.Equals(Estado, "Registrada", "O estado na proposta não foi alterado para 'Registrada'");

			#endregion

			oEstadoDaPropostaFoiAlterado.Validate();
		}

		/// <summary>
		/// Autoriza a proposta
		/// </summary>
		public virtual void Autorizar()
		{
			#region Pré-condições

			IAssertion maquinaDeEstadoEstaInicializada = Assertion.NotNull(MaquinaDeEstado, "A máquina de estados da proposta deve ser inicializada");

			#endregion

			maquinaDeEstadoEstaInicializada.Validate();

			MaquinaDeEstado.AlterarEstadoPelaAcao("Autorizar");

			#region Pós-condições

			IAssertion oEstadoDaPropostaFoiAlterado = Assertion.Equals(Estado, "Autorizada", "O estado na proposta não foi alterado para 'Autorizada'");

			#endregion

			oEstadoDaPropostaFoiAlterado.Validate();
		}

		/// <summary>
		/// Recusa a proposta
		/// </summary>
		public virtual void Recusar()
		{
			#region Pré-condições

			IAssertion maquinaDeEstadoEstaInicializada = Assertion.NotNull(MaquinaDeEstado, "A máquina de estados da proposta deve ser inicializada");

			#endregion

			maquinaDeEstadoEstaInicializada.Validate();

			MaquinaDeEstado.AlterarEstadoPelaAcao("Recusar");

			#region Pós-condições

			IAssertion oEstadoDaPropostaFoiAlterado = Assertion.Equals(Estado, "NaoAutorizada", "O estado na proposta não foi alterado para 'NaoAutorizada'");

			#endregion

			oEstadoDaPropostaFoiAlterado.Validate();
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