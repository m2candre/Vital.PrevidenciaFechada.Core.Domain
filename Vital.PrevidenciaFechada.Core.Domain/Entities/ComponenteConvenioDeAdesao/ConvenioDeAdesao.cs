using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    /// <summary>
    /// Convênio de adesão de um plano com um (Patrocinador/Instituidor)
    /// </summary>
    public class ConvenioDeAdesao
    {
        /// <summary>
        /// Id do contrato
        /// </summary>
        public virtual Guid Id { get; set; }

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
        /// Construtor
        /// </summary>
        public ConvenioDeAdesao()
        {
            ModelosDeProposta = new List<ModeloDeProposta>();
            Propostas = new List<Proposta>();
        }

        /// <summary>
        /// Adiciona uma proposta ao convênio de adesão de uma Pessoa Jurídica com um plano
        /// </summary>
        /// <param name="proposta">proposta</param>
        public virtual void Adicionar(Proposta proposta)
        {
            #region Pré-Condições

            IAssertion aPropostaFoiInformada = Assertion.NotNull(proposta, "A proposta não foi informada");
            IAssertion aListaDePropostasFoiInicializada = Assertion.NotNull(Propostas, "A lista de propostas não foi inicializada");

            #endregion

            aPropostaFoiInformada.and(aListaDePropostasFoiInicializada).Validate();

            int quantidadeDePropostasAntesDeAdicionar = Propostas.Count;

            Propostas.Add(proposta);

            #region Pós-Condições

            IAssertion foiAdicionadaUmaPropostaComSucesso = Assertion.GreaterThan(Propostas.Count, quantidadeDePropostasAntesDeAdicionar, "Não foi possível adicionar uma proposta ao convênio de adesão");

            #endregion

            foiAdicionadaUmaPropostaComSucesso.Validate();
        }

        /// <summary>
        /// Obtem o único modelo de proposta publicado
        /// </summary>
        /// <returns>ModeloDeProposta</returns>
        public virtual ModeloDeProposta ObterModeloDePropostaPublicado()
        {
            return ModelosDeProposta.SingleOrDefault(x => x.DataDePublicacao == ModelosDeProposta.Max(y => y.DataDePublicacao));
        }
    }
}
