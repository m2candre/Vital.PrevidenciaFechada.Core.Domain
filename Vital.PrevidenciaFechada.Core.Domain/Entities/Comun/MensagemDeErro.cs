using System;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.Comun
{
    public class MensagemDeErro
    {
        /// <summary>
        /// ID da mensagem de erro
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Nome do campo com erro
        /// </summary>
        public virtual string Campo { get; set; }

        /// <summary>
        /// Mensagem de erro do campo
        /// </summary>
        public virtual string Mensagem { get; set; }
    }
}