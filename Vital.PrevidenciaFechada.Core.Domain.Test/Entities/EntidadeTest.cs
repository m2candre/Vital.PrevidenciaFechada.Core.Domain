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
        public void adicionar_convenio_a_entidade()
        {
            _entidade = new Entidade();
            _entidade.Nome = "Entidade_1";

            ConvenioDeAdesao convenio = new ConvenioDeAdesao();
			convenio.Id = Guid.NewGuid();
            
            _entidade.AdicionarConvenio(convenio);

            Assert.AreEqual(_entidade.ConveniosDeAdesao.Count , 1);
        }

        [Test]
        public void buscar_plano_em_entidade()
        {
            _entidade = new Entidade();
            _entidade.Nome = "Entidade_1";

            Guid idDoConvenio = Guid.NewGuid();

			ConvenioDeAdesao convenio = new ConvenioDeAdesao();
            convenio.Id = idDoConvenio;
            
            _entidade.AdicionarConvenio(convenio);

            var retornado = _entidade.BuscarConvenioPorId(idDoConvenio);

            Assert.IsTrue(convenio == retornado);
        }

    }
}