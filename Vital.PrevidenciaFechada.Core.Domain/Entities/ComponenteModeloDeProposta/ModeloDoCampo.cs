using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano
{
    public class ModeloDoCampo : IAggregateRoot<Guid>
    {
        /// <summary>
        /// id do modelo de proposta
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Nome do modelo do campo
        /// </summary>
        public virtual string NomeDoModelo { get; set; }

        /// <summary>
        /// Html do modelo para o formulário
        /// </summary>
        public virtual string ModeloParaFormulario { get; set; }

        /// <summary>
        /// Html do modelo para impressão
        /// </summary>
        public virtual string ModeloParaImpressao { get; set; }

        /// <summary>
        /// Cria um modelo de campo
        /// </summary>
        /// <param name="nomeDoModelo"></param>
        /// <param name="modeloParaFormulario"></param>
        /// <param name="modeloParaImpressao"></param>
        public ModeloDoCampo(string nomeDoModelo, string modeloParaFormulario, string modeloParaImpressao)
        {
            this.NomeDoModelo = nomeDoModelo;
            this.ModeloParaFormulario = modeloParaFormulario;
            this.ModeloParaImpressao = modeloParaImpressao;
        }
    }
}
