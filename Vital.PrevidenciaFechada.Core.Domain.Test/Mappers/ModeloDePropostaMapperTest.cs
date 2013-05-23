using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteModeloDeProposta;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Mappers;
using Vital.PrevidenciaFechada.DTO.Messages.Atuarial.ModeloDeProposta;
using Vital.PrevidenciaFechada.DTO.Messages.Core;
// ~

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Mappers
{
    public class ModeloDePropostaMapperTest
    {
        private ModeloDePropostaMapper _modeloDePropostaMapper;

        [TestFixtureSetUp]
        public void Init()
        {
            _modeloDePropostaMapper = new ModeloDePropostaMapper();
        }

        /// <summary>
        /// Cria um Modelo da Proposta contendo Campos e mapeia para Modelo da Proposta DTO
        /// Verifica se os campos contidos internamente estao sendo mapeados corretamente
        /// </summary>
        [Test]
        public void verifica_conversao_da_entidade_para_dto()
        {
            var modeloDeProposta = new ModeloDeProposta();

            var testCampo = new CampoDeProposta();

            testCampo.Nome = "ayrton_senna";
            testCampo.Valor = "john player special";

            modeloDeProposta.AdicionarCampo(testCampo);

            var modeloDaPropostaDTO = _modeloDePropostaMapper.ObterModeloDaPropostaDTO(modeloDeProposta);

            Assert.IsTrue(modeloDaPropostaDTO.Campos[0].Nome == "ayrton_senna");
            Assert.IsTrue(modeloDaPropostaDTO.Campos[0].Valor == "john player special");
        }

        /// <summary>
        /// Cria um dto do modelo da proposta contendo campos e testa a conversao para modelo da proprosta
        /// Verifica se os campos contidos internamente estao sendo mapeados corretamente
        /// </summary>
        [Test]
        public void verifica_a_conversao_do_dto_para_entidade()
        {
            var modeloDePropostaDTO = new ModeloDePropostaDTO();

            var testCampoDTO = new CampoDaPropostaDTO();

            testCampoDTO.Nome = "ayrton_senna";
            testCampoDTO.Valor = "Lotus";

            modeloDePropostaDTO.Campos = new List<CampoDaPropostaDTO>();
            modeloDePropostaDTO.Campos.Add(testCampoDTO);

            var modeloDaProposta = _modeloDePropostaMapper.ObterModeloDaProposta(modeloDePropostaDTO);

            Assert.IsTrue(modeloDaProposta.Campos[0].Nome == "ayrton_senna");
            Assert.IsTrue(modeloDaProposta.Campos[0].Valor == "Lotus");
        }
    }
}
