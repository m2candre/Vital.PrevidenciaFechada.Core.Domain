using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteUsuario
{
    /// <summary>
    /// Testes da Entidade Usuário
    /// </summary>
    [TestFixture]
    public class UsuarioTest
    {

        /// <summary>
        /// Valida a criação de uma Entidade Usuário
        /// </summary>
        [Test]
        public void criar_Usuario()
        {
            var id = Guid.NewGuid();
            var identidade = new Identidade { Name = "Nome", Id = id, LastName = "Sobrenome" };

            var usuario = new Usuario(identidade);

           Assert.AreSame(identidade, usuario.Identity);

            Assert.Throws<NotImplementedException>(() => usuario.IsInRole("test"));

        }
    }
}
