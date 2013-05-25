using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vital.PrevidenciaFechada.Core.Domain.Repository.QuerySpecification;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
	/// <summary>
	/// Repositório concreto genérico
	/// </summary>
	/// <typeparam name="T">Tipo do Repositório</typeparam>
	public class Repositorio<T> : BaseRepository<T>, IRepositorio<T>
	{
		private VitalCriterion _vitalCriterion;

		/// <summary>
		/// Wrapper de ICriterion da Vital
		/// </summary>
		public VitalCriterion VitalCriterion
		{
			get
			{
				if (_vitalCriterion == null)
					_vitalCriterion = new VitalCriterion();

				return _vitalCriterion;
			}
			set { _vitalCriterion = value; }
		}

		/// <summary>
		/// Obtém uma entidade de acordo com os critérios informados
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <returns></returns>
		public T Obter(Expression<Func<T, bool>> criterios)
		{
			var criteria = Session.CreateCriteria(typeof(T)).Add(VitalCriterion.Where<T>(criterios));
			return criteria.UniqueResult<T>();
		}

		/// <summary>
		/// Obtém uma entidade de acordo com os critérios e dados de consulta informados
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <param name="consulta"></param>
		/// <returns></returns>
		public T Obter(Expression<Func<T, bool>> criterios, ConsultaDTO consulta)
		{
			var criteria = MontarCriterios<T>(criterios);
			ConfigurarCriteriosComPaginacao<T>(consulta, criteria);
			ConfigurarFiltros(consulta, criteria);
			ConfigurarOrdenacao(consulta, criteria);

			return criteria.UniqueResult<T>();
		}

		/// <summary>
		/// Obtém uma lista de entidades de acordo com os critérios informados
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <returns></returns>
		public IList<T> ObterLista(Expression<Func<T, bool>> criterios)
		{
			var criteria = Session.CreateCriteria(typeof(T)).Add(VitalCriterion.Where<T>(criterios));
			return criteria.List<T>();
		}

		/// <summary>
		/// Obtém uma lista de entidades de acordo com os critérios e dados de consulta informados
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <param name="consulta"></param>
		/// <returns></returns>
		public IList<T> ObterLista(Expression<Func<T, bool>> criterios, ConsultaDTO consulta)
		{
			var criteria = MontarCriterios<T>(criterios);
			ConfigurarCriteriosComPaginacao<T>(consulta, criteria);
			ConfigurarFiltros(consulta, criteria);
			ConfigurarOrdenacao(consulta, criteria);

			return criteria.List<T>();
		}

		/// <summary>
		/// Monta um ICriteria com a expressão de critérios informada
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <returns></returns>
		private ICriteria MontarCriterios<T>(Expression<Func<T, bool>> criterios)
		{
			var criteria = Session.CreateCriteria(typeof(T))
								  .Add(VitalCriterion.Where<T>(criterios));
			return criteria;
		}

		/// <summary>
		/// Configurar ICriteria com dados de paginação, se tiver sido informado
		/// </summary>
		/// <param name="consulta"></param>
		/// <param name="criteria"></param>
		private void ConfigurarCriteriosComPaginacao<T>(ConsultaDTO consulta, ICriteria criteria)
		{
			if (PaginacaoEstaConfigurada(consulta.Paginacao)) {
				SetarResultadosDaPaginacao<T>(criteria, consulta.Paginacao);
				criteria.SetFirstResult(((consulta.Paginacao.PaginaAtual - 1) * consulta.Paginacao.Linhas))
						.SetMaxResults(consulta.Paginacao.Linhas);
			}
		}

		/// <summary>
		/// Obtém todos os resultados, sem paginação, para calcular a quantidade de páginas necessárias para exibir os resultados
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="paginacaoDTO"></param>
		private void SetarResultadosDaPaginacao<T>(ICriteria criteria, PaginacaoDTO paginacao)
		{
			paginacao.TotalDeRegistros = criteria.List<T>().Count;
			paginacao.TotalDePaginas = (int)Math.Ceiling((float)paginacao.TotalDeRegistros / (float)paginacao.Linhas);
		}

		/// <summary>
		/// Verifica se o objeto paginação e sua parâmetros principais foram setados
		/// </summary>
		/// <param name="paginacao"></param>
		/// <returns></returns>
		private bool PaginacaoEstaConfigurada(PaginacaoDTO paginacao)
		{
			return paginacao != null && paginacao.Linhas > 0 && paginacao.PaginaAtual > 0;
		}

		/// <summary>
		/// Configurar ICriteria com filtros usando QuerySpecification, se houver filtro informado
		/// </summary>
		/// <param name="consulta"></param>
		/// <param name="criteria"></param>
		private static void ConfigurarFiltros(ConsultaDTO consulta, ICriteria criteria)
		{
			foreach (var filtro in consulta.Filtros) {
				var especificacoesDeConsulta = new EspecificacaoAdicionarClausulasDeWhereParaCamposDeId(filtro).Or(
					new EspecificacaoAdicionarClausulaLikeParaCamposDeTexto(filtro));

				especificacoesDeConsulta.MontarCriterios(criteria);
			}
		}

		/// <summary>
		/// Configurar ICriteria campo de ordenação, se foi informado
		/// </summary>
		/// <param name="consulta"></param>
		/// <param name="criteria"></param>
		private void ConfigurarOrdenacao(ConsultaDTO consulta, ICriteria criteria)
		{
			if (consulta.CampoOrdenacao != null)
				criteria.AddOrder(VitalCriterion.OrderBy(consulta.CampoOrdenacao, consulta.OrdemCrescente));
		}
	}
}