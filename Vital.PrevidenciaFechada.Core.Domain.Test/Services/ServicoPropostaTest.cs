using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vital.InfraStructure.ExceptionHandling;
using Vital.Interfaces;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteConvenioDeAdesao;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Services;
using Vital.PrevidenciaFechada.DTO.Messages;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services
{
	[TestFixture]
	public class ServicoPropostaTest
	{
		private IRepositorioProposta _repositorioProposta;
		private IRepositorioConvenioDeAdesao _repositorioConvenio;
		private IGerenciadorDeArquivoProvider _gerenciadorDeArquivo;
		private ServicoProposta _servicoProposta;

		[SetUp]
		public void iniciar()
		{
			_repositorioProposta = MockRepository.GenerateMock<IRepositorioProposta>();
			_repositorioConvenio = MockRepository.GenerateMock<IRepositorioConvenioDeAdesao>();
			_gerenciadorDeArquivo = MockRepository.GenerateMock<IGerenciadorDeArquivoProvider>();
			_servicoProposta = new ServicoProposta(_repositorioProposta, _repositorioConvenio, _gerenciadorDeArquivo);
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
			Guid idDoConvenioDeAdesao = Guid.NewGuid();
			DateTime dataDaBusca = DateTime.Now;
			
			ConsultaDTO consultaDTO = new ConsultaDTO();

			ConvenioDeAdesao convenio = new ConvenioDeAdesao();
			convenio.AdicionarProposta(new Proposta { Estado = "Registrada", DataDeCriacao = DateTime.Now.AddDays(-10) });
			convenio.AdicionarProposta(new Proposta { Estado = "Registrada", DataDeCriacao = DateTime.Now.AddDays(-15) });

			_repositorioConvenio.Expect(x => x.PorId(idDoConvenioDeAdesao)).Return(convenio);

			_servicoProposta.Data = dataDaBusca;

			List<Proposta> propostas = _servicoProposta.ObterPropostasPorPeriodo(idDoConvenioDeAdesao, 20, consultaDTO) as List<Proposta>;

			Assert.IsNotNull(propostas);
			Assert.IsTrue(propostas.All(p => p.Estado == "Registrada" && p.DataDeCriacao >= dataDaBusca.AddDays(-20)));
		}

		[Test]
		public void obter_propostas_por_estado_e_periodo_e_quantidade_de_dias_vazia_calculando_trinta_dias_retorna_lista_corretamente()
		{
			Guid idDoConvenioDeAdesao = Guid.NewGuid();
			DateTime dataDaBusca = DateTime.Now;

			ConsultaDTO consultaDTO = new ConsultaDTO();

			ConvenioDeAdesao convenio = new ConvenioDeAdesao();
			convenio.AdicionarProposta(new Proposta { Estado = "Registrada", DataDeCriacao = DateTime.Now.AddDays(-10) });
			convenio.AdicionarProposta(new Proposta { Estado = "Registrada", DataDeCriacao = DateTime.Now.AddDays(-15) });

			_repositorioConvenio.Expect(x => x.PorId(idDoConvenioDeAdesao)).Return(convenio);

			_servicoProposta.Data = dataDaBusca;

			List<Proposta> propostas = _servicoProposta.ObterPropostasPorPeriodo(idDoConvenioDeAdesao, 0, consultaDTO) as List<Proposta>;

			Assert.IsNotNull(propostas);

			Assert.IsTrue(propostas.All(p => p.Estado == "Registrada" && p.DataDeCriacao >= dataDaBusca.AddDays(-30)));
		}

		[Test]
		public void criar_nova_proposta_com_ultimo_numero_gerado_mais_um()
		{
			Guid idDoConvenioDeAdesao = Guid.NewGuid();
			int ultimoNumero = 123;

			ModeloDeProposta modelo = new ModeloDeProposta();
		    var campo = new CampoDeProposta();
		    campo.Nome = "CPF";

			modelo.AdicionarCampo(campo);
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
        [Ignore("Não é possível serializar IList e também não é possível mapear no Hibernate List concreta")]
		public void serializar_xml_gravando_arquivo_em_disco()
		{
			Proposta propostaParaSerializar = new Proposta();
			propostaParaSerializar.Id = Guid.NewGuid();
			propostaParaSerializar.Numero = 123456;

			ModeloDeProposta modelo = new ModeloDeProposta();
			modelo.AdicionarCampo(new CampoDeProposta { Nome = "CPF" });

			propostaParaSerializar.ModeloDeProposta = new ModeloDeProposta();

			Guid idArquivoGerado = Guid.NewGuid();
			
			ArquivoUploadDTO dto = new ArquivoUploadDTO();
			ArquivoUploadDTO dtoRetorno = new ArquivoUploadDTO { Id = idArquivoGerado };

			_gerenciadorDeArquivo.Expect(x => x.Gravar(dto)).Return(dtoRetorno);

			_servicoProposta.ArquivoDTO = dto;
			_servicoProposta.SerializarXMLGravandoEmDisco(propostaParaSerializar);

			Assert.That(dtoRetorno.Id, Is.EqualTo(idArquivoGerado));
		}

		[Test]
		public void setar_nova_proposta_no_servico()
		{
			Assert.That(_servicoProposta.NovaProposta, Is.Not.Null);
		}

		[Test]
		public void setar_arquivo_dto_no_servico()
		{
			Assert.That(_servicoProposta.ArquivoDTO, Is.Not.Null);
		}
	}
}