using NUnit.Framework;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteProposta
{
	[TestFixture]
	public class PropostaTest
	{
		[Test]
		public void alterar_estado_com_sucesso()
		{
			Proposta proposta = new Proposta();
			proposta.Autorizar();

			Assert.That(proposta.Estado, Is.EqualTo("Autorizada"));
		}

		[Test]
		public void iniciar_maquina_de_estado_na_proposta()
		{
			MaquinaDeEstadoDaProposta maquina = new MaquinaDeEstadoDaProposta("Iniciada", new Proposta());
			maquina.Maquina = null;

			StateMachine<string, string> maquinaDeEstado = maquina.Maquina;

			Assert.That(maquinaDeEstado.State, Is.EqualTo("Iniciada"));
		}

		[Test]
		public void alterar_propriedade_estado_na_proposta()
		{
			Proposta proposta = new Proposta();
			proposta.Estado = "Teste";

			Assert.That(proposta.Estado, Is.EqualTo("Teste"));
		}

		[Test]
		public void maquina_de_estado_da_proposta_esta_nula_ao_autorizar()
		{
			Proposta proposta = new Proposta();
			proposta.MaquinaDeEstado = null;

			Assert.Throws<Exception>(() => proposta.Autorizar(), "A máquina de estados da proposta deve ser inicializada");
		}
	}
}
