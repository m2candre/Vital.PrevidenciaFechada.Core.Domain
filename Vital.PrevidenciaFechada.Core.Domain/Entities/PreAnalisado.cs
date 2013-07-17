using System;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities
{
	/// <summary>
	/// Pré-Analisado
	/// </summary>
	public class PreAnalisado : IAggregateRoot<Guid>
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual Guid Id { get; set; }

		/// <summary>
		/// Id do Convênio
		/// </summary>
		public virtual Guid IdDoConvenioDeAdesao { get; set; }

		/// <summary>
		/// CPF
		/// </summary>
		public virtual string Cpf { get; set; }

		/// <summary>
		/// Nome
		/// </summary>
		public virtual string Nome { get; set; }

		/// <summary>
		/// Matrícula
		/// </summary>
		public virtual string Matricula { get; set; }

		/// <summary>
		/// Data de Registro
		/// </summary>
		public virtual DateTime? DataDeRegistro { get; set; }

		/// <summary>
		/// Observação
		/// </summary>
		public virtual string Observacao { get; set; }

		/// <summary>
		/// Tipo de Rejeição
		/// </summary>
		public virtual TipoRejeicao TipoDeRejeicao { get; set; }

		/// <summary>
		/// Construtor
		/// </summary>
		public PreAnalisado()
		{

		}
	}
}