using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteProposta
{
    [TestFixture]
    public class PropostaVOTest
    {
        [Test]
        public void informar_o_nome_do_participante_com_sucesso()
        {
            PropostaVO propostaVO = new PropostaVO();

            Assert.That(string.IsNullOrWhiteSpace(propostaVO.NomeDoParticipante), Is.True);

            var propostaAtuializada = propostaVO.InformarNome("Fulano");

            Assert.That(string.IsNullOrWhiteSpace(propostaVO.NomeDoParticipante), Is.True);
            Assert.That(propostaAtuializada.NomeDoParticipante, Is.EqualTo("Fulano"));
            
        }
    }
}
