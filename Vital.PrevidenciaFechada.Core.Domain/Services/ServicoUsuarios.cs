﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
	public class ServicoUsuarios
	{
		/// <summary>
		/// Repositório de usuário
		/// </summary>
		private IRepositorio<Usuario> _repositorioUsuario;

		/// <summary>
		/// Token informado para busca de usuário
		/// </summary>
		private string _token;

		/// <summary>
		/// Critérios para a busca de usuário por token
		/// </summary>
		private Expression<Func<Usuario, bool>> _criterios;

		/// <summary>
		/// Critérios para a busca de usuário por token
		/// </summary>
		public Expression<Func<Usuario, bool>> Criterios
		{
			get
			{
				if (_criterios == null)
					_criterios = u => u.TokenDeSessao == _token;

				return _criterios;
			}
			set { _criterios = value; }
		}

		/// <summary>
		/// Construtor injetando o repositório de usuário
		/// </summary>
		/// <param name="repositorioUsuario"></param>
		public ServicoUsuarios(IRepositorio<Usuario> repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public virtual Usuario ObterUsuarioPorToken(string token)
		{
			#region Pré-condições

			Assertion.IsFalse(string.IsNullOrEmpty(token), "Para obter o usuário, o token de sessão deve ser informado").Validate();

			#endregion

			_token = token;
			var usuario = _repositorioUsuario.ObterPor<Usuario>(Criterios);

			#region Pós-condições

			Assertion.NotNull(usuario, "Não foi encontrado usuário com o token informado").Validate();

			#endregion

			usuario.DataHoraDaUltimaRequisicao = DateTime.Now;
			_repositorioUsuario.Adicionar(usuario);

			return usuario;
		}
	}
}