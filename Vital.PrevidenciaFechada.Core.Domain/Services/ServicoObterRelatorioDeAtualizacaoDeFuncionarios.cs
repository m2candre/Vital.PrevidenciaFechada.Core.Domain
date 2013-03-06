using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteFuncionarioPreInscricao;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.Core.Domain.Services.Interfaces;
using Vital.PrevidenciaFechada.Core.Domain.ValueObject;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
    /// <summary>
    /// Serviço obtem um relatório de atualização de funcionários 
    /// </summary>
    public class ServicoObterRelatorioDeAtualizacaoDeFuncionarios : IServicoObterRelatorioDeAtualizacaoDeFuncionarios
    {
        private IRepositorio<FuncionarioPreInscricao> _funcionariosPreInscricao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcionarioPreInscricao">repositporio de funcionários da pre inscricao</param>
        public ServicoObterRelatorioDeAtualizacaoDeFuncionarios(IRepositorio<FuncionarioPreInscricao> funcionarioPreInscricao)
        {
            _funcionariosPreInscricao = funcionarioPreInscricao;
        }

        /// <summary>
        /// Obtem um DTO contendo a quantidade de novos funcionários e de funcionários que necessitam de update
        /// </summary>
        /// <param name="listaDeCpf">lista de cpf</param>
        /// <returns>RelatorioDeAtualizacaoDeFuncionariosDTO</returns>
        public RelatorioDeAtualizacaoDeFuncionariosDTO ObterRelatorio(IList<string> listaDeCpf)
        {
            //TODO: refatoração de Plano -> Configuração -> Pessoa Jurídica -> Funcionários da pré inscrição
            var todosFuncionariosDaPreInscricao = _funcionariosPreInscricao.Todos();

            var quantidadeDeFuncionariosParaAtualizar = ObterQuantidadeDeFuncionariosParaAtualizar(listaDeCpf, todosFuncionariosDaPreInscricao);

            var quantidadeDeNovos = ObterQuantidadeDeFuncionariosNovos(listaDeCpf.Count, quantidadeDeFuncionariosParaAtualizar);

            return ObterRelatorioDeAcordoCom(quantidadeDeNovos, quantidadeDeFuncionariosParaAtualizar);
        }

        /// <summary>
        /// Obtem um dto de relatório preenchido com o número de funcionários para insert e update
        /// </summary>
        /// <param name="quantidadeDeNovosFuncionarios">quantidade de novos funcionários</param>
        /// <param name="quantidadeDeFuncionariosParaAtualizar">quantidade de funcionários para atualizar</param>
        /// <returns>Relatório de atualização de funcionários</returns>
        private RelatorioDeAtualizacaoDeFuncionariosDTO ObterRelatorioDeAcordoCom(int quantidadeDeNovosFuncionarios, int quantidadeDeFuncionariosParaAtualizar)
        {
            var relatorio = new RelatorioDeAtualizacaoDeFuncionariosDTO();

            relatorio.NumeroDeNovosRegistros = quantidadeDeNovosFuncionarios;
            relatorio.NumeroDeRegistrosParaAtualizar = quantidadeDeFuncionariosParaAtualizar;

            return relatorio;
        }

        /// <summary>
        /// Obtem a quantidade de funcionários novos
        /// </summary>
        /// <param name="quantidadeTotalDeFuncionarios">quantidade total de funcionários</param>
        /// <param name="quantidadeDeFuncionarioaParaAtualizar">quantidade de funcionários para atualizar</param>
        /// <returns>int</returns>
        private int ObterQuantidadeDeFuncionariosNovos(int quantidadeTotalDeFuncionarios, int quantidadeDeFuncionarioaParaAtualizar)
        {
            return quantidadeTotalDeFuncionarios - quantidadeDeFuncionarioaParaAtualizar;
        }

        /// <summary>
        /// Obtem a quantidade de funcionários para atualizar
        /// </summary>
        /// <param name="listaDeCpf">lista de cpf</param>
        /// <param name="todosFuncionariosDaPreInscricao">todos os funcionários da pré-inscrição</param>
        /// <returns></returns>
        private int ObterQuantidadeDeFuncionariosParaAtualizar(IList<string> listaDeCpf, IList<FuncionarioPreInscricao> todosFuncionariosDaPreInscricao)
        {
            return (from c in listaDeCpf
                    join f in todosFuncionariosDaPreInscricao on c equals f.CPFDoParticipante
                    select f).Count();
        }
    }
}
