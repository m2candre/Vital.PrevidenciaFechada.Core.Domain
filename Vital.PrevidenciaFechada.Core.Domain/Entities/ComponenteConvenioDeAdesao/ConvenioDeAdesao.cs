using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
	/// <summary>
	/// Convênio de adesão de um plano com um (Patrocinador/Instituidor)
	/// </summary>
	public class ConvenioDeAdesao : IAggregateRoot<Guid>
	{
		/// <summary>
		/// Id do contrato
		/// </summary>
		public virtual Guid Id { get; set; }

		/// <summary>
		/// Plano aderido
		/// </summary>
		public virtual Plano Plano { get; set; }

		/// <summary>
		/// Entidade a qual o convênio pertence
		/// </summary>
		public virtual Entidade Entidade { get; set; }

		/// <summary>
		/// Pessoa jurídica ( Patrocinador/ Instituidor )
		/// </summary>
		public virtual PessoaJuridica PessoaJuridica { get; set; }

		/// <summary>
		/// Proposta
		/// </summary>
		public virtual IList<Proposta> Propostas { get; set; }

		/// <summary>
		/// Modelos de proposta
		/// </summary>
		public virtual IList<ModeloDeProposta> ModelosDeProposta { get; set; }

		/// <summary>
		/// Construtor padrão para criação pelo NHibernate
		/// </summary>
		public ConvenioDeAdesao()
		{
			ModelosDeProposta = new List<ModeloDeProposta>();
			Propostas = new List<Proposta>();
		}

		/// <summary>
		/// Contrutor
		/// </summary>
		/// <remarks>O convênio de adesão só pode ser criado para formalizar </remarks>
		/// <param name="entidade"></param>
		/// <param name="pessoaJuridica"></param>
		/// <param name="plano"></param>
		public ConvenioDeAdesao(Entidade entidade, PessoaJuridica pessoaJuridica, Plano plano)
			: this()
		{
			#region Pré-condições

			IAssertion aEntidadeFoiInformada = Assertion.NotNull(entidade, "A entidade deve ser informada");
			IAssertion aPessoaJuridicaFoiInformada = Assertion.NotNull(pessoaJuridica, "A pessoa jurídica deve ser informada");
			IAssertion oPlanoFoiInformado = Assertion.NotNull(plano, "O plano deve ser informado");

			#endregion

			aEntidadeFoiInformada.and(aPessoaJuridicaFoiInformada).and(oPlanoFoiInformado).Validate();

			Entidade = entidade;
			PessoaJuridica = pessoaJuridica;
			Plano = plano;
		}

		/// <summary>
		/// Adiciona uma proposta ao convênio de adesão de uma Pessoa Jurídica com um plano
		/// </summary>
		/// <param name="proposta">proposta</param>
		public virtual void AdicionarProposta(Proposta proposta)
		{
			#region Pré-Condições

			IAssertion aPropostaFoiInformada = Assertion.NotNull(proposta, "A proposta não foi informada");
			IAssertion aListaDePropostasFoiInicializada = Assertion.NotNull(Propostas, "A lista de propostas não foi inicializada");
			IAssertion existeModeloDePropostaPublicado = Assertion.NotNull(ObterModeloDePropostaPublicado(), "O convênio de adesão não possui modelo de proposta publicado");

			#endregion

			aPropostaFoiInformada.and(aListaDePropostasFoiInicializada).Validate();

			int quantidadeDePropostasAntesDeAdicionar = Propostas.Count;

			proposta.IdDoConvenioDeAdesao = Id;
			proposta.ModeloDeProposta = ObterModeloDePropostaPublicado();
			Propostas.Add(proposta);

			#region Pós-Condições

			IAssertion foiAdicionadaUmaPropostaComSucesso = Assertion.GreaterThan(Propostas.Count, quantidadeDePropostasAntesDeAdicionar, "Não foi possível adicionar uma proposta ao convênio de adesão");

			#endregion

			foiAdicionadaUmaPropostaComSucesso.Validate();
		}

		/// <summary>
		/// Adiciona um modelo de proposta ao convênio de adesão de uma Pessoa Jurídica a um Plano
		/// </summary>
		/// <param name="modeloDeProposta"></param>
		public virtual void AdicionarModeloDeProposta(ModeloDeProposta modeloDeProposta)
		{
			#region Pré-Condições

			IAssertion oModeloDePropostaFoiInformado = Assertion.NotNull(modeloDeProposta, "O modelo de proposta não foi informado");
			IAssertion aListaDeModelosDePropostaFoiInicializada = Assertion.NotNull(ModelosDeProposta, "A lista modelos de proposta não foi inicializada");

			#endregion

			oModeloDePropostaFoiInformado.and(aListaDeModelosDePropostaFoiInicializada).Validate();

			int quantidadeDeModelosAntesDeAdicionar = ModelosDeProposta.Count;

			ModelosDeProposta.Add(modeloDeProposta);

			#region Pós-condição

			IAssertion umModeloDePropostaFoiAdicionado = Assertion.Equals(quantidadeDeModelosAntesDeAdicionar + 1, ModelosDeProposta.Count, "O modelo de proposta não foi adiciona na lista");

			#endregion

			umModeloDePropostaFoiAdicionado.Validate();
		}

		/// <summary>
		/// Obtém uma proposta por ID
		/// </summary>
		/// <param name="idDaProposta"></param>
		/// <returns></returns>
		public virtual Proposta ObterPropostaPorId(Guid idDaProposta)
		{
			#region Pré-condições

			Assertion.NotNull(idDaProposta, "O ID da proposta deve ser informado").Validate();

			#endregion

			var proposta = Propostas.SingleOrDefault(p => p.Id == idDaProposta);

			#region Pós-condições

			Assertion.NotNull(proposta, "Não foi encontrada proposta com o ID informado").Validate();

			#endregion

			return proposta;
		}

		/// <summary>
		/// Obtém uma lista de propostas em rascunho
		/// </summary>
		/// <returns></returns>
		public virtual IList<Proposta> ObterPropostasEmRascunho()
		{
			return Propostas.Where(p => p.Estado == "EmRascunho").ToList();
		}

        /// <summary>
        /// Obtém o modelo de proposta em rascunho
        /// </summary>
        /// <returns></returns>
        public virtual ModeloDeProposta ObterModeloDePropostaEmRascunho()
        {
            return ModelosDeProposta.SingleOrDefault(m => m.Publicada == false);
        }

		/// <summary>
		/// Obtem o único modelo de proposta publicado
		/// </summary>
		/// <returns>ModeloDeProposta</returns>
		public virtual ModeloDeProposta ObterModeloDePropostaPublicado()
		{
			var modeloDePropostaPublicado = ModelosDeProposta.SingleOrDefault(x => x.DataDePublicacao == ModelosDeProposta.Max(y => y.DataDePublicacao));

			#region Pós-condições

			IAssertion oModeloDePropostaFoiEncontrado = Assertion.NotNull(modeloDePropostaPublicado, "Modelo de proposta não encontrado");

			#endregion

			oModeloDePropostaFoiEncontrado.Validate();

			return modeloDePropostaPublicado;
		}
	}
}