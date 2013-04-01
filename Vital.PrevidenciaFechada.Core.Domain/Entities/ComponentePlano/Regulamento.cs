using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vital.InfraStructure.DSL.DesignByContract;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    /// <summary>
    /// Regulamento de um plano de previdência fechada
    /// </summary>
    public class Regulamento
    {
        /// <summary>
        /// Regras do plano
        /// </summary>
        public virtual IList<IRegra> Regras { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public Regulamento()
        {
            Regras = new List<IRegra>();
        }

        /// <summary>
        /// Adiciona uma regra ao regulamento
        /// </summary>
        /// <param name="regra">regr</param>
        public virtual void Adicionar(IRegra regra)
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
    }
}
