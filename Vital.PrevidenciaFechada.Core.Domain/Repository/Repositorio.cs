using NHibernate;
using CriteriaExpression = NHibernate.Criterion.Expression;
using CriteriaOrder = NHibernate.Criterion.Order;
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
		//#region REFATORAR e REMOVER
		
		//public Repositorio()
		//{

		//}

		//public void Adicionar(IAggregateRoot<Guid> entidade)
		//{
		//	base.Salvar(entidade);
		//}

		//public void AdicionarLista(List<IAggregateRoot<Guid>> entidades)
		//{
		//	base.Salvar(entidades);
		//}

		//public void Remover(IAggregateRoot<Guid> entidade)
		//{
		//	base.Deletar(entidade);
		//}

		//public T PorId(Guid id)
		//{
		//	return base.Obter<T>(id);
		//}

		//public IList<T> Todos()
		//{
		//	return base.Todos<T>();
		//}

		//public IList<T> Todas()
		//{
		//	return base.Todos<T>();
		//}

		///// <summary>
		///// Obtem Todos os objetos filtrados e paginados de acordo com um critério específico
		///// </summary>
		///// <typeparam name="T">Objeto a ser filtrado</typeparam>
		///// <param name="criterios">criterios</param>
		///// <param name="consulta">parametros de consulta</param>
		///// <returns>Lista de objetos</returns>
		//public IList<T> ObterTodosFiltradosComCriterio<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios, ConsultaDTO consulta) where T : class
		//{
		//	var criteria = Session.CreateCriteria(typeof(T))
		//		 .Add(VitalCriterion.Where<T>(criterios))
		//		 .SetFirstResult(((consulta.PaginaAtual - 1) * consulta.Linhas))
		//		 .SetMaxResults(consulta.Linhas);

		//	if (consulta.CampoOrdenacao != null)
		//		criteria.AddOrder(VitalCriterion.OrderBy(consulta.CampoOrdenacao, consulta.OrdemCrescente));

		//	return criteria.List<T>();
		//}

		///// <summary>
		///// Obtém uma lista de objetos de acordo com um ou mais critérios
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="criterios"></param>
		///// <returns></returns>
		//IList<T> IRepositorio<T>.ObterListaPor<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios)
		//{
		//	var criteria = Session.CreateCriteria(typeof(T))
		//		 .Add(VitalCriterion.Where<T>(criterios)).List<T>();

		//	return criteria;
		//}

		///// <summary>
		///// Obtém um objeto de acordo com um critério específico
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="criterios"></param>
		///// <returns></returns>
		//public T ObterPor<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios) where T : class
		//{
		//	var criteria = Session.CreateCriteria(typeof(T))
		//		 .Add(VitalCriterion.Where<T>(criterios));

		//	return criteria.UniqueResult<T>();
		//}

		///// <summary>
		///// Filtrar todos 
		///// </summary>
		///// <param name="consulta">objeto de consulta</param>
		///// <returns>Lista de Objetos</returns>
		//public IList<T> FiltrarTodos(ConsultaDTO consulta)
		//{
		//	var criterios = Session.CreateCriteria(typeof(T));

		//	foreach (var filtro in consulta.Filtros)
		//	{
		//		var especificacoesDeConsulta = new EspecificacaoAdicionarClausulasDeWhereParaCamposDeId(filtro).Or(
		//			 new EspecificacaoAdicionarClausulaLikeParaCamposDeTexto(filtro));

		//		especificacoesDeConsulta.MontarCriterios(criterios);
		//	}

		//	if (consulta.CampoOrdenacao != null)
		//		criterios.AddOrder(VitalCriterion.OrderBy(consulta.CampoOrdenacao, consulta.OrdemCrescente));

		//	return criterios.List<T>();
		//}

		///// <summary>
		///// Obtém lista de objetos de acordo com filtros e navegação em páginas
		///// </summary>
		///// <param name="consulta"></param>
		///// <returns></returns>
		//public IList<T> FiltrarPaginandoTodos(ConsultaDTO consulta)
		//{
		//	var criteria = Session.CreateCriteria(typeof(T))
		//		.SetFirstResult(((consulta.PaginaAtual - 1) * consulta.Linhas))
		//		.SetMaxResults(consulta.Linhas);

		//	foreach (var filtro in consulta.Filtros)
		//	{
		//		var especificacoesDeConsulta = new EspecificacaoAdicionarClausulasDeWhereParaCamposDeId(filtro).Or(
		//			new EspecificacaoAdicionarClausulaLikeParaCamposDeTexto(filtro));

		//		especificacoesDeConsulta.MontarCriterios(criteria);
		//	}

		//	if (consulta.CampoOrdenacao != null)
		//	{
		//		Order order = new Order(consulta.CampoOrdenacao, consulta.OrdemCrescente);
		//		criteria.AddOrder(order);
		//	}

		//	return criteria.List<T>();
		//}

		//#endregion

		/// <summary>
		/// Obtém uma entidade de acordo com os critérios informados
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <returns></returns>
		public T Obter(Expression<Func<T, bool>> criterios)
		{
			var criteria = Session.CreateCriteria(typeof(T)).Add(CriteriaExpression.Where<T>(criterios));
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
			var criteria = Session.CreateCriteria(typeof(T)).Add(CriteriaExpression.Where<T>(criterios));
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
								  .Add(CriteriaExpression.Where<T>(criterios));
			return criteria;
		}

		/// <summary>
		/// Configurar ICriteria com dados de paginação, se tiver sido informado
		/// </summary>
		/// <param name="consulta"></param>
		/// <param name="criteria"></param>
		private void ConfigurarCriteriosComPaginacao<T>(ConsultaDTO consulta, ICriteria criteria)
		{
			if (PaginacaoEstaConfigurada(consulta.Paginacao))
			{
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
			foreach (var filtro in consulta.Filtros)
			{
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
				criteria.AddOrder(new CriteriaOrder(consulta.CampoOrdenacao, consulta.OrdemCrescente));
		}	
	}
}