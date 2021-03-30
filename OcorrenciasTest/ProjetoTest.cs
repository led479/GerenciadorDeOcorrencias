using GerenciadorDeOcorrencias.Controllers;
using GerenciadorDeOcorrencias.Models;
using System;
using System.Linq;
using Xunit;

namespace OcorrenciasTest
{
    public class ProjetoTest
    {
        private Empresa empresa;
        private ProjetosController ctrl;

        public ProjetoTest()
        {
            empresa = new Empresa { Nome = "Casas Bahia" };
            EmpresasController.GetInstance().AddEmpresa(empresa);

            ctrl = ProjetosController.GetInstance();
        }

        [Fact]
        public void DeveAdicionarProjeto()
        {
            var projeto = new Projeto { Nome = "Empilhadeira" };

            ctrl.AddProjeto(projeto, empresa.Id);

            Assert.Contains(projeto, empresa.Projetos);
            Assert.Contains(projeto, ctrl.Projetos);
        }

        [Fact]
        public void NaoDeveAdicionarProjetoSemNome()
        {
            var projeto = new Projeto();

            Assert.Throws<ApplicationException>(() => ctrl.AddProjeto(projeto, empresa.Id));
        }

        [Fact]
        public void NaoDeveAdicionarProjetoEmEmpresaInexistente()
        {
            var projeto = new Projeto { Nome = "Empilhadeira" };

            Assert.Throws<ApplicationException>(() => ctrl.AddProjeto(projeto, "-1"));
        }
    }
}
