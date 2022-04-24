using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //pegar as notificações e transformar numa lista

            //como o metodo ObterNotificacoes não é assincrono
            //e eu estou num contexto assincrono, recebo ele com o Task.FromResult
            //para possibilitar a compatibilidade
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());

            notificacoes.ForEach(n => ViewData.ModelState.AddModelError(string.Empty, n.Mensagem));
            return View();
        }

    }
}
