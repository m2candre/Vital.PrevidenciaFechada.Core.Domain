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
	public class MaquinaDeEstadoDaPropostaTest
	{
		[Test]
		public void alterar_estado_pela_acao()
		{
			Proposta proposta = new Proposta();
			MaquinaDeEstadoDaProposta maquina = new MaquinaDeEstadoDaProposta("Iniciada", proposta);

			maquina.AlterarPelaAcao("Autorizar");

			Assert.That(maquina.Maquina.State, Is.EqualTo("Autorizada"));
		}

		[Test]
		public void alterar_estado_atraves_de_uma_acao_nao_mapeada_retorna_excecao()
		{
			Proposta proposta = new Proposta();
			MaquinaDeEstadoDaProposta maquina = new MaquinaDeEstadoDaProposta("Iniciada", proposta);

			Assert.Throws<InvalidOperationException>(() => maquina.AlterarPelaAcao("TestarNaoExistente"), "No valid leaving transitions are permitted from state 'Iniciada' for trigger 'TestarNaoExistente'. Consider ignoring the trigger.");
		}

		[Test]
		public void configurar_maquina_de_estado()
		{
			Proposta proposta = new Proposta();
			MaquinaDeEstadoDaProposta maquina = new MaquinaDeEstadoDaProposta("Iniciada", proposta);

			Assert.That(maquina.Maquina.PermittedTriggers.Count(), Is.EqualTo(2));
			Assert.IsTrue(maquina.Maquina.PermittedTriggers.Contains("Autorizar"));
		}
	}
}