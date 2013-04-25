using NUnit.Framework;
using System;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteConvenioDeAdesao
{
	[TestFixture]
	public class ConvenioDeAdesaoTest
	{
		private ConvenioDeAdesao _convenio;

		[SetUp]
		public void iniciar()
		{
			Entidade entidade = new Entidade { Id = Guid.NewGuid(), Nome = "Teste" };
			PessoaJuridica pj = new Patrocinador { Id = Guid.NewGuid(), RazaoSocial = "Patrocinador_teste" };
			Plano plano = new Plano { Id = Guid.NewGuid(), Nome = "Plano_teste" };

			ModeloDeProposta modelo = new ModeloDeProposta { Id = Guid.NewGuid() };
			modelo.AdicionarCampo(new CampoDeProposta { Nome = "Nome" });
			modelo.Publicar();
			
			_convenio = new ConvenioDeAdesao(entidade, pj, plano);
			_convenio.AdicionarModeloDeProposta(modelo);
		}

		[Test]
		public void adicionar_proposta_ao_convenio_com_modelo_publicado()
		{
			Proposta proposta1 = new Proposta { Id = Guid.NewGuid() };
			Proposta proposta2 = new Proposta { Id = Guid.NewGuid() };

			_convenio.AdicionarProposta(proposta1);
			_convenio.AdicionarProposta(proposta2);

			Assert.That(_convenio.Propostas.Count, Is.EqualTo(2));
			Assert.IsTrue(proposta1.ModeloDeProposta.Publicada);
			Assert.IsTrue(proposta2.ModeloDeProposta.Publicada);
		}

		[Test]
		public void adicionar_modelo_de_proposta()
		{
			ModeloDeProposta modelo1 = new ModeloDeProposta { Id = Guid.NewGuid() };
			ModeloDeProposta modelo2 = new ModeloDeProposta { Id = Guid.NewGuid() };

			_convenio.AdicionarModeloDeProposta(modelo1);
			_convenio.AdicionarModeloDeProposta(modelo2);

			Assert.That(_convenio.ModelosDeProposta.Count, Is.EqualTo(3));
		}

		[Test]
		public void obter_modelo_de_proposta_publicado()
		{
			Guid idModeloPublicado = Guid.NewGuid();

			ModeloDeProposta modeloPublicado = _convenio.ObterModeloDePropostaPublicado();

			Assert.IsTrue(modeloPublicado.Publicada);
		}
	}
}