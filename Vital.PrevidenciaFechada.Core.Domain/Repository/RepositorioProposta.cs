using NHibernate;
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
		/// Obtém uma lista de propostas respeitando determinados critérios
		/// </summary>
		/// <param name="criterio">critérios para a busca</param>
		/// <returns></returns>
		public IList<Proposta> ObterPropostasPorCriterio(Expression<Func<Proposta, bool>> criterios)
		{
			return Session.QueryOver<Proposta>().Where(criterios).List();
		}
	}
}