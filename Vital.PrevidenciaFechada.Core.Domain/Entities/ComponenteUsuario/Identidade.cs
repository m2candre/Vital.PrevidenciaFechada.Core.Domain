using System;
using System.Collections.Generic;
using System.Security.Principal;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePessoaJuridica;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario
{
    /// <summary>
    /// Identidade do Usuário no Sistema
    /// </summary>
    public class Identidade : IAggregateRoot<Guid>, IIdentity
    {
        /// <summary>
        /// Id do Usuário
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Convênio de Adesão do contexto atual
        /// </summary>
        public virtual ConvenioDeAdesao ConvenioDeAdesao { get; set; }

        /// <summary>
        /// Pessoa Jurídica
        /// </summary>
        public virtual PessoaJuridica PessoaJuridica { get; set; }

        /// <summary>
        /// Construtor para serialização
        /// </summary>
		public Identidade() { }

        /// <summary>
        /// Construtor com dependencia de nome
        /// </summary>
        /// <param name="name">Nome do Usuário</param>
        public Identidade(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Tipo de Autenticação desse Menbership
        /// </summary>
        public virtual string AuthenticationType
        {
            get { return "Custom"; }
        }

        /// <summary>
        /// Determina se o usuário está autenticado
        /// </summary>
        public virtual bool IsAuthenticated
        {
            get { return false; }
        }
    }
}
