using GerenciadorDeOcorrencias.Enums;
using GerenciadorDeOcorrencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeOcorrencias.Controllers
{
    public class OcorrenciasController
    {
        private static OcorrenciasController _instance;
        public List<Ocorrencia> Ocorrencias { get; set; }

        public OcorrenciasController()
        {
            Ocorrencias = new List<Ocorrencia>();
        }

        public static OcorrenciasController GetInstance()
        {
            if (_instance == null)
                _instance = new OcorrenciasController();

            return _instance;
        }

        public void AddOcorrencia(Ocorrencia ocorrencia, string projetoId, string responsavelId)
        {
            var projeto = ProjetosController.GetInstance().Projetos.FirstOrDefault(x => x.Id == projetoId);
            if (projeto == null)
                throw new ApplicationException($"Projeto com ID = '{projetoId}' não foi encontrado");

            var responsavel = FuncionariosController.GetInstance().Funcionarios.FirstOrDefault(x => x.Id == responsavelId);
            if (responsavel == null)
                throw new ApplicationException($"Funcionario com ID = '{responsavelId}' não foi encontrado");

            if (responsavel.Ocorrencias.Where(x => x.Estado == EstadoOcorrenciaEnum.ABERTA).Count() == 10)
                throw new ApplicationException($"Funcionario com ID = '{responsavelId}' pode ter no máximo 10 ocorrências abertas");

            ocorrencia.Estado = EstadoOcorrenciaEnum.ABERTA;

            ocorrencia.Responsavel = responsavel;
            projeto.Ocorrencias.Add(ocorrencia);
            responsavel.Ocorrencias.Add(ocorrencia);

            Ocorrencias.Add(ocorrencia);
        }

        public void MudaResponsavel(string ocorrenciaId, string novoResponsavelId)
        {
            var ocorrencia = Ocorrencias.FirstOrDefault(x => x.Id == ocorrenciaId);
            if (ocorrencia == null)
                throw new ApplicationException($"Ocorrencia com ID = '{ocorrenciaId}' não foi encontrada");

            if (ocorrencia.Estado == EstadoOcorrenciaEnum.FECHADA)
                throw new ApplicationException($"Ocorrencia com ID = '{ocorrenciaId}' foi fechada e não permite a mudança de responsável");

            var novoResponsavel = FuncionariosController.GetInstance().Funcionarios.FirstOrDefault(x => x.Id == novoResponsavelId);
            if (novoResponsavel == null)
                throw new ApplicationException($"Funcionario com ID = '{novoResponsavelId}' não foi encontrado");

            if (novoResponsavel.Ocorrencias.Where(x => x.Estado == EstadoOcorrenciaEnum.ABERTA).Count() == 10)
                throw new ApplicationException($"Funcionario com ID = '{novoResponsavelId}' pode ter no máximo 10 ocorrências abertas");

            ocorrencia.Responsavel.Ocorrencias.Remove(ocorrencia);

            ocorrencia.Responsavel = novoResponsavel;
            novoResponsavel.Ocorrencias.Add(ocorrencia);
        }

        public void MudaPrioridade(string ocorrenciaId, PrioridadeOcorrenciaEnum prioridade)
        {
            var ocorrencia = Ocorrencias.FirstOrDefault(x => x.Id == ocorrenciaId);
            if (ocorrencia == null)
                throw new ApplicationException($"Ocorrencia com ID = '{ocorrenciaId}' não foi encontrada");

            if (ocorrencia.Estado == EstadoOcorrenciaEnum.FECHADA)
                throw new ApplicationException($"Ocorrencia com ID = '{ocorrenciaId}' foi fechada e não permite a mudança de prioridade");

            ocorrencia.Prioridade = prioridade;
        }

        public void FechaOcorrencia(string ocorrenciaId)
        {
            var ocorrencia = Ocorrencias.FirstOrDefault(x => x.Id == ocorrenciaId);
            if (ocorrencia == null)
                throw new ApplicationException($"Ocorrencia com ID = '{ocorrenciaId}' não foi encontrada");

            ocorrencia.Estado = EstadoOcorrenciaEnum.FECHADA;
        }

        
    }
}
