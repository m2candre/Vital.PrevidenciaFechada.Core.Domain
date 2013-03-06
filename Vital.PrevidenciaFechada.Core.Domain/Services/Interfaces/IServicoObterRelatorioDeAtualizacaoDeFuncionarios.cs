using System;
using System.Collections.Generic;
using Vital.PrevidenciaFechada.Core.Domain.ValueObject;

namespace Vital.PrevidenciaFechada.Core.Domain.Services.Interfaces
{
    /// <summary>
    /// Serviço obtem um relatório de atualização de funcionários 
    /// </summary>
    public interface IServicoObterRelatorioDeAtualizacaoDeFuncionarios
    {
        /// <summary>
        /// Obtem um DTO contendo a quantidade de novos funcionários e de funcionários que necessitam de update
        /// </summary>
        /// <param name="listaDeCpf">lista de cpf</param>
        /// <returns>RelatorioDeAtualizacaoDeFuncionariosDTO</returns>
        RelatorioDeAtualizacaoDeFuncionariosDTO ObterRelatorio(IList<string> listaDeCpf);
    }
}
