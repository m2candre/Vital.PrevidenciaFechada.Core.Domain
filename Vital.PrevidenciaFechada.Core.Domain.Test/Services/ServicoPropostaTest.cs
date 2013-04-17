using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vital.InfraStructure.ExceptionHandling;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteConvenioDeAdesao;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Services;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services
{
	[TestFixture]
	public class ServicoPropostaTest
	{
		private IRepositorioProposta _repositorioProposta;
		private IRepositorioConvenioDeAdesao _repositorioConvenio;
		private ServicoProposta _servicoProposta;

		[SetUp]
		public void iniciar()
		{
			_repositorioProposta = MockRepository.GenerateMock<IRepositorioProposta>();
			_repositorioConvenio = MockRepository.GenerateMock<IRepositorioConvenioDeAdesao>();
			_servicoProposta = new ServicoProposta(_repositorioProposta, _repositorioConvenio);
		}

		[Test]
		public void obter_criticas()
		{
			Guid idDoConvenioDeAdesao = Guid.NewGuid();
			
			ConvenioDeAdesao convenio = new ConvenioDeAdesao { Id = idDoConvenioDeAdesao };
			convenio.Plano = new Plano { Id = Guid.NewGuid(), Nome = "Plano" };

			_repositorioConvenio.Expect(x => x.PorId(convenio.Id)).Return(convenio);

			var criticas = _servicoProposta.ObterCriticasDaProposta(new PropostaDTO() { CPF = "123" }, idDoConvenioDeAdesao);

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
			Expression<Func<Proposta, bool>> criterios = proposta => proposta.Estado == "Registrada" && proposta.DataDeCriacao >= dataDaBusca;

			var criteriosDeConsulta = MockRepository.GenerateMock<CriteriosDeConsultaPorPlanoEstadoData>();
			criteriosDeConsulta.Expect(x => x.ObterCriterio(idDoPlano, "Registrada", dataDaBusca.AddDays(-20))).Return(criterios);

			ConsultaDTO consultaDTO = new ConsultaDTO();

			List<Proposta> listaRetorno = new List<Proposta>
			{
				new Proposta { Estado = "Registrada", DataDeCriacao = DateTime.Now.AddDays(-10) },
				new Proposta { Estado = "Registrada", DataDeCriacao = DateTime.Now.AddDays(-15) }
			};

			_servicoProposta.CriteriosConsulta = criteriosDeConsulta;
			_servicoProposta.Data = dataDaBusca;
			_repositorioProposta.Expect(x => x.ObterTodosFiltradosComCriterio<Proposta>(criterios, consultaDTO)).Return(listaRetorno);

			List<Proposta> propostas = _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(idDoPlano, "Registrada", 20, consultaDTO).ToList();

			Assert.IsNotNull(propostas);
			Assert.IsTrue(propostas.All(p => p.Estado == "Registrada" && p.DataDeCriacao >= dataDaBusca.AddDays(-20)));
		}

		[Test]
		public void obter_propostas_por_estado_e_periodo_e_quantidade_de_dias_vazia_calculando_trinta_dias_retorna_lista_corretamente()
		{
			Guid idDoPlano = Guid.NewGuid();
			DateTime dataDaBusca = DateTime.Now;
			Expression<Func<Proposta, bool>> criterios = proposta => proposta.Estado == "Registrada" && proposta.DataDeCriacao >= dataDaBusca;

			var criteriosDeConsulta = MockRepository.GenerateMock<CriteriosDeConsultaPorPlanoEstadoData>();
			criteriosDeConsulta.Expect(x => x.ObterCriterio(idDoPlano, "Registrada", dataDaBusca.AddDays(-30))).Return(criterios);

			ConsultaDTO consultaDTO = new ConsultaDTO();

			List<Proposta> listaRetorno = new List<Proposta>
			{
				new Proposta { Estado = "Registrada", DataDeCriacao = DateTime.Now.AddDays(-20) },
				new Proposta { Estado = "Registrada", DataDeCriacao = DateTime.Now.AddDays(-25) }
			};

			_servicoProposta.CriteriosConsulta = criteriosDeConsulta;
			_servicoProposta.Data = dataDaBusca;
			_repositorioProposta.Expect(x => x.ObterTodosFiltradosComCriterio<Proposta>(criterios, consultaDTO)).Return(listaRetorno);

			List<Proposta> propostas = _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(idDoPlano, "Registrada", 0, consultaDTO).ToList();

			Assert.IsNotNull(propostas);

			Assert.IsTrue(propostas.All(p => p.Estado == "Registrada" && p.DataDeCriacao >= dataDaBusca.AddDays(-30)));
		}

		[Test]
		public void criar_nova_proposta_com_ultimo_numero_gerado_mais_um()
		{
			Guid idDoConvenioDeAdesao = Guid.NewGuid();
			int ultimoNumero = 123;

			ModeloDeProposta modelo = new ModeloDeProposta { TemplateDeFormulario = "<div></div>", TemplateDeImpressao = "<div></div>" };
			modelo.AdicionarCampo("Nome");
			modelo.Publicar();

			ConvenioDeAdesao convenio = new ConvenioDeAdesao { Id = idDoConvenioDeAdesao };
			convenio.ModelosDeProposta = new List<ModeloDeProposta> { modelo };

			_repositorioConvenio.Expect(x => x.UltimoNumeroDeProposta(convenio.Id)).Return(ultimoNumero);
			_repositorioConvenio.Expect(x => x.PorId(convenio.Id)).Return(convenio);

			_servicoProposta.Data = DateTime.Now;
			Proposta novaProposta = new Proposta { DataDeCriacao = _servicoProposta.Data };

			_servicoProposta.NovaProposta = novaProposta;
			_servicoProposta.CriarNovaProposta(idDoConvenioDeAdesao);

			Assert.That(novaProposta.Numero, Is.EqualTo(124));
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
		public void obter_propostas_sem_informar_o_id_do_plano_lanca_excecao()
		{
			Assert.That(() => _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(Guid.Empty, "Registrada", 20, new ConsultaDTO()), Throws.Exception.TypeOf<BusinessException>().With.Property("Message").EqualTo("O ID do plano deve ser informado"));
		}

		[Test]
		public void obter_propostas_sem_informar_o_estado_da_proposta_lanca_excecao()
		{
			Assert.That(() => _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(Guid.NewGuid(), "", 20, new ConsultaDTO()), Throws.Exception.TypeOf<BusinessException>().With.Property("Message").EqualTo("O estado da proposta deve ser informado"));
		}

		[Test]
		public void obter_propostas_sem_informar_o_dto_de_propostas_lanca_excecao()
		{
			Assert.That(() => _servicoProposta.ObterPropostasPorPlanoEstadoEPeriodo(Guid.NewGuid(), "Registrada", 20, null), Throws.Exception.TypeOf<BusinessException>().With.Property("Message").EqualTo("O DTO de consulta não foi informado corretamente"));
		}
	}
}