using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteConvenioDeAdesao;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    /// <summary>
    /// Repositório de Convênio de Adesão
    /// </summary>
    public class RepositorioConvenioDeAdesao : Repositorio<ConvenioDeAdesao>, IRepositorioConvenioDeAdesao
    {
        /// <summary>
        /// Último número de proposta
        /// </summary>
        /// <param name="PlanoId">PlanoId</param>
        /// <param name="PessoaJuridicaId">PessoaJuridicaId</param>
        /// <returns>int</returns>
        public int UltimoNumeroDeProposta(Guid PlanoId, Guid PessoaJuridicaId)
        {
            //PLANO, PESSOAJURIDICA, Proposta, Convenio de Adesão

            var criterio = Session.CreateCriteria(typeof(ConvenioDeAdesao), "c")
                  .CreateCriteria("c.PessoaJuridica", "pj", NHibernate.SqlCommand.JoinType.InnerJoin)
                  .Add(Expression.Eq("pj.Id", PessoaJuridicaId));

            var convenios = criterio.List<ConvenioDeAdesao>();

            var criterioPlano = Session.CreateCriteria(typeof(Plano), "p")
                .Add(Expression.Eq("p.Id", PlanoId));

            var plano = criterioPlano.UniqueResult<Plano>();

            int ultimoNumeroDaProposta = (from cp in plano.ConvenioDeAdesao
                                             join c in convenios on cp.Id equals c.Id
                                             select c.Propostas.Max(x=> x.Numero)).SingleOrDefault();

            return ultimoNumeroDaProposta;
        }
    }
}
