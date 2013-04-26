using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteModeloDeProposta;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
	/// <summary>
	/// Proposta
	/// </summary>
	[Serializable]
	public class Proposta : IAggregateRoot<Guid>
	{
		private XmlSerializer _xmlSerializer;
		private MaquinaDeEstadoDaProposta _maquinaDeEstadoDaProposta;
		private string _estado;

		#region Propriedades

		/// <summary>
		/// ID da proposta
		/// </summary>
		public virtual Guid Id { get; set; }

		/// <summary>
		/// Número da Proposta
		/// </summary>
		public virtual int Numero { get; set; }

		/// <summary>
		/// Data da proposta
		/// </summary>
		public virtual DateTime DataDeCriacao { get; set; }

		/// <summary>
		/// Nome do estado atual
		/// </summary>
		public virtual string Estado
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_estado))
					_estado = "EmRascunho";

				return _estado;
			}
			set { _estado = value; }
		}

		/// <summary>
		/// ID do arquivo de dados que armazena a proposta serializada
		/// </summary>
		public virtual Guid IdDoArquivoDeDados { get; set; }

		/// <summary>
		/// Valores dos campos da proposta
		/// </summary>
		public virtual List<ValorDeCampo> Valores { get; set; }

		/// <summary>
		/// Objeto responsável por controlar a transição de estados da proposta
		/// </summary>
		[XmlIgnore]
		public virtual MaquinaDeEstadoDaProposta MaquinaDeEstado
		{
			get
			{
				if (_maquinaDeEstadoDaProposta == null)
					_maquinaDeEstadoDaProposta = new MaquinaDeEstadoDaProposta("EmRascunho", this);

				return _maquinaDeEstadoDaProposta;
			}
			set { _maquinaDeEstadoDaProposta = value; }
		}

		/// <summary>
		/// Modelo de Proposta
		/// </summary>
		[XmlIgnore]
		public virtual ModeloDeProposta ModeloDeProposta { get; set; }

		/// <summary>
		/// Serializador XML para a Proposta
		/// </summary>
		[XmlIgnore]
		public virtual XmlSerializer XmlSerializer
		{
			get
			{
				if (_xmlSerializer == null)
					_xmlSerializer = new XmlSerializer(this.GetType());

				return _xmlSerializer;
			}
			set { _xmlSerializer = value; }
		}

		#endregion

		/// <summary>
		/// Construtor
		/// </summary>
		public Proposta()
		{
			Valores = new List<ValorDeCampo>();
			DataDeCriacao = DateTime.Now;
		}

		/// <summary>
		/// Autoriza a proposta
		/// </summary>
		public virtual void Autorizar()
		{
			AlterarEstadoPelaAcao("Autorizar");
		}

		/// <summary>
		/// Rejeita a a proposta
		/// </summary>
		public virtual void Rejeitar()
		{
			AlterarEstadoPelaAcao("Rejeitar");
		}

		/// <summary>
		/// Recusa a proposta
		/// </summary>
		public virtual void Recusar()
		{
			AlterarEstadoPelaAcao("Recusar");
		}

		/// <summary>
		/// Altera o Estado na máquina de estado da proposta pela ação
		/// </summary>
		/// <param name="acao">acao</param>
		private void AlterarEstadoPelaAcao(string acao)
		{
			#region Pré-condições

			IAssertion maquinaDeEstadoEstaInicializada = Assertion.NotNull(MaquinaDeEstado, "A máquina de estados da proposta deve ser inicializada");

			#endregion

			maquinaDeEstadoEstaInicializada.Validate();

			MaquinaDeEstado.AlterarEstadoPelaAcao(acao);
		}

		/// <summary>
		/// Altera o estado da proposta
		/// </summary>
		/// <param name="estado">estado</param>
		public virtual void AlterarEstado(string estado)
		{
			#region Pré-condições

			IAssertion oEstadoFoiInformado = Assertion.IsFalse(string.IsNullOrWhiteSpace(estado), "O estado não foi informado");

			#endregion

			oEstadoFoiInformado.Validate();

			Estado = estado;

			#region Pós-condições

			IAssertion oEstadoDaPropostaFoiAlterado = Assertion.Equals(Estado, estado, "O estado não foi alterado corretamente");

			#endregion

			oEstadoDaPropostaFoiAlterado.Validate();
		}

		/// <summary>
		/// Serializa os dados da proposta em formato XML
		/// </summary>
		/// <returns></returns>
		public virtual byte[] Serializar()
		{
			MemoryStream stream = new MemoryStream();

			XmlSerializer.Serialize(stream, this);

			byte[] arquivo = stream.GetBuffer();

			#region Pós-condições

			IAssertion oArquivoGeradoNaoEstaVazio = Assertion.IsTrue(arquivo.Length > 0, "O arquivo serializado não possui dados");

			#endregion

			oArquivoGeradoNaoEstaVazio.Validate();

			return arquivo;
		}
	}
}