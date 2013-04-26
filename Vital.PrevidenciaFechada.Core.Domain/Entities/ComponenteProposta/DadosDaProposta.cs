using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
	/// <summary>
	/// Armazena os dados dinâmicos da proposta: Campo/Valor
	/// </summary>
	[Serializable]
	public class DadosDaProposta
	{
		/// <summary>
		/// Nome do campo da proposta
		/// </summary>
		public virtual string Nome { get; set; }
		
		/// <summary>
		/// Valor do campo
		/// </summary>
		public virtual string Valor { get; set; }
	}
}