using System;
using NUnit.Framework;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities
{
    [TestFixture]
    public class EntidadeTest
    {
        private Entidade _entidade = null;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _entidade = new Entidade();
            _entidade.Nome = "Entidade_1";
        }

        [Test]
        public void criar_nova_entidade()
        {
            _entidade = new Entidade();
            _entidade.Nome = "Entidade_1";

            Assert.IsNotNull(_entidade);
        }

        [Test]
        public void adicionar_plano_a_entidade()
        {
            _entidade = new Entidade();
            _entidade.Nome = "Entidade_1";

            Plano plano = new Plano();
            plano.Nome = "Plano_1";
            
            _entidade.AdicionarPlano(plano);

            Assert.AreEqual(_entidade.Planos.Count , 1);
        }

        [Test]
        public void buscar_plano_em_entidade()
        {
            _entidade = new Entidade();
            _entidade.Nome = "Entidade_1";

            Guid idDoPlano = Guid.NewGuid();

            Plano plano = new Plano();
            plano.Id = idDoPlano;
            plano.Nome = "Plano_1";

            _entidade.AdicionarPlano(plano);

            var retornado = _entidade.BuscarPlanoCom(idDoPlano);

            Assert.IsTrue(plano == retornado);
        }

    }
}