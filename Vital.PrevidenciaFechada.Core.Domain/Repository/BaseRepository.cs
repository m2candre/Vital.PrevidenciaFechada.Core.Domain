using System.Collections.Generic;
using NHibernate;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using System;
using Vital.InfraStructure.Persistence.SessionManagement;
using Vital.InfraStructure.Persistence.Session;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    public abstract class BaseRepository
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

        public virtual void Salvar(IAggregateRoot<Guid> root)
        {
            Session.SaveOrUpdate(root);
        }

        public virtual void SalvarLista(List<IAggregateRoot<Guid>> roots)
        {
            foreach (var root in roots)
            {
                Session.SaveOrUpdate(root);
            }
        }

        public virtual void Deletar(IAggregateRoot<Guid> root)
        {
            Session.Delete(root);
        }

        public virtual IList<T> Todos<T>()
        {
            var objs = Session.CreateCriteria(typeof(T)).List<T>();
            return objs;
        }

        public virtual T Obter<T>(Guid id)
        {
            var obj = Session.Get<T>(id);
            return obj;
        }

        #endregion
    }
}
