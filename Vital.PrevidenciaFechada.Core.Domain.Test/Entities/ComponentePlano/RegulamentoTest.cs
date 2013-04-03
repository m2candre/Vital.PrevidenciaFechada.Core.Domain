using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano.Regras.ValidacoesDeDados;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

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
            Assert.That(regulamento.Regras.Count, Is.EqualTo(3));
        }


        [Test]
        public void obter_uma_lista_de_criticas_passando_uma_proposta_com_nome_e_cpf_invalido_deve_retornar_critica_com_sucesso()
        {
            PropostaVO propostaComNome = new PropostaVO("Fulano", "1111");

            Assert.That(regulamento.ObterCriticasDaProposta(propostaComNome).First().Mensagem, Is.EqualTo("Cpf está inválido"));
        }
    }
}
