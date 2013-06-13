using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario;
using Vital.PrevidenciaFechada.DTO.Messages;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Mappers
{
    /// <summary>
    /// Mapeia entidade Usuário em DTO e vice versa
    /// </summary>
    public class UsuarioMapper
    {
        /// <summary>
        /// Construtor definindo o esquema de mapping
        /// </summary>
        public UsuarioMapper()
        {
			Mapper.CreateMap<Usuario, UsuarioDTO>();
            Mapper.CreateMap<UsuarioDTO, Usuario>();
        }

        /// <summary>
        /// Mapeia um Usuário em UsuarioDTO
        /// </summary>
        /// <param name="identidade">Objeto de Identidade para mapeamento</param>
        /// <returns></returns>
        public virtual UsuarioDTO ObterDTO(Usuario usuario)
        {
            return Mapper.Map<Usuario, UsuarioDTO>(usuario);
        }

        /// <summary>
        /// Mapeia um UsuárioDTO em Usuario
        /// </summary>
        /// <param name="dto">Objeto de Entidade para mapeamento</param>
        /// <returns></returns>
        public virtual Usuario ObterEntidade(UsuarioDTO usuarioDTO)
        {
            return Mapper.Map<UsuarioDTO, Usuario>(usuarioDTO);
        }

        /// <summary>
        /// Mapeia uma lista de Usuários em uma Lista de UsuarioDTO
        /// </summary>
        /// <param name="usuarios">Lista das Identidades dos usuários cadastrados</param>
        /// <returns></returns>
        public virtual List<UsuarioDTO> ObterListaDeUsuarioDTO(List<Usuario> usuarios)
        {
            var usuariosDTO = new List<UsuarioDTO>();
            usuarios.ForEach(usr => usuariosDTO.Add(ObterDTO(usr)));
            return usuariosDTO;
        }
    }
}