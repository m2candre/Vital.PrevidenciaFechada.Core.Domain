using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		[Test]
		public void obter_propostas_por_estado_e_periodo_retorna_lista_corretamente()
		{
			Guid idDoPlano = Guid.NewGuid();
			DateTime dataDaBusca = DateTime.Now;
			Expression<Func<Proposta, bool>> criterios = proposta => proposta.Plano.Id == idDoPlano && proposta.Estado == "Registrada" && proposta.Data >= dataDaBusca;

			var criteriosDeConsulta = MockRepository.GenerateMock<CriteriosDeConsultaPorPlanoEstadoData>();
			criteriosDeConsulta.Expect(x => x.ObterCriterio(idDoPlano, "Registrada", dataDaBusca.AddDays(-20))).Return(criterios);

			ConsultaDTO consultaDTO = new ConsultaDTO();

			List<Proposta> listaRetorno = new List<Proposta>
			{
				new Proposta { Plano = new Plano { Id = idDoPlano }, Estado = "Registrada", Data = DateTime.Now.AddDays(-10) },
				new Proposta { Plano = new Plano { Id = idDoPlano }, Estado = "Registrada", Data = DateTime.Now.AddDays(-15) }
			};

			_servicoProposta.CriteriosConsulta = criteriosDeConsulta;
			_servicoProposta.Data = dataDaBusca;
			_repositorioProposta.Expect(x => x.ObterTodosFiltradosComCriterio<Proposta>(criterios, consultaDTO)).Return(listaRetorno);

			List<Proposta> propostas = _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(idDoPlano, "Registrada", 20, consultaDTO).ToList();

			Assert.IsNotNull(propostas);
			Assert.IsTrue(propostas.All(p => p.Plano.Id == idDoPlano && p.Estado == "Registrada" && p.Data >= dataDaBusca.AddDays(-20)));
		}

		[Test]
		public void obter_propostas_por_estado_e_periodo_e_quantidade_de_dias_vazia_calculando_trinta_dias_retorna_lista_corretamente()
		{
			Guid idDoPlano = Guid.NewGuid();
			DateTime dataDaBusca = DateTime.Now;
			Expression<Func<Proposta, bool>> criterios = proposta => proposta.Plano.Id == idDoPlano && proposta.Estado == "Registrada" && proposta.Data >= dataDaBusca;

			var criteriosDeConsulta = MockRepository.GenerateMock<CriteriosDeConsultaPorPlanoEstadoData>();
			criteriosDeConsulta.Expect(x => x.ObterCriterio(idDoPlano, "Registrada", dataDaBusca.AddDays(-30))).Return(criterios);

			ConsultaDTO consultaDTO = new ConsultaDTO();

			List<Proposta> listaRetorno = new List<Proposta>
			{
				new Proposta { Plano = new Plano { Id = idDoPlano }, Estado = "Registrada", Data = DateTime.Now.AddDays(-20) },
				new Proposta { Plano = new Plano { Id = idDoPlano }, Estado = "Registrada", Data = DateTime.Now.AddDays(-25) }
			};

			_servicoProposta.CriteriosConsulta = criteriosDeConsulta;
			_servicoProposta.Data = dataDaBusca;
			_repositorioProposta.Expect(x => x.ObterTodosFiltradosComCriterio<Proposta>(criterios, consultaDTO)).Return(listaRetorno);

			List<Proposta> propostas = _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(idDoPlano, "Registrada", 0, consultaDTO).ToList();

			Assert.IsNotNull(propostas);

			Assert.IsTrue(propostas.All(p => p.Plano.Id == idDoPlano && p.Estado == "Registrada" && p.Data >= dataDaBusca.AddDays(-30)));
		}

		[Test]
		public void obter_data_atual_corretamente()
		{
			Assert.That(_servicoProposta.Data, Is.Not.Null);
		}

		[Test]
		public void obter_criterios_para_consulta_corretamente()
		{
			Assert.That(_servicoProposta.CriteriosConsulta, Is.Not.Null);
		}

		[Test]
		public void se_construtor_nao_recebe_injecao_do_repositorio_lanca_excecao()
		{
			ServicoConsultarPropostas servico;
			Assert.That(() => servico = new ServicoConsultarPropostas(null), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O repositório não foi injetado corretamente"));
		}

		[Test]
		public void obter_propostas_sem_informar_o_id_do_plano_lanca_excecao()
		{
			Assert.That(() => _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(Guid.Empty, "Registrada", 20, new ConsultaDTO()), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O ID do plano deve ser informado"));
		}

		[Test]
		public void obter_propostas_sem_informar_o_estado_da_proposta_lanca_excecao()
		{
			Assert.That(() => _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(Guid.NewGuid(), "", 20, new ConsultaDTO()), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O estado da proposta deve ser informado"));
		}

		[Test]
		public void obter_propostas_sem_informar_o_dto_de_propostas_lanca_excecao()
		{
			Assert.That(() => _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(Guid.NewGuid(), "Registrada", 20, null), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O DTO de consulta não foi informado corretamente"));
		}
	}
}