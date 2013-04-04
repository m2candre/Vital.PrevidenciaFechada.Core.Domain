using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras.ValidacoesDeDados;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    /// <summary>
    /// Regulamento de um plano de previdência fechada
    /// </summary>
    public class Regulamento
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Regras do plano
        /// </summary>
        public virtual IList<Regra> Regras { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public Regulamento()
        {
            Regras = new List<Regra>();

            Adicionar(new RegraCpfObrigatorio());
            Adicionar(new RegraNomeDoParticipanteObrigatorio());
            Adicionar(new RegraValidarCpf());
        }

        /// <summary>
        /// Adiciona uma regra ao regulamento
        /// </summary>
        /// <param name="regra">regr</param>
        public virtual void Adicionar(Regra regra)
        {
            #region Pré-Condições

            IAssertion regraFoiInformada = Assertion.NotNull(regra, "A regra não foi informada");
            IAssertion aColecaoDeRegrasFoiInicializada = Assertion.NotNull(Regras, "A coleção de regras não foi inicializada corretamente");

            #endregion

            regraFoiInformada.and(aColecaoDeRegrasFoiInicializada).Validate();

            int quantidadeDeRegrasAntesDeAdicionar = Regras.Count;

            Regras.Add(regra);

            #region Pós-Condições

            IAssertion foiAdicionadaUmaNovaRegraNaColecaoDeRegras = Assertion.GreaterThan(Regras.Count, quantidadeDeRegrasAntesDeAdicionar, "Não foi possível adicionar uma nova regra");

            #endregion

            foiAdicionadaUmaNovaRegraNaColecaoDeRegras.Validate();
        }

        /// <summary>
        /// Obtem uma lista de criticas da proposta
        /// </summary>
        /// <param name="proposta">dados da proposta</param>
        /// <returns>Lista de críticas</returns>
        public virtual IList<CriticaVO> ObterCriticasDaProposta(PropostaVO proposta)
        {
            #region Pré-Condições

            IAssertion aPropostaFoiInformada = Assertion.NotNull(proposta, "A proposta não foi informada");
            IAssertion aListadeRegrasFoiInicializada = Assertion.NotNull(Regras, "A lista de regras não foi inicializada");

            #endregion

            aPropostaFoiInformada.and(aListadeRegrasFoiInicializada).Validate();

            foreach (var regra in Regras)
                proposta = regra.Validar(proposta);

            return proposta.Criticas;
        }
    }
}
