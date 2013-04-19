using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.QuerySpecification;
using Vital.PrevidenciaFechada.DTO.Messages;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository.QuerySpecification
{
    /// <summary>
    /// Especificação de Clausula Like para tipo de texto
    /// </summary>
    public class EspecificacaoAdicionarClausulaLikeParaCamposDeTexto: Especificacao<FiltroDTO>
    {
        private FiltroDTO _filtroDTO;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="filtroDTO">Dados de filtro</param>
        public EspecificacaoAdicionarClausulaLikeParaCamposDeTexto(FiltroDTO filtroDTO)
        {
            _filtroDTO = filtroDTO;
        }

        /// <summary>
        /// Verifica se a especificação é verdadeira
        /// </summary>
        /// <returns>bool</returns>
        public override bool EstaSatisfeita()
        {
            return _filtroDTO.Valor.GetType() == typeof(string);
        }

        /// <summary>
        /// Monta critérios de consulta
        /// </summary>
        /// <param name="criterios">critérios de consulta</param>
        public override void MontarCriterios(ICriteria criterios)
        {
            if (EstaSatisfeita())
            {
                criterios.Add(Expression.Like(_filtroDTO.Campo, _filtroDTO.Valor.ToString(), MatchMode.Anywhere));
            }
        }
    }
}
