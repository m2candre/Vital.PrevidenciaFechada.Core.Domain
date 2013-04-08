using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteConvenioDeAdesao
{
	[TestFixture]
	public class ConvenioDeAdesaoTest
	{
		[Test]
		public void adicionar_proposta_ao_convenio()
		{
			Proposta proposta1 = new Proposta { Id = Guid.NewGuid() };
			Proposta proposta2 = new Proposta { Id = Guid.NewGuid() };

			ConvenioDeAdesao convenio = new ConvenioDeAdesao();
			convenio.AdicionarProposta(proposta1);
			convenio.AdicionarProposta(proposta2);

			Assert.That(convenio.Propostas.Count, Is.EqualTo(2));
		}

		[Test]
		public void adicionar_modelo_de_proposta()
		{
			ModeloDeProposta modelo1 = new ModeloDeProposta { Id = Guid.NewGuid() };
			ModeloDeProposta modelo2 = new ModeloDeProposta { Id = Guid.NewGuid() };

			ConvenioDeAdesao convenio = new ConvenioDeAdesao();
			convenio.AdicionarModeloDeProposta(modelo1);
			convenio.AdicionarModeloDeProposta(modelo2);

			Assert.That(convenio.ModelosDeProposta.Count, Is.EqualTo(2));
		}

		[Test]
		public void obter_modelo_de_proposta_publicado()
		{
			Guid idModeloPublicado = Guid.NewGuid();

			ModeloDeProposta modelo1 = new ModeloDeProposta { Id = Guid.NewGuid() };
			ModeloDeProposta modelo2 = new ModeloDeProposta { Id = idModeloPublicado };
			modelo2.AdicionarCampo("CPF");
			modelo2.Publicar();

			ConvenioDeAdesao convenio = new ConvenioDeAdesao();
			convenio.AdicionarModeloDeProposta(modelo1);
			convenio.AdicionarModeloDeProposta(modelo2);

			ModeloDeProposta modeloPublicado = convenio.ObterModeloDePropostaPublicado();

			Assert.That(modelo2.Id, Is.EqualTo(modeloPublicado.Id));
		}
	}
}