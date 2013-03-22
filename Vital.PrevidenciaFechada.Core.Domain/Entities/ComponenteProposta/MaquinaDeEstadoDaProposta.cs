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
	/// Estado da proposta
	/// </summary>
	public class MaquinaDeEstadoDaProposta
	{
		private Proposta _proposta;
		private string _estadoInicial;
		private StateMachine<string, string> _maquina;

		/// <summary>
		/// Máquina de estados da proposta
		/// </summary>
		public virtual StateMachine<string, string> Maquina
		{
			get 
			{
				if (_maquina == null)
					_maquina = new StateMachine<string, string>(_estadoInicial);
				return _maquina; 
			}
			set { _maquina = value; }
		}

		/// <summary>
		/// Construtor
		/// </summary>
		public MaquinaDeEstadoDaProposta(string estadoInicial, Proposta proposta)
		{
			_proposta = proposta;
			_estadoInicial = estadoInicial;
			_maquina = new StateMachine<string, string>(_estadoInicial);

			ConfigurarMaquinaDeEstados();
		}

		/// <summary>
		/// Altera o estado da proposta 
		/// </summary>
		/// <param name="acao">acao</param>
		public virtual void AlterarPelaAcao(string acao)
		{
			Maquina.Fire(acao);

			_proposta.AlterarEstado(Maquina.State);
		}

		/// <summary>
		/// Configura a máquina de estados da proposta
		/// </summary>
		private void ConfigurarMaquinaDeEstados()
		{
			Maquina.Configure("Iniciada")
				.Permit("Autorizar", "Autorizada")
				.Permit("Recusar", "NaoAutorizada");
		}
	}
}
