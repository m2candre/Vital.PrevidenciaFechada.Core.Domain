using System;
using System.Collections.Generic;
using NHibernate;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using NHibernate.Criterion;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    public class Repositorio<T> : BaseRepository, IRepositorio<T>
    {
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

        public IList<T> FiltrarTodos(ConsultaDTO consulta)
        {
            var criteria = Session.CreateCriteria(typeof(T));

            foreach (var filtro in consulta.Filtros)
                criteria.Add(Expression.Like(filtro.Campo, filtro.Valor, MatchMode.Anywhere));

            Order order = new Order(consulta.CampoOrdenacao, consulta.OrdemCrescente);

            criteria.AddOrder(order);

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
