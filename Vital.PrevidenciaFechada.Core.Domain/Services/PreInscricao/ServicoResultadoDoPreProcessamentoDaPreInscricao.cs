using System;
using System.Collections.Generic;
using Vital.Extensions.Data.Datatable;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.DTO.Messages.Adesao.PreInscricao;

namespace Vital.PrevidenciaFechada.Core.Domain.Services.PreInscricao
{
	/// <summary>
	/// Obtem um resultado do pre processamento da pré inscrição
	/// </summary>
	public class ServicoResultadoDoPreProcessamentoDaPreInscricao
	{
		private IRepositorio<PreInscrito> _preInscritos;

		/// <summary>
		/// Construtor
		/// </summary>
		/// <param name="preInscritos">Repositório de dados de pré-inscritos</param>
		public ServicoResultadoDoPreProcessamentoDaPreInscricao(IRepositorio<PreInscrito> preInscritos)
		{
			_preInscritos = preInscritos;
		}

		/// <summary>
		/// Obtem um resultado do pre processamento da pré inscrição
		/// </summary>
		public virtual PreInscricaoDTO Obter(PreInscricaoDTO preInscricaoDTO)
		{
			#region Pré-Condições

			IAssertion oDtoFoiInformado = Assertion.NotNull(preInscricaoDTO, "O DTO de pré inscrição não foi informado");
			IAssertion oIDDoConvenioDeAdesaoFoiInformado = Assertion.IsFalse(preInscricaoDTO.IdDoConvenioDeAdesao == Guid.Empty, "O ID do convênio de adesão não foi informado");			
			IAssertion aTabelaDePreInscritosFoiInformada = Assertion.LambdaAssertion(() => preInscricaoDTO.TabelaDePreInscritos != null, "A estrutura de dados de pré-inscritos não foi informada");
			IAssertion aTabelaDePreInscritosFoiPreenchida = Assertion.LambdaAssertion(() => preInscricaoDTO.TabelaDePreInscritos.Rows.Count > 0, "A estrutura de dados de pré-inscritos está vazia");
			IAssertion oRepositorioFoiInicializado = Assertion.NotNull(_preInscritos, "O repositório de pré-inscritos não foi devidamente injetado");

			#endregion

			oDtoFoiInformado.and(oIDDoConvenioDeAdesaoFoiInformado).and(oRepositorioFoiInicializado).AND(aTabelaDePreInscritosFoiInformada).AND(aTabelaDePreInscritosFoiPreenchida).Validate();

			var preInscritosDoArquivo = preInscricaoDTO.TabelaDePreInscritos.ConverterDataTableParaListaDeObjeto<PreInscrito>();

			InformarPreinscritosExistentes(preInscricaoDTO, preInscritosDoArquivo);

			InformarNovosPreInscritos(preInscricaoDTO, preInscritosDoArquivo.Count);

			return preInscricaoDTO;
		}

		/// <summary>
		/// Obtém a lista completa de pré-inscritos para inserir e atualizar
		/// </summary>
		/// <param name="preInscritosDoArquivo"></param>
		public virtual List<PreInscrito> ObterPreInscritosParaInserir(List<PreInscrito> preInscritosDoArquivo, Guid idDoConvenioDeAdesao)
		{
			#region Pré-condições

			Assertion.NotNull(preInscritosDoArquivo, "A lista de pré-inscritos não foi informada").Validate();
			IAssertion aListaDePreInscritosContemRegistros = Assertion.GreaterThan(preInscritosDoArquivo.Count, 0, "A lista de pré-inscritos não contém informações");

			#endregion

			aListaDePreInscritosContemRegistros.Validate();

			return ObterPreInscritosExistentesPorCPF(preInscritosDoArquivo, idDoConvenioDeAdesao);
		}

		/// <summary>
		/// Informar os pré-inscritos existentes
		/// </summary>
		/// <param name="preInscritosDoArquivo">lista de preinscritos vinda do arquivo</param>
		private void InformarPreinscritosExistentes(PreInscricaoDTO preInscricaoDTO, List<PreInscrito> preInscritosDoArquivo)
		{
			var listaEncontrados = ObterPreInscritosExistentesPorCPF(preInscritosDoArquivo, preInscricaoDTO.IdDoConvenioDeAdesao);
			preInscricaoDTO.PreInscritosParaAtualizar = listaEncontrados.Count;
		}

		/// <summary>
		/// Obtém uma lista de entidades PreInscrito por CPF
		/// </summary>
		/// <param name="preInscritosDoArquivo"></param>
		/// <returns></returns>
		private List<PreInscrito> ObterPreInscritosExistentesPorCPF(List<PreInscrito> preInscritosDoArquivo, Guid idDoConvenioDeAdesao)
		{
			#region Pré-condições

			Assertion.NotNull(preInscritosDoArquivo, "A lista de pré-inscritos não foi informada").Validate();
			IAssertion aListaDePreInscritosContemRegistros = Assertion.GreaterThan(preInscritosDoArquivo.Count, 0, "A lista de pré-inscritos não contém informações");

			#endregion

			aListaDePreInscritosContemRegistros.Validate();

			List<PreInscrito> listaExistentes = new List<PreInscrito>();
			foreach (PreInscrito preInscritoDoArquivo in preInscritosDoArquivo)
			{
				PreInscrito preInscritoEncontrado = ObterPreInscritoPorCPF(preInscritoDoArquivo.CPFDoParticipante, idDoConvenioDeAdesao);
				if (preInscritoEncontrado != null)
				{
					PrencherNovasInformacoesPeloArquivo(preInscritoEncontrado, preInscritoDoArquivo);
					listaExistentes.Add(preInscritoEncontrado);
				}
			}

			return listaExistentes;
		}

		/// <summary>
		/// Obtém um PreInscrito, se já estiver cadastrado
		/// </summary>
		/// <param name="cpf"></param>
		/// <returns></returns>
		private PreInscrito ObterPreInscritoPorCPF(string cpf, Guid idDoConvenioDeAdesao)
		{
			return _preInscritos.Obter(preInscrito => preInscrito.CPFDoParticipante == cpf.Trim() && preInscrito.IdDoConvenioDeAdesao == idDoConvenioDeAdesao);
		}

		/// <summary>
		/// Informar novos pré-inscritos
		/// </summary>
		/// <param name="quantidadeNoArquivo">pre inscritos do arquivo</param>
		/// <param name="preInscritosDoRepositorio">pre inscritos do repositorio</param>
		private void InformarNovosPreInscritos(PreInscricaoDTO preInscricaoDTO, int quantidadeNoArquivo)
		{
			int preInscritosEncontrados = preInscricaoDTO.PreInscritosParaAtualizar;

			preInscricaoDTO.NovosPreInscritos = quantidadeNoArquivo - preInscritosEncontrados;
		}

		/// <summary>
		/// Preenche o objeto PreInscrito do contexto da Session do NHibernate com as infomações contidas no arquivo
		/// </summary>
		/// <param name="preInscritoPersistido"></param>
		/// <param name="preInscritoDoArquivo"></param>
		private void PrencherNovasInformacoesPeloArquivo(PreInscrito preInscritoPersistido, PreInscrito preInscritoDoArquivo)
		{
			foreach (var propriedade in preInscritoDoArquivo.GetType().GetProperties())
			{
				if (propriedade.Name != "Id")
					preInscritoPersistido.GetType().GetProperty(propriedade.Name).SetValue(preInscritoPersistido, propriedade.GetValue(preInscritoDoArquivo));
			}
		}
	}
}