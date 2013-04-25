using System;
using System.Collections.Generic;
using NUnit.Framework;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using System.Linq;
namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteModeloDeProposta
{
    [TestFixture]
    public class ModeloDePropostaTest
    {
        private CampoDeProposta _campoDeProposta;
        private readonly IList<ModeloDoCampo> _listaDeModelosDeCampos = new List<ModeloDoCampo>();

        [TestFixtureSetUp]
        public void inicializar()
        {
            gera_modelos_de_campos();
        }

        private void gera_modelos_de_campos()
        {
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Título", "<div class='@Css'><h1>@titulo</h1></div>", "<div class='@Css'><h1>@titulo</h1></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Logo", "<div class='@Css'><img class='logo @alinhamento' src='@valor' /></div>", "<div class='@Css'><img class='logo pull-right' src='@valor' /></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Estático", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Título secundário", "<h2>@titulo</h2>", "<h2>@titulo</h2>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Título secundário sem sublinhado", "<h3>@titulo</h3>", "<h3>@titulo</h3>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Container de texto", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' value='@valor'/></span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Seleção única", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><ul class='inline'><li><label><input type='radio' name='[@indice].Valor' value='@valor'/>@rotulo</label></li></ul></span></div>", "<div class='@Css'><span>@titulo</span><span><ul class='inline'><li>@rotulo</li></ul></span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Texto livre", "<div class='@Css'><span class='internal'>@valor</span></div>", "<div class='@Css'><span class='internal'>@valor</span></div>"));
            
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Número", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' class='numero' value='@valor'/></span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("CEP", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' class='cep' value='@valor'/></span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("CPF", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' class='cpf' value='@valor'/></span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Data", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' class='data' value='@valor'/></span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Telefone", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' class='telefone' value='@valor'/></span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Campo com Sufixo", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' value='@valor'/></span><div class='text pull-right'>@sufixo</div></div>", "<div class='@css'><span>@titulo</span><span>@valor</span><div class='text pull-right'> @sufixo</div></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Campo com Prefixo", "<div class='@Css'><span>@titulo</span><span>@prefixo <input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' value='@valor'/></span></div>", "<div class='@css'><span>@titulo</span><span>@sufixo @valor</span></div> "));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Linha com Checkbox", "<div class='@Css'><label><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='checkbox' name='[@indice].Valor' value='@valor'/>@titulo</label></div>", "<div class=\"line-check checked\">@valor</div>  "));
        }

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


        [Test]
        [Ignore]
        public void renderizar_formulario_sem_dados()
        {
            var modeloProposta = new ModeloDeProposta();

            var _nomeDoProponente = new CampoDeProposta
            {
                Id = Guid.NewGuid(),
                Nome = "Nome",
                Titulo = "Nome do proponente",
                TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45),
                OrdemFormulario = 0,
                OrdemImpressao = 0
            };
            _nomeDoProponente.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");

            modeloProposta.AdicionarCampo(_nomeDoProponente);

            var _cpf = new CampoDeProposta
            {
                Id = Guid.NewGuid(),
                Nome = "CPF",
                Titulo = "Nome do proponente",
                TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45),
                OrdemFormulario = 0,
                OrdemImpressao = 0
            };
            _cpf.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");

            modeloProposta.AdicionarCampo(_cpf);

            Assert.That(modeloProposta.RenderizarTemplateDeFormulario(), Is.EqualTo("<div class='field45'><span>Nome do proponente</span><span><input type='hidden' name='[0].Nome' value='Nome'/><input type='text' name='[0].Valor' value=''/></span></div><div class='field45'><span>Nome do proponente</span><span><input type='hidden' name='[0].Nome' value='CPF'/><input type='text' name='[0].Valor' value=''/></span></div>"));
         }

        [Test]
        [Ignore]
        public void renderizar_impressao_sem_dados()
        {
            var modeloProposta = new ModeloDeProposta();

            var _nomeDoProponente = new CampoDeProposta
            {
                Id = Guid.NewGuid(),
                Nome = "Nome",
                Titulo = "Nome do proponente",
                TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45),
                OrdemFormulario = 0,
                OrdemImpressao = 0
            };
            _nomeDoProponente.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");

            modeloProposta.AdicionarCampo(_nomeDoProponente);

            var _cpf = new CampoDeProposta
            {
                Id = Guid.NewGuid(),
                Nome = "CPF",
                Titulo = "Nome do proponente",
                TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45),
                OrdemFormulario = 0,
                OrdemImpressao = 0
            };
            _cpf.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");

            modeloProposta.AdicionarCampo(_cpf);


            Assert.That(modeloProposta.RenderizarTemplateDeImpressao(), Is.EqualTo("<div class='field45'><span>Nome do proponente</span><span><input type='hidden' name='[0].Nome' value='Nome'/><input type='text' name='[0].Valor' value=''/></span></div><div class='field45'><span>Nome do proponente</span><span><input type='hidden' name='[0].Nome' value='CPF'/><input type='text' name='[0].Valor' value=''/></span></div>"));
        }

    }
}