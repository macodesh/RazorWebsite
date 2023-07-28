// Importa o tipo Pizza.
using ContosoPizza.Models;
// Importa PizzaService.
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        // Readonly indica que o valor não pode ser alterado após definido.
        private readonly PizzaService _service;
        // Essa propriedade contém uma lista de objetos do tipo Pizza.
        // PizzaList é inicializado para default! para indicar que ele conterá um valor posteriormente.
        // Verificações de nulidade não são necessárias.
        public IList<Pizza> PizzaList { get; set; } = default!;

        // "BindProperty" vincula a propriedade à página Razor.
        [BindProperty]
        // Quando uma requisição POST é feita, NewPizza é preenchida com as informações da pessoa usuária.
        public Pizza NewPizza { get; set; } = default!;

        // Construtor da classe.
        // Aceita um objeto de tipo PizzaService.
        // O objeto é fornecido pela injeção de dependência.
        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }

        // Esse método recupera a lista de pizzas e salva na propriedade PizzaList.
        // Ele é executado ao requisitar a página.
        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost()
        {
            // Determina se a entrada da pessoa usuária é válida.
            // As regras de validação são inferidas de atributos da classe Pizza em Models/Pizza.cs.
            if (!ModelState.IsValid || NewPizza == null)
            {
                // Retorna a página caso a entrada da pessoa usuária seja inválida.
                return Page();
            }

            // Adiciona uma nova pizza ao objeto _service.
            _service.AddPizza(NewPizza);

            // Redireciona a pessoa usuária para o manipulador Get, que renderizará novamente a página.
            return RedirectToAction("Get");
        }

        // Esse método é chamado quando o botão "Excluir" é clicado.
        public IActionResult OnPostDelete(int id)
        {
            // O id é usado para identificar a pizza que será deletada.
            // Está associado ao valor da rota id na URL.
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
