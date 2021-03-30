using GerenciadorDeOcorrencias.Controllers;
using GerenciadorDeOcorrencias.Models;
using System;
using System.Linq;
using Xunit;

namespace OcorrenciasTest
{
    public class FuncionarioTest
    {
        private Empresa empresa;
        private FuncionariosController ctrl;

        public FuncionarioTest()
        {
            empresa = new Empresa { Nome = "Casas Bahia" };
            EmpresasController.GetInstance().AddEmpresa(empresa);

            ctrl = FuncionariosController.GetInstance();
        }

        [Fact]
        public void DeveAdicionarFuncionarioNaEmpresa()
        {
            var funcionario = new Funcionario { Nome = "Bruno" };

            ctrl.AddFuncionario(funcionario, empresa.Id);

            Assert.Contains(funcionario, empresa.Funcionarios);
            Assert.Contains(funcionario, ctrl.Funcionarios);
        }

        [Fact]
        public void NaoDeveAdicionarFuncionarioSemNome()
        {
            var funcionario = new Funcionario();

            Assert.Throws<ApplicationException>(() => ctrl.AddFuncionario(funcionario, empresa.Id));
        }

        [Fact]
        public void NaoDeveAdicionarFuncionarioEmEmpresaInexistente()
        {
            var funcionario = new Funcionario { Nome = "Bruno" };

            Assert.Throws<ApplicationException>(() => ctrl.AddFuncionario(funcionario, "-1"));
        }
    }
}
