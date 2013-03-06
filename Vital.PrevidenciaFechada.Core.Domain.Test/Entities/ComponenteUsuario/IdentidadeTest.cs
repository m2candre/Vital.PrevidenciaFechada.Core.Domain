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

        /// <summary>
        /// Vincula o usuário a duas entidades e valida os campos
        /// </summary>
        [Test]
        public void criar_identidade_adicionando_duas_Entidades()
        {
            var id = Guid.NewGuid();
            var identidade = new Identidade { Name = "Tulne", Id = id, LastName = "Vieira" };

            Assert.AreEqual(id, identidade.Id);
            Assert.AreEqual("Tulne", identidade.Name);
            Assert.AreEqual("Vieira", identidade.LastName);
            Assert.AreEqual("Custom", identidade.AuthenticationType);
            Assert.AreEqual(false, identidade.IsAuthenticated);

            identidade.Entidades.Add(new Entidade(){Nome = "Mongeral"});
            identidade.Entidades.Add(new Entidade() { Nome = "Data A" });

            Assert.AreEqual(2, identidade.Entidades.Count);
        }

        [Test]
        public void construtor_deve_setar_o_nome_e_inicializar_a_colecao_de_entidades()
        {
            var identidade = new Identidade("nome");

            Assert.That(identidade.Name, Is.EqualTo("nome"));
            Assert.That(identidade.Entidades.Count, Is.EqualTo(0));
        }

    }
}
