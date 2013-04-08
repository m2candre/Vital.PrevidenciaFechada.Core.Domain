using System;
using NUnit.Framework;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using System.Linq;
namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponentePlano
{
    [TestFixture]
    public class ModeloDePropostaTest
    {
        [Test]
        public void valida_valores_default_para_modelo_em_Rascunho()
        {
            var modeloProposta = new ModeloDeProposta();

            Assert.AreEqual(modeloProposta.Campos.Count, 0);
            Assert.IsNull(modeloProposta.DataDePublicacao);
            Assert.IsFalse(modeloProposta.Publicada);
        }

        [Test]
        public void adicionar_campo()
        {
            var modeloProposta = new ModeloDeProposta();
            modeloProposta.AdicionarCampo("Nome completo");
            Assert.AreEqual(modeloProposta.Campos.Count, 1);
            Assert.IsTrue(modeloProposta.ExisteCampoComNome("Nome completo"));
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void adicionar_campo_com_nome_vazio_lança_exception()
        {
            var modeloProposta = new ModeloDeProposta();
            modeloProposta.AdicionarCampo("");
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void adicionar_campo_com_nome_ja_existente_lança_exception()
        {
            var modeloProposta = new ModeloDeProposta();
            modeloProposta.AdicionarCampo("Nome");
            modeloProposta.AdicionarCampo("Nome");
        }

        [Test]
        public void remover_campo()
        {
            var modeloProposta = new ModeloDeProposta();
            var campoCpf = modeloProposta.AdicionarCampo("CPF");
            modeloProposta.AdicionarCampo("Nome");

            Assert.AreEqual(modeloProposta.Campos.Count,2);

            modeloProposta.RemoverCampo(campoCpf);

            Assert.AreEqual(modeloProposta.Campos.Count, 1);
            Assert.IsFalse(modeloProposta.ExisteCampoComNome("CPF"));
        }

        [Test]
        public void existe_campo_pelo_nome()
        {
            var modeloProposta = new ModeloDeProposta();
            modeloProposta.AdicionarCampo("CPF");

            Assert.IsTrue(modeloProposta.ExisteCampoComNome("CPF"));

        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void obter_campo_por_id_nao_existente_lança_expeption()
        {
            var campoNaoExiste = new CampoDeProposta("teste");
            var modelo = new ModeloDeProposta();
            modelo.RemoverCampo(campoNaoExiste);
        }

        [Test]
        public void obter_campo_do_modelo_de_proposta_com_sucesso()
        {
            var modelo = new ModeloDeProposta();

            modelo.AdicionarCampo("nome");

            Guid id = modelo.Campos.First().Id;

            var campo = modelo.ObterCampoPeloId(id);

            Assert.That(campo.Id, Is.EqualTo(id));
        }

		[Test]
		public void copiar_modelo_para_rascunho()
		{
			var modeloProposta = new ModeloDeProposta();
			modeloProposta.AdicionarCampo("Nome");
			modeloProposta.AdicionarCampo("Identidade");

			var modeloCopiado = modeloProposta.CopiarParaRascunho();

			Assert.AreNotSame(modeloProposta, modeloCopiado);
			Assert.That(modeloProposta.Campos.Count, Is.EqualTo(modeloCopiado.Campos.Count));
		}
    }
}