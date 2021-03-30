using GerenciadorDeOcorrencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeOcorrencias.Controllers
{
    public class EmpresasController
    {
        private static EmpresasController _instance;
        public List<Empresa> Empresas { get; set; }

        public EmpresasController()
        {
            Empresas = new List<Empresa>();
        }

        public static EmpresasController GetInstance()
        {
            if (_instance == null)
                _instance = new EmpresasController();

            return _instance;
        }

        public void AddEmpresa(Empresa empresa)
        {
            Empresas.Add(empresa);
        }
    }
}
