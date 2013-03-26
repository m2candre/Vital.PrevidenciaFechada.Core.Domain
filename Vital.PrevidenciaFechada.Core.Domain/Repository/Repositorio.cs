using System;
using System.Collections.Generic;
using NHibernate;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using NHibernate.Criterion;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
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

        public Repositorio(ISession session)
            : base(session)
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
                 .SetMaxResults(consulta.Linhas)     
                 .AddOrder(VitalCriterion.OrderBy(consulta.CampoOrdenacao, consulta.OrdemCrescente));

            return criteria.List<T>();
        }

        /// <summary>
        /// Filtrar todos 
        /// </summary>
        /// <param name="consulta">objeto de consulta</param>
        /// <returns>Lista de Objetos</returns>
        public IList<T> FiltrarTodos(ConsultaDTO consulta)
        {
            var criteria = Session.CreateCriteria(typeof(T));

            foreach (var filtro in consulta.Filtros)
                criteria.Add(VitalCriterion.Like(filtro.Campo, filtro.Valor, MatchMode.Anywhere));

            criteria.AddOrder(VitalCriterion.OrderBy(consulta.CampoOrdenacao, consulta.OrdemCrescente));

            return criteria.List<T>();
        }

        public IList<T> FiltrarPaginandoTodos(ConsultaDTO consulta)
        {
            var criteria = Session.CreateCriteria(typeof(T))
                .SetFirstResult(((consulta.PaginaAtual - 1) * consulta.Linhas))
                .SetMaxResults(consulta.Linhas);

            foreach (var filtro in consulta.Filtros)
                criteria.Add(Expression.Like(filtro.Campo, filtro.Valor, MatchMode.Anywhere));

            Order order = new Order(consulta.CampoOrdenacao, consulta.OrdemCrescente);

            criteria.AddOrder(order);

            return criteria.List<T>();
        }
    }
}
