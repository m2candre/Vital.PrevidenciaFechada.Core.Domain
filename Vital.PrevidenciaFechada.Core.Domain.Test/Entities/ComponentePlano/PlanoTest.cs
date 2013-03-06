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
        /// Valida se a criar um novo plano vazio ele já possui um modelo de Proposta em Rascunho disponível
        /// </summary>
        [Test]
		public void novo_plano_ja_possui_modelo_de_proposta_em_modo_rascunho()
		{
			Plano plano = new Plano();
			ModeloDeProposta modeloProposta = plano.ModeloDePropostaEmRascunho;

			Assert.IsNotNull(modeloProposta);
			Assert.IsFalse(modeloProposta.Publicada);
            Assert.AreEqual(modeloProposta.Campos.Count,0);
		}

        /// <summary>
        /// Verifica se modifica o rascunho e publicar altera de fato o estado do Modelo recem publicado
        /// </summary>
        [Test]
		public void publicar_modelo_proposta_a_partir_do_rascunho()
		{
			var plano = new Plano();
		    var modeloPropostaRascunho = plano.ModeloDePropostaEmRascunho;
			var campo1 = modeloPropostaRascunho.AdicionarCampo("Nome");
            var campo2 = modeloPropostaRascunho.AdicionarCampo("CPF");
            var campo3 = modeloPropostaRascunho.AdicionarCampo("Identidade");
			
			plano.PublicarModeloDeProposta();

		    var modeloPropostaPublicada = plano.ModeloDePropostaAtual;
			Assert.IsNotNull(modeloPropostaPublicada);
            Assert.AreEqual(modeloPropostaPublicada.Campos.Count,3);

            Assert.IsTrue(modeloPropostaPublicada.ExisteCampoComNome("Nome"));
            Assert.IsTrue(modeloPropostaPublicada.ExisteCampoComNome("CPF"));
            Assert.IsTrue(modeloPropostaPublicada.ExisteCampoComNome("Identidade"));
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