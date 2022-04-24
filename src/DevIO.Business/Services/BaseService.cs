using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                Notificar(erro.ErrorMessage);
            }
        }
        protected void Notificar(string mensagem)
        {
            //Propagar o erro até a camada de apresentação
            _notificador.Handle(new Notificacao(mensagem));
        }

        //TV = Entidade de validação, TE = entidade de dominio
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where  TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);
            if (validator.IsValid) return true;

            Notificar(validator);
            return false;
        }
    }
}
