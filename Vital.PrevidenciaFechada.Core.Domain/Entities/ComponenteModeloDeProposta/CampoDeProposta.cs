﻿using System;
using System.Collections.Generic;
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
        public virtual HashSet<ValoresDoCampo> ValoresDoCampo { get; set; }

        /// <summary>
        /// Título do campo
        /// </summary>
        public virtual string Titulo { get; set; }

        /// <summary>
        /// Valor
        /// </summary>
        public virtual string Valor { get; set; }

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
        /// Exibe um campo com o dobro da altura
        /// </summary>
        public virtual bool CampoDuplo { get; set; }

        /// <summary>
        /// Exibe o sufixo de um campo
        /// </summary>
        public virtual string Sufixo { get; set; }

        /// <summary>
        /// Exibe o prefixo de um tempo
        /// </summary>
        public virtual string Prefixo { get; set; }

        /// <summary>
        ///  Renderizar o campo utilizando o modelo de impressão
        /// </summary>
        /// <returns></returns>
        public string RenderizarParaImpressao()
        {
            return ModeloDoCampo.ModeloParaImpressao.Replace("@Css", TamanhoDoCampo.ObterClasse()).Replace("@titulo", this.Titulo);
        }

        /// <summary>
        ///  Renderizar o campo utilizando o modelo de formulário
        /// </summary>
        /// <returns></returns>
        public string RenderizarParaFormulario()
        {
            return ModeloDoCampo.ModeloParaFormulario.Replace("@Css", TamanhoDoCampo.ObterClasse()).Replace("@titulo", this.Titulo);
        }
    }
}