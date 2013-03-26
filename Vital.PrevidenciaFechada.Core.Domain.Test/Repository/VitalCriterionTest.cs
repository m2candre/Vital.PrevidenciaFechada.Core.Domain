using NHibernate.Criterion;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Repository
{
    [TestFixture]
    public class VitalCriterionTest
    {
        private VitalCriterion _vitalCriterion;

        [SetUp]
        public void inicializar()
        {
            _vitalCriterion = new VitalCriterion();
        }

        [Test]
        public void obtendo_clausula_where_com_sucesso()
        { 
            Expression<Func<Proposta, bool>> criterio = p => p.Estado == "Iniciada";

            var criterion = _vitalCriterion.Where<Proposta>(criterio);

            Assert.NotNull(criterion);
            Assert.IsInstanceOf<ICriterion>(criterion);
        }

        [Test]
        public void obtendo_clausula_like_com_sucesso()
        {
            var criterion = _vitalCriterion.Like("Nome", "Fulano", MatchMode.Anywhere);
            
            Assert.NotNull(criterion);
            Assert.IsInstanceOf<ICriterion>(criterion);
        }

        [Test]
        public void obtendo_objeto_de_ordenacao_com_sucesso()
        {
            var order = _vitalCriterion.OrderBy("Nome", true);

            Assert.NotNull(order);
            Assert.IsInstanceOf<Order>(order);
        }
    }
}
