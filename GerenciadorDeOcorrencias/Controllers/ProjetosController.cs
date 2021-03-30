using GerenciadorDeOcorrencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeOcorrencias.Controllers
{
    public class ProjetosController
    {
        private static ProjetosController _instance;
        public List<Projeto> Projetos { get; set; }

        public ProjetosController()
        {
            Projetos = new List<Projeto>();
        }

        public static ProjetosController GetInstance()
        {
            if (_instance == null)
                _instance = new ProjetosController();

            return _instance;
        }

        public void AddProjeto(Projeto projeto, string empresaId)
        {
            if (string.IsNullOrEmpty(projeto.Nome))
                throw new ApplicationException("Projeto sem nome");

            var empresa = EmpresasController.GetInstance().Empresas.FirstOrDefault(x => x.Id == empresaId);

            if (empresa == null)
                throw new ApplicationException($"Empresa com ID = '{empresaId}' não foi encontrada");

            empresa.Projetos.Add(projeto);
            Projetos.Add(projeto);
        }
    }
}