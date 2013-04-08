using NUnit.Framework;
using System;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponentePlano
{
    [TestFixture]
    public class CampoDePropostaTest
    {
        [Test]
        public void atualizar_o_nome_do_campo_com_sucesso()
        {
            var campo = new CampoDeProposta();

            campo.AtualizarNome("nome");

            Assert.That(campo.Nome, Is.EqualTo("nome"));
        }

		[Test]
		public void copiar_para_rascunho()
		{
			CampoDeProposta campo = new CampoDeProposta("Identidade");
			CampoDeProposta campoCopiado = campo.CopiarParaRascunho();

			Assert.AreNotSame(campo, campoCopiado);
		}

        [Test]
        [ExpectedException]
        public void construir_campo_com_o_nome_nulo_deve_disparar_execao()
        {
            var campo = new CampoDeProposta(null);
        }
    }
}
