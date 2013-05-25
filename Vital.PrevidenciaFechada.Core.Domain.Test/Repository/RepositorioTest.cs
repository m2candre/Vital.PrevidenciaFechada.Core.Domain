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
using System.Linq.Expressions;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using Vital.PrevidenciaFechada.DTO.Messages.Core;
using Vital.PrevidenciaFechada.DTO.Messages;

namespace Vital.PrevidenciaFechada.Core.Domain.Test.Repository
{
    [TestFixture]
    public class RepositorioTest
    {
        private ISession session;
        private ICriteria criteria;
        private ICriterion criterion;
        private VitalCriterion vitalCriterion;

        [SetUp]
        public void inicializar()
        {
            session = MockRepository.GenerateMock<ISession>();
            criteria = MockRepository.GenerateMock<ICriteria>();
            criterion = MockRepository.GenerateMock<ICriterion>();
            vitalCriterion = MockRepository.GenerateMock<VitalCriterion>();
        }

        [Test]
        public void construir_repositorio_com_sucesso()
        {
            Repositorio<Plano> planos = new Repositorio<Plano>();
        }

        [Test]
        public void adicionar_registro_ao_repositorio_de_agregacao_com_sucesso()
        {
            var plano = new Plano();

            session.Expect(x => x.SaveOrUpdate(plano));

            var planos = new Repositorio<Plano>();
			planos.Session = session;

            planos.Salvar(plano);
        }

        [Test]
        public void adicionar_registro_ao_repositorio_com_excecao()
        {
            string message = string.Empty;

            try
            {
                var plano = new Plano();

                session.Expect(x => x.SaveOrUpdate(plano)).Throw(new Exception("teste"));

                var planos = new Repositorio<Plano>();
				planos.Session = session;

                planos.Salvar(plano);
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
            var planos = new Repositorio<Plano>();
			planos.Session = session;

            List<IAggregateRoot<Guid>> lista = new List<IAggregateRoot<Guid>> { new Plano() };

            planos.Salvar(lista);

            session.VerifyAllExpectations();
        }

        [Test]
        public void remover_registro_do_repositorio_com_sucesso()
        {
            var plano = new Plano();

            session.Expect(x => x.Delete(plano));

            var planos = new Repositorio<Plano>();
			planos.Session = session;

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

            criteria.Expect(x => x.List<Plano>()).Return(lista);

            session.Expect(x => x.CreateCriteria(typeof(Plano))).Return(criteria);

            var planos = new Repositorio<Plano>();
			planos.Session = session;

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

            criteria.Expect(x => x.List<Plano>()).Return(lista);

            session.Expect(x => x.CreateCriteria(typeof(Plano))).Return(criteria);

            var planos = new Repositorio<Plano>();
			planos.Session = session;

            planos.Todos();

            session.VerifyAllExpectations();
        }

        [Test]
        public void obter_por_id_sucesso_test()
        {
            Plano plano = new Plano();
            Guid Id = Guid.NewGuid();

            session.Expect(x => x.Get<Plano>(Id)).Return(plano);

            var planos = new Repositorio<Plano>();
			planos.Session = session;

            planos.ObterPorId(Id);

            session.VerifyAllExpectations();
        }

        [Test]
        public void obter_todos_os_objetos_filtrados_de_acordo_com_criterios_paginados_e_ordenados_em_ordem_crescente()
        {
            Expression<Func<Proposta, bool>> criterio = p => p.Estado == "Iniciada";

            vitalCriterion.Expect(x => x.Where<Proposta>(criterio)).Return(criterion);

            var order = new Order(Projections.Property("Numero"), true);
            vitalCriterion.Expect(x => x.OrderBy("Numero", true)).Return(order);

            criteria.Expect(x => x.List<Proposta>()).Return(new List<Proposta>());
            criteria.Expect(x => x.AddOrder(order)).Return(criteria);
            criteria.Expect(x => x.SetMaxResults(1)).Return(criteria);
            criteria.Expect(x => x.SetFirstResult(1)).Return(criteria);
            criteria.Expect(x => x.Add(criterion)).Return(criteria);

            session.Expect(x => x.CreateCriteria(typeof(Proposta))).Return(criteria);

            Repositorio<Proposta> repositorio = new Repositorio<Proposta>();
			repositorio.VitalCriterion = vitalCriterion;
			repositorio.Session = session;
            
			repositorio.ObterLista(criterio, new ConsultaDTO { OrdemCrescente = true, CampoOrdenacao = "Numero", Paginacao = new PaginacaoDTO { PaginaAtual = 2, Linhas = 1 } });

            criteria.VerifyAllExpectations();
            vitalCriterion.VerifyAllExpectations();
            session.VerifyAllExpectations();
        }

		[Test]
		public void obter_unico_objeto_de_acordo_com_criterio()
		{
			Expression<Func<Plano, bool>> criterio = p => p.Nome == "Teste";

			vitalCriterion.Expect(x => x.Where<Plano>(criterio)).Return(criterion);
			criteria.Expect(x => x.Add(criterion)).Return(criteria);

			session.Expect(x => x.CreateCriteria(typeof(Plano))).Return(criteria);

			Repositorio<Plano> repositorio = new Repositorio<Plano>();
			repositorio.VitalCriterion = vitalCriterion;
			repositorio.Session = session;
			
			repositorio.ObterLista(criterio);

			criteria.VerifyAllExpectations();
			vitalCriterion.VerifyAllExpectations();
			session.VerifyAllExpectations();
		}
    }
}
