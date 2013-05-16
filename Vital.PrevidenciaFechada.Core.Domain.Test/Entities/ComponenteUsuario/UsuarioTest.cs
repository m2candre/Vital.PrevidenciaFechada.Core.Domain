using NUnit.Framework;
using System;
using System.Collections.Generic;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteUsuario
{
	[TestFixture]
	public class UsuarioTest
	{
		[Test]
		public void obter_primeiro_id_do_convenio_de_adesao_da_lista()
		{
			Guid idConvenio1 = Guid.NewGuid();
			Guid idConvenio2 = Guid.NewGuid();

			Usuario usuario = new Usuario();
			usuario.ConveniosDeAdesao = new List<ConvenioDeAdesao>
			{
				new ConvenioDeAdesao { Id = idConvenio1 },
				new ConvenioDeAdesao { Id = idConvenio2 }
			};

			Assert.That(usuario.IdDoConvenioDeAdesaoAtual, Is.EqualTo(idConvenio1));
		}

		[Test]
		public void obter_id_do_convenio_de_adesao_ja_selecionado()
		{
			Guid idConvenioAtual = Guid.NewGuid();
			Guid idConvenio1 = Guid.NewGuid();
			Guid idConvenio2 = Guid.NewGuid();

			Usuario usuario = new Usuario { IdDoConvenioDeAdesaoAtual = idConvenioAtual };
			usuario.ConveniosDeAdesao = new List<ConvenioDeAdesao>
			{
				new ConvenioDeAdesao { Id = idConvenio1 },
				new ConvenioDeAdesao { Id = idConvenio2 }
			};

			Assert.That(usuario.IdDoConvenioDeAdesaoAtual, Is.EqualTo(idConvenioAtual));
		}
	}
}