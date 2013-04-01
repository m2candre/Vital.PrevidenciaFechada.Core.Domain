using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
	public interface IRepositorioProposta : IRepositorio<Proposta>
	{
		int ObterUltimoNumeroDaProposta();
	}
}