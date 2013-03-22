using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
	/// <summary>
	/// Proposta
	/// </summary>
	public class Proposta : IAggregateRoot<Guid>
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual Guid Id { get; set; }

		/// <summary>
		/// Nome do prospect
		/// </summary>
		public virtual string Nome { get; set; }

		/// <summary>
		/// Número da Proposta
		/// </summary>
		public virtual string Numero { get; set; }

		/// <summary>
		/// Data da proposta
		/// </summary>
		public virtual DateTime Data { get; set; }

		/// <summary>
		/// Nome do estado atual
		/// </summary>
		public virtual string Estado { get; set; }

		/// <summary>
		/// Objeto responsável por controlar a transição de estados da proposta
		/// </summary>
		public virtual MaquinaDeEstadoDaProposta MaquinaDeEstado { get; set; }

		/// <summary>
		/// Plano
		/// </summary>
		public virtual Plano Plano { get; set; }

		/// <summary>
		/// Construtor
		/// </summary>
		public Proposta()
		{
			if (MaquinaDeEstado == null) MaquinaDeEstado = new MaquinaDeEstadoDaProposta("Iniciada", this);
			if (string.IsNullOrWhiteSpace(Estado)) Estado = "Iniciada";
		}

		/// <summary>
		/// Autoriza a proposta
		/// </summary>
		public virtual void Autorizar()
		{
			IAssertion maquinaDeEstadoEstaInicializada = Assertion.NotNull(MaquinaDeEstado, "A máquina de estados da proposta deve ser inicializada");
			maquinaDeEstadoEstaInicializada.Validate();

			MaquinaDeEstado.AlterarPelaAcao("Autorizar");
		}

		/// <summary>
		/// Recusa a proposta
		/// </summary>
		public virtual void Recusar()
		{
			IAssertion maquinaDeEstadoEstaInicializada = Assertion.NotNull(MaquinaDeEstado, "A máquina de estados da proposta deve ser inicializada");
			maquinaDeEstadoEstaInicializada.Validate();

			MaquinaDeEstado.AlterarPelaAcao("Recusar");
		}

		/// <summary>
		/// Altera o estado da proposta
		/// </summary>
		/// <param name="estado">estado</param>
		public virtual void AlterarEstado(string estado)
		{
			Estado = estado;
		}
	}
}
