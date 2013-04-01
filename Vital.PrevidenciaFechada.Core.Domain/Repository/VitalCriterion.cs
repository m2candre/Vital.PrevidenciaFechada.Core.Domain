using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.DesignByContract;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    /// <summary>
    /// Classe de critério da vital
    /// </summary>
    public class VitalCriterion
    {
        /// <summary>
        /// Adiciona uma cláusula Where de acordo com critério
        /// </summary>
        /// <typeparam name="T">Tipo da query</typeparam>
        /// <param name="criterios">critérios</param>
        /// <returns>ICriterion</returns>
        public virtual ICriterion Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios)
        {
            return Expression.Where(criterios);
        }

        /// <summary>
        /// Like 
        /// </summary>
        /// <param name="propertyName">nome da propriedade</param>
        /// <param name="value">valor de comparação</param>
        /// <param name="matchMode">estratégia de comparação</param>
        /// <returns>ICriterion</returns>
        public virtual ICriterion Like(string propertyName, string value, MatchMode matchMode)
        {
            return Expression.Like(propertyName, value, matchMode);
        }

        /// <summary>
        /// Obter objeto Order por projection e ascending
        /// </summary>
        /// <param name="projection">Projection</param>
        /// <param name="ascending">ascending</param>
        /// <returns>Order</returns>
        public virtual Order OrderBy(string projection, bool ascending)
        {
            return new Order(projection, ascending);
        }

		/// <summary>
		/// Obter o valor máximo de uma determinada propriedade persistida
		/// </summary>
		/// <typeparam name="T">Tipo da entidade pesquisada</typeparam>
		/// <param name="expressao">Expressão contendo a propriedade</param>
		/// <returns></returns>
		public virtual IProjection Max(string projecion)
		{

			return Projections.Max(projecion);
		}
    }
}
