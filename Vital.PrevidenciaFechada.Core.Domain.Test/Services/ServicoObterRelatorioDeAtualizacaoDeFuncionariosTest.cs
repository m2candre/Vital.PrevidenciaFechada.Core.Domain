using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteFuncionarioPreInscricao;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.Core.Domain.Services;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services
{
    [TestFixture]
    public class ServicoObterRelatorioDeAtualizacaoDeFuncionariosTest
    {
        private IRepositorio<FuncionarioPreInscricao> _funcionariosPreInscricao;
        private ServicoObterRelatorioDeAtualizacaoDeFuncionarios _servico;

        [SetUp]
        public void inicializar()
        {
            _funcionariosPreInscricao = MockRepository.GenerateMock<IRepositorio<FuncionarioPreInscricao>>();
            _servico = new ServicoObterRelatorioDeAtualizacaoDeFuncionarios(_funcionariosPreInscricao);
        }

        [Test]
        public void obter_relatorio_com_sucesso()
        {
            List<FuncionarioPreInscricao> lista = new List<FuncionarioPreInscricao>();
            lista.Add(new FuncionarioPreInscricao{ CPFDoParticipante ="2"});
            lista.Add(new FuncionarioPreInscricao{ CPFDoParticipante ="3"});

            _funcionariosPreInscricao.Expect(x => x.Todos()).Return(lista);

            var relatorio = _servico.ObterRelatorio(new List<string> { "1", "2", "3" });

            Assert.That(relatorio.NumeroDeNovosRegistros, Is.EqualTo(1));
            Assert.That(relatorio.NumeroDeRegistrosParaAtualizar, Is.EqualTo(2));
        }
    }
}
