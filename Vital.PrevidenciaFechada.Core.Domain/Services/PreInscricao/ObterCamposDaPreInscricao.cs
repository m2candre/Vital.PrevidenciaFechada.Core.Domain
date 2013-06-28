using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities;

namespace Vital.PrevidenciaFechada.Core.Domain.Services.PreInscricao
{
    /// <summary>
    /// Obter campos da pre inscrição
    /// </summary>
    public class ObterCamposDaPreInscricao
    {
       /// <summary>
       /// Obtem uma lista de campos de pre inscritos
       /// </summary>
       /// <returns></returns>
       public virtual List<string> Obter()
       {
           var propriedades = ObterListaDePropriedadesDoObjetoPreInscrito();

           List<string> listaDeCamposDaPreInscricao = new List<string>();

           foreach (var propriedade in propriedades)
           {
			   if (propriedade.Name != "Id" && propriedade.Name != "IdDoConvenioDeAdesao")
                   listaDeCamposDaPreInscricao.Add(propriedade.Name);
           }

           #region Pós-Condições

           IAssertion listaDeCamposEstaPreenchida = Assertion.IsTrue(listaDeCamposDaPreInscricao.Any(), "A lista de campos da pré-inscrição não foi preenchida");

           #endregion

           listaDeCamposEstaPreenchida.Validate();

           return listaDeCamposDaPreInscricao;
       }

       /// <summary>
       /// Obtem um array de propertyInfo do objeto PreInscrito
       /// </summary>
       /// <returns>PropertyInfo[]</returns>
       private PropertyInfo[] ObterListaDePropriedadesDoObjetoPreInscrito()
       {
           PreInscrito preInscrito = new PreInscrito();

           return preInscrito.GetType().GetProperties();
       }
    }
}
