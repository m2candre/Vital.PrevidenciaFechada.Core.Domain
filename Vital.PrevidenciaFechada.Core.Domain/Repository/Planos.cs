using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Vital.Atuarial.Domain.Entities.ComponentePlano;
using Vital.Atuarial.Domain.Repository.SessionStrategy;

namespace Vital.Atuarial.Domain.Repository
{
    public class Planos : BaseRepository, IPlanos
    {
        public Planos()
        {

        }

        public Planos(ISession session)
            : base(session)
        {

        }
    }
}
