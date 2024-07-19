using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        // variável para extrair e acessar o contatoRepositorio
        private readonly IContatoRepositorio _contatoRepositorio;  
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        // Métodos GET
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                }
                else 
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar seu contato!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro) 
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu contato, mais detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
            
        }

        // Métodos POST
        [HttpPost]
        public IActionResult Criar(ContatoModel contato) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index"); // Vai ser alterado depois pro padrão REST    
                }

                return View(contato);
            }
            catch (Exception erro) 
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index"); // Vai ser alterado depois pro padrão REST  
            }
        }
        // Métodos POST
        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index"); // Vai ser alterado depois pro padrão REST
                }
                return View("Editar", contato);
            }
            catch(Exception erro) 
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index"); // Vai ser alterado depois pro padrão REST  
            }

        }
    }
}
