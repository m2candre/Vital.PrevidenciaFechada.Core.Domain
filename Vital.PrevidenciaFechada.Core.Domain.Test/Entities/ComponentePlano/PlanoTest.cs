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
    }
}