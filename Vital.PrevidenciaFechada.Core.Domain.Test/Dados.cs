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
            IList<ModeloDoCampo> _listaDeModelosDeCampos = new List<ModeloDoCampo>();

            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Título", "<div class='@Css'><h1>@titulo</h1></div>", "<div class='@Css'><h1>@titulo</h1></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Logo", "<div class='@Css'><img class='logo @alinhamento' src='@valor' /></div>", "<div class='@Css'><img class='logo pull-right' src='@valor' /></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Estático", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Título secundário", "<h2>@titulo</h2>", "<h2>@titulo</h2>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Título secundário sem sublinhado", "<h3>@titulo</h3>", "<h3>@titulo</h3>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Container de texto", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><input type='text' name='[@indice].Valor' value='@valor'/></span></div>", "<div class='@Css'><span>@titulo</span><span>@valor</span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Seleção única", "<div class='@Css'><span>@titulo</span><span><input type='hidden' name='[@indice].Nome' value='@nome'/><ul class='inline'><li><label><input type='radio' name='[@indice].Valor' value='@valor'/>@rotulo</label></li></ul></span></div>", "<div class='@Css'><span>@titulo</span><span><ul class='inline'><li>@rotulo</li></ul></span></div>"));
            _listaDeModelosDeCampos.Add(new ModeloDoCampo("Texto livre", "<div class='@Css'><span class='internal'>@valor</span></div>", "<div class='@Css'><span class='internal'>@valor</span></div>"));

            //_listaDeModelosDeCampos.Add(new ModeloDoCampo("Campo com Sufixo", "<div class='@Css'><span class='internal'>@valor</span></div>", "<div class='@Css'><span class='internal'>@valor</span></div>"));

            //_listaDeModelosDeCampos.Add(new ModeloDoCampo("Campo com Prefixo", "<div class='@Css'><span class='internal'>@valor</span></div>", "<div class='@Css'><span class='internal'>@valor</span></div>"));

            //_listaDeModelosDeCampos.Add(new ModeloDoCampo("Linha com Checkbox", "<div class='@Css'><span class='internal'>@valor</span></div>", "<div class='@Css'><span class='internal'>@valor</span></div>"));

        }///





        //    containerSufixo = @"<div class='@Css'><span>@titulo</span><span>@componente</span><div class='@estiloComplementar'>@informacaoComplementar</div></div>";
        //    containerPrefixo = @"<div class='@Css'><span>@titulo</span><span>@informacaoComplementar @componente</span></div>";




     

        //    containerHeaderTabela = "<table>@componente";
        //    containerLinhaTabela = "@componente";
        //    containerFooterTabela = "@componente</table>";

        //}

        //private void MontarTipos()

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

