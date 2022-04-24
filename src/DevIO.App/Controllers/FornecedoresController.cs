#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevIO.App.Data;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using DevIO.App.Extensions;

namespace DevIO.App.Controllers
{
    [Authorize]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
            IMapper mapper,
            IFornecedorService fornecedorService,
            INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        // GET: Fornecedores
        [Route("lista-de-fornecedores")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return View(fornecedores);
        }

        // GET: Fornecedores/Details/5
        [Route("dados-do-fornecedor/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NoContent();
            }

            return View(fornecedorViewModel);
        }

        // GET: Fornecedores/Create
        [Route("novo-fornecedor")]
        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("novo-fornecedor")]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid)
                return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar(fornecedor);

            if (!OperacaoValida()) return View(fornecedorViewModel);

            return RedirectToAction("Index");
        }

        // GET: Fornecedores/Edit/5
        [Route("editar-fornecedor/{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Editar")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NoContent();
            }
            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NoContent();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);

            if (!OperacaoValida()) return View(fornecedorViewModel);

            return RedirectToAction("Index");
        }

        // GET: Fornecedores/Delete/5
        [Route("excluir-fornecedor/{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Excluir")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NoContent();
            }

            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir-fornecedor/{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Excluir")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null) return NoContent();

            await _fornecedorService.Remover(id);

            if (!OperacaoValida()) return View(fornecedorViewModel);

            return RedirectToAction("Index");
        }

        [Route("obter-endereco-fornecedor/{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEndereco", fornecedor );
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Editar")]
        public async Task<ActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if(fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Editar")]
        public async Task<ActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", fornecedorViewModel);
            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));

            if (!OperacaoValida()) return PartialView("_AtualizarEndereco", fornecedorViewModel);

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.Id });

            return Json(new { success = true, url });
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid fornecedorId)
        {
            var endereco = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEnderedo(fornecedorId));
            return endereco;
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid fornecedorId)
        {
            var fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(fornecedorId));
            return fornecedor;
        }
    }
}
