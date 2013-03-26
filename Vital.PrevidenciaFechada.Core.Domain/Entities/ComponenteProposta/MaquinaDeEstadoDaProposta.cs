using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.DesignByContract;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
	/// <summary>
	/// Máquina de estado da proposta
	/// </summary>
	public class MaquinaDeEstadoDaProposta
	{
		private Proposta _proposta;
		private string _estadoInicial;
		private StateMachine<string, string> _maquina;

		/// <summary>
		/// Obtem o estado atual da máquina (State)
		/// </summary>
		public string EstadoAtual
		{
			get { return _maquina.State; }
		}

		/// <summary>
		/// Construtor
		/// </summary>
		public MaquinaDeEstadoDaProposta(string estadoInicial, Proposta proposta)
		{
			#region Pré-condições

			IAssertion oEstadoInicialFoiInformado = Assertion.IsFalse(string.IsNullOrWhiteSpace(estadoInicial), "O estado inicial não foi informado");
			IAssertion aPropostaForInformada = Assertion.NotNull(proposta, "A proposta não foi informada");
			
			#endregion

			oEstadoInicialFoiInformado.and(aPropostaForInformada).Validate();

			_proposta = proposta;
			_estadoInicial = estadoInicial;
			_maquina = new StateMachine<string, string>(_estadoInicial);

			ConfigurarMaquinaDeEstados();

			#region Pós-condições

			IAssertion oEstadoInicialDaMaquinaFoiDefinido = Assertion.Equals(_maquina.State, "Iniciada", "O estado inicial da máquina não foi definido");

			#endregion

			oEstadoInicialDaMaquinaFoiDefinido.Validate();
		}

		/// <summary>
		/// Altera o estado da proposta de acordo com uma ação
		/// </summary>
		/// <param name="acao">acao</param>
		public virtual void AlterarEstadoPelaAcao(string acao)
		{
			#region Pré-condições

			IAssertion aAcaoFoiInformada = Assertion.IsFalse(string.IsNullOrWhiteSpace(acao), "A ação não foi informada");

			#endregion

			aAcaoFoiInformada.Validate();

			_maquina.Fire(acao);

			_proposta.AlterarEstado(_maquina.State);

			#region Pós-condições

			IAssertion oEstadoDaPropostaFoiAlterado = Assertion.Equals(_proposta.Estado, _maquina.State, "O estado da proposta não foi definido de acordo com a máquina");

			#endregion

			oEstadoDaPropostaFoiAlterado.Validate();
		}

		/// <summary>
		/// Configura a máquina de estados da proposta
		/// </summary>
		private void ConfigurarMaquinaDeEstados()
		{
			#region Pré-condições

			IAssertion aMaquinaDeEstadoFoiInicializada = Assertion.NotNull(_maquina, "A máquina de estado não foi inicializada");

			#endregion

			aMaquinaDeEstadoFoiInicializada.Validate();

			_maquina.Configure("Iniciada")
				.Permit("Autorizar", "Autorizada")
				.Permit("Recusar", "NaoAutorizada");
		}
	}
}
