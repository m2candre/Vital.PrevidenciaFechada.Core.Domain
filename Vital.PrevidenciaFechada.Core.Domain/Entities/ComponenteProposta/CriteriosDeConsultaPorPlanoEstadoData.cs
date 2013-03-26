using System;
using System.Linq.Expressions;
using Vital.InfraStructure.DSL.DesignByContract;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
	/// <summary>
	/// Obtém critérios para consulta de propostas
	/// </summary>
	public class CriteriosDeConsultaPorPlanoEstadoData
	{
		/// <summary>
		/// Obtém critérios para consulta por Plano, Estado da proposta e período inicial
		/// </summary>
		/// <param name="idDoPlano">ID do plano</param>
		/// <param name="estado">Estado da proposta</param>
		/// <param name="dataDaBusca">Data inicial</param>
		/// <returns></returns>
		public virtual Expression<Func<Proposta, bool>> ObterCriterio(Guid idDoPlano, string estado, DateTime dataDaBusca)
		{
			#region Pré-condições

			IAssertion oIDDoPlanoFoiInformado = Assertion.IsTrue(idDoPlano != Guid.Empty, "O ID do plano deve ser informado");
			IAssertion oEstadoFoiInformado = Assertion.IsFalse(string.IsNullOrWhiteSpace(estado), "O estado da proposta deve ser informado");
			IAssertion aQuantidadeDeDiasFoiInformada = Assertion.GreaterThan(dataDaBusca, default(DateTime), "A data da busca possui um valor inválido");

			#endregion

			oIDDoPlanoFoiInformado.and(oEstadoFoiInformado).and(aQuantidadeDeDiasFoiInformada).Validate();

			Expression<Func<Proposta, bool>> criterio = proposta => proposta.Plano.Id == idDoPlano && proposta.Estado == estado && proposta.Data >= dataDaBusca;

			#region Pós-condições

			IAssertion oCriterioFoiConstruido = Assertion.NotNull(criterio, "O critério não foi construído corretamente");

			#endregion

			oCriterioFoiConstruido.Validate();

			return criterio;
		}
	}
}