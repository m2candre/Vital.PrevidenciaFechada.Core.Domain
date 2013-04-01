using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras.ValidacoesDeDados;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteProposta.Regras.ValidacoesDeDados
{
    [TestFixture]
    public class RegraCPFTest
    {
        [Test]
        public void cpf_valido_nao_gera_nenhuma_critica()
        {
            RegraValidarCpf validadorCpf = new RegraValidarCpf();

            var proposta = new PropostaVO().InformarCPF("57747353200");

            var propostaAtualizada = validadorCpf.Validar(proposta);

            Assert.That(propostaAtualizada.Criticas.Count, Is.EqualTo(0));
        }

        [Test]
        public void cpf_invalido_gera_critica()
        {
            RegraValidarCpf validadorCpf = new RegraValidarCpf();

            var proposta = new PropostaVO().InformarCPF("12321311");

            var propostaAtualizada = validadorCpf.Validar(proposta);

            Assert.That(propostaAtualizada.Criticas.First(), Is.EqualTo("Cpf está inválido"));
        }
    }
}
