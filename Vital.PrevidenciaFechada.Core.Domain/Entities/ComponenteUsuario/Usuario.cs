using System;
using System.Security.Principal;

namespace Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteUsuario
{
    public class Usuario : IPrincipal
    {
        /// <summary>
        /// Identidade do Usuário 
        /// </summary>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// Construtor com a dependencia Indentidade
        /// </summary>
        /// <param name="identity"></param>
        public Usuario(IIdentity identity)
        {
            this.Identity = identity;
        }

        /// <summary>
        /// Verifica se o Usuário está em determinado perfil
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
