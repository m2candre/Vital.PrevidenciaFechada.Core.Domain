using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteUsuario
{
    /// <summary>
    /// Testes da Classe Identidade
    /// </summary>
    [TestFixture]
    public class IdentidadeTest
    {
        /// <summary>
        /// Valida se é possível criar um Identidade de Usuário no sistema
        /// </summary>
        [Test]
        public void criar_identidade()
        {
            var id = Guid.NewGuid();
            var identidade = new Identidade {Name = "Nome", Id = id, LastName = "Sobrenome"};

            Assert.AreEqual(id, identidade.Id);
            Assert.AreEqual("Nome", identidade.Name);
            Assert.AreEqual("Sobrenome", identidade.LastName);
            Assert.AreEqual("Custom", identidade.AuthenticationType);
            Assert.AreEqual(false, identidade.IsAuthenticated);
        }
    }
}