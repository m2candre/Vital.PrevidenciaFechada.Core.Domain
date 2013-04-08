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
        public void criar_novo_convenio()
        {
			_entidade.AdicionarConvenio(new ConvenioDeAdesao { Id = Guid.NewGuid(), Plano = new Plano { Nome = "Teste_Plano" } });
			Assert.IsNotNull(_entidade.ConveniosDeAdesao.FirstOrDefault(convenio => convenio.Plano.Nome == "Teste_Plano"));
        }
    }
}