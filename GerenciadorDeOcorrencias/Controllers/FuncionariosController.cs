using GerenciadorDeOcorrencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeOcorrencias.Controllers
{
    public class FuncionariosController
    {
        private static FuncionariosController _instance;

        public List<Funcionario> Funcionarios { get; set; }

        public FuncionariosController()
        {
            Funcionarios = new List<Funcionario>();
        }

        public static FuncionariosController GetInstance()
        {
            if (_instance == null)
                _instance = new FuncionariosController();

            return _instance;
        }

        public void AddFuncionario(Funcionario funcionario, string empresaId)
        {
            if (string.IsNullOrEmpty(funcionario.Nome))
                throw new ApplicationException("Funcionário sem nome");

            var empresa = EmpresasController.GetInstance().Empresas.FirstOrDefault(x => x.Id == empresaId);

            if (empresa == null)
                throw new ApplicationException($"Empresa com ID = '{empresaId}' não foi encontrada");

            empresa.Funcionarios.Add(funcionario);
            Funcionarios.Add(funcionario);
        }
    }
}