using System;
using System.Collections.Generic;
using System.Linq;
using Vital.InfraStructure.DSL.DesignByContract;
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
        /// Lista de Convênios de Adesão
        /// </summary>
		/// <remarks>Uma entidade administra plano através de um convênio de adesão</remarks>
        public virtual IList<ConvenioDeAdesao> ConveniosDeAdesao { get; set; }

        /// <summary>
        /// Construtor, sem dependencias
        /// </summary>
        public Entidade()
        {
            ConveniosDeAdesao = new List<ConvenioDeAdesao>();
        }

        /// <summary>
        /// Adiciona um plano a Entidade
        /// </summary>
        /// <param name="plano">Plano novo da Entidade</param>
        public virtual void AdicionarConvenio(ConvenioDeAdesao convenioDeAdesao)
        {
            ConveniosDeAdesao.Add(convenioDeAdesao);
        }

        /// <summary>
        /// Retorna um Plano pelo ID
        /// </summary>
        /// <param name="id">Id do Plano</param>
        /// <returns></returns>
        public virtual ConvenioDeAdesao BuscarConvenioPorId(Guid id)
		{
			#region Pré-condições

			IAssertion aListaDeConveniosEstaPreenchida = Assertion.NotNull(ConveniosDeAdesao, "A lista de convênios de adesão não pode estar nula");

			#endregion

			aListaDeConveniosEstaPreenchida.Validate();

			return ConveniosDeAdesao.FirstOrDefault(p => p.Id == id);
        }
    }
}