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
    public class RegraCpfObrigatoriOTest
    {
        [Test]
        public void cpf_nulo_deve_retornar_critica()
        {
            RegraCpfObrigatorio regraNome = new RegraCpfObrigatorio();

            var proposta = new PropostaVO();

            var propostaAtualizada = regraNome.Validar(proposta);

            Assert.That(propostaAtualizada.Criticas.First().Mensagem, Is.EqualTo("O cpf do participante é obrigatório"));
        }

        [Test]
        public void cpf_preenchido_nao_deve_retornar_critica()
        {
            RegraCpfObrigatorio regraNome = new RegraCpfObrigatorio();

            var proposta = new PropostaVO().InformarCPF("123123123213");

            var propostaAtualizada = regraNome.Validar(proposta);

            Assert.That(propostaAtualizada.Criticas.Count, Is.EqualTo(0));
        }
    }
}
