using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Services.PreInscricao;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Services.PreInscricao
{
    [TestFixture]
    public class ObterCamposDaPreInscricaoTest
    {
        [Test]
        public void obter_lista_de_campos_da_pre_inscricao_com_sucesso()
        {
            var camposPreInscricao = new ObterCamposDaPreInscricao();

            var listaDeCamposDaPreInscricao = camposPreInscricao.Obter();

            CollectionAssert.AllItemsAreNotNull(listaDeCamposDaPreInscricao);
            Assert.That(listaDeCamposDaPreInscricao.First(), Is.EqualTo("NomeDoSegurado"));
        }
    }
}
