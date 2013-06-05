using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Repository
{
    public interface IRepositorio<T>
    {
        void Salvar(IAggregateRoot<Guid> entidade);
        void Salvar(List<IAggregateRoot<Guid>> entidades);
		void Remover(IAggregateRoot<Guid> entidade);

		T ObterPorId(Guid id);
		
		T Obter(Expression<Func<T, bool>> criterios);
		T Obter(Expression<Func<T, bool>> criterios, ConsultaDTO consulta);

		IList<T> Todos();
        IList<T> ObterLista(ConsultaDTO consulta);
		IList<T> ObterLista(Expression<Func<T, bool>> criterios);
        IList<T> ObterLista(Expression<Func<T, bool>> criterios, ConsultaDTO consulta);
	}
}