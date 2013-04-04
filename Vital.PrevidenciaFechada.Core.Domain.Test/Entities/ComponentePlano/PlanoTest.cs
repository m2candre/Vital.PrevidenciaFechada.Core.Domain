using System;
using System.Linq;
using NUnit.Framework;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponentePlano
{
    [TestFixture]
    public class PlanoTest
    {
        private Guid _guid;
        private Entidade _entidade = null;

        [TestFixtureSetUp]
        public void Setup()
        {
            _guid = Guid.NewGuid();

            _entidade = new Entidade();
            _entidade.Id = Guid.NewGuid();
            _entidade.Nome = "Entidade_1";
        }

        /// <summary>
        /// Cria um novo plano e valida o nome
        /// </summary>
        [Test]
        public void criar_novo_plano()
        {
            _entidade.AdicionarPlano(new Plano { Nome = "Plano_1" });
            Assert.IsNotNull(_entidade.Planos.FirstOrDefault(plano => plano.Nome == "Plano_1"));
        }
    }
}