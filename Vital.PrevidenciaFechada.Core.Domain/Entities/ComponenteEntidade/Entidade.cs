using System;
using System.Collections.Generic;
using System.Linq;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade
{
    /// <summary>
    /// Entidade Fechada de Previdência Complementar , Possui de (1..N) Plano(s) vinculado(s)
    /// </summary>
    public class Entidade : IAggregateRoot<Guid>
    {
        /// <summary>
        /// Id da Entidade
        /// </summary>
        public virtual Guid Id { get; set; }
        
        /// <summary>
        /// Nome da Entidade
        /// </summary>
        public virtual string Nome { get; set; }

        /// <summary>
        /// Lista de Planos da Entidade
        /// </summary>
        public virtual IList<Plano> Planos { get; set; }

        /// <summary>
        /// Construtor, sem dependencias
        /// </summary>
        public Entidade()
        {
            Planos = new List<Plano>();
        }

        /// <summary>
        /// Adiciona um plano a Entidade
        /// </summary>
        /// <param name="plano">Plano novo da Entidade</param>
        public virtual void AdicionarPlano(Plano plano)
        {
            Planos.Add(plano);
        }

        /// <summary>
        /// Retorna um Plano pelo ID
        /// </summary>
        /// <param name="id">Id do Plano</param>
        /// <returns></returns>
        public virtual Plano BuscarPlanoCom(Guid id)
        {
            //TODO: incluir Dbc, e se não há planos??
            return Planos.FirstOrDefault(p => p.Id == id);
        }
    }
}