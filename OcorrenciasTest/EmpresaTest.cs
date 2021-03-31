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

        //TODO: Testar sem nenhuma empresa

        //TODO: Testar com duas empresas
    }
}
