using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteConvenioDeAdesao;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Mappers;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.DTO.Messages.Core;
using Vital.Extensions.DateTimeExtensions;
using System.Linq.Expressions;
using Vital.Interfaces;
using System.IO;
using Vital.PrevidenciaFechada.DTO.Messages;
using System.Xml.Serialization;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
	/// <summary>
	/// Serviço de domínio para Proposta
	/// </summary>
	public class ServicoProposta
	{
		private IRepositorioProposta _repositorioProposta;
		private IRepositorioConvenioDeAdesao _repositorioConvenio;
		private Proposta _proposta;
		private DateTime _data;
		private ArquivoUploadDTO _arquivoDTO;

		public ArquivoUploadDTO ArquivoDTO
		{
			get
			{
				if (_arquivoDTO == null)
					_arquivoDTO = new ArquivoUploadDTO();

				return _arquivoDTO;
			}
			set { _arquivoDTO = value; }
		}

		/// <summary>
		/// Gerenciador de arquivo
		/// </summary>
		private IGerenciadorDeArquivoProvider _gerenciadorDeArquivo;

		/// <summary>
		/// Obtém uma nova proposta setando a data de criação para a data/hora atual
		/// </summary>
		public Proposta NovaProposta
		{
			get
			{
				if (_proposta == null)
					_proposta = new Proposta { DataDeCriacao = Data };

				return _proposta;
			}
			set { _proposta = value; }
		}

		/// <summary>
		/// Obtém data atual, se data não estiver definida
		/// </summary>
		public virtual DateTime Data
		{
			get
			{
				if (_data == default(DateTime))
					_data = DateTime.Now;
				return _data;
			}
			set { _data = value; }
		}

		/// <summary>
		/// Construtor com injeção de dependência dos repositório de Proposta e Plano
		/// </summary>
		public ServicoProposta(IRepositorioProposta repositorioProposta, IRepositorioConvenioDeAdesao repositorioConvenio, IGerenciadorDeArquivoProvider gerenciadorDeArquivo)
		{
			#region Pré-condições

			IAssertion oRepositorioDePropostaFoiInjetado = Assertion.NotNull(repositorioProposta, "O repositório de proposta não foi injetado corretamente");
			IAssertion oRepositorioDeConvenioFoiInjetado = Assertion.NotNull(repositorioConvenio, "O repositório de convênio de adesão não foi injetado corretamente");
			IAssertion oGerenciadorDeArquivoFoiInjetado = Assertion.NotNull(gerenciadorDeArquivo, "O gerenciador de arquivo não foi injetado corretamente");

			#endregion

			oRepositorioDePropostaFoiInjetado.and(oRepositorioDeConvenioFoiInjetado).and(oGerenciadorDeArquivoFoiInjetado).Validate();

			_repositorioProposta = repositorioProposta;
			_repositorioConvenio = repositorioConvenio;
			_gerenciadorDeArquivo = gerenciadorDeArquivo;

			#region Pós-condições

			IAssertion oRepositorioDePropostaFoiInjetadoCorretamente = Assertion.Equals(_repositorioProposta, repositorioProposta, "O repositório de proposta da classe não está igual ao repositório injetado");
			IAssertion oRepositorioDeConvenioFoiInjetadoCorretamente = Assertion.Equals(_repositorioConvenio, repositorioConvenio, "O repositório de plano da classe não está igual ao repositório injetado");
			IAssertion oGerenciadorDeArquivoFoiSetadoNoServico = Assertion.Equals(_gerenciadorDeArquivo, gerenciadorDeArquivo, "O gerenciador de arquivo não foi definido corretamente no Serviço de Proposta");

			#endregion

			oRepositorioDePropostaFoiInjetadoCorretamente.and(oRepositorioDeConvenioFoiInjetadoCorretamente).and(oGerenciadorDeArquivoFoiInjetado).Validate();
		}

		/// <summary>
		/// Realiza consulta para obter as propostas pelo estado e período, informado em dias
		/// </summary>
		/// <param name="idDoConvenioDeAdesao">ID do convênio de adesão</param>
		/// <param name="estado">Estado das propostas</param>
		/// <param name="quantidadeDeDias">Período em quantidade dias</param>
		/// <returns></returns>
		public virtual IList<Proposta> ObterPropostasPorPeriodo(Guid idDoConvenioDeAdesao, int quantidadeDeDias, ConsultaDTO consultaDTO)
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInjetadoNoServico = Assertion.NotNull(_repositorioProposta, "O repositório não foi injetado corretamente");
			IAssertion oDTODeConsultaFoiInformado = Assertion.NotNull(consultaDTO, "O DTO de consulta não foi informado corretamente");
			IAssertion oIDDoPlanoFoiInformado = Assertion.IsTrue(idDoConvenioDeAdesao != Guid.Empty, "O ID do Convênio de Adesão deve ser informado");

			#endregion

			oRepositorioFoiInjetadoNoServico.and(oDTODeConsultaFoiInformado).and(oIDDoPlanoFoiInformado).Validate();

			DateTime dataDaBusca = dataDaBusca = ObterDataParaConsulta(quantidadeDeDias);

			var convenioDeAdesao = _repositorioConvenio.PorId(idDoConvenioDeAdesao);

			var propostasEncontradas = convenioDeAdesao.Propostas.Where(p => p.DataDeCriacao >= dataDaBusca);

			#region Pós-condições

			Assertion.NotNull(propostasEncontradas, "A lista de propostas encontradas não pode ser nula").Validate();

			#endregion

			return propostasEncontradas.ToList();
		}

		/// <summary>
		/// Obtem uma lista de críticas da proposta
		/// </summary>
		/// <param name="propostaDTO">proposta</param>
		/// <returns>Lista de críticas</returns>
		public virtual IList<CriticaDTO> ObterCriticasDaProposta(PropostaDTO propostaDTO, Guid idDoConvenioDeAdesao)
		{
			var convenio = _repositorioConvenio.PorId(idDoConvenioDeAdesao);

			var plano = convenio.Plano;

			var propostaVO = new PropostaMapper().DePropostaDTOParaPropostaVO(propostaDTO);

			IList<CriticaVO> criticasVO = plano.Regulamento.ObterCriticasDaProposta(propostaVO);

			return new CriticaMapper().DeCriticaVOParaCriticaDTO(criticasVO);
		}

		/// <summary>
		/// Cria uma proposta com um novo número e persiste no banco de dados
		/// </summary>
		public virtual void CriarNovaProposta(Guid idDoConvenioDeAdesao)
		{
			int ultimoNumeroDeProposta = _repositorioConvenio.UltimoNumeroDeProposta(idDoConvenioDeAdesao);

			ConvenioDeAdesao convenioDeAdesao = _repositorioConvenio.PorId(idDoConvenioDeAdesao);
			
			ModeloDeProposta modeloDePropostaPublicado = convenioDeAdesao.ObterModeloDePropostaPublicado();

			NovaProposta.Numero = ultimoNumeroDeProposta + 1;
			NovaProposta.ModeloDeProposta = modeloDePropostaPublicado;

			_repositorioProposta.Adicionar(NovaProposta);
			convenioDeAdesao.AdicionarProposta(NovaProposta);
			
			_repositorioConvenio.Adicionar(convenioDeAdesao);
		}

		/// <summary>
		/// Armazena e indexa o arquivo com a proposta serializada utilizando o Serviço de Arquivos
		/// </summary>
		/// <param name="proposta">Proposta a ser serializada</param>
		/// <returns>ID do arquivo criado</returns>
		public virtual Guid GravarArquivoDeDados(Proposta proposta)
		{
			#region Pré-condições

			IAssertion aPropostaFoiInformada = Assertion.NotNull(proposta, "A proposta não pode ser nula");

			#endregion

			aPropostaFoiInformada.Validate();

			ArquivoDTO.Arquivo = proposta.Serializar();
			ArquivoDTO.Extensao = "xml";
			ArquivoUploadDTO dtoRetorno = _gerenciadorDeArquivo.Gravar(ArquivoDTO);

			IndexarArquivoDeDados(ArquivoDTO, proposta.Id);

			#region Pós-condições

			IAssertion oIDDoArquivoFoiGerado = Assertion.IsTrue(dtoRetorno.Id != Guid.Empty, "O ID do arquivo não foi gerado");

			#endregion

			oIDDoArquivoFoiGerado.Validate();

			return dtoRetorno.Id;
		}

		/// <summary>
		/// Atualiza o arquivo de dados com novas informações serializadas da proposta
		/// </summary>
		/// <param name="proposta">Proposta com dados novos</param>
		public virtual void AtualizarArquivoDeDados(Proposta proposta)
		{
			#region Pré-condições

			IAssertion aPropostaPossuiOIDDoAquivoDeDados = Assertion.IsTrue(proposta.IdDoArquivoDeDados != Guid.Empty, "A proposta não possui o ID do arquivo de dados. Não será possível localizá-lo.");

			#endregion

			aPropostaPossuiOIDDoAquivoDeDados.Validate();

			ArquivoDTO.Arquivo = proposta.Serializar();
			ArquivoDTO.Id = proposta.IdDoArquivoDeDados;
			ArquivoDTO.Extensao = "xml";
			
			_gerenciadorDeArquivo.Atualizar(ArquivoDTO);
			IndexarArquivoDeDados(ArquivoDTO, proposta.Id);
		}

		/// <summary>
		/// Carrega o arquivo de dados
		/// </summary>
		/// <param name="idDoArquivo">ID do arquivo de dados</param>
		/// <returns></returns>
		public virtual Proposta CarregarPropostaDoArquivoDeDados(Guid idDoArquivo)
		{
			#region Pré-condições

			IAssertion oIDDoArquivo = Assertion.IsTrue(idDoArquivo != Guid.Empty, "O ID do arquivo não foi informado corretamente");

			#endregion

			oIDDoArquivo.Validate();

			byte[] arquivo = RecuperarArquivoDeDados(idDoArquivo);
			Proposta proposta = Deserializar(arquivo);

			#region Pós-condições

			IAssertion aPropostaDeserializadaNaoEstaNula = Assertion.NotNull(proposta, "A proposta não foi deserializada corretamente");
			IAssertion oIDDaPropostaEstaPreenchido = Assertion.NotNull(proposta.Id, "O ID da proposta não foi preenchido na deserialização");
			IAssertion aListaDeValoresNaoEstaVazia = Assertion.GreaterThan(proposta.Valores.Count, 0, "A lista de valores da proposta não pode estar vazia");

			#endregion

			aPropostaDeserializadaNaoEstaNula.and(oIDDaPropostaEstaPreenchido).and(aListaDeValoresNaoEstaVazia).Validate();

			return proposta;
		}

		/// <summary>
		/// Retorna o binário do arquivo de dados obtido do Serviço de Arquivos
		/// </summary>
		/// <param name="idDoArquivo">ID do arquivo a recuperar</param>
		/// <returns>byte[]</returns>
		private byte[] RecuperarArquivoDeDados(Guid idDoArquivo)
		{
			ArquivoDTO.Id = idDoArquivo;
			ArquivoDTO.Extensao = "xml";

			ArquivoUploadDTO retorno = _gerenciadorDeArquivo.Obter(ArquivoDTO);

			return retorno.Arquivo;
		}

		/// <summary>
		/// Deserializa o arquivo de dados para a Proposta
		/// </summary>
		/// <param name="arquivoDeDados">Conteúdo do arquivo de dados</param>
		/// <returns>Proposta preenchida</returns>
		private Proposta Deserializar(byte[] arquivoDeDados)
		{
			#region Pré-condições

			IAssertion oArquivoInformadoNaoEstaVazio = Assertion.IsTrue(arquivoDeDados.Length > 0, "O arquivo de dados não possui informações");

			#endregion

			oArquivoInformadoNaoEstaVazio.Validate();

			MemoryStream stream = new MemoryStream(arquivoDeDados);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Proposta));

			return (Proposta)xmlSerializer.Deserialize(stream);
		}

		/// <summary>
		/// Obtem uma data para a busca de propostas de acordo com o parametro de dias
		/// caso a quantidade de dias seja igual a Zero o padrão são 30 dias
		/// </summary>
		/// <param name="quantidadeDeDias">quantidade de dias</param>
		/// <returns>DateTime</returns>
		private DateTime ObterDataParaConsulta(int quantidadeDeDias)
		{
			DateTime dataDaBusca = DateTime.MinValue;

			if (quantidadeDeDias == default(int))
				dataDaBusca = Data.SubtrairDias(30);
			else
				dataDaBusca = Data.SubtrairDias(quantidadeDeDias);

			return dataDaBusca;
		}

		/// <summary>
		/// Indexa o arquivo de dados da proposta utilizando as chaves "IdDaProposta" e "XMLDaProposta" e ID da proposta como valor para ambas as chaves
		/// </summary>
		/// <param name="idDoArquivo">ID do arquivo gerado</param>
		/// <param name="idDaProposta">ID da proposta</param>
		private void IndexarArquivoDeDados(ArquivoUploadDTO arquivoDTO, Guid idDaProposta)
		{
			Dictionary<string, string> indicesDoArquivo = new Dictionary<string, string>();
			indicesDoArquivo.Add("IdDaProposta", idDaProposta.ToString());
			indicesDoArquivo.Add("XMLDaProposta", idDaProposta.ToString());

			_gerenciadorDeArquivo.IndexarPor(arquivoDTO, indicesDoArquivo);
		}
	}
}