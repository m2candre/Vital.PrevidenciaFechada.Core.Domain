using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.InfraStructure.ExceptionHandling;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.Core.Domain.Services.PreInscricao;
using Vital.PrevidenciaFechada.DTO.Messages.Adesao.PreInscricao;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services.PreInscricao
{
	[TestFixture]
	public class ServicoResultadoDoPreProcessamentoDaPreInscricaoTest
	{
		private ServicoResultadoDoPreProcessamentoDaPreInscricao _servico;
		private DataTable tabela;
		private IRepositorio<PreInscrito> _preInscritos;

		[SetUp]
		public void inicializar()
		{
			tabela = new DataTable();
			tabela.Columns.Add("NomeDoSegurado", typeof(string));
			tabela.Columns.Add("DataDeNascimento", typeof(string));
			tabela.Columns.Add("SexoDoParticipante", typeof(string));
			tabela.Columns.Add("NumeroDeMatriculaDoServidor", typeof(string));
			tabela.Columns.Add("Setor", typeof(string));
			tabela.Columns.Add("Lotacao", typeof(string));
			tabela.Columns.Add("DDDFoneResidencial", typeof(string));
			tabela.Columns.Add("TelefoneResidencial", typeof(string));
			tabela.Columns.Add("DDDFoneCelular", typeof(string));
			tabela.Columns.Add("TelefoneCelular", typeof(string));
			tabela.Columns.Add("DDDFoneComercial", typeof(string));
			tabela.Columns.Add("TelefoneComercial", typeof(string));
			tabela.Columns.Add("EnderecoDeEmail", typeof(string));
			tabela.Columns.Add("Logradouro", typeof(string));
			tabela.Columns.Add("Bairro", typeof(string));
			tabela.Columns.Add("Numero", typeof(string));
			tabela.Columns.Add("Complemento", typeof(string));
			tabela.Columns.Add("Localidade", typeof(string));
			tabela.Columns.Add("CEP", typeof(string));
			tabela.Columns.Add("CPFDoParticipante", typeof(string));
			tabela.Columns.Add("NumeroDeIdentidade", typeof(string));
			tabela.Columns.Add("NaturezaDoDocumentoDeIdentidade", typeof(string));
			tabela.Columns.Add("DataDeExpedicao", typeof(string));
			tabela.Columns.Add("OrgaoExpedidor", typeof(string));
			tabela.Columns.Add("NomeDoPai", typeof(string));
			tabela.Columns.Add("NomeDaMae", typeof(string));
			tabela.Columns.Add("Naturalidade", typeof(string));
			tabela.Columns.Add("Nacionalidade", typeof(string));
			tabela.Columns.Add("EstadoCivil", typeof(string));
			tabela.Columns.Add("NomeDoConjuge", typeof(string));
			tabela.Columns.Add("PessoaPoliticamenteExposta", typeof(string));
			tabela.Columns.Add("Cargo", typeof(string));
			tabela.Columns.Add("RemuneracaoNaInscricao", typeof(string));
			tabela.Columns.Add("SituacaoPatrimonial", typeof(string));
			tabela.Columns.Add("DataDeAdmissao", typeof(string));
			tabela.Columns.Add("Banco", typeof(string));
			tabela.Columns.Add("Agencia", typeof(string));
			tabela.Columns.Add("DigitoVerificadorAgencia", typeof(string));
			tabela.Columns.Add("Conta", typeof(string));
			tabela.Columns.Add("DigitoVerificadorConta", typeof(string));
			tabela.Columns.Add("TipoDaConta", typeof(string));
			tabela.Columns.Add("FontePagadora", typeof(string));
			tabela.Columns.Add("PaisResidencial", typeof(string));
			tabela.Columns.Add("PISPASEP", typeof(string));
			tabela.Columns.Add("DataNoCargo", typeof(string));
			tabela.Columns.Add("IdDoConvenioDeAdesao", typeof(string));

			var linha = tabela.NewRow();

			linha["CPFDoParticipante"] = "123";
			linha["IdDoConvenioDeAdesao"] = Guid.NewGuid();

			var novaLinha = tabela.NewRow();

			novaLinha["CPFDoParticipante"] = "12333";
			novaLinha["IdDoConvenioDeAdesao"] = Guid.NewGuid();

			tabela.Rows.Add(linha);
			tabela.Rows.Add(novaLinha);

			_preInscritos = MockRepository.GenerateMock<IRepositorio<PreInscrito>>();
		}

		[Test]
		public void obter_resultado_da_pre_inscricao_com_quantidade_de_dados_do_arquivo_maior_que_a_quantidade_de_registros_existentes_sucesso()
		{
			Guid idConvenio = Guid.NewGuid();

			PreInscricaoDTO preInscricaoDTO = new PreInscricaoDTO() { TabelaDePreInscritos = tabela, IdDoConvenioDeAdesao = idConvenio };

			var listaDePreInscritos = new List<PreInscrito>
            { 
                new PreInscrito{ CPFDoParticipante="333", IdDoConvenioDeAdesao = idConvenio },
                new PreInscrito{ CPFDoParticipante="555", IdDoConvenioDeAdesao = idConvenio }
            };

			_preInscritos.Expect(x => x.Obter(preInscrito => preInscrito.CPFDoParticipante == listaDePreInscritos[0].CPFDoParticipante && preInscrito.IdDoConvenioDeAdesao == idConvenio)).Return(listaDePreInscritos[0]);

			_servico = new ServicoResultadoDoPreProcessamentoDaPreInscricao(_preInscritos);

			var resultadoDoPreProcessamento = _servico.Obter(preInscricaoDTO);

			Assert.That(resultadoDoPreProcessamento.PreInscritosParaAtualizar, Is.EqualTo(0));
			Assert.That(resultadoDoPreProcessamento.NovosPreInscritos, Is.EqualTo(2));
		}
	}
}
