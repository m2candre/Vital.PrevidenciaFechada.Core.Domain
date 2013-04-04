using NUnit.Framework;
using System;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponentePlano
{
    [TestFixture]
    public class CampoDoModeloDePropostaTest
    {
        [Test]
        public void atualizar_o_nome_do_campo_com_sucesso()
        {
            var campo = new CampoDeProposta();

            campo.AtualizarNome("nome");

            Assert.That(campo.Nome, Is.EqualTo("nome"));
        }

        [Test]
        [ExpectedException]
        public void construir_campo_com_o_nome_nulo_deve_disparar_execao()
        {
            var campo = new CampoDeProposta(null);
        }

		[Test]
		public void copiar_para_rascunho()
		{
			var campo = new CampoDeProposta("CPF");
			var campoCopiado = campo.CopiarParaRascunho();

			Assert.That(campo.Nome, Is.EqualTo(campoCopiado.Nome));
			Assert.That(campo, Is.Not.EqualTo(campoCopiado));
		}

		[Test]
		public void atualizar_nome()
		{
			var campo = new CampoDeProposta("CPF");
			var campoCopiado = campo.CopiarParaRascunho();
			campoCopiado.AtualizarNome("Nome");

			Assert.That(campoCopiado.Nome, Is.EqualTo("Nome"));
			Assert.That(campo.Nome, Is.Not.EqualTo(campoCopiado.Nome));
		}
    }
}
