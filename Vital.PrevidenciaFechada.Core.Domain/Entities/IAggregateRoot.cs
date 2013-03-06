using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities
{
    /// <summary>
    /// Interface para o Padrão de Agregação do DDD
    /// </summary>
    /// <typeparam name="T">Tipo de Entidade</typeparam>
    public interface IAggregateRoot<T>
    {
        T Id { get; set; }
    }
}
