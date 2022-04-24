using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DevIO.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class ApagaElementoByClaimTagHelper : TagHelper
    {
        //meio de acessar o contexto view HTTP
        //Pode ser usado em qualquer lugar da aplicação
        //para obter o usuário logado
        private readonly IHttpContextAccessor _contextAccessor;

        public ApagaElementoByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //o output é o que a TagHelper vai produzir de conteúdo para a view
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimValue, IdentityClaimValue);

            if (temAcesso) return;

            output.SuppressOutput(); // não vai gerar o elemento na view
        }
    }

    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]

    public class DesabilitaLinkByClaimTagHelper : TagHelper
    {
        //meio de acessar o contexto view HTTP
        //Pode ser usado em qualquer lugar da aplicação
        //para obter o usuário logado
        private readonly IHttpContextAccessor _contextAccessor;

        public DesabilitaLinkByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //o output é o que a TagHelper vai produzir de conteúdo para a view
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (temAcesso) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão"));

        }
    }

    [HtmlTargetElement("*", Attributes ="supress-by-action")]
    public class ApagaElementoByActionTagHelper : TagHelper
    {
        //meio de acessar o contexto view HTTP
        //Pode ser usado em qualquer lugar da aplicação
        //para obter o usuário logado
        private readonly IHttpContextAccessor _contextAccessor;

        public ApagaElementoByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //o output é o que a TagHelper vai produzir de conteúdo para a view
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();
            //Se o elemento que está referenciando essa tag 'supress-by-action'
            // não estiver sendo renderizado dentro da Action passada
            // no parâmetro do 'supress-by-action' então não renderize
            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }
}
