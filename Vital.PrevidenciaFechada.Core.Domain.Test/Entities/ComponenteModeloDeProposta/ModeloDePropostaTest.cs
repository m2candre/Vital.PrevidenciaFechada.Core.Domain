using System;
using NUnit.Framework;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using System.Linq;
namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteModeloDeProposta
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

            var campo = new CampoDeProposta {Nome = "Nome completo"};
            modeloProposta.AdicionarCampo(campo);
            Assert.AreEqual(modeloProposta.Campos.Count, 1);
            Assert.IsTrue(modeloProposta.ExisteCampoComNome("Nome completo"));
        }



        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void adicionar_campo_com_nome_ja_existente_lança_exception()
        {
            var modeloProposta = new ModeloDeProposta();
            var campo = new CampoDeProposta { Nome = "Nome completo" };
            modeloProposta.AdicionarCampo(campo);
            modeloProposta.AdicionarCampo(campo);
        }

        [Test]
        public void remover_campo()
        {
            var modeloProposta = new ModeloDeProposta();
            var campo = new CampoDeProposta { Nome = "Nome completo" };
            modeloProposta.AdicionarCampo(campo);


            Assert.AreEqual(modeloProposta.Campos.Count,1);

            modeloProposta.RemoverCampo(campo);

            Assert.AreEqual(modeloProposta.Campos.Count, 0);
            Assert.IsFalse(modeloProposta.ExisteCampoComNome("Nome completo"));
        }

        [Test]
        public void existe_campo_pelo_nome()
        {
            var modeloProposta = new ModeloDeProposta();
            var campo = new CampoDeProposta { Nome = "Nome completo" };
            modeloProposta.AdicionarCampo(campo);

            Assert.IsTrue(modeloProposta.ExisteCampoComNome("Nome completo"));

        }

        [Test]
        public void obter_campo_do_modelo_de_proposta_com_sucesso()
        {
            Guid idNovo = Guid.NewGuid();

            var modelo = new ModeloDeProposta();
            var campo = new CampoDeProposta { Id = idNovo, Nome = "Nome completo" };
            modelo.AdicionarCampo(campo);

            Guid id = modelo.Campos.First().Id;

            var campoObtido = modelo.ObterCampoPeloId(id);

            Assert.That(campoObtido.Id, Is.EqualTo(idNovo));
        }

    }
}