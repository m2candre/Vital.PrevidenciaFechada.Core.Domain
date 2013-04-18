using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;


namespace Vital.PrevidenciaFechada.Core.Domain.Test
{
    public class Dados
    {
        private IList<ClasseDeTamanhoDoCampo> listaTamanhoDosCampos;

        public void AdicionarTamanhos()
        {
            var listaCampos = new[] {45, 75, 100, 110, 135, 140, 150, 157, 192, 214, 240, 249, 253, 260, 270, 280, 320, 380, 395, 400, 411, 450, 543, 570, 580, 600, 700, 990};
            listaTamanhoDosCampos = listaCampos.Select(listaCampo => new ClasseDeTamanhoDoCampo(listaCampo)).ToList();
            
        }

        public void AdicionarModelosDeCampo()
        {
            IList<ModeloDoCampo> listaDeModelosDosCampos = new List<ModeloDoCampo>();

            listaDeModelosDosCampos.Add(new ModeloDoCampo("Título", "<div class='@Css'><h1>@titulo</h1></div>", "<div class='@Css'><h1>@titulo</h1></div>"));
            listaDeModelosDosCampos.Add(new ModeloDoCampo("Logo", "<div class='@Css'><img class='logo pull-right' src='@valor' /></div>", "<div class='@Css'><img class='logo pull-right' src='@valor' /></div>"));

            listaDeModelosDosCampos.Add(new ModeloDoCampo("Estático", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));

        }




        //private void MontarContainers()
        //{
      

        //    containerTituloSecundario = "<h2>@componente</h2>" + "\r\n";
        //    containerTituloSecundarioSemSublinhado = "<h3>@componente</h3>" + "\r\n";
        //    containerTexto = @"<div class='@Css'><span>@titulo</span><span>@componente</span></div>" + "\r\n";
        //    containerRadio = @"<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><ul class='inline'>@componente</ul></span></div>" + "\r\n";
        //    containerTextoComRadio = @"<div class='@Css'><span>@titulo</span><span>@componente @subContainer</span></div>";
        //    subContainerRadio = @"<input type='hidden' name='[@indice].Nome' value='@nome'/><ul class='@Css'>@componente</ul>";
        //    containerTextoSpan = @"<div class='@Css'>@componente</div>";
        //    containerSufixo = @"<div class='@Css'><span>@titulo</span><span>@componente</span><div class='@estiloComplementar'>@informacaoComplementar</div></div>";
        //    containerPrefixo = @"<div class='@Css'><span>@titulo</span><span>@informacaoComplementar @componente</span></div>";

        //    containerTituloDuasLinhas = @"<div class='@Css'><span>@titulo</span></div>";

        //    ContainerTabela = @"<table>@componente</table>";

        //    containerHeaderTabela = "<table>@componente";
        //    containerLinhaTabela = "@componente";
        //    containerFooterTabela = "@componente</table>";

        //}

        //private void MontarTipos()
        //{
 

        //    tipoEstatico = new TipoCampoDoModeloDePropostaDTO { Html = "", Nome = "Estatico" };
        //    tipoTituloSecundario = new TipoCampoDoModeloDePropostaDTO { Html = "@titulo", Nome = "TituloSecundario" };
        //    tipoTituloSecundarioSemSublinhado = new TipoCampoDoModeloDePropostaDTO { Html = "@titulo", Nome = "TituloSecundarioSemSublinhado" };
        //    tipoTexto = new TipoCampoDoModeloDePropostaDTO { Html = @"<input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' value='@valor'/>", Nome = "Texto" };
        //    tipoRadioButton = new TipoCampoDoModeloDePropostaDTO { Html = @"<li><label><input type='radio' name='[@indice].Valor' value='@valor'/>@valor</label></li>", Nome = "Radio" };
        //    tipoTextoComRadio = new TipoCampoDoModeloDePropostaDTO { Html = @"<input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' value='@valor' />", Nome = "TextoComRadio" };
        //    tipoSpan = new TipoCampoDoModeloDePropostaDTO { Html = "<span class='internal'>@valor</span>", Nome = "TextoSpan" };

        //    tipoTextoLivre = new TipoCampoDoModeloDePropostaDTO { Html = @"@titulo", Nome = "Titulo" };
        //    tipoLinhaCheckbox = new TipoCampoDoModeloDePropostaDTO() { Html = @"<label><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='checkbox' name='[@indice].Valor' value='@valor'/>@titulo</label>", Nome = "tipoLinhaCheckbox" };

        //    tipoTabela = new TipoCampoDoModeloDePropostaDTO() { Html = "<tr><td>@valor</td><td><input type='hidden' name='[@indice].Nome' value='@nome_@inputChave'/><textarea name='[@indice].Valor'>@ConteudoDoInput</textarea></td></tr>", Nome = "Tabela" };
        //    tipoLinhaTabela = new TipoCampoDoModeloDePropostaDTO() { Html = "<tr><td>@titulo</td><td><input type='hidden' name='[@indice].Nome' value='@nome'/><textarea name='[@indice].Valor'>@ConteudoDoInput</textarea></td></tr>", Nome = "LinhaTabela" };
        //    tipoTituloDuasLinhas = new TipoCampoDoModeloDePropostaDTO { Html = @"@titulo", Nome = "Titulo" };
        //}





        public void Campos()
        {
            ModeloDeProposta modeloDeProposta = new ModeloDeProposta();

            CampoDeProposta campoDeProposta = new CampoDeProposta();
            campoDeProposta.Id = Guid.NewGuid();
            campoDeProposta.Nome = "Nome";
            campoDeProposta.Titulo = "Nome do proponente";
            campoDeProposta.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45);
            campoDeProposta.ModeloDoCampo = new ModeloDoCampo("Título", "<div class='@Css'><h1>@titulo</h1></div>", "<div class='@Css'><h1>@titulo</h1></div>");

            modeloDeProposta.AdicionarCampo(campoDeProposta);

        }
    }
}

