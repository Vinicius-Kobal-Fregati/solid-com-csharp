using System.Collections.Generic;
using System.Linq;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    // Ao invés de mudarmos o arquivo padrão, criamos um novo alterando pontualmente o que queremos
    public class ArquivamentoAdminService : IAdminService
    {
        IAdminService _defaultService;

        // Padrão Decorator, quando não formos mudar seu comportamento chamamos o próprio método
        public ArquivamentoAdminService(ILeilaoDao dao)
        {
            _defaultService = new DefaultAdminService(dao);
        }

        public void CadastraLeilao(Leilao leilao)
        {
            _defaultService.CadastraLeilao(leilao);
        }

        public void ModificaLeilao(Leilao leilao)
        {
            _defaultService.ModificaLeilao(leilao);
        }

        public void RemoveLeilao(Leilao leilao)
        {
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao)
            {
                leilao.Situacao = SituacaoLeilao.Arquivado;
                _defaultService.ModificaLeilao(leilao);
            }
        }

        public void FinalizaPregaoDoLeilaoComId(int id)
        {
            _defaultService.FinalizaPregaoDoLeilaoComId(id);
        }

        public void IniciaPregaoDoLeilaoComId(int id)
        {
            _defaultService.IniciaPregaoDoLeilaoComId(id);
        }

        public IEnumerable<Categoria> ConsultaCategorias()
        {
            return _defaultService.ConsultaCategorias();
        }

        public IEnumerable<Leilao> ConsultaLeiloes()
        {
            return _defaultService
                .ConsultaLeiloes()
                .Where(l => l.Situacao != SituacaoLeilao.Arquivado);
        }

        public Leilao ConsultaLeilaoPorId(int id)
        {
            return _defaultService.ConsultaLeilaoPorId(id);
        }
    }
}