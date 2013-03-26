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
	public class ServicoConsultarPropostasTest
	{
		private IRepositorio<Proposta> _repositorio;
		private ServicoConsultarPropostas _servico;

		[SetUp]
		public void iniciar()
		{
			_repositorio = MockRepository.GenerateMock<IRepositorio<Proposta>>();
			_servico = new ServicoConsultarPropostas(_repositorio);
		}

		[Test]
		public void obter_data_atual_corretamente()
		{
			Assert.That(_servico.Data, Is.Not.Null);
		}

		[Test]
		public void obter_criterios_para_consulta_corretamente()
		{
			Assert.That(_servico.CriteriosConsulta, Is.Not.Null);
		}

		[Test]
		public void se_construtor_nao_recebe_injecao_do_repositorio_lanca_excecao()
		{
			ServicoConsultarPropostas servico;
			Assert.That(() => servico = new ServicoConsultarPropostas(null), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O repositório não foi injetado corretamente"));
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
				new Proposta { Plano = new Plano { Id = idDoPlano }, Estado = "Registrada", Data = DateTime.Parse("10/03/2013 10:00") },
				new Proposta { Plano = new Plano { Id = idDoPlano }, Estado = "Registrada", Data = DateTime.Parse("11/03/2013 10:00") }
			};

			_servico.CriteriosConsulta = criteriosDeConsulta;
			_servico.Data = dataDaBusca;
			_repositorio.Expect(x => x.ObterTodosFiltradosComCriterio<Proposta>(criterios, consultaDTO)).Return(listaRetorno);

			List<Proposta> propostas = _servico.ObterPropostasPorPlanoEstadoEPeriodo(idDoPlano, "Registrada", 20, consultaDTO).ToList();

			Assert.IsNotNull(propostas);
			Assert.IsTrue(propostas.All(p => p.Plano.Id == idDoPlano && p.Estado == "Registrada" && p.Data >= dataDaBusca.AddDays(-20)));
		}

		[Test]
		public void obter_propostas_sem_informar_o_id_do_plano_lanca_excecao()
		{
			Assert.That(() => _servico.ObterPropostasPorPlanoEstadoEPeriodo(Guid.Empty, "Registrada", 20, new ConsultaDTO()), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O ID do plano deve ser informado"));
		}

		[Test]
		public void obter_propostas_sem_informar_o_estado_da_proposta_lanca_excecao()
		{
			Assert.That(() => _servico.ObterPropostasPorPlanoEstadoEPeriodo(Guid.NewGuid(), "", 20, new ConsultaDTO()), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O estado da proposta deve ser informado"));
		}

		[Test]
		public void obter_propostas_sem_informar_a_quantidade_de_dias_lanca_excecao()
		{
			Assert.That(() => _servico.ObterPropostasPorPlanoEstadoEPeriodo(Guid.NewGuid(), "Registrada", default(int), new ConsultaDTO()), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("A quantidade de dias deve ser maior que 0"));
		}

		[Test]
		public void obter_propostas_sem_informar_o_dto_de_propostas_lanca_excecao()
		{
			Assert.That(() => _servico.ObterPropostasPorPlanoEstadoEPeriodo(Guid.NewGuid(), "Registrada", 20, null), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O DTO de consulta não foi informado corretamente"));
		}
	}
}