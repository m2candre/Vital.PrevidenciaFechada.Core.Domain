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
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.Core.Domain.Services;
using Vital.PrevidenciaFechada.DTO.Messages;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services
{
	[TestFixture]
	public class ServicoPropostaTest
	{
		private IRepositorio<Proposta> _repositorioProposta;
		private IRepositorioConvenioDeAdesao _repositorioConvenio;
		private IGerenciadorDeArquivoProvider _gerenciadorDeArquivo;
		private ServicoProposta _servicoProposta;

		[SetUp]
		public void iniciar()
		{
			_repositorioProposta = MockRepository.GenerateMock<IRepositorio<Proposta>>();
			_repositorioConvenio = MockRepository.GenerateMock<IRepositorioConvenioDeAdesao>();
			_gerenciadorDeArquivo = MockRepository.GenerateMock<IGerenciadorDeArquivoProvider>();
			_servicoProposta = new ServicoProposta(_repositorioProposta, _repositorioConvenio, _gerenciadorDeArquivo);
		}

		[Test]
		[Ignore("As críticas estão amarradas à entidade Proposta, mas precisam mudar")]
		public void obter_criticas()
		{
			Guid idDoConvenioDeAdesao = Guid.NewGuid();
			
			ConvenioDeAdesao convenio = new ConvenioDeAdesao { Id = idDoConvenioDeAdesao };
			convenio.Plano = new Plano { Id = Guid.NewGuid(), Nome = "Plano" };

			_repositorioConvenio.Expect(x => x.PorId(convenio.Id)).Return(convenio);

			var criticas = _servicoProposta.ObterCriticasDaProposta(new PropostaDTO(), idDoConvenioDeAdesao);

			Assert.That(criticas.First().Mensagem, Is.EqualTo("Nome do participante é obrigatório"));
			Assert.That(criticas.First().Campo, Is.EqualTo("Nome"));
			Assert.That(criticas[1].Campo, Is.EqualTo("Cpf"));
			Assert.That(criticas[1].Mensagem, Is.EqualTo("Cpf está inválido"));
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
		public void gravar_novo_arquivo_xml_de_proposta_serializada()
		{
			Proposta propostaParaSerializar = new Proposta();
			propostaParaSerializar.Id = Guid.NewGuid();
			propostaParaSerializar.Numero = 123456;
			propostaParaSerializar.Valores = new List<DadosDaProposta>
			{
				new DadosDaProposta { Nome = "CPF", Valor = "123" },
				new DadosDaProposta { Nome = "Nome" , Valor = "Julio" }
			};

			ModeloDeProposta modelo = new ModeloDeProposta();
			modelo.AdicionarCampo(new CampoDeProposta { Nome = "CPF" });

			propostaParaSerializar.ModeloDeProposta = new ModeloDeProposta();

			Guid idArquivoGerado = Guid.NewGuid();

			ArquivoUploadDTO dto = new ArquivoUploadDTO();
			ArquivoUploadDTO dtoRetorno = new ArquivoUploadDTO { Id = idArquivoGerado };

			_gerenciadorDeArquivo.Expect(x => x.Gravar(dto)).Return(dtoRetorno);

			_servicoProposta.ArquivoDTO = dto;
			_servicoProposta.GravarArquivoDeDados(propostaParaSerializar);

			_gerenciadorDeArquivo.VerifyAllExpectations();
			Assert.That(dtoRetorno.Id, Is.EqualTo(idArquivoGerado));
		}

		[Test]
		public void atualiza_arquivo_xml_de_proposta_serializada_com_novos_dados()
		{
			Guid idArquivoGerado = Guid.NewGuid();

			Proposta propostaParaSerializar = new Proposta();
			propostaParaSerializar.Id = Guid.NewGuid();
			propostaParaSerializar.IdDoArquivoDeDados = idArquivoGerado;
			propostaParaSerializar.Numero = 123456;
			propostaParaSerializar.Valores = new List<DadosDaProposta>
			{
				new DadosDaProposta { Nome = "CPF", Valor = "123" },
				new DadosDaProposta { Nome = "Nome" , Valor = "Julio" }
			};

			ModeloDeProposta modelo = new ModeloDeProposta();
			modelo.AdicionarCampo(new CampoDeProposta { Nome = "CPF" });

			propostaParaSerializar.ModeloDeProposta = new ModeloDeProposta();

			ArquivoUploadDTO dto = new ArquivoUploadDTO { Id = idArquivoGerado };

			_gerenciadorDeArquivo.Expect(x => x.Atualizar(dto));

			_servicoProposta.ArquivoDTO = dto;
			_servicoProposta.AtualizarArquivoDeDados(propostaParaSerializar);

			_gerenciadorDeArquivo.VerifyAllExpectations();
			Assert.That(dto.Id, Is.EqualTo(idArquivoGerado));
		}

		[Test]
		public void carregar_proposta_do_arquivo_de_dados()
		{
			Guid idDaProposta = Guid.NewGuid();
			Guid idArquivoGerado = Guid.NewGuid();

			Proposta propostaParaSerializar = new Proposta();
			propostaParaSerializar.Id = idDaProposta;
			propostaParaSerializar.IdDoArquivoDeDados = idArquivoGerado;
			propostaParaSerializar.Numero = 123456;
			propostaParaSerializar.Valores = new List<DadosDaProposta>
			{
				new DadosDaProposta { Nome = "CPF", Valor = "123" },
				new DadosDaProposta { Nome = "Nome" , Valor = "Julio" }
			};
			byte[] propostaSerializada = propostaParaSerializar.Serializar();

			ArquivoUploadDTO dto = new ArquivoUploadDTO { Id = idArquivoGerado };
			ArquivoUploadDTO dtoRetorno = new ArquivoUploadDTO { Id = idArquivoGerado, Arquivo = propostaSerializada };

			_gerenciadorDeArquivo.Expect(x => x.Obter(dto)).Return(dtoRetorno);

			_servicoProposta.ArquivoDTO = dto;
			Proposta propostaRecuperada = _servicoProposta.CarregarPropostaDoArquivoDeDados(idArquivoGerado);

			Assert.That(propostaRecuperada, Is.Not.Null);
			Assert.That(propostaRecuperada.Id, Is.EqualTo(propostaParaSerializar.Id));
			Assert.That(propostaRecuperada.IdDoArquivoDeDados, Is.EqualTo(propostaParaSerializar.IdDoArquivoDeDados));
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