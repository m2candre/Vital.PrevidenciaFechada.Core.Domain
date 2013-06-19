using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Mappers;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Mappers
{
    public class ConvenioDeAdesaoMapperTest
    {
        private ConvenioDeAdesaoMapper _convenioDeAdesaoMapper;

        [TestFixtureSetUp]
        public void Init()
        {
            _convenioDeAdesaoMapper = new ConvenioDeAdesaoMapper();
        }

        /// <summary>
        /// Verifica se consegue transformar mapear uma entidade num dto
        /// </summary>
        [Test]
        public void verifica_entidade_x_dto()
        {
            //entidade
            var entidade = new Entidade();
            entidade.Nome = "Entidade de Teste";
            entidade.Id = Guid.NewGuid();

            //plano
            var plano = new Plano();
            plano.Id = Guid.NewGuid();
            plano.Nome = "Plano de Teste";

            //pessoa juridica
            var pj = new Instituidor();
            pj.RazaoSocial = "1234567890";
            pj.Id = Guid.NewGuid();
            //~

            //convenio
            var convenio = new ConvenioDeAdesao(entidade,pj,plano);
            convenio.Id = Guid.NewGuid();
            //~

            var dto = _convenioDeAdesaoMapper.ObterDTO(convenio);

            Assert.IsTrue(dto.Entidade.Nome == "Entidade de Teste");
            Assert.IsTrue(dto.Plano.Nome == "Plano de Teste");
            Assert.IsTrue(dto.PessoaJuridica.RazaoSocial == "1234567890");
        }

        /// <summary>
        /// Verifica se consegue transformar uma lista de entidade em dto
        /// Inderetamente testa o ObterDTO
        /// </summary>
        [Test]
        public void verifica_lista_de_entidade_x_dto()
        {
            //entidade
            var entidade1 = new Entidade();
            entidade1.Nome = "Entidade de Teste";
            entidade1.Id = Guid.NewGuid();

            var entidade2 = new Entidade();
            entidade2.Nome = "Entidade de Teste";
            entidade2.Id = Guid.NewGuid();

            //plano
            var plano1 = new Plano();
            plano1.Id = Guid.NewGuid();
            plano1.Nome = "Plano de Teste";

            var plano2 = new Plano();
            plano2.Id = Guid.NewGuid();
            plano2.Nome = "Plano de Teste";

            //pessoa juridica
            var pj1 = new Instituidor();
            pj1.RazaoSocial = "1234567890";
            pj1.Id = Guid.NewGuid();

            var pj2 = new Instituidor();
            pj2.RazaoSocial = "1234567890";
            pj2.Id = Guid.NewGuid();
            //~

            //convenio
            var convenio1 = new ConvenioDeAdesao(entidade1, pj1, plano1);
            convenio1.Id = Guid.NewGuid();

            var modeloProposta = new ModeloDeProposta();
            convenio1.AdicionarModeloDeProposta(modeloProposta);

            var proposta = new Proposta();
            convenio1.AdicionarProposta(proposta);

            var convenio2 = new ConvenioDeAdesao(entidade2, pj2, plano2);
            convenio2.Id = Guid.NewGuid();
            //~

            var listaEntidade = new List<ConvenioDeAdesao>();
            listaEntidade.Add(convenio1);
            listaEntidade.Add(convenio2);

            var listaDTO = _convenioDeAdesaoMapper.ObterListaDeConvenioDeAdesaoDTO(listaEntidade);

            Assert.IsTrue(listaDTO.Count == 2);
        }

        /// <summary>
        /// Verifica se consegue transformar uma lista de entidade em dto
        /// Inderetamente testa o ObterDTO
        /// </summary>
        [Test]
        public void verifica_lista_de_entidade_x_dto_com_os_dados_de_teste_do_banco()
        {
            var mongeral = new Entidade { Nome = "MONGERAL" };
            Patrocinador pj = new Patrocinador { RazaoSocial = "Teste query" };
            var quanta = new Entidade { Nome = "QUANTA" };
            var abepom = new Plano() { Nome = "ABEPOM" };
            var contabilprev = new Plano() { Nome = "CONTABILPREV" };
            var ciadPrev = new Plano() { Nome = "CIAD-PREV" };
            var precaver = new Plano() { Nome = "PRECAVER" };

            var convenioprecaver = new ConvenioDeAdesao(quanta, pj, precaver);
            var conveniociadPrev = new ConvenioDeAdesao(quanta, pj, ciadPrev);
            var conveniocontabilprev = new ConvenioDeAdesao(mongeral, pj, contabilprev);
            var convenioabepom = new ConvenioDeAdesao(mongeral, pj, abepom);

            var listaEntidade = new List<ConvenioDeAdesao>();

            listaEntidade.Add(convenioprecaver);
            listaEntidade.Add(conveniociadPrev);
            listaEntidade.Add(conveniocontabilprev);
            listaEntidade.Add(convenioabepom);

            var listaDTO = _convenioDeAdesaoMapper.ObterListaDeConvenioDeAdesaoDTO(listaEntidade);

            Assert.IsTrue(listaDTO.Count == 4);
        }
    }
}
