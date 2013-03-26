using NUnit.Framework;
using System;
using System.Linq.Expressions;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteProposta
{
	[TestFixture]
	public class CriteriosDeConsultaPorPlanoEstadoDataTest
	{
		[Test]
		public void obter_criterios_retorna_expressao_com_criterios_da_consulta()
		{
			CriteriosDeConsultaPorPlanoEstadoData criterios = new CriteriosDeConsultaPorPlanoEstadoData();
			Expression<Func<Proposta, bool>> criterio = criterios.ObterCriterio(Guid.NewGuid(), "Registrada", DateTime.Now);

			Assert.That(criterio, Is.Not.Null);
		}

		[Test]
		public void obter_criterio_sem_informar_o_id_do_plano_lanca_excecao()
		{
			CriteriosDeConsultaPorPlanoEstadoData criterio = new CriteriosDeConsultaPorPlanoEstadoData();
			Assert.That(() => criterio.ObterCriterio(Guid.Empty, "Registrada", DateTime.Now), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O ID do plano deve ser informado"));
		}

		[Test]
		public void obter_propostas_sem_informar_o_estado_da_proposta_lanca_excecao()
		{
			CriteriosDeConsultaPorPlanoEstadoData criterio = new CriteriosDeConsultaPorPlanoEstadoData();
			Assert.That(() => criterio.ObterCriterio(Guid.NewGuid(), "", DateTime.Now), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("O estado da proposta deve ser informado"));
		}

		[Test]
		public void obter_propostas_sem_informar_a_quantidade_de_dias_lanca_excecao()
		{
			CriteriosDeConsultaPorPlanoEstadoData criterio = new CriteriosDeConsultaPorPlanoEstadoData();
			Assert.That(() => criterio.ObterCriterio(Guid.NewGuid(), "Registrada", default(DateTime)), Throws.Exception.TypeOf<Exception>().With.Property("Message").EqualTo("A data da busca possui um valor inválido"));
		}
	}
}