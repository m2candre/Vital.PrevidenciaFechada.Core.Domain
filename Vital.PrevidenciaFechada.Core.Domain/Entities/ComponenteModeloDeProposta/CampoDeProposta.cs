using System;
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
        /// Construtor
        /// </summary>
        public CampoDeProposta()
        {
            
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