﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using Vital.InfraStructure.DSL.DesignByContract;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{

    /// <summary>
    /// Representa um campo dentro de uma Proposta
    /// </summary>
    public class CampoDeProposta
    {
        /// <summary>
        /// Identificador do Campo
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Nome do Campo
        /// </summary>
        public virtual string Nome { get; set; }

        /// <summary>
        /// Valor preenchido no campo
        /// </summary>
        public virtual string Valor { get; set; }

        /// <summary>
        /// Valor preenchido no campo
        /// </summary>
        public virtual string ValorPadrao { get; set; }

        /// <summary>
        /// Classe css com o tamanho do campo
        /// </summary>
        public virtual ClasseDeTamanhoDoCampo TamanhoDoCampo { get; set; }

        /// <summary>
        /// O campo pode ser um título, imagem, seleção única
        /// </summary>
		public virtual ModeloDoCampo ModeloDoCampo { get; set; }

        /// <summary>
        /// Registra os valores para um modelo
        /// </summary>
		public virtual IList<ValoresDoCampo> ValoresDoCampo { get; set; }

        /// <summary>
        /// Título do campo
        /// </summary>
		public virtual string Titulo { get; set; }
		
        /// <summary>
        /// Permite que o campo seja exibido nos grids de proposta
        /// </summary>
		public virtual bool ExibirNoGrid { get; set; }

        /// <summary>
        /// Campo ref. ao DTO de participantes
        /// </summary>
		public virtual bool CompoeParticipante { get; set; }


        /// <summary>
        /// Ordena os campos de uma proposta para impressão
        /// </summary>
		public virtual int OrdemImpressao { get; set; }

        /// <summary>
        /// Ordena os campos de uma proposta para o preenchimento do formulário
        /// </summary>
		public virtual int OrdemFormulario { get; set; }

        /// <summary>
        /// Permite que o campo fique visivel na impressão
        /// </summary>
		public virtual bool VisivelNaImpressao { get; set; }

        /// <summary>
        /// Exibe o título de um campo
        /// </summary>
		public virtual bool ExibirTitulo { get; set; }

        /// <summary>
        /// Exibe a borda do layout do campo
        /// </summary>
		public virtual bool ExibirBorda { get; set; }

        /// <summary>
        /// Alinha um campo
        /// </summary>
		public virtual string Alinhamento { get; set; }

        /// <summary>
        /// Exibe a altura
        /// </summary>
		public virtual bool CampoDuplo { get; set; }

        /// <remarks>Determina a altura do campo de acordo com o CSS. ex. .field50</remarks>
        public virtual string Altura { get; set; }

        /// <summary>
        /// Exibe o sufixo de um campo
        /// </summary>
		public virtual string Sufixo { get; set; }

        /// <summary>
        /// Exibe o prefixo de um campo
        /// </summary>
		public virtual string Prefixo { get; set; }

        public CampoDeProposta()
        {
            ValoresDoCampo = new List<ValoresDoCampo>();
            VisivelNaImpressao = true;
            ExibirBorda = true;
            ExibirNoGrid = false;
            ExibirTitulo = true;
            
        }

        /// <summary>
        ///  Renderizar o campo utilizando o modelo de impressão
        /// </summary>
        /// <returns></returns>
        public virtual string RenderizarParaImpressao()
        {
            #region Pré-Condições
            IAssertion existeModeloParaImpressao = Assertion.IsFalse(string.IsNullOrEmpty(ModeloDoCampo.ModeloParaImpressao), "Não existe modelo de impressão para este campo");
            #endregion
            existeModeloParaImpressao.Validate(this);

            return Renderizar(ModeloDoCampo.ModeloParaImpressao);
        }

        /// <summary>
        ///  Renderizar o campo utilizando o modelo de formulário
        /// </summary>
        /// <returns></returns>
        public virtual string RenderizarParaFormulario()
        {
            #region Pré-Condições
            IAssertion existeModeloParaFormulario = Assertion.IsFalse(string.IsNullOrEmpty(ModeloDoCampo.ModeloParaFormulario), "Não existe modelo de formulário para este campo");
            #endregion
            existeModeloParaFormulario.Validate(this);

            return Renderizar(ModeloDoCampo.ModeloParaFormulario);
        }

        /// <summary>
        /// Executa as substituições
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        private string Renderizar(string modelo)
        {
            string templateCompleto = string.Empty;

            Regex regex = new Regex("<ul class='inline'>(.*)</ul>");

            Match match = regex.Match(modelo);

            if (match.Success && this.ValoresDoCampo.Count > 0)
            {
                string modeloDoValor = match.Groups[1].Value;

                foreach (ValoresDoCampo valor in ValoresDoCampo)
                {
                    string campo = modeloDoValor.Replace("@valor", valor.Valor).Replace("@rotulo", valor.Rotulo);

                    if (this.Valor == valor.Valor)
                    {
                        if (!campo.Contains("type="))
                        {
                            templateCompleto += campo.Replace("<li>", "<li class=\"checked\">");
                            
                        }
                        else
                        {
                            templateCompleto += campo.Replace("type=", "checked=\"checked\" type="); 
                        }
                    }
                    else
                    {
                        templateCompleto += modeloDoValor.Replace("@valor", valor.Valor).Replace("@rotulo", valor.Rotulo);
                    }
                } 

                modelo = modelo.Replace(match.Groups[1].Value, templateCompleto);
            }

			return modelo.Replace("@Css", TamanhoDoCampo.ObterClasse()).Replace("@titulo", this.Titulo).Replace("@valor", this.Valor).Replace("@padrao", this.ValorPadrao).Replace("@alinhamento", this.Alinhamento).Replace("@nome", this.Nome);
        }
    }
}
