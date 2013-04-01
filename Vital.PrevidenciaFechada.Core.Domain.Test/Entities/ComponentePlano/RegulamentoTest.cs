using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponentePlano
{
    [TestFixture]
    public class RegulamentoTest
    {
        private Regulamento regulamento;

        [SetUp]
        public void inicializar()
        {
            regulamento = new Regulamento();
        }

        [Test]
        public void adicionar_regras()
        {
            regulamento.Adicionar(MockRepository.GenerateMock<IRegra>());

            Assert.That(regulamento.Regras.Count, Is.EqualTo(1));
        }
    }
}
