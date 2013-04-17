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

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
	/// <summary>
	/// Serviço de domínio para Proposta
	/// </summary>
	public class ServicoProposta
	{
		private IRepositorioProposta _repositorioProposta;
		private IRepositorioConvenioDeAdesao _repositorioConvenio;
		private CriteriosDeConsultaPorPlanoEstadoData _criteriosConsulta;
		private Proposta _proposta;
		private DateTime _data;

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
		/// Obtém a classe de criação dos critérios para a consulta
		/// </summary>
		public virtual CriteriosDeConsultaPorPlanoEstadoData CriteriosConsulta
		{
			get
			{
				if (_criteriosConsulta == null)
					_criteriosConsulta = new CriteriosDeConsultaPorPlanoEstadoData();

				return _criteriosConsulta;
			}
			set { _criteriosConsulta = value; }
		}

		/// <summary>
		/// Construtor com injeção de dependência dos repositório de Proposta e Plano
		/// </summary>
		public ServicoProposta(IRepositorioProposta repositorioProposta, IRepositorioConvenioDeAdesao repositorioConvenio)
		{
			#region Pré-condições

			IAssertion oRepositorioDePropostaFoiInjetado = Assertion.NotNull(repositorioProposta, "O repositório de proposta não foi injetado corretamente");
			IAssertion oRepositorioDeConvenioFoiInjetado = Assertion.NotNull(repositorioConvenio, "O repositório de convênio de adesão não foi injetado corretamente");

			#endregion

			oRepositorioDePropostaFoiInjetado.and(oRepositorioDeConvenioFoiInjetado).Validate();

			_repositorioProposta = repositorioProposta;
			_repositorioConvenio = repositorioConvenio;

			#region Pós-condições

			IAssertion oRepositorioDePropostaFoiInjetadoCorretamente = Assertion.Equals(_repositorioProposta, repositorioProposta, "O repositório de proposta da classe não está igual ao repositório injetado");
			IAssertion oRepositorioDeConvenioFoiInjetadoCorretamente = Assertion.Equals(_repositorioConvenio, repositorioConvenio, "O repositório de plano da classe não está igual ao repositório injetado");

			#endregion

			oRepositorioDePropostaFoiInjetadoCorretamente.Validate();
		}

		/// <summary>
		/// Realiza consulta para obter as propostas pelo estado e período, informado em dias
		/// </summary>
		/// <param name="idDoPlano">ID do plano do contexto</param>
		/// <param name="estado">Estado das propostas</param>
		/// <param name="quantidadeDeDias">Período em quantidade dias</param>
		/// <returns></returns>
		public virtual IList<Proposta> ObterPropostasPorPlanoEstadoEPeriodo(Guid idDoPlano, string estado, int quantidadeDeDias, ConsultaDTO consultaDTO)
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInjetadoNoServico = Assertion.NotNull(_repositorioProposta, "O repositório não foi injetado corretamente");
			IAssertion oDTODeConsultaFoiInformado = Assertion.NotNull(consultaDTO, "O DTO de consulta não foi informado corretamente");
			IAssertion oIDDoPlanoFoiInformado = Assertion.IsTrue(idDoPlano != Guid.Empty, "O ID do plano deve ser informado");
			IAssertion oEstadoFoiInformado = Assertion.IsFalse(string.IsNullOrWhiteSpace(estado), "O estado da proposta deve ser informado");

			#endregion

			oRepositorioFoiInjetadoNoServico.and(oDTODeConsultaFoiInformado).and(oIDDoPlanoFoiInformado).and(oEstadoFoiInformado).Validate();

			DateTime dataDaBusca = dataDaBusca = ObterDataParaConsulta(quantidadeDeDias);

			var criterios = CriteriosConsulta.ObterCriterio(idDoPlano, estado, dataDaBusca);

			var propostasEncontradas = _repositorioProposta.ObterTodosFiltradosComCriterio<Proposta>(criterios, consultaDTO);

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
	}
}