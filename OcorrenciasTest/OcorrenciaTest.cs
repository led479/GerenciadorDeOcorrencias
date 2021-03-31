using GerenciadorDeOcorrencias.Controllers;
using GerenciadorDeOcorrencias.Enums;
using GerenciadorDeOcorrencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OcorrenciasTest
{
    public class OcorrenciaTest
    {
        private OcorrenciasController ctrl;

        public OcorrenciaTest()
        {
            ctrl = OcorrenciasController.GetInstance();
        }

        [Fact] // Teste novo
        public void CriaOcorrenciaCorretamente()
        {
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };

            Assert.Equal(EstadoOcorrenciaEnum.ABERTA, ocorrencia.Estado);
            Assert.Equal(TipoOcorrenciaEnum.TAREFA, ocorrencia.Tipo);
            Assert.Equal(PrioridadeOcorrenciaEnum.ALTA, ocorrencia.Prioridade);
            Assert.Null(ocorrencia.Responsavel);
            Assert.NotNull(ocorrencia.Id);
            Assert.NotNull(ocorrencia.Resumo);
        }

        [Fact]
        public void DeveAdicionarOcorrenciaNoProjeto()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };

            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);

            Assert.Equal(EstadoOcorrenciaEnum.ABERTA, ocorrencia.Estado);
            Assert.Equal(responsavel, ocorrencia.Responsavel);
            Assert.Contains(ocorrencia, projeto.Ocorrencias);
        }

        [Fact]
        public void DeveAdicionarOcorrenciaNoProjetoDoTipoBug()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar", Tipo = TipoOcorrenciaEnum.BUG };

            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);

            Assert.Contains(ocorrencia, projeto.Ocorrencias);
        }

        [Fact]
        public void DeveAdicionarOcorrenciaNoProjetoDoTipoMelhoria()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar", Tipo = TipoOcorrenciaEnum.MELHORIA };

            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);

            Assert.Contains(ocorrencia, projeto.Ocorrencias);
        }

        [Fact]
        public void DeveAdicionarOcorrenciaNoProjetoDoTipoTarefa()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar", Tipo = TipoOcorrenciaEnum.TAREFA };

            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);

            Assert.Contains(ocorrencia, projeto.Ocorrencias);
        }

        [Fact]
        public void ResponsavelPodeTerNoMaximoDezOcorrenciasAbertas()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            TestHelper.CreateOcorrencias(10, projeto.Id, responsavel.Id);

            var ocorrencia11 = new Ocorrencia { Resumo = "Falha no momento de empilhar" };

            var countAnterior = ctrl.Ocorrencias.Count;
            Assert.Throws<ApplicationException>(() => ctrl.AddOcorrencia(ocorrencia11, projeto.Id, responsavel.Id));
            var countPosterior = ctrl.Ocorrencias.Count;

            Assert.Equal(countAnterior, countPosterior);
        }

        [Fact]
        public void MudaResponsavelDaOcorrencia()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);
            var novoResponsavel = new Funcionario { Nome = "Fabiano" };
            FuncionariosController.GetInstance().AddFuncionario(novoResponsavel, empresa.Id);

            ctrl.MudaResponsavel(ocorrencia.Id, novoResponsavel.Id);

            Assert.DoesNotContain(ocorrencia, responsavel.Ocorrencias);
            Assert.Contains(ocorrencia, novoResponsavel.Ocorrencias);
            Assert.Equal(novoResponsavel, ocorrencia.Responsavel);
        }

        [Fact]
        public void NaoPermiteMudarResponsavelDeOcorrenciaFechada()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);
            ctrl.FechaOcorrencia(ocorrencia.Id);
            var novoResponsavel = new Funcionario { Nome = "Fabiano" };
            FuncionariosController.GetInstance().AddFuncionario(novoResponsavel, empresa.Id);

            Assert.Throws<ApplicationException>(() =>   ctrl.MudaResponsavel(ocorrencia.Id, novoResponsavel.Id));
            Assert.Contains(ocorrencia, responsavel.Ocorrencias);
        }

        [Fact]
        public void FechaOcorrenciaComSucesso()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);

            ctrl.FechaOcorrencia(ocorrencia.Id);

            Assert.Equal(EstadoOcorrenciaEnum.FECHADA, ocorrencia.Estado);
        }

        [Fact]
        public void PodeAdicionarMaisDeDezDesdeQueEstejaFechada()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();

            TestHelper.CreateOcorrencias(9, projeto.Id, responsavel.Id);
            var ocorrencia10 = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia10, projeto.Id, responsavel.Id);
            ctrl.FechaOcorrencia(ocorrencia10.Id);

            var ocorrencia11 = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia11, projeto.Id, responsavel.Id);

            Assert.Contains(ocorrencia11,   ctrl.Ocorrencias);
        }

        [Fact]
        public void MudaPrioridadeAlta()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);

            ctrl.MudaPrioridade(ocorrencia.Id, PrioridadeOcorrenciaEnum.ALTA);

            Assert.Equal(PrioridadeOcorrenciaEnum.ALTA, ocorrencia.Prioridade);
        }

        [Fact]
        public void MudaPrioridadeMedia()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);

            ctrl.MudaPrioridade(ocorrencia.Id, PrioridadeOcorrenciaEnum.MEDIA);

            Assert.Equal(PrioridadeOcorrenciaEnum.MEDIA, ocorrencia.Prioridade);
        }

        [Fact]
        public void MudaPrioridadeBaixa()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);

            ctrl.MudaPrioridade(ocorrencia.Id, PrioridadeOcorrenciaEnum.BAIXA);

            Assert.Equal(PrioridadeOcorrenciaEnum.BAIXA, ocorrencia.Prioridade);
        }

        [Fact]
        public void NaoPermiteMudarPrioridadeDeOcorrenciaFechada()
        {
            var (empresa, projeto, responsavel) = TestHelper.CreateEmpresaProjetoFuncionario();
            var ocorrencia = new Ocorrencia { Resumo = "Falha no momento de empilhar" };
            ctrl.AddOcorrencia(ocorrencia, projeto.Id, responsavel.Id);
            ctrl.FechaOcorrencia(ocorrencia.Id);
            var prioridadeOriginal = ocorrencia.Prioridade;

            Assert.Throws<ApplicationException>(() =>   ctrl.MudaPrioridade(ocorrencia.Id, PrioridadeOcorrenciaEnum.BAIXA));
            Assert.Equal(prioridadeOriginal, ocorrencia.Prioridade); // Prioridade default = ALTA
        }
    }
}
