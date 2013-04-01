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

        [Test]
        public void informar_o_cpf_do_participante_com_sucesso()
        {
            PropostaVO propostaVO = new PropostaVO();

            Assert.That(string.IsNullOrWhiteSpace(propostaVO.CpfDoParticipante), Is.True);

            var propostaAtuializada = propostaVO.InformarCPF("123");

            Assert.That(string.IsNullOrWhiteSpace(propostaVO.CpfDoParticipante), Is.True);
            Assert.That(propostaAtuializada.CpfDoParticipante, Is.EqualTo("123"));

        }

        [Test]
        public void informar_critica_a_proposta_com_sucesso()
        {
            PropostaVO propostaVO = new PropostaVO();

            Assert.That(propostaVO.Criticas.Count, Is.EqualTo(0));

            var propostaAtuializada = propostaVO.InformarCritica("critica");

            Assert.That(propostaVO.Criticas.Count, Is.EqualTo(0));
            Assert.That(propostaAtuializada.Criticas.Count, Is.EqualTo(1));
        }
    }
}
