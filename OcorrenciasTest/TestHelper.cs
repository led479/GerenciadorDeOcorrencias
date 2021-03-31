using GerenciadorDeOcorrencias.Controllers;
using GerenciadorDeOcorrencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcorrenciasTest
{
    public static class TestHelper
    {
        public static (Empresa, Projeto, Funcionario) CreateEmpresaProjetoFuncionario()
        {
            var empresa = new Empresa { Nome = "Casas Bahia" };
            EmpresasController.GetInstance().AddEmpresa(empresa);

            var projeto = new Projeto { Nome = "Empilhadeira" };
            ProjetosController.GetInstance().AddProjeto(projeto, empresa.Id);

            var responsavel = new Funcionario { Nome = "Bruno" };
            FuncionariosController.GetInstance().AddFuncionario(responsavel, empresa.Id);

            return (empresa, projeto, responsavel);
        }

        public static void CreateOcorrencias(int count, string projetoId, string responsavelId)
        {
            for (int i = 0; i < count; i++)
            {
                var ocorrencia = new Ocorrencia { Resumo = $"Falha no momento de empilhar {i}" };
                OcorrenciasController.GetInstance().AddOcorrencia(ocorrencia, projetoId, responsavelId);
            }
        }
    }
}
