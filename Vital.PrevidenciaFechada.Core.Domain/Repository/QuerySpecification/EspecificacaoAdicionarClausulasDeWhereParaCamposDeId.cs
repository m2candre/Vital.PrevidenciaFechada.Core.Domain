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
    /// Especificação de Where para Id - Guid
    /// </summary>
    public class EspecificacaoAdicionarClausulasDeWhereParaCamposDeId : Especificacao<FiltroDTO>
    {
        private FiltroDTO _filtroDTO;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="filtroDTO">Dados de filtro</param>
        public EspecificacaoAdicionarClausulasDeWhereParaCamposDeId(FiltroDTO filtroDTO)
        {
            _filtroDTO = filtroDTO;
        }

        /// <summary>
        /// Verifica se a especificação é verdadeira
        /// </summary>
        /// <returns>bool</returns>
        public override bool EstaSatisfeita()
        {
            Guid id = Guid.Empty;

            return Guid.TryParse(_filtroDTO.Valor.ToString(), out id);
        }

        /// <summary>
        /// Monta uma critérios de consulta
        /// </summary>
        /// <param name="criterios">critérios de consulta</param>
        public override void MontarCriterios(ICriteria criterios)
        {
            if (EstaSatisfeita())
            {
                criterios
                   .Add(Expression.Eq(_filtroDTO.Campo, new Guid(_filtroDTO.Valor.ToString())));
            }
        }
    }
}
