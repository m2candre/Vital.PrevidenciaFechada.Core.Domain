using System;
using System.Collections.Generic;
using NHibernate;
using Vital.PrevidenciaFechada.Core.Domain.Entities;

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
    }
}
