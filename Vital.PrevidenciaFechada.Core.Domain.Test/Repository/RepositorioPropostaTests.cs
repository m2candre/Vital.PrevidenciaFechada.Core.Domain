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
		public void construtor_padrao_para_repositorio()
		{
			RepositorioProposta repositorio = new RepositorioProposta();
			Assert.IsNotNull(repositorio);
		}

		[Test]
		public void obter_ultimo_numero_da_proposta()
		{
			
		}
	}
}
