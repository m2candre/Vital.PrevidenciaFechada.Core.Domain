using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.DTO.Messages;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Mappers
{
    /// <summary>
    /// Mapeia a entidade Convenio de Adesao em DTO
    /// </summary>
    public class ConvenioDeAdesaoMapper
    {
        public ConvenioDeAdesaoMapper()
        {
            Mapper.CreateMap<ConvenioDeAdesao, ConvenioDeAdesaoDTO>();
            Mapper.CreateMap<ConvenioDeAdesaoDTO, ConvenioDeAdesao>();

            //Entidades contidas dentro do Convenio de Adesao

            Mapper.CreateMap<Entidade, EntidadeDTO>();
            Mapper.CreateMap<EntidadeDTO, Entidade>();

            Mapper.CreateMap<Plano, PlanoDTO>();
            Mapper.CreateMap<PlanoDTO, Plano>();

            Mapper.CreateMap<PessoaJuridica, PessoaJuridicaDTO>();
            Mapper.CreateMap<PessoaJuridicaDTO, PessoaJuridica>();
        }

        /// <summary>
        /// Mapeia um Convenio de Adesao em DTO
        /// </summary>
        /// <param name="entidade">Entidade para mapeamento</param>
        /// <returns></returns>
        public virtual ConvenioDeAdesaoDTO ObterDTO(ConvenioDeAdesao convenioDeAdesao)
        {
            return Mapper.Map<ConvenioDeAdesao, ConvenioDeAdesaoDTO>(convenioDeAdesao);
        }

        /// <summary>
        /// Mapeia um Convenio de Adesao DTO em entidade Convenio de Adesao
        /// </summary>
        /// <param name="dto">Objeto de Entidade para mapeamento</param>
        /// <returns></returns>
        public virtual ConvenioDeAdesao ObterEntidade(ConvenioDeAdesaoDTO ConvenioDeAdesaoDTO)
        {
            return Mapper.Map<ConvenioDeAdesaoDTO, ConvenioDeAdesao>(ConvenioDeAdesaoDTO);
        }

        /// <summary>
        /// Mapeia uma lista de Convenios de Adesao em uma Lista de Convenio de Adesao DTO
        /// </summary>
        /// <param name="usuarios">Lista das Identidades dos usuários cadastrados</param>
        /// <returns></returns>
        public virtual List<ConvenioDeAdesaoDTO> ObterListaDeConvenioDeAdesaoDTO(List<ConvenioDeAdesao> convenioDeAdesao)
        {
            var usuariosDTO = new List<ConvenioDeAdesaoDTO>();
            convenioDeAdesao.ForEach(usr => usuariosDTO.Add(ObterDTO(usr)));
            return usuariosDTO;
        }
    }
}
