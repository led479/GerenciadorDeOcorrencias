using GerenciadorDeOcorrencias.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeOcorrencias.Models
{
    public class Ocorrencia
    {
        public string Id { get; set; }
        public string Resumo { get; set; }
        public EstadoOcorrenciaEnum Estado { get; set; }
        public PrioridadeOcorrenciaEnum Prioridade { get; set; }
        public TipoOcorrenciaEnum Tipo { get; set; }
        public Funcionario Responsavel { get; set; }

        public Ocorrencia()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
