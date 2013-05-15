using System;
using System.Security.Principal;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario
{
    public class Usuario : IAggregateRoot<Guid>
	{
		/// <summary>
		/// ID do usuário
		/// </summary>
		public virtual Guid Id { get; set; }

		/// <summary>
		/// ID do Convênio de Adesão
		/// </summary>
		public virtual Guid IdDoConvenioDeAdesao { get; set; }

		/// <summary>
		/// Hash do token de sessão para habilitar o acesso do usuário
		/// </summary>
		public virtual Guid TokenDeSessao { get; set; }

		/// <summary>
		/// Data/hora da última requisição enviada pelo usuário para controlar limite por tempo ocioso
		/// </summary>
		public virtual DateTime? DataHoraDaUltimaRequisicao { get; set; }

		/// <summary>
		/// Nome do usuário
		/// </summary>
		public virtual string Nome { get; set; }

		/// <summary>
		/// Login do usuário
		/// </summary>
		public virtual string NomeDeUsuario { get; set; }

		/// <summary>
		/// Hash MD5 da senha do usuário
		/// </summary>
		public virtual string Senha { get; set; }

		/// <summary>
		/// E-mail do usuário
		/// </summary>
		public virtual string Email { get; set; }
    }
}