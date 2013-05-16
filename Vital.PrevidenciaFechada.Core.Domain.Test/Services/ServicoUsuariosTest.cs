using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Linq.Expressions;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.Core.Domain.Services;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services
{
	[TestFixture]
	public class ServicoUsuariosTest
	{
		[Test]
		public void obter_usuario_por_token()
		{
			string token = Guid.NewGuid().ToString();
			Usuario usuario = new Usuario { Id = Guid.NewGuid(), TokenDeSessao = token };

			Expression<Func<Usuario, bool>> criterios = u => u.TokenDeSessao == token;

			var repositorioUsuario = MockRepository.GenerateMock<IRepositorio<Usuario>>();
			repositorioUsuario.Expect(x => x.ObterPor<Usuario>(criterios)).Return(usuario);

			ServicoUsuarios servico = new ServicoUsuarios(repositorioUsuario);
			servico.Criterios = criterios;

			Usuario usuarioObtido = servico.ObterUsuarioPorToken(token);

			Assert.That(usuarioObtido.TokenDeSessao, Is.EqualTo(token));
		}
	}
}