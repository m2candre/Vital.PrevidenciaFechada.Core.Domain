using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NHibernate;
using Rhino.Mocks;
using Vital.PrevidenciaFechada.Core.Domain.Entities;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponentePlano;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Repository
{
    [TestFixture]
    public class RepositorioTest
    {
        [Test]
        public void construir_repositorio_com_sucesso()
        {
            Repositorio<Plano> planos = new Repositorio<Plano>();
        }

        [Test]
        public void adicionar_registro_ao_repositorio_de_agregacao_com_sucesso()
        {
            var session = MockRepository.GenerateMock<ISession>();

            var plano = new Plano();

            session.Expect(x => x.SaveOrUpdate(plano));

            var planos = new Repositorio<Plano>(session);

            planos.Adicionar(plano);
        }

        [Test]
        public void adicionar_registro_ao_repositorio_com_excecao()
        {
            string message = string.Empty;

            try
            {
                var session = MockRepository.GenerateMock<ISession>();

                var plano = new Plano();

                session.Expect(x => x.SaveOrUpdate(plano)).Throw(new Exception("teste"));

                var planos = new Repositorio<Plano>(session);

                planos.Adicionar(plano);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            Assert.AreEqual("teste", message);
        }

        [Test]
        public void adicionar_lista_de_registros_ao_repositorio_com_sucesso()
        {
            var session = MockRepository.GenerateMock<ISession>();

            var planos = new Repositorio<Plano>(session);

            List<IAggregateRoot<Guid>> lista = new List<IAggregateRoot<Guid>> { new Plano() };

            planos.AdicionarLista(lista);

            session.VerifyAllExpectations();
        }

        [Test]
        public void remover_registro_do_repositorio_com_sucesso()
        {
            var session = MockRepository.GenerateMock<ISession>();

            var plano = new Plano();

            session.Expect(x => x.Delete(plano));

            var planos = new Repositorio<Plano>(session);

            planos.Remover(plano);
        }

        [Test]
        public void obter_todos_com_sucesso()
        {
            IList<Plano> lista = new List<Plano>
            {
                new Plano()
            };

            Guid Id = Guid.NewGuid();

            var session = MockRepository.GenerateMock<ISession>();

            ICriteria criteria = MockRepository.GenerateMock<ICriteria>();

            criteria.Expect(x => x.List<Plano>()).Return(lista);

            session.Expect(x => x.CreateCriteria(typeof(Plano))).Return(criteria);

            var planos = new Repositorio<Plano>(session);

            planos.Todos();

            session.VerifyAllExpectations();
        }

        [Test]
        public void obter_todas_com_sucesso()
        {
            IList<Plano> lista = new List<Plano>
            {
                new Plano()
            };

            Guid Id = Guid.NewGuid();

            var session = MockRepository.GenerateMock<ISession>();

            ICriteria criteria = MockRepository.GenerateMock<ICriteria>();

            criteria.Expect(x => x.List<Plano>()).Return(lista);

            session.Expect(x => x.CreateCriteria(typeof(Plano))).Return(criteria);

            var planos = new Repositorio<Plano>(session);

            planos.Todas();

            session.VerifyAllExpectations();
        }

        [Test]
        public void obter_por_id_sucesso_test()
        {
            Plano plano = new Plano();
            Guid Id = Guid.NewGuid();

            var session = MockRepository.GenerateMock<ISession>();

            session.Expect(x => x.Get<Plano>(Id)).Return(plano);

            var planos = new Repositorio<Plano>(session);

            planos.PorId(Id);

            session.VerifyAllExpectations();
        }
    }
}
