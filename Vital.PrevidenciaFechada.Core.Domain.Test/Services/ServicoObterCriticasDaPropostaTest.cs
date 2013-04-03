using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.Core.Domain.Services;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services
{
    [TestFixture]
    public class ServicoObterCriticasDaPropostaTest
    {
        [Test]
        public void obter_criticas()
        { 
            var planos = MockRepository.GenerateMock<IRepositorio<Plano>>();

            Guid idDoPLano = Guid.NewGuid();

            planos.Expect(x=> x.PorId(idDoPLano)).Return(new Plano());

            var servico = new ServicoObterCriticasDaProposta(planos);

           var criticas =  servico.Obter(new PropostaDTO() { CPF = "123" }, idDoPLano);

           Assert.That(criticas.First().Mensagem, Is.EqualTo("Nome do participante é obrigatório"));
           Assert.That(criticas.First().Campo, Is.EqualTo("Nome"));
           Assert.That(criticas[1].Campo, Is.EqualTo("Cpf"));
           Assert.That(criticas[1].Mensagem, Is.EqualTo("Cpf está inválido"));
        }
    }
}
