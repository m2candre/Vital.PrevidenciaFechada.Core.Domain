using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Mappers
{
    /// <summary>
    /// Mapper de PropostaVO para PropostaDTO
    /// </summary>
    public class PropostaMapper
    {

        /// <summary>
        /// Construtor - Configuração do mapper
        /// </summary>
        public PropostaMapper()
        {
            Mapper.CreateMap<PropostaDTO, PropostaVO>()
                .ForMember(dest => dest.CpfDoParticipante, opcao => opcao.MapFrom(source => source.CPF))
                .ForMember(dest => dest.NomeDoParticipante, opcao => opcao.MapFrom(source => source.Nome))
                .ForMember(dest => dest.Criticas, opcao => opcao.Ignore());
        }


        /// <summary>
        /// Mapper de propostaDTO para propostaVO
        /// </summary>
        /// <param name="propostaDTO">propostaDTO</param>
        /// <returns>PropostaVO</returns>
        public virtual PropostaVO DePropostaDTOParaPropostaVO(PropostaDTO propostaDTO)
        {
            return Mapper.Map<PropostaDTO, PropostaVO>(propostaDTO);
        }
    }
}
