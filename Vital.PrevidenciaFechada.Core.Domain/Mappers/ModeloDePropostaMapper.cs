using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Vital.PrevidenciaFechada.DTO.Messages.Core;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Mappers
{
    public class ModeloDePropostaMapper
    {
        public ModeloDePropostaDTO ObterModeloDaPropostaDTO(ModeloDeProposta modeloDaProposta)
        {
            return Mapper.Map<ModeloDeProposta, ModeloDePropostaDTO>(modeloDaProposta);
        }

        public ModeloDeProposta ObterModeloDaProposta(ModeloDePropostaDTO modeloDaPropostaDTO)
        {
            return Mapper.Map<ModeloDePropostaDTO, ModeloDeProposta>(modeloDaPropostaDTO);
        }
    }
}
