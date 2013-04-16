using System;
using System.Linq;
using System.Collections.Generic;
using Vital.InfraStructure.DSL.DesignByContract;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    /// <summary>
    /// Representa um modelo de proposta de um plano
    /// </summary>
	public class ModeloDeProposta : IAggregateRoot<Guid>
	{
        /// <summary>
        /// Identificador
        /// </summary>
		public virtual Guid Id { get; set; }
		
        /// <summary>
        /// Lista de Campos do Modelo de Proposta
        /// </summary>
        public virtual IList<CampoDeProposta> Campos { get; set; }
		
        /// <summary>
        /// Determina se é um modelo já publicado
        /// </summary>
        public virtual bool Publicada { get; protected set; }

        /// <summary>
        /// Template HTML para formulário da proposta
        /// </summary>
        public virtual string TemplateDeFormulario { get; set; }

		/// <summary>
		/// Template HTML para impressão da proposta
		/// </summary>
		public virtual string TemplateDeImpressao { get; set; }

        /// <summary>
        /// Data em que o Modelo foi publicado
        /// </summary>
        public virtual DateTime? DataDePublicacao { get; protected set; }

        /// <summary>
        /// Construtor, sem dependencia
        /// </summary>
		public ModeloDeProposta()
		{
			Campos = new List<CampoDeProposta>();
            DataDePublicacao = null;
			Publicada = false;
		}

        /// <summary>
        /// Muda o status do Modelo de Proposta para publicada
        /// </summary>
        public virtual void Publicar()
        {
            //TODO: Não sei se esse é o melhor nome para o método. Também não sei se é responsabilidade dessa entidade fazer a publicação. Talvez isso deva ficar no plano ou numa máquina de estados a parte

            #region Pré-Condições

            IAssertion existemCamposParaPublicar = Assertion.IsFalse(Campos.Count == 0, "O Rascunho do Modelo de Proposta não pode estar sem campos");

            existemCamposParaPublicar.Validate(this);

            #endregion

            DataDePublicacao = DateTime.Now;
            Publicada = true;
        }

        /// <summary>
        /// Copia a si mesma (ModeloDeProposta) mudando apenas os IDs internos. O objetivo aqui é tornar uma cópia disponível para rascunho a a partir de um modelo atual
        /// </summary>
        /// <returns></returns>
        public virtual ModeloDeProposta CopiarParaRascunho()
        {
            var copia = new ModeloDeProposta() {};

            foreach (var campoNoModeloDeProposta in Campos)
            {
                copia.AdicionarCampo(campoNoModeloDeProposta.CopiarParaRascunho());
            }

            return copia;
        }

        /// <summary>
        /// Valida se já existe um campo na lista desse modelo com o Nome passado
        /// </summary>
        /// <param name="nome">Nome do campo para procura</param>
        /// <returns></returns>
        public virtual bool ExisteCampoComNome(string nome)
        {
            return Campos.Any(c => c.Nome == nome);
        }

        /// <summary>
        /// Valida o novo campo e o adiciona a lista de campos atuais
        /// </summary>
        /// <param name="campo">Novo campo</param>
		private void AdicionarCampo(CampoDeProposta campo)
        {
            #region Pré-Condições

            Assertion.NotNull(campo, "campo é obrigatório").Validate();
            IAssertion nomeDoCampoFoiDevidamentePreenchido = Assertion.IsFalse(string.IsNullOrWhiteSpace(campo.Nome), "O nome do campo deve ser informado");
            IAssertion naoExisteNenhumCampoComONomeInformado = Assertion.IsFalse(ExisteCampoComNome(campo.Nome), string.Format("Já existe um campo com o nome {0}", campo.Nome));

            #endregion

            nomeDoCampoFoiDevidamentePreenchido.and(naoExisteNenhumCampoComONomeInformado).Validate(this);

            campo.Id = Guid.NewGuid();

			Campos.Add(campo);
		}

        /// <summary>
        /// Cria um novo campo no Modelo Atual
        /// </summary>
        /// <param name="nomeDoCampo">Nome do campo</param>
		public virtual CampoDeProposta AdicionarCampo(string nomeDoCampo)
        {
            var campoNovo = new CampoDeProposta(nomeDoCampo);
            AdicionarCampo(campoNovo);
            return campoNovo;
        }

        /// <summary>
        /// Exclui um campo da lista de Campos
        /// </summary>
        /// <param name="campoASerExcluido">Campo para exclusão</param>
        public virtual void RemoverCampo(CampoDeProposta campoASerExcluido)
        {
            #region Pré-Condições

            Assertion.NotNull(campoASerExcluido, "campo é obrigatório").Validate();
            IAssertion nomeDoCampoFoiDevidamentePreenchido = Assertion.IsFalse(string.IsNullOrWhiteSpace(campoASerExcluido.Nome), "O nome do campo deve ser informado");
            IAssertion naoExisteNenhumCampoComONomeInformado = Assertion.IsTrue(ExisteCampoComNome(campoASerExcluido.Nome), string.Format("Não existe um campo com o nome {0}", campoASerExcluido.Nome));

            #endregion

            nomeDoCampoFoiDevidamentePreenchido.and(naoExisteNenhumCampoComONomeInformado).Validate(this);

            Campos.Remove(campoASerExcluido);
        }

        /// <summary>
        /// Obtem um campo do modelo de proposta pelo nome
        /// </summary>
        /// <param name="nome">Nome do campo</param>
        /// <returns>CampoDeProposta</returns>
        public virtual CampoDeProposta ObterCampoPeloId(Guid id)
        {
            #region Pré-Condições

            Assertion.NotNull(Campos, "A lista de campos não foi inicializada").Validate(this);

            #endregion

            var campo = Campos.SingleOrDefault(x => x.Id == id);

            #region Pós-Condições

            IAssertion campoFoiEncontradoComSucesso = Assertion.Equals(campo.Id, id, "O campo não foi encontrado");

            #endregion

            campoFoiEncontradoComSucesso.Validate(this);


            return campo;
        }

		/// <summary>
		/// Renderiza um HTML de formulário com base nos campos existentes na proposta
		/// </summary>
		/// <returns></returns>
		public virtual string RenderizarTemplateDeFormulario()
		{
			#region Pré-condições

			Assertion.NotNull(TemplateDeFormulario, "O template de formulário deste Modelo de Proposta não foi definido").Validate();

			#endregion

			return string.Empty;
		}

		/// <summary>
		/// Renderiza um HTML para exibição em impressão com base nos campos existentes na proposta
		/// </summary>
		/// <returns></returns>
		public virtual string RenderizarTemplateDeImpressao()
		{
			#region Pré-condições

			Assertion.NotNull(TemplateDeImpressao, "O template de impressão deste Modelo de Proposta não foi definido").Validate();

			#endregion

			return string.Empty;
		}
    }
}