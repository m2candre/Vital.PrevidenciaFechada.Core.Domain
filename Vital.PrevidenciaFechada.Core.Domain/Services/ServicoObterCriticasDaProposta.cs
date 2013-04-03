using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Mappers;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
    /// <summary>
    /// Servico obter críticas da proposta
    /// </summary>
    public class ServicoObterCriticasDaProposta
    {
        private IRepositorio<Plano> _planos;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="planos">Repositório de planos</param>
        public ServicoObterCriticasDaProposta(IRepositorio<Plano> planos)
        {
            _planos = planos;
        }

        /// <summary>
        /// Obtem uma lista de críticas da proposta
        /// </summary>
        /// <param name="propostaDTO">proposta</param>
        /// <returns>Lista de críticas</returns>
        public virtual IList<CriticaDTO> Obter(PropostaDTO propostaDTO, Guid IdDoPlano)
        {
            var plano = _planos.PorId(IdDoPlano);

            var propostaVO = new PropostaMapper().DePropostaDTOParaPropostaVO(propostaDTO);

            IList<CriticaVO> criticasVO = plano.Regulamento.ObterCriticasDaProposta(propostaVO);

            return new CriticaMapper().DeCriticaVOParaCriticaDTO(criticasVO);
        }
    }
}
