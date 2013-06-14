using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario;
using Vital.PrevidenciaFechada.DTO.Messages;
using Vital.PrevidenciaFechada.DTO.Messages.Atuarial.InscricaoEmPapel;
using Vital.PrevidenciaFechada.DTO.Messages.Comun;

namespace Vital.PrevidenciaFechada.Core.Domain.Mappers
{
    public class RejeicaoDePropostaMapper
    {
        public RejeicaoDePropostaMapper()
        {
            Mapper.CreateMap<RejeicaoDeProposta, RejeicaoDePropostaDTO>();
            Mapper.CreateMap<RejeicaoDePropostaDTO, RejeicaoDeProposta>();
            Mapper.CreateMap<TipoRejeicao, TipoRejeicaoDTO>();
            Mapper.CreateMap<TipoRejeicaoDTO, TipoRejeicao>();
        }

        public virtual RejeicaoDeProposta ObterEntidade(RejeicaoDePropostaDTO rejeicaoDePropostaDTO)
        {
            return Mapper.Map<RejeicaoDePropostaDTO, RejeicaoDeProposta>(rejeicaoDePropostaDTO);
        }
    }
}
