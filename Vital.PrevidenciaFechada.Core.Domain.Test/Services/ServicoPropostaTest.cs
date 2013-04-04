using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.Core.Domain.Services;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services
{
	[TestFixture]
	public class ServicoPropostaTest
	{
		private IRepositorio<Plano> _repositorioPlano;
		private IRepositorioProposta _repositorioProposta;
		private ServicoProposta _servicoProposta;

		[SetUp]
		public void iniciar()
		{
			_repositorioPlano = MockRepository.GenerateMock<IRepositorio<Plano>>();
			_repositorioProposta = MockRepository.GenerateMock<IRepositorioProposta>();
			_servicoProposta = new ServicoProposta(_repositorioProposta, _repositorioPlano);
		}

		[Test]
		public void obter_criticas()
		{
			Guid idDoPLano = Guid.NewGuid();

			_repositorioPlano.Expect(x => x.PorId(idDoPLano)).Return(new Plano());

			var criticas = _servicoProposta.ObterCriticasDaProposta(new PropostaDTO() { CPF = "123" }, idDoPLano);

			Assert.That(criticas.First().Mensagem, Is.EqualTo("Nome do participante é obrigatório"));
			Assert.That(criticas.First().Campo, Is.EqualTo("Nome"));
			Assert.That(criticas[1].Campo, Is.EqualTo("Cpf"));
			Assert.That(criticas[1].Mensagem, Is.EqualTo("Cpf está inválido"));
		}
	}
}