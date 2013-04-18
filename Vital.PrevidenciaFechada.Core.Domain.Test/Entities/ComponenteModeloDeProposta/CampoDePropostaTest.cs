using System.Collections.Generic;
using NUnit.Framework;
using System;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using System.Linq;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteModeloDeProposta
{
    [TestFixture]
    public class CampoDePropostaTest
    {
        private CampoDeProposta _campoDeProposta;
        private readonly IList<ModeloDoCampo> _listaDeModelosDeCampos = new List<ModeloDoCampo>();

        [TestFixtureSetUp]
        public void inicializar()
        {
            _campoDeProposta = new CampoDeProposta
                                   {
                                       Id = Guid.NewGuid(),
                                       Nome = "Nome",
                                       Titulo = "Nome do proponente",
                                       TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45)
                                   };

            gera_modelos_de_campos();
        }

        /// <summary>
        /// Gera os modelos de campos
        /// </summary>
        private void gera_modelos_de_campos()
        {
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Título", "<div class='@Css'><h1>@titulo</h1></div>", "<div class='@Css'><h1>@titulo</h1></div>"));
        }

        [Test]
        public void renderizar_campo_com_modelo_titulo_no_formulario()
        {
            _campoDeProposta.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Título");

            Assert.That(_campoDeProposta.RenderizarParaFormulario(), Is.EqualTo("<div class='field45'><h1>Nome do proponente</h1></div>"));
        }

        [Test]
        public void renderizar_campo_com_modelo_titulo_na_impressao()
        {
            _campoDeProposta.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Título");

            Assert.That(_campoDeProposta.RenderizarParaImpressao(), Is.EqualTo("<div class='field45'><h1>Nome do proponente</h1></div>"));
        }
    }
}
