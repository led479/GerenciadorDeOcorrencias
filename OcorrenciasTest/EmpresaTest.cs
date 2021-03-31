using GerenciadorDeOcorrencias.Controllers;
using GerenciadorDeOcorrencias.Models;
using System;
using System.Linq;
using Xunit;

namespace OcorrenciasTest
{
    public class EmpresaTest
    {
        [Fact]
        public void DeveAdicionarUmaEmpresa()
        {
            var empresa = new Empresa { Nome = "Casas Bahia" };

            EmpresasController.GetInstance().AddEmpresa(empresa);

            Assert.Contains(empresa, EmpresasController.GetInstance().Empresas);
        }

        [Fact] // Teste novo
        public void ListaDeveExistirMesmoSemNenhumaEmpresa()
        {
            Assert.NotNull(EmpresasController.GetInstance().Empresas);
        }

        [Fact] // Teste novo
        public void DeveAdicionarDuasEmpresas()
        {
            var empresa = new Empresa { Nome = "Casas Bahia" };
            var empresa2 = new Empresa { Nome = "Casas Bahia 2" };

            EmpresasController.GetInstance().AddEmpresa(empresa);
            EmpresasController.GetInstance().AddEmpresa(empresa2);

            Assert.Contains(empresa, EmpresasController.GetInstance().Empresas);
            Assert.Contains(empresa2, EmpresasController.GetInstance().Empresas);
        }
    }
}
