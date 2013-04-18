using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Vital.InfraStructure.DSL.DesignByContract;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{

    /// <summary>
    /// Representa um campo dentro de uma Proposta
    /// </summary>
	[Serializable]
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
		[XmlIgnore]
		public virtual string Valor { get; set; }

        /// <summary>
        /// Classe css com o tamanho do campo
        /// </summary>
		[XmlIgnore]
        public virtual ClasseDeTamanhoDoCampo TamanhoDoCampo { get; set; }

        /// <summary>
        /// O campo pode ser um título, imagem, seleção única
        /// </summary>
		[XmlIgnore]
		public virtual ModeloDoCampo ModeloDoCampo { get; set; }

        /// <summary>
        /// Registra os valores para um modelo
        /// </summary>
		[XmlIgnore]
		public virtual HashSet<ValoresDoCampo> ValoresDoCampo { get; set; }

        /// <summary>
        /// Título do campo
        /// </summary>
		[XmlIgnore]
		public virtual string Titulo { get; set; }
		
        /// <summary>
        /// Permite que o campo seja exibido nos grids de proposta
        /// </summary>
		[XmlIgnore]
		public virtual bool ExibirNoGrid { get; set; }

        /// <summary>
        /// Campo ref. ao DTO de participantes
        /// </summary>
		[XmlIgnore]
		public virtual bool CompoeParticipante { get; set; }

        /// <summary>
        /// Ordena os campos de uma proposta para impressão
        /// </summary>
		[XmlIgnore]
		public virtual int OrdemImpressao { get; set; }

        /// <summary>
        /// Ordena os campos de uma proposta para o preenchimento do formulário
        /// </summary>
		[XmlIgnore]
		public virtual int OrdemFormulario { get; set; }

        /// <summary>
        /// Permite que o campo fique visivel na impressão
        /// </summary>
		[XmlIgnore]
		public virtual bool VisivelNaImpressao { get; set; }

        /// <summary>
        /// Exibe o título de um campo
        /// </summary>
		[XmlIgnore]
		public virtual bool ExibirTitulo { get; set; }

        /// <summary>
        /// Exibe a borda do layout do campo
        /// </summary>
		[XmlIgnore]
		public virtual bool ExibirBorda { get; set; }

        /// <summary>
        /// Alinha um campo
        /// </summary>
		[XmlIgnore]
		public virtual string Alinhamento { get; set; }

        /// <summary>
        /// Exibe um campo com o dobro da altura
        /// </summary>
		[XmlIgnore]
		public virtual bool CampoDuplo { get; set; }

        /// <summary>
        /// Exibe o sufixo de um campo
        /// </summary>
		[XmlIgnore]
		public virtual string Sufixo { get; set; }

        /// <summary>
        /// Exibe o prefixo de um tempo
        /// </summary>
		[XmlIgnore]
		public virtual string Prefixo { get; set; }

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
            return modelo.Replace("@Css", TamanhoDoCampo.ObterClasse()).Replace("@titulo", this.Titulo).Replace("@valor", this.Valor).Replace("@alinhamento", this.Alinhamento);
        }
    }
}