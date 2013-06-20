using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.Comun
{
    /// <summary>
    /// Erro
    /// </summary>
    public class Erro : IAggregateRoot<Guid>
    {
        /// <summary>
        /// ID do erro gerado
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Código do erro
        /// </summary>
        public virtual int Codigo { get; set; }

        /// <summary>
        /// Mensagens de erro no formato Campo/Mensagem
        /// </summary>
        public virtual IList<MensagemDeErro> Mensagens { get; set; }

        /// <summary>
        /// Mensagem da exceção técnica
        /// </summary>
        public virtual string Excecao { get; set; }

        /// <summary>
        /// Stack Trace
        /// </summary>
        public virtual string StackTrace { get; set; }

        /// <summary>
        /// Data do Registro
        /// </summary>
        public virtual DateTime? DataDoRegistro { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public Erro()
        {
            Mensagens = new List<MensagemDeErro>();
        }
    }
}
