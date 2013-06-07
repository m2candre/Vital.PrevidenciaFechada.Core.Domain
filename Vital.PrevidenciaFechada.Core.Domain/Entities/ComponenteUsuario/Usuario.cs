using System;
using System.Collections.Generic;
using System.Security.Principal;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario
{
    public class Usuario : IAggregateRoot<Guid>
	{
		private Guid _idDoConvenioDeAdesaoAtual;

		/// <summary>
		/// ID do usuário
		/// </summary>
		public virtual Guid Id { get; set; }

		/// <summary>
		/// Convênios de Adesão nos quais o usuário possui acesso
		/// </summary>
		public virtual IList<ConvenioDeAdesao> ConveniosDeAdesao { get; set; }

		/// <summary>
		/// ID do Convênio de Adesão atualmente selecionado
		/// Se não possuir um convênio selecionado no último acesso, o primeiro da lista de Convênios é retornado
		/// </summary>
		public virtual Guid IdDoConvenioDeAdesaoAtual
		{
			get
			{
				if (_idDoConvenioDeAdesaoAtual == Guid.Empty && ConveniosDeAdesao.Count > 0)
					_idDoConvenioDeAdesaoAtual = ConveniosDeAdesao[0].Id;

				return _idDoConvenioDeAdesaoAtual;
			}
			set { _idDoConvenioDeAdesaoAtual = value; }
		}

		/// <summary>
		/// Hash do token de sessão para habilitar o acesso do usuário
		/// </summary>
		public virtual string Token { get; set; }

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

		/// <summary>
		/// Gera um token de acesso para envio de requisições a API
		/// </summary>
		/// <returns></returns>
		public virtual void GerarTokenDeSessao()
		{
			Token = Guid.NewGuid().ToString();
		}

		public Usuario()
		{
			ConveniosDeAdesao = new List<ConvenioDeAdesao>();
		}
    }
}