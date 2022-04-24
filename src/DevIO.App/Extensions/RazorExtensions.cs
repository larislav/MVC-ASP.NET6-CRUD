using Microsoft.AspNetCore.Mvc.Razor;

namespace DevIO.App.Extensions
{
    //metodos de extensao: classe estática
    public static class RazorExtensions
    {
        public static string FormatarDocumento(this RazorPage page, int tipoPessoa, string documento)
        {
            return tipoPessoa == 1 ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
