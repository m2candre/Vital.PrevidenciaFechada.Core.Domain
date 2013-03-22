using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
	public interface IRepositorioProposta : IRepositorio<Proposta>
	{
		IList<Proposta> ObterPropostasPorCriterio(Expression<Func<Proposta, bool>> criterios);
	}
}