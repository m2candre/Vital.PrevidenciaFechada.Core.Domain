using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
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
            #region Pré-Condições

            IAssertion existemCamposParaPublicar = Assertion.IsFalse(Campos.Count == 0, "O rascunho do Modelo de Proposta não pode estar sem campos");

            existemCamposParaPublicar.Validate(this);

            #endregion

            DataDePublicacao = DateTime.Now;
            Publicada = true;
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
        /// Obtém um campo do modelo de proposta por nome
        /// </summary>
        /// <param name="nome">Nome do campo</param>
        /// <returns></returns>
        public virtual CampoDeProposta ObterCampo(string nome)
        {
            return Campos.SingleOrDefault(c => c.Nome == nome);
        }

        /// <summary>
        /// Valida o novo campo e o adiciona a lista de campos atuais
        /// </summary>
        /// <param name="campo">Novo campo</param>
		public virtual void AdicionarCampo(CampoDeProposta campo)
        {
            #region Pré-Condições

            Assertion.NotNull(campo, "campo é obrigatório").Validate();
            IAssertion nomeDoCampoFoiDevidamentePreenchido = Assertion.IsFalse(string.IsNullOrWhiteSpace(campo.Nome), "O nome do campo deve ser informado");
            IAssertion naoExisteNenhumCampoComONomeInformado = Assertion.IsFalse(ExisteCampoComNome(campo.Nome), string.Format("Já existe um campo com o nome {0}", campo.Nome));

            #endregion

            nomeDoCampoFoiDevidamentePreenchido.and(naoExisteNenhumCampoComONomeInformado).Validate(this);

			Campos.Add(campo);
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

			#endregion

            StringBuilder sb = new StringBuilder();

            sb.Append(RenderizarTemplateTopo());
            sb.Append(RenderizarCss());

		    int indice = 0;

		    foreach (CampoDeProposta campoDeProposta in Campos.OrderBy(o => o.OrdemFormulario))
		    {
		        string campo = campoDeProposta.RenderizarParaFormulario();

                if (campo.Contains("@indice"))
                {
                    sb.Append(campo.Replace("@indice", indice.ToString()));
                    indice++;
                }
                else
                {
                    sb.Append(campo);
                }
                
		    }

		    sb.Append(RenderizarScripts());
            sb.Append(RenderizarRodape());
			return sb.ToString();
		}

		/// <summary>
		/// Renderiza um HTML para exibição em impressão com base nos campos existentes na proposta
		/// </summary>
		/// <returns></returns>
		public virtual string RenderizarTemplateDeImpressao()
		{
            #region Pré-condições

            #endregion

            StringBuilder sb = new StringBuilder();

		    sb.Append(RenderizarTemplateTopo());
		    sb.Append(RenderizarCss());
            foreach (CampoDeProposta campoDeProposta in Campos.OrderBy(o => o.OrdemFormulario).Where(a => a.VisivelNaImpressao))
            {
                sb.Append(campoDeProposta.RenderizarParaFormulario());
            }

		    sb.Append(RenderizarRodape());
            return sb.ToString();
		}


        private string RenderizarCss()
        {
            return @"<link rel='stylesheet' href='/Css/ModelosDeProposta.css'>";

        }

        /// <summary>
        /// Renderiza o topo do template
        /// </summary>
        /// <returns></returns>
        private string RenderizarTemplateTopo()
        {
            return @" <div id='container' class='container-modelo-de-proposta'>
                            <div class='span form'>
                                <div class='row'>";
        }

        /// <summary>
        /// Renderiza o rodapé do template
        /// </summary>
        /// <returns></returns>
        private string RenderizarRodape()
        {
            return @"</div>
                        </div>
                    </div>
                ";
        }

        /// <summary>
        /// Renderiza scripts
        /// </summary>
        /// <returns></returns>
        private string RenderizarScripts()
        {
            return @"<script src='//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js'></script>
            <script type='text/javascript'>
                $(function() {
                    $('input').focus(function () {
                        $(this).parent().parent().css('background-color', '#FFFFBF');
                    });

                    $('input').blur(function () {
                        $(this).parent().parent().css('background-color', 'transparent');
                    });
                });
            </script>";
        }
	}
}