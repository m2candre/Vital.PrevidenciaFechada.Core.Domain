using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteProposta.Regras.CamposObrigatorios
{
    [TestFixture]
    public class RegraNomeDoParticipanteObrigatorioTest
    {
        [Test]
        public void nome_do_participante_nulo_deve_retornar_critica()
        {
            RegraNomeDoParticipanteObrigatorio regraNome = new RegraNomeDoParticipanteObrigatorio();

            var proposta = new PropostaVO();

            var propostaAtualizada = regraNome.Validar(proposta);

            Assert.That(propostaAtualizada.Criticas.First().Mensagem, Is.EqualTo("Nome do participante é obrigatório"));
        }

        [Test]
        public void nome_do_participante_preenchido_nao_deve_retornar_critica()
        {
            RegraNomeDoParticipanteObrigatorio regraNome = new RegraNomeDoParticipanteObrigatorio();

            var proposta = new PropostaVO().InformarNome("Fulano");
            
            var propostaAtualizada = regraNome.Validar(proposta);

            Assert.That(propostaAtualizada.Criticas.Count, Is.EqualTo(0));
        }
    }
}
