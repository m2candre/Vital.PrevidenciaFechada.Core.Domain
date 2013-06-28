using AutoMapper;
using System.Collections.Generic;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using Vital.PrevidenciaFechada.DTO.Messages.Adesao;

namespace Vital.PrevidenciaFechada.Core.Domain.Mappers
{
    /// <summary>
    /// Classe de mapeamento entre DTO de pré inscritos e pré inscritos
    /// </summary>
    public class PreInscritosMapper
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public PreInscritosMapper()
        {
            Mapper.CreateMap<PreInscrito, FuncionarioPreInscricaoDTO>();
        }

        /// <summary>
        /// Obter lista de Funcionários da pré inscrição
        /// </summary>
        /// <param name="funcionariosPreInscricao">Pre inscritos</param>
        /// <returns><see cref="List<FuncionarioPreInscricaoDTO"/>Lista de objetos</returns>
        public virtual List<FuncionarioPreInscricaoDTO> ObterFuncionariosPreInscricaoDTO(List<PreInscrito> funcionariosPreInscricao)
        {
            List<FuncionarioPreInscricaoDTO> funcionariosDTO = new List<FuncionarioPreInscricaoDTO>();

            foreach (var funcionario in funcionariosPreInscricao)
            {
                funcionariosDTO.Add(Mapper.Map<PreInscrito, FuncionarioPreInscricaoDTO>(funcionario));
            }

            return funcionariosDTO;
        }
    }
}