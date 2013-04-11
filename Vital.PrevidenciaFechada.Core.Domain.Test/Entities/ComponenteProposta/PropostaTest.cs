using NUnit.Framework;
using Rhino.Mocks;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.ExceptionHandling;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteDocumento;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Entities.ComponenteProposta
{
	/// <summary>
	/// Stub de Proposta para forçar valor null na MaquinaDeEstado
	/// </summary>
	public class PropostaStub : Proposta
	{
		/// <summary>
		/// Propriedade para forçar null no get da MaquinaDeEstado
		/// </summary>
		public override MaquinaDeEstadoDaProposta MaquinaDeEstado
		{
			get { return null; }
			set { base.MaquinaDeEstado = value; }
		}
	}

	[TestFixture]
	public class PropostaTest
	{
		private Proposta _proposta;

		[SetUp]
		public void iniciar()
		{
			_proposta = new Proposta();
		}

		[Test]
		public void se_estado_nao_for_setado_setar_com_estado_inicial()
		{
			_proposta.Estado = null;

			Assert.That(_proposta.Estado, Is.Not.Empty);
            Assert.That(_proposta.Estado, Is.EqualTo("EmRascunho"));
		}

		[Test]
		public void maquina_da_proposta_sendo_setada_com_sucesso()
		{
			_proposta.MaquinaDeEstado = new MaquinaDeEstadoDaProposta("EmRascunho", _proposta);

			Assert.That(_proposta.MaquinaDeEstado, Is.Not.Null);
            Assert.That(_proposta.MaquinaDeEstado.EstadoAtual, Is.EqualTo("EmRascunho"));
		}

		[Test]
		public void alterar_estado_para_autorizada_com_sucesso()
		{
			_proposta.Autorizar();

			Assert.That(_proposta.Estado, Is.EqualTo("Autorizada"));
		}

		[Test]
		public void autorizar_com_maquina_de_estado_nula_lanca_excecao()
		{
			PropostaStub propostaStub = new PropostaStub();

            Assert.That(() => propostaStub.Autorizar(), Throws.Exception.TypeOf<BusinessException>().With.Property("Message").EqualTo("A máquina de estados da proposta deve ser inicializada"));
		}

		[Test]
		public void recusar_com_maquina_de_estado_nula_lanca_excecao()
		{
			PropostaStub propostaStub = new PropostaStub();

            Assert.That(() => propostaStub.Recusar(), Throws.Exception.TypeOf<BusinessException>().With.Property("Message").EqualTo("A máquina de estados da proposta deve ser inicializada"));
		}

		[Test]
		public void alterar_estado_para_nao_autorizada_com_sucesso()
		{
			_proposta.Recusar();

			Assert.That(_proposta.Estado, Is.EqualTo("NaoAutorizada"));
		}

		[Test]
		public void alterar_estado_sem_informar_parametro_estado_lanca_excecao()
		{
            Assert.That(() => _proposta.AlterarEstado(""), Throws.Exception.TypeOf<BusinessException>().With.Property("Message").EqualTo("O estado não foi informado"));
		}

		[Test]
		public void alterar_propriedade_estado_na_proposta()
		{
			_proposta.Estado = "Teste";

			Assert.That(_proposta.Estado, Is.EqualTo("Teste"));
		}

        [Test]
        public void adicionar_documento_na_proposta_com_sucesso()
        {
            _proposta.AdicionarDocumento(new Documento { Nome = "teste", Id = Guid.NewGuid(), Token = Guid.NewGuid() });

            Assert.That(_proposta.Documentos.Count, Is.EqualTo(1));
        }
	}
}