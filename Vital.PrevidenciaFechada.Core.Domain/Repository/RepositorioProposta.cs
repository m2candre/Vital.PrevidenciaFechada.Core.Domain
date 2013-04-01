using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
	public class RepositorioProposta : Repositorio<Proposta>, IRepositorioProposta
	{
		/// <summary>
		/// Construtor padrão
		/// </summary>
		public RepositorioProposta() { }

		/// <summary>
		/// Construtor injetando ISession
		/// </summary>
		/// <param name="session"></param>
		public RepositorioProposta(ISession session)
			: base(session) { }

		/// <summary>
		/// Obtém o último número de proposta cadastrado
		/// </summary>
		/// <returns></returns>
		public int ObterUltimoNumeroDaProposta()
		{
			return (int)Session.CreateCriteria<Proposta>()
				.SetProjection(VitalCriterion.Max("Numero"))
				.UniqueResult();
		}
	}
}