using System;
using System.Collections.Generic;
using System.Security.Principal;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteEntidade;

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
        /// Lista de Entidades as quais o usuário faz parte
        /// </summary>
        public virtual IList<Entidade> Entidades { get; set; }

        /// <summary>
        /// Construtor para serialização
        /// </summary>
        public Identidade() 
        {
            Entidades = new List<Entidade>();
        }

        /// <summary>
        /// Construtor com dependencia de nome
        /// </summary>
        /// <param name="name">Nome do Usuário</param>
        public Identidade(string name)
        {
            this.Name = name;
            Entidades = new List<Entidade>();
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
