using AutoMapper;
using System.Collections.Generic;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteModeloDeProposta
{
    /// <summary>
    /// Realiza mapeamentos CampoDaProposta x CampoDaPropostaDTO e CampoDaPropostaDTO x CampoDaProposta
    /// </summary>
    public class CampoDePropostaMapper
    {
        /// <summary>
        /// Construtor onde são declarados os Mappers
        /// </summary>
        public CampoDePropostaMapper()
        {
            Mapper.CreateMap<CampoDaPropostaDTO, CampoDeProposta>()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(orig => orig.Nome))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(orig => orig.Valor));

            Mapper.CreateMap<CampoDeProposta, CampoDaPropostaDTO>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(orig => orig.Titulo))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(orig => orig.Nome));
        }

        /// <summary>
        /// Converte de CampoDeProposta para CampoDaPropostaDTO
        /// </summary>
        /// <param name="campoDaProposta">CampoDeProposta</param>
        /// <returns></returns>
        public CampoDaPropostaDTO ObterCampoDaPropostaDTO(CampoDeProposta campoDaProposta)
        {
            CampoDaPropostaDTO dto = Mapper.Map<CampoDeProposta, CampoDaPropostaDTO>(campoDaProposta);
            return dto;
        }

        /// <summary>
        /// Converte de CampoDaPropostaDTO para CampoDaProposta
        /// </summary>
        /// <param name="campoDaProposta">CampoDeProposta</param>
        /// <returns></returns>
        public CampoDeProposta ObterCampoDaProposta(CampoDaPropostaDTO campoDaPropostaDTO)
        {
            CampoDeProposta campoDeProposta = Mapper.Map<CampoDaPropostaDTO, CampoDeProposta>(campoDaPropostaDTO);
            return campoDeProposta;
        }

        /// <summary>
        /// Converte uma lista de CampoDeProposta para uma lista de CampoDaPropostaDTO
        /// </summary>
        /// <param name="camposDaProposta"></param>
        /// <returns></returns>
        public List<CampoDaPropostaDTO> ObterListaDeCamposDaPropostaDTO(List<CampoDeProposta> camposDaProposta)
        {
            List<CampoDaPropostaDTO> camposDTO = new List<CampoDaPropostaDTO>();
            camposDaProposta.ForEach(campo => camposDTO.Add(ObterCampoDaPropostaDTO(campo)));

            return camposDTO;
        }
    }
}
