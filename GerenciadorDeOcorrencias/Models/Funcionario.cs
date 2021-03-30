using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeOcorrencias.Models
{
    public class Funcionario
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        // MAX: 10
        public List<Ocorrencia> Ocorrencias { get; set; }

        public Funcionario()
        {
            Id = Guid.NewGuid().ToString();

            Ocorrencias = new List<Ocorrencia>();
        }
    }
}
