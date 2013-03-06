namespace Vital.PrevidenciaFechada.Core.Domain.ValueObject
{
    /// <summary>
    /// Relatório de atualização de funcionário
    /// </summary>
    public class RelatorioDeAtualizacaoDeFuncionariosDTO
    {
        /// <summary>
        /// Arquivo
        /// </summary>
        public byte[] Arquivo { get; set; }

        /// <summary>
        /// Número de registros para atualizar
        /// </summary>
        public int NumeroDeRegistrosParaAtualizar{ get; set; }

        /// <summary>
        /// Número de novos registros
        /// </summary>
        public int NumeroDeNovosRegistros { get; set; }
    }
}