using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.Core.Domain.Services;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services
{
	[TestFixture]
	public class ServicoGerarNumeroDePropostaTest
	{
		private IRepositorioProposta _repositorio;
		private ServicoGerarNumeroDeProposta _servico;

		[SetUp]
		public void iniciar()
		{
			_repositorio = MockRepository.GenerateMock<IRepositorioProposta>();
			_servico = new ServicoGerarNumeroDeProposta(_repositorio);
		}

		[Test]
		public void obter_ultimo_numero_gerado_retorna_1_se_nao_houver_nenhuma_proposta()
		{
			_repositorio.Expect(x => x.ObterUltimoNumeroDaProposta()).Return(0);

			string numeroGerado = _servico.GerarNumeroDeProposta();

			Assert.That(numeroGerado, Is.EqualTo("1"));
		}

		[Test]
		public void obter_ultimo_numero_gerado_retorna_o_maior_numero_de_proposta_cadastrada()
		{
			_repositorio.Expect(x => x.ObterUltimoNumeroDaProposta()).Return(10);

			string numeroGerado = _servico.GerarNumeroDeProposta();

			Assert.That(numeroGerado, Is.EqualTo("11"));
		}
	}
}