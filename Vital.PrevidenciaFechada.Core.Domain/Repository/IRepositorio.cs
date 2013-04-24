using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    public interface IRepositorio<T>
    {
        void Adicionar(IAggregateRoot<Guid> entidade);
        void AdicionarLista(List<IAggregateRoot<Guid>> entidades);
        void Remover(IAggregateRoot<Guid> entidade);
        T PorId(Guid id);
		IList<T> FiltrarTodos(ConsultaDTO consulta);
        IList<T> FiltrarPaginandoTodos(ConsultaDTO consulta);
        IList<T> Todos();
        IList<T> Todas();

        /// <summary>
        /// Obtem Todos os objetos filtrados e paginados de acordo com um critério específico
        /// </summary>
        /// <typeparam name="T">Objeto a ser filtrado</typeparam>
        /// <param name="criterios">criterios</param>
        /// <param name="consulta">parametros de consulta</param>
        /// <returns>Lista de objetos</returns>
		IList<T> ObterTodosFiltradosComCriterio<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios, ConsultaDTO consulta) where T : class;

		/// <summary>
		/// Obtém uma lista de objetos de acordo com um ou mais critérios
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <returns></returns>
		IList<T> ObterListaPor<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios) where T : class;

		/// <summary>
		/// Obtém um objeto de acordo com um critério específico
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="criterios"></param>
		/// <returns></returns>
		T ObterPor<T>(System.Linq.Expressions.Expression<Func<T, bool>> criterios) where T : class;
    }
}
