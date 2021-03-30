using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeOcorrencias.Models
{
    public class Empresa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public List<Funcionario> Funcionarios { get; set; }
        public List<Projeto> Projetos { get; set; }

        public Empresa()
        {
            Id = Guid.NewGuid().ToString();

            Funcionarios = new List<Funcionario>();
            Projetos = new List<Projeto>();
        }
    }
}
