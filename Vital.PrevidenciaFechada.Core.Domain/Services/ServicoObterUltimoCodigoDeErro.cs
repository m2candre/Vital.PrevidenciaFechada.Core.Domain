using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.Comun;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
    /// <summary>
    /// Serviço para obter último código de erro
    /// </summary>
    public class ServicoObterUltimoCodigoDeErro
    {
        private IRepositorio<Erro> _erros;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="erros">repositório de erros</param>
        public ServicoObterUltimoCodigoDeErro(IRepositorio<Erro> erros)
        {
            _erros = erros;
        }

        /// <summary>
        /// Obtem o último numero de erro
        /// </summary>
        /// <returns></returns>
        public int Obter()
        {
			var erros = _erros.Todos();
			
			if (erros == null || erros.Count == 0) return 1;
            return erros.Max(x => x.Numero) + 1;
        }
    }
}
