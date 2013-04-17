using System;
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
        public virtual string Nome { get; protected set; }

        /// <summary>
        /// Classe css com o tamanho do campo
        /// </summary>
        public virtual TamanhoDoCampo TamanhoDoCampo { get; set; }

        /// <summary>
        /// O campo pode ser um título, imagem, seleção única
        /// </summary>
        public virtual ModeloDoCampo ModeloDoCampo { get; set; }

        /// <summary>
        /// O campo pode ser do tipo CPF, CEP, Data
        /// </summary>
        public virtual TipoDoCampo TipoDoCampo { get; set; }

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
        /// Construtor
        /// </summary>
        public CampoDeProposta()
        {

        }

        public virtual string Render()
        {


            return string.Empty;
        }

        public static implicit operator string(CampoDeProposta campoDeProposta)
        {
            return campoDeProposta.Render();
        }

        public override string ToString()
        {
            return Render();
        }

        /// <summary>
        /// Construtor - Dependencia com o Nome do Campo
        /// </summary>
        /// <param name="nome">Nome do novo campo</param>
        public CampoDeProposta(string nome)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(nome), "O nome do campo deve ser informado").Validate(this);

            Nome = nome;
        }

        /// <summary>
        /// Copia a si mesmo mudando apenas o ID interno. O objetivo aqui é tornar uma cópia disponível para rascunho a partir do atual
        /// </summary>
        /// <returns></returns>
        public virtual CampoDeProposta CopiarParaRascunho()
        {
            return new CampoDeProposta(Nome);
        }

        /// <summary>
        /// Atualiza o nome do campo
        /// </summary>
        /// <param name="nome">nome</param>
        public virtual void AtualizarNome(string nome)
        {
            #region Pré-Condições

            IAssertion nomeNaoEstaVazioOuNulo = Assertion.IsFalse(string.IsNullOrEmpty(nome), "Nome do campo não informado");

            #endregion

            nomeNaoEstaVazioOuNulo.Validate(this);

            Nome = nome;

            #region Pós-Condições

            IAssertion nomeFoiAtualizadoCorretamente = Assertion.Equals(Nome, nome, "O Nome não foi atualizado corretamente");

            #endregion

            nomeFoiAtualizadoCorretamente.Validate(this);
        }
    }
}