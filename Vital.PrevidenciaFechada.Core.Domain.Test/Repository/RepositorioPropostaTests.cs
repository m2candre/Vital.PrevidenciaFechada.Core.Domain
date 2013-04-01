using NHibernate;
using NHibernate.Criterion;
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
		public void construtor_padrao_para_repositorio()
		{
			RepositorioProposta repositorio = new RepositorioProposta();
			Assert.IsNotNull(repositorio);
		}

		[Test]
		public void obter_ultimo_numero_da_proposta()
		{
			ISession session = MockRepository.GenerateMock<ISession>();
			ICriteria criteria = MockRepository.GenerateMock<ICriteria>();
			IProjection projection = MockRepository.GenerateMock<IProjection>();
			VitalCriterion vitalCriterion = MockRepository.GenerateMock<VitalCriterion>();

			criteria.Expect(x => x.UniqueResult()).Return(10);

			vitalCriterion.Expect(x => x.Max("Numero")).Return(projection);

			criteria.Expect(x => x.SetProjection(projection)).Return(criteria);

			session.Expect(x => x.CreateCriteria<Proposta>()).Return(criteria);

			RepositorioProposta repositorio = new RepositorioProposta(session);
			repositorio.VitalCriterion = vitalCriterion;
			
			int ultimoNumero = repositorio.ObterUltimoNumeroDaProposta();

			session.VerifyAllExpectations();
			criteria.VerifyAllExpectations();
			vitalCriterion.VerifyAllExpectations();
		}
	}
}