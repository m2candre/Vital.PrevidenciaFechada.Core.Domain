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
    }
}