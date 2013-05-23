using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using Vital.PrevidenciaFechada.Core.Domain.Repository.QuerySpecification;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    /// <summary>
    /// Repositório concreto genérico
    /// </summary>
    /// <typeparam name="T">Tipo do Repositório</typeparam>
    public class Repositorio<T> : BaseRepository, IRepositorio<T>
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

        public Repositorio()
        {

        }

        public void Adicionar(IAggregateRoot<Guid> entidade)
        {
            base.Salvar(entidade);
        }

        public void AdicionarLista(List<IAggregateRoot<Guid>> entidades)
        {
            base.SalvarLista(entidades);
        }

        public void Remover(IAggregateRoot<Guid> entidade)
        {
            base.Deletar(entidade);
        }

        public T PorId(Guid id)
        {
            return base.Obter<T>(id);
        }

        public IList<T> Todos()
        {
            return base.Todos<T>();
        }

        public IList<T> Todas()
        {
            return base.Todos<T>();
        }

        /// <summary>
        /// Obtem Todos os objetos filtrados e paginados de acordo com um critério específico
        /// </summary>
        /// <typeparam name="T">Objeto a ser filtrado</typeparam>
        /// <param name="criterios">criterios</param>
        /// <param name="consulta">parametros de consulta</param>
        /// <returns>Lista de objetos</returns>
        public IList<T> ObterTodosFiltradosComCriterio<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios, ConsultaDTO consulta) where T : class
        {
			var criteria = Session.CreateCriteria(typeof(T))
				 .Add(VitalCriterion.Where<T>(criterios))
				 .SetFirstResult(((consulta.PaginaAtual - 1) * consulta.Linhas))
				 .SetMaxResults(consulta.Linhas);

			if (consulta.CampoOrdenacao != null)
				criteria.AddOrder(VitalCriterion.OrderBy(consulta.CampoOrdenacao, consulta.OrdemCrescente));

            return criteria.List<T>();
        }

		/// <summary>
		/// Obtém uma lista de objetos de acordo com um ou mais critérios
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <returns></returns>
		IList<T> IRepositorio<T>.ObterListaPor<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios)
		{
			var criteria = Session.CreateCriteria(typeof(T))
                 .Add(VitalCriterion.Where<T>(criterios)).List<T>();

			return criteria;
		}

        /// <summary>
        /// Obtém um objeto de acordo com um critério específico
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criterios"></param>
        /// <returns></returns>
        public T ObterPor<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios) where T : class
        {
            var criteria = Session.CreateCriteria(typeof(T))
                 .Add(VitalCriterion.Where<T>(criterios));

            return criteria.UniqueResult<T>();
        }

        /// <summary>
        /// Filtrar todos 
        /// </summary>
        /// <param name="consulta">objeto de consulta</param>
        /// <returns>Lista de Objetos</returns>
        public IList<T> FiltrarTodos(ConsultaDTO consulta)
        {
            var criterios = Session.CreateCriteria(typeof(T));

            foreach (var filtro in consulta.Filtros)
            {
                var especificacoesDeConsulta = new EspecificacaoAdicionarClausulasDeWhereParaCamposDeId(filtro).Or(
                     new EspecificacaoAdicionarClausulaLikeParaCamposDeTexto(filtro));

                especificacoesDeConsulta.MontarCriterios(criterios);
            }

			if (consulta.CampoOrdenacao != null)
				criterios.AddOrder(VitalCriterion.OrderBy(consulta.CampoOrdenacao, consulta.OrdemCrescente));

            return criterios.List<T>();
        }

        /// <summary>
        /// Obtém lista de objetos de acordo com filtros e navegação em páginas
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public IList<T> FiltrarPaginandoTodos(ConsultaDTO consulta)
        {
            var criteria = Session.CreateCriteria(typeof(T))
                .SetFirstResult(((consulta.PaginaAtual - 1) * consulta.Linhas))
                .SetMaxResults(consulta.Linhas);

            foreach (var filtro in consulta.Filtros)
            {
                var especificacoesDeConsulta = new EspecificacaoAdicionarClausulasDeWhereParaCamposDeId(filtro).Or(
                    new EspecificacaoAdicionarClausulaLikeParaCamposDeTexto(filtro));

                especificacoesDeConsulta.MontarCriterios(criteria);
            }

			if (consulta.CampoOrdenacao != null)
			{
				Order order = new Order(consulta.CampoOrdenacao, consulta.OrdemCrescente);
				criteria.AddOrder(order);
			}

            return criteria.List<T>();
        }
	}
}