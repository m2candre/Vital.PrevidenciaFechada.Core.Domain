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
    /// Mapper de CriticaVO para CriticaDTO
    /// </summary>
    public class CriticaMapper
    {

        /// <summary>
        /// Construtor - Configuração do Mapper
        /// </summary>
        public CriticaMapper()
        {
            Mapper.CreateMap<CriticaVO, CriticaDTO>();
        }

        /// <summary>
        /// Mapper de CriticaVO para CriticaDTO
        /// </summary>
        /// <param name="criticaVO">CriticaVO</param>
        /// <returns>CriticaDTO</returns>
        public virtual IList<CriticaDTO> DeCriticaVOParaCriticaDTO(IList<CriticaVO> criticasVO)
        {
            List<CriticaDTO> criticasDTO = new List<CriticaDTO>();

            foreach (var criticaVO in criticasVO)
            {
                criticasDTO.Add(Mapper.Map<CriticaVO, CriticaDTO>(criticaVO));
            }

            return criticasDTO;
        }
    }
}
