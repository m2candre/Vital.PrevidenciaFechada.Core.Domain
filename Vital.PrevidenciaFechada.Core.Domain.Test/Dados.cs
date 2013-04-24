﻿using System;
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
            var listaCampos = new[] { 45, 75, 100, 110, 135, 140, 150, 157, 192, 214, 215, 240, 249, 250, 253, 260, 270, 280, 320, 370, 380, 395, 400, 411, 450, 543, 570, 580, 600, 620, 700, 990 };
            listaTamanhoDosCampos = listaCampos.Select(listaCampo => new ClasseDeTamanhoDoCampo(listaCampo)).ToList();
            
        }

        IList<ModeloDoCampo> _listaDeModelosDeCampos;
        public void AdicionarModelosDeCampo()
        {
            _listaDeModelosDeCampos = new List<ModeloDoCampo>();

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


            CampoDeProposta cTitulo = new CampoDeProposta();
            cTitulo.Nome = "Título";
            cTitulo.Titulo = "Proposta de inscrição<br />Planos de benefícios previdenciários ABEPOMPREV";
            cTitulo.OrdemFormulario = 0;
            cTitulo.OrdemImpressao = 0;
            cTitulo.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(620);
            cTitulo.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Título");
            modeloDeProposta.AdicionarCampo(cTitulo);

            CampoDeProposta cLogo = new CampoDeProposta();
            cLogo.Nome = "Logo";
            cLogo.Valor = "data:image/png;base64,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";
            cLogo.Alinhamento = "pull-right";
            cLogo.ExibirTitulo = false;
            cLogo.OrdemFormulario = 1;
            cLogo.OrdemImpressao = 1;
            cLogo.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(370);
            cLogo.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Logo");
            modeloDeProposta.AdicionarCampo(cLogo);

            CampoDeProposta cNumeroDaInstituidora = new CampoDeProposta();
            cNumeroDaInstituidora.Nome = "NúmeroDaInstituidora";
            cNumeroDaInstituidora.Titulo = "N° da Instituidora";
            cNumeroDaInstituidora.OrdemFormulario = 2;
            cNumeroDaInstituidora.OrdemImpressao = 2;
            cNumeroDaInstituidora.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(140);
            cNumeroDaInstituidora.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Número");
            modeloDeProposta.AdicionarCampo(cNumeroDaInstituidora);

            CampoDeProposta cNomeAssociacaoInstituidora = new CampoDeProposta();
            cNomeAssociacaoInstituidora.Nome = "NomeDaAssociacaoInstituidora";
            cNomeAssociacaoInstituidora.Titulo = "Nome da Associação Instituidora";
            cNomeAssociacaoInstituidora.OrdemFormulario = 3;
            cNomeAssociacaoInstituidora.OrdemImpressao = 3;
            cNomeAssociacaoInstituidora.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(450);
            cNomeAssociacaoInstituidora.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");

            CampoDeProposta cNumeroDaProposta = new CampoDeProposta();
            cNumeroDaProposta.Nome = "NúmeroDaProposta";
            cNumeroDaProposta.Titulo = "Nº da Proposta";
            cNumeroDaProposta.OrdemFormulario = 4;
            cNumeroDaProposta.OrdemImpressao = 4;
            cNumeroDaProposta.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(380);
            cNumeroDaProposta.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Número");
            modeloDeProposta.AdicionarCampo(cNumeroDaProposta);

            CampoDeProposta cQualificacaoProponente = new CampoDeProposta();
            cQualificacaoProponente.Nome = "QualificacaoProponente";
            cQualificacaoProponente.Titulo = "QUALIFICAÇÃO DO PROPONENTE";
            cQualificacaoProponente.OrdemFormulario = 5;
            cQualificacaoProponente.OrdemImpressao = 5;
            cQualificacaoProponente.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cQualificacaoProponente.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cQualificacaoProponente);

            CampoDeProposta NomeCompletoDoProponente = new CampoDeProposta();
            NomeCompletoDoProponente.Nome = "NomeCompletoDoProponente";
            NomeCompletoDoProponente.Titulo = "Nome Completo do Proponente";
            NomeCompletoDoProponente.OrdemFormulario = 6;
            NomeCompletoDoProponente.OrdemImpressao = 6;
            NomeCompletoDoProponente.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(600);
            NomeCompletoDoProponente.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Título secundário");
            modeloDeProposta.AdicionarCampo(NomeCompletoDoProponente);

            //TODO: Radio button para escolha do CPF (Titular ou Dependente)

            //ValoresDoCampo valores = new ValoresDoCampo();
            //valores.Rotulo = "Solteiro";
            //valores.Valor = "EstadoCivil";
            //valores.Ordem = 0;

            //ValoresDoCampo valores2 = new ValoresDoCampo();
            //cNomeAssociacaoInstituidora.ValoresDoCampo.Add(valores);
            //cNomeAssociacaoInstituidora.ValoresDoCampo.Add(valores2);


            //modeloDeProposta.AdicionarCampo(cNomeAssociacaoInstituidora);


            CampoDeProposta cNaturezaDocDeIdentificacao = new CampoDeProposta();
            cNaturezaDocDeIdentificacao.Nome = "NaturezaDocDeIdentificacao";
            cNaturezaDocDeIdentificacao.Titulo = "Natureza do Doc. Identificação";
            cNaturezaDocDeIdentificacao.OrdemFormulario = 6;
            cNaturezaDocDeIdentificacao.OrdemImpressao = 6;
            cNaturezaDocDeIdentificacao.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cNaturezaDocDeIdentificacao.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cNaturezaDocDeIdentificacao);

            CampoDeProposta cNumero = new CampoDeProposta();
            cNumero.Nome = "Número";
            cNumero.Titulo = "Número";
            cNumero.OrdemFormulario = 0;
            cNumero.OrdemImpressao = 0;
            cNumero.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cNumero.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Número");
            modeloDeProposta.AdicionarCampo(cNumero);

            CampoDeProposta cOrgaoExpedidor = new CampoDeProposta();
            cOrgaoExpedidor.Nome = "OrgaoExpedidor";
            cOrgaoExpedidor.Titulo = "Órgão Expedidor";
            cOrgaoExpedidor.OrdemFormulario = 0;
            cOrgaoExpedidor.OrdemImpressao = 0;
            cOrgaoExpedidor.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cOrgaoExpedidor.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cOrgaoExpedidor);

            CampoDeProposta cDataDeExpedicao = new CampoDeProposta();
            cDataDeExpedicao.Nome = "DataDeExpedicao";
            cDataDeExpedicao.Titulo = "Data de Expedição";
            cDataDeExpedicao.OrdemFormulario = 0;
            cDataDeExpedicao.OrdemImpressao = 0;
            cDataDeExpedicao.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cDataDeExpedicao.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Data");
            modeloDeProposta.AdicionarCampo(cDataDeExpedicao);

            CampoDeProposta cDataDeNascimento = new CampoDeProposta();
            cDataDeNascimento.Nome = "DataDeNascimento";
            cDataDeNascimento.Titulo = "Data de Nascimento";
            cDataDeNascimento.OrdemFormulario = 0;
            cDataDeNascimento.OrdemImpressao = 0;
            cDataDeNascimento.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cDataDeNascimento.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Data");
            modeloDeProposta.AdicionarCampo(cDataDeNascimento);

            CampoDeProposta cNaturalidade = new CampoDeProposta();
            cNaturalidade.Nome = "Naturalidade";
            cNaturalidade.Titulo = "Naturalidade";
            cNaturalidade.OrdemFormulario = 0;
            cNaturalidade.OrdemImpressao = 0;
            cNaturalidade.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cNaturalidade.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cNaturalidade);

            CampoDeProposta cNacionalidade = new CampoDeProposta();
            cNacionalidade.Nome = "Nacionalidade";
            cNacionalidade.Titulo = "Nacionalidade";
            cNacionalidade.OrdemFormulario = 0;
            cNacionalidade.OrdemImpressao = 0;
            cNacionalidade.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cNacionalidade.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cNacionalidade);


            CampoDeProposta cIdade = new CampoDeProposta();
            cIdade.Nome = "Idade";
            cIdade.Titulo = "Idade";
            cIdade.OrdemFormulario = 0;
            cIdade.OrdemImpressao = 0;
            cIdade.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cIdade.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Número");
            modeloDeProposta.AdicionarCampo(cIdade);

            //TODO: Radio button para escolha de sexo Masc. ou Fem.

            //TODO: Radio button para escola do estado civil (solteiro, casado)

            CampoDeProposta cOcupacao = new CampoDeProposta();
            cOcupacao.Nome = "Ocupação";
            cOcupacao.Titulo = "Proposta de inscrição<br />Planos de benefícios previdenciários ABEPOMPREV";
            cOcupacao.OrdemFormulario = 0;
            cOcupacao.OrdemImpressao = 0;
            cOcupacao.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(580);
            cOcupacao.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cOcupacao);

            CampoDeProposta cNomeDoConjugue = new CampoDeProposta();
            cNomeDoConjugue.Nome = "NomeDoCônjuge";
            cNomeDoConjugue.Titulo = "Nome do Cônjuge";
            cNomeDoConjugue.OrdemFormulario = 0;
            cNomeDoConjugue.OrdemImpressao = 0;
            cNomeDoConjugue.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(990);
            cNomeDoConjugue.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cNomeDoConjugue);

            //TODO: Radio button para renda mensal

            CampoDeProposta cEnderecoResidencial = new CampoDeProposta();
            cEnderecoResidencial.Nome = "EndereçoResidencial";
            cEnderecoResidencial.Titulo = "Endereço Residencial";
            cEnderecoResidencial.OrdemFormulario = 0;
            cEnderecoResidencial.OrdemImpressao = 0;
            cEnderecoResidencial.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(700);
            cEnderecoResidencial.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cEnderecoResidencial);

            CampoDeProposta cBairro = new CampoDeProposta();
            cBairro.Nome = "Bairro";
            cBairro.Titulo = "Bairro";
            cBairro.OrdemFormulario = 0;
            cBairro.OrdemImpressao = 0;
            cBairro.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(280);
            cBairro.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cBairro);

            CampoDeProposta cCidade = new CampoDeProposta();
            cCidade.Nome = "Cidade";
            cCidade.Titulo = "Cidade";
            cCidade.OrdemFormulario = 0;
            cCidade.OrdemImpressao = 0;
            cCidade.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(380);
            cCidade.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cCidade);

            CampoDeProposta cUF = new CampoDeProposta();
            cUF.Nome = "UF";
            cUF.Titulo = "UF";
            cUF.OrdemFormulario = 0;
            cUF.OrdemImpressao = 0;
            cUF.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45);
            cUF.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cUF);

            CampoDeProposta cCEP = new CampoDeProposta();
            cCEP.Nome = "CEP";
            cCEP.Titulo = "CEP";
            cCEP.OrdemFormulario = 0;
            cCEP.OrdemImpressao = 0;
            cCEP.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cCEP.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "CEP");
            modeloDeProposta.AdicionarCampo(cCEP);

            CampoDeProposta cDDD = new CampoDeProposta();
            cDDD.Nome = "DDD";
            cDDD.Titulo = "DDD";
            cDDD.OrdemFormulario = 0;
            cDDD.OrdemImpressao = 0;
            cDDD.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45);
            cDDD.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Número");
            modeloDeProposta.AdicionarCampo(cDDD);

            CampoDeProposta cTelefone1 = new CampoDeProposta();
            cTelefone1.Nome = "Telefone1";
            cTelefone1.Titulo = "Telefone 1";
            cTelefone1.OrdemFormulario = 0;
            cTelefone1.OrdemImpressao = 0;
            cTelefone1.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cTelefone1.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Telefone");
            modeloDeProposta.AdicionarCampo(cTelefone1);

            CampoDeProposta cTelefone2 = new CampoDeProposta();
            cTelefone2.Nome = "Telefone2";
            cTelefone2.Titulo = "Telefone 2";
            cTelefone2.OrdemFormulario = 0;
            cTelefone2.OrdemImpressao = 0;
            cTelefone2.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(280);
            cTelefone2.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Telefone");
            modeloDeProposta.AdicionarCampo(cTelefone2);

            CampoDeProposta cEnderecoComercial = new CampoDeProposta();
            cEnderecoComercial.Nome = "EndereçoComercial";
            cEnderecoComercial.Titulo = "Endereço Comercial";
            cEnderecoComercial.OrdemFormulario = 0;
            cEnderecoComercial.OrdemImpressao = 0;
            cEnderecoComercial.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(700);
            cEnderecoComercial.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cEnderecoComercial);

            CampoDeProposta cEnderecoComercialBairro = new CampoDeProposta();
            cEnderecoComercialBairro.Nome = "EndereçoComercialBairro";
            cEnderecoComercialBairro.Titulo = "Bairro";
            cEnderecoComercialBairro.OrdemFormulario = 0;
            cEnderecoComercialBairro.OrdemImpressao = 0;
            cEnderecoComercialBairro.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(250);
            cEnderecoComercialBairro.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cEnderecoComercialBairro);

            CampoDeProposta cEnderecoComercialCidade = new CampoDeProposta();
            cEnderecoComercialCidade.Nome = "EnderecoComercialCidade";
            cEnderecoComercialCidade.Titulo = "Cidade";
            cEnderecoComercialCidade.OrdemFormulario = 0;
            cEnderecoComercialCidade.OrdemImpressao = 0;
            cEnderecoComercialCidade.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(450);
            cEnderecoComercialCidade.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cEnderecoComercialCidade);

            CampoDeProposta cEnderecoComercialUF = new CampoDeProposta();
            cEnderecoComercialUF.Nome = "EnderecoComercialUF";
            cEnderecoComercialUF.Titulo = "UF";
            cEnderecoComercialUF.OrdemFormulario = 0;
            cEnderecoComercialUF.OrdemImpressao = 0;
            cEnderecoComercialUF.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45);
            cEnderecoComercialUF.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cEnderecoComercialUF);

            CampoDeProposta cEnderecoComercialCEP = new CampoDeProposta();
            cEnderecoComercialCEP.Nome = "EnderecoComercialCEP";
            cEnderecoComercialCEP.Titulo = "CEP";
            cEnderecoComercialCEP.OrdemFormulario = 0;
            cEnderecoComercialCEP.OrdemImpressao = 0;
            cEnderecoComercialCEP.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(215);
            cEnderecoComercialCEP.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "CEP");
            modeloDeProposta.AdicionarCampo(cEnderecoComercialCEP);

            CampoDeProposta cEnderecoComercialDDD = new CampoDeProposta();
            cEnderecoComercialDDD.Nome = "EnderecoComercialDDD";
            cEnderecoComercialDDD.Titulo = "DDD";
            cEnderecoComercialDDD.OrdemFormulario = 0;
            cEnderecoComercialDDD.OrdemImpressao = 0;
            cEnderecoComercialDDD.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45);
            cEnderecoComercialDDD.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Número");
            modeloDeProposta.AdicionarCampo(cEnderecoComercialDDD);

            CampoDeProposta cEnderecoComercialTelefone1 = new CampoDeProposta();
            cEnderecoComercialTelefone1.Nome = "EnderecoComercialTelefone1";
            cEnderecoComercialTelefone1.Titulo = "Telefone 1";
            cEnderecoComercialTelefone1.OrdemFormulario = 0;
            cEnderecoComercialTelefone1.OrdemImpressao = 0;
            cEnderecoComercialTelefone1.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cEnderecoComercialTelefone1.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Telefone");
            modeloDeProposta.AdicionarCampo(cEnderecoComercialTelefone1);

            CampoDeProposta cEnderecoComercialTelefone2 = new CampoDeProposta();
            cEnderecoComercialTelefone2.Nome = "EnderecoComercialTelefone2";
            cEnderecoComercialTelefone2.Titulo = "Telefone 2";
            cEnderecoComercialTelefone2.OrdemFormulario = 0;
            cEnderecoComercialTelefone2.OrdemImpressao = 0;
            cEnderecoComercialTelefone2.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(280);
            cEnderecoComercialTelefone2.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Telefone");
            modeloDeProposta.AdicionarCampo(cEnderecoComercialTelefone2);

            CampoDeProposta cEmail = new CampoDeProposta();
            cEmail.Nome = "Email";
            cEmail.Titulo = "Email";
            cEmail.OrdemFormulario = 0;
            cEmail.OrdemImpressao = 0;
            cEmail.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(395);
            cEmail.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cEmail);

            CampoDeProposta cNomeCompletoRepresentanteLegal = new CampoDeProposta();
            cNomeCompletoRepresentanteLegal.Nome = "NomeCompletoRepresentanteLegal";
            cNomeCompletoRepresentanteLegal.Titulo = "Nome Completo do Representante Legal (obrigatório se o proponente for menor de idade ou se não for o titular da Associação Instituidora)";
            cNomeCompletoRepresentanteLegal.OrdemFormulario = 0;
            cNomeCompletoRepresentanteLegal.OrdemImpressao = 0;
            cNomeCompletoRepresentanteLegal.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(700);
            cNomeCompletoRepresentanteLegal.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cNomeCompletoRepresentanteLegal);

            CampoDeProposta cCPFRepresentanteLegal = new CampoDeProposta();
            cCPFRepresentanteLegal.Nome = "CPFRepresentanteLegal";
            cCPFRepresentanteLegal.Titulo = "CPF do Representante Legal";
            cCPFRepresentanteLegal.OrdemFormulario = 0;
            cCPFRepresentanteLegal.OrdemImpressao = 0;
            cCPFRepresentanteLegal.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(280);
            cCPFRepresentanteLegal.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "CPF");
            modeloDeProposta.AdicionarCampo(cCPFRepresentanteLegal);

            CampoDeProposta cFiliacao = new CampoDeProposta();
            cFiliacao.Nome = "Filiacao";
            cFiliacao.Titulo = "Filiação";
            cFiliacao.OrdemFormulario = 0;
            cFiliacao.OrdemImpressao = 0;
            cFiliacao.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(700);
            cFiliacao.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cFiliacao);

            //TODO: Radio Button do Endereço para Correspondência (opções: comercial, residencial)

            CampoDeProposta cTituloPlanoBeneficios = new CampoDeProposta();
            cTituloPlanoBeneficios.Nome = "";
            cTituloPlanoBeneficios.Titulo = "PLANO DE BENEFÍCIOS POR CONTRIBUIÇÃO DEFINIDA \"CD\" - Baseado exclusivamente nos recursos constituídos";
            cTituloPlanoBeneficios.OrdemFormulario = 0;
            cTituloPlanoBeneficios.OrdemImpressao = 0;
            cTituloPlanoBeneficios.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cTituloPlanoBeneficios.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Título secundário");
            modeloDeProposta.AdicionarCampo(cTituloPlanoBeneficios);

            CampoDeProposta cRendaProgramadaNaoVitalicia = new CampoDeProposta();
            cRendaProgramadaNaoVitalicia.Nome = "RendaProgramadaNaoVitalicia";
            cRendaProgramadaNaoVitalicia.Valor = "RENDA PROGRAMADA NÃO VITALÍCIA";
            cRendaProgramadaNaoVitalicia.OrdemFormulario = 0;
            cRendaProgramadaNaoVitalicia.OrdemImpressao = 0;
            cRendaProgramadaNaoVitalicia.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cRendaProgramadaNaoVitalicia.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cRendaProgramadaNaoVitalicia);

            CampoDeProposta cIdadeParaEntrarEmBeneficio = new CampoDeProposta();
            cIdadeParaEntrarEmBeneficio.Nome = "IdadeParaEntradaEmbeneficio";
            cIdadeParaEntrarEmBeneficio.Titulo = "Idade para Entrada em Benefício";
            cIdadeParaEntrarEmBeneficio.OrdemFormulario = 0;
            cIdadeParaEntrarEmBeneficio.OrdemImpressao = 0;
            cIdadeParaEntrarEmBeneficio.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cIdadeParaEntrarEmBeneficio.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cIdadeParaEntrarEmBeneficio);

            CampoDeProposta cValorDaContribuicao1 = new CampoDeProposta();
            cValorDaContribuicao1.Nome = "ValorDaContribuicao1";
            cValorDaContribuicao1.Titulo = "Valor da Contribuição (1)";
            cValorDaContribuicao1.Valor = "R$";
            cValorDaContribuicao1.OrdemFormulario = 0;
            cValorDaContribuicao1.OrdemImpressao = 0;
            cValorDaContribuicao1.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cValorDaContribuicao1.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cValorDaContribuicao1);

            CampoDeProposta cTributacao = new CampoDeProposta();
            cTributacao.Nome = "Tributacao";
            cTributacao.Titulo = "Tributação";
            cTributacao.OrdemFormulario = 0;
            cTributacao.OrdemImpressao = 0;
            cTributacao.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(380);
            cTributacao.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cTributacao);

            CampoDeProposta cAliquotasProgressivas = new CampoDeProposta();
            cAliquotasProgressivas.Nome = "AliquotasProgressivas";
            cAliquotasProgressivas.Titulo = "Alíquotas Progressivas";
            cAliquotasProgressivas.OrdemFormulario = 0;
            cAliquotasProgressivas.OrdemImpressao = 0;
            cAliquotasProgressivas.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(240);
            cAliquotasProgressivas.ModeloDoCampo = _listaDeModelosDeCampos.SingleOrDefault(s => s.NomeDoModelo == "Container de texto");
            modeloDeProposta.AdicionarCampo(cAliquotasProgressivas);


            modeloDeProposta.RenderizarTemplateDeFormulario();
        }
    }
}

