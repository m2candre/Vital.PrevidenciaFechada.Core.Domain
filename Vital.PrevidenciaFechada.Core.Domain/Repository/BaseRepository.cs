using NHibernate;
using System;
using System.Collections.Generic;
using Vital.InfraStructure.Persistence.Session;
using Vital.PrevidenciaFechada.Core.Domain.Entities;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    public abstract class BaseRepository<T>
    {
        private ISession _session;
        public virtual ISession Session
        {
            get
			{
				if (_session == null)
				{
					_session = SessionProvider.GetCurrentSession();
				}
				return _session;
			}
			set { _session = value; }
        }

        #region Métodos Genericos para acesso ao DB

        public BaseRepository()
        {

        }

        public virtual void Salvar(IAggregateRoot<Guid> entidade)
        {
            Session.SaveOrUpdate(entidade);
        }

        public virtual void Salvar(List<IAggregateRoot<Guid>> listaDeEntidades)
        {
            foreach (var entidade in listaDeEntidades)
            {
                Session.SaveOrUpdate(entidade);
            }
        }

        public virtual void Remover(IAggregateRoot<Guid> entidade)
        {
            Session.Delete(entidade);
        }

        public virtual IList<T> Todos()
        {
            var objs = Session.CreateCriteria(typeof(T)).List<T>();
            return objs;
        }

		public virtual T ObterPorId(Guid id)
        {
            var obj = Session.Get<T>(id);
            return obj;
        }

        #endregion
    }
}