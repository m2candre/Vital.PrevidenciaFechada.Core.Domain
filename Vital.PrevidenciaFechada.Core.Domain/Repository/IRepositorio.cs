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
        IList<T> FiltrarTodos(ConsultaDTO<T> consulta);
        IList<T> FiltrarPaginandoTodos(ConsultaDTO<T> consulta);
        IList<T> Todos();
        IList<T> Todas();

    }
}
