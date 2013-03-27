using System;
using System.Collections.Generic;
using System.Linq;
using Vital.Extensions.DateTimeExtensions;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
	/// <summary>
	/// Serviço de consulta de propostas por critérios de Plano, Estado e Data
	/// </summary>
	public class ServicoConsultarPropostas
	{
		private IRepositorio<Proposta> _repositorio;
		private CriteriosDeConsultaPorPlanoEstadoData _criteriosConsulta;
		private DateTime _data;

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
		/// Construtor do serviço de domínio injetando o repositório de proposta
		/// </summary>
		public ServicoConsultarPropostas(IRepositorio<Proposta> repositorio)
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInformado = Assertion.NotNull(repositorio, "O repositório não foi injetado corretamente");
			
			#endregion

			oRepositorioFoiInformado.Validate();

			_repositorio = repositorio;
			
			#region Pós-condições

			IAssertion oRepositorioFoiInjetadoCoretamente = Assertion.Equals(_repositorio, repositorio, "O repositório da classe não está igual ao repositório injetado");
			
			#endregion

			oRepositorioFoiInjetadoCoretamente.Validate();
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

			IAssertion oRepositorioFoiInjetadoNoServico = Assertion.NotNull(_repositorio, "O repositório não foi injetado corretamente");
			IAssertion oDTODeConsultaFoiInformado = Assertion.NotNull(consultaDTO, "O DTO de consulta não foi informado corretamente");
			IAssertion oIDDoPlanoFoiInformado = Assertion.IsTrue(idDoPlano != Guid.Empty, "O ID do plano deve ser informado");
			IAssertion oEstadoFoiInformado = Assertion.IsFalse(string.IsNullOrWhiteSpace(estado), "O estado da proposta deve ser informado");

			#endregion

			oRepositorioFoiInjetadoNoServico.and(oDTODeConsultaFoiInformado).and(oIDDoPlanoFoiInformado).and(oEstadoFoiInformado).Validate();

            DateTime dataDaBusca = dataDaBusca = ObterDataParaConsulta(quantidadeDeDias);

			var criterios = CriteriosConsulta.ObterCriterio(idDoPlano, estado, dataDaBusca);

			var propostasEncontradas = _repositorio.ObterTodosFiltradosComCriterio<Proposta>(criterios, consultaDTO);

			#region Pós-condições

			Assertion.NotNull(propostasEncontradas, "A lista de propostas encontradas não pode ser nula").Validate();

			#endregion

			return propostasEncontradas.ToList();
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