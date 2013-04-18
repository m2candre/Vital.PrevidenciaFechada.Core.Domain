using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;


namespace Vital.PrevidenciaFechada.Core.Domain.Test
{
    public class Dados
    {
        private IList<ClasseDeTamanhoDoCampo> listaTamanhoDosCampos;

        public void AdicionarTamanhos()
        {
            var listaCampos = new[] {45, 75, 100, 110, 135, 140, 150, 157, 192, 214, 240, 249, 253, 260, 270, 280, 320, 380, 395, 400, 411, 450, 543, 570, 580, 600, 700, 990};
            listaTamanhoDosCampos = listaCampos.Select(listaCampo => new ClasseDeTamanhoDoCampo(listaCampo)).ToList();
            
        }

        public void AdicionarModelosDeCampo()
        {
            IList<ModeloDoCampo> listaDeModelosDosCampos = new List<ModeloDoCampo>();
    
            listaDeModelosDosCampos.Add(new ModeloDoCampo("Título", "<div class='@Css'><h1>@titulo</h1></div>", "<div class='@Css'><h1>@titulo</h1></div>"));
        }



        public void Campos()
        {
            ModeloDeProposta modeloDeProposta = new ModeloDeProposta();

            CampoDeProposta campoDeProposta = new CampoDeProposta();
            campoDeProposta.Id = Guid.NewGuid();
            campoDeProposta.Nome = "Nome";
            campoDeProposta.Titulo = "Nome do proponente";
            campoDeProposta.TamanhoDoCampo = new ClasseDeTamanhoDoCampo(45);
            campoDeProposta.ModeloDoCampo = new ModeloDoCampo("Título", "<div class='@Css'><h1>@titulo</h1></div>", "<div class='@Css'><h1>@titulo</h1></div>");

            modeloDeProposta.AdicionarCampo(campoDeProposta);

        }
    }
}

