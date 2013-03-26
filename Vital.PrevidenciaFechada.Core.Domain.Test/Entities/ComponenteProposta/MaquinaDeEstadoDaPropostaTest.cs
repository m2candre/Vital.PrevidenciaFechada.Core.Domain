using NUnit.Framework;
using System;
using System.Linq;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteProposta
{
	[TestFixture]
	public class MaquinaDeEstadoDaPropostaTest
	{
		private MaquinaDeEstadoDaProposta _maquina;

		[SetUp]
		public void iniciar()
		{
			Proposta proposta = new Proposta();
			_maquina = new MaquinaDeEstadoDaProposta("Iniciada", proposta);
		}

		[Test]
		public void construir_a_maquina_sem_passar_estado_inicial_lanca_excecao()
		{
			MaquinaDeEstadoDaProposta maquina;
			Assert.That(() => maquina = new MaquinaDeEstadoDaProposta("", new Proposta()), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O estado inicial não foi informado"));
		}

		[Test]
		public void construir_a_maquina_sem_passar_o_objeto_proposta_lanca_excecao()
		{
			MaquinaDeEstadoDaProposta maquina;
			Assert.That(() => maquina = new MaquinaDeEstadoDaProposta("Iniciada", null), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("A proposta não foi informada"));
		}

		[Test]
		public void alterar_estado_pela_acao()
		{
			_maquina.AlterarPelaAcao("Autorizar");

			Assert.That(_maquina.EstadoAtual, Is.EqualTo("Autorizada"));
		}

		[Test]
		public void alterar_estado_lanca_excecao_se_acao_nao_for_informada()
		{
			Assert.That(() => _maquina.AlterarPelaAcao(""), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("A ação não foi informada"));
		}

		[Test]
		public void alterar_estado_atraves_de_uma_acao_nao_mapeada_retorna_excecao()
		{
			Assert.Throws<InvalidOperationException>(() => _maquina.AlterarPelaAcao("TestarNaoExistente"), "No valid leaving transitions are permitted from state 'Iniciada' for trigger 'TestarNaoExistente'. Consider ignoring the trigger.");
		}
	}
}