using System;
using System.Linq;
using NUnit.Framework;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;

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

		[Test]
		public void adicionar_tipo_de_documento()
		{
			var plano = new Plano();
			plano.AdicionarTipoDeDocumento(new TipoDeDocumento { Descricao = "Cpf" });
			plano.AdicionarTipoDeDocumento(new TipoDeDocumento { Descricao = "Identidade" });

			Assert.That(plano.TiposDeDocumento.Count, Is.EqualTo(2));
			Assert.That(plano.TiposDeDocumento[0].Descricao, Is.EqualTo("Cpf"));
			Assert.That(plano.TiposDeDocumento[1].Descricao, Is.EqualTo("Identidade"));
		}

		[Test]
		public void adicionar_tipo_de_documento_quando_lista_esta_nula()
		{
			var plano = new Plano();
			plano.TiposDeDocumento = null;
			plano.AdicionarTipoDeDocumento(new TipoDeDocumento { Descricao = "Identidade" });

			Assert.That(plano.TiposDeDocumento, Is.Not.Null);
			Assert.That(plano.TiposDeDocumento.Count, Is.EqualTo(1));
		}

		[Test]
		public void existe_tipo_de_documento_retorna_true()
		{
			var plano = new Plano();
			plano.AdicionarTipoDeDocumento(new TipoDeDocumento { Descricao = "Cpf" });
			plano.AdicionarTipoDeDocumento(new TipoDeDocumento { Descricao = "Identidade" });

			Assert.IsTrue(plano.ExisteTipoDeDocumentoComONome("Cpf"));
		}

		[Test]
		public void existe_tipo_de_documento_retorna_false()
		{
			var plano = new Plano();
			plano.AdicionarTipoDeDocumento(new TipoDeDocumento { Descricao = "Cpf" });
			plano.AdicionarTipoDeDocumento(new TipoDeDocumento { Descricao = "Identidade" });

			Assert.IsFalse(plano.ExisteTipoDeDocumentoComONome("Conta de luz"));
		}
    }
}