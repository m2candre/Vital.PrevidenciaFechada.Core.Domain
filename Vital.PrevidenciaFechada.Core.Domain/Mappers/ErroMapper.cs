using AutoMapper;
using Vital.PrevidenciaFechada.Core.Domain.Entities.Comun;
using Vital.PrevidenciaFechada.DTO.Messages.Comun;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Mappers
{
    public class ErroMapper
    {
        public ErroMapper()
        {
            Mapper.CreateMap<MensagemDeErro, MensagemDTO>();
            Mapper.CreateMap<MensagemDTO, MensagemDeErro>();

            Mapper.CreateMap<Erro, ErroDTO>();
            Mapper.CreateMap<ErroDTO, Erro>();
        }

        /// <summary>
        /// Obtém a entidade Erro a partir do seu DTO
        /// </summary>
        /// <param name="dto">DTO de erro</param>
        /// <returns></returns>
        public Erro ObterErro(ErroDTO dto)
        {
            return Mapper.Map<ErroDTO, Erro>(dto);
        }

        /// <summary>
        /// Obtém o DTO de erro a partir da entidade
        /// </summary>
        /// <param name="erro">Entidade Erro</param>
        /// <returns></returns>
        public ErroDTO ObterErroDTO(Erro erro)
        {
            return Mapper.Map<Erro, ErroDTO>(erro);
        }
    }
}