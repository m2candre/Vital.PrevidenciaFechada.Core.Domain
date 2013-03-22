using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Repository
{
	[TestFixture]
	public class RepositorioPropostaTests
	{
		[Test]
		public void obter_propostas_por_criterio_retorna_lista_corretamente()
		{
			Expression<Func<Proposta, bool>> criterio = p => p.Estado == "Iniciada";

			var session = MockRepository.GenerateMock<ISession>();

			var iqueryOver = MockRepository.GenerateMock<IQueryOver<Proposta, Proposta>>();

			var iqueryOverComLista = MockRepository.GenerateMock<IQueryOver<Proposta, Proposta>>();

			iqueryOverComLista.Expect(x=> x.List()).Return(new List<Proposta>());

			iqueryOver.Expect(x => x.Where(criterio)).Return(iqueryOverComLista);

			session.Expect(x => x.QueryOver<Proposta>()).Return(iqueryOver);

			RepositorioProposta repositorio = new RepositorioProposta(session);

			repositorio.ObterPropostasPorCriterio(criterio);
		}
	}
}
