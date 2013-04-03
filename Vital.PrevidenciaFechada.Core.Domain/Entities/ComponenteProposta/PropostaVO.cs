using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta
{
    /// <summary>
    /// Value Object de crítica
    /// </summary>
    public class CriticaVO
    {
        /// <summary>
        /// Campo
        /// </summary>
        public virtual string Campo { get; set; }

        /// <summary>
        /// Crítica
        /// </summary>
        public virtual string Critica { get; set; }
    }

    /// <summary>
    /// Value Object de proposta para transitar dados de validação
    /// </summary>
    public class PropostaVO : ICloneable
    {
        #region Propriedades

        /// <summary>
        /// Nome do participante
        /// </summary>
        public virtual string NomeDoParticipante { get; private set; }

        /// <summary>
        /// Cpf do participante
        /// </summary>
        public virtual string CpfDoParticipante { get; private set; }

        /// <summary>
        /// Mensagens - Restrições para o cadastro de proposta.
        /// </summary>
        public virtual IList<CriticaVO> Criticas { get; private set; }

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        public PropostaVO()
        {
            Criticas = new List<CriticaVO>();
        }

        /// <summary>
        /// Construtor passando nome do participante
        /// </summary>
        /// <param name="nomeDoParticipante">Nome do participante da proposta</param>
        public PropostaVO(string nomeDoParticipante)
            : this()
        {
            NomeDoParticipante = nomeDoParticipante;
        }

        /// <summary>
        /// Construtor passando nome do participante e o cpf
        /// </summary>
        /// <param name="cpfDoParticipante">Cpf do participante</param>
        /// <param name="nomeDoParticipante">Nome do participante</param>
        public PropostaVO(string nomeDoParticipante, string cpfDoParticipante)
            : this()
        {
            NomeDoParticipante = nomeDoParticipante;
            CpfDoParticipante = cpfDoParticipante;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Informar o nome do Participante
        /// </summary>
        /// <param name="nome">nome do participante</param>
        /// <returns>PropostaVO</returns>
        public virtual PropostaVO InformarNome(string nome)
        {
            var propostaVO = (PropostaVO)Clone();

            propostaVO.NomeDoParticipante = nome;

            return propostaVO;
        }

        /// <summary>
        /// Informa o cpf do participante
        /// </summary>
        /// <param name="cpf">número do cpf</param>
        /// <returns>PropostaVO</returns>
        public virtual PropostaVO InformarCPF(string cpf)
        {
            var propostaVO = (PropostaVO)Clone();

            propostaVO.CpfDoParticipante = cpf;

            return propostaVO;
        }

        /// <summary>
        /// Informa uma crítica a proposta
        /// </summary>
        /// <param name="critica"></param>
        /// <returns></returns>
        public virtual PropostaVO InformarCritica(string critica, string campo)
        {
            CriticaVO criticaVO = new CriticaVO { Critica = critica, Campo = campo };

            var propostaVO = (PropostaVO)Clone();

            propostaVO.Criticas.Add(criticaVO);

            return propostaVO;
        }

        /// <summary>
        /// Clona o objeto atual em um outro objeto
        /// </summary>
        /// <returns>Objeto clonado</returns>
        public virtual object Clone()
        {
            PropostaVO proposta = new PropostaVO();

            proposta.NomeDoParticipante = this.NomeDoParticipante;
            proposta.CpfDoParticipante = this.CpfDoParticipante;
            proposta.Criticas.ToList().AddRange(this.Criticas);

            return proposta;
        }

        #endregion


    }
}
