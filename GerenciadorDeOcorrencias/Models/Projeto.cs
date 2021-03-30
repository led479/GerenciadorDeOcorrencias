using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeOcorrencias.Models
{
    public class Projeto
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public List<Funcionario> Funcionarios { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }

        public Projeto()
        {
            Id = Guid.NewGuid().ToString();

            Funcionarios = new List<Funcionario>();
            Ocorrencias = new List<Ocorrencia>();
        }
    }
}
