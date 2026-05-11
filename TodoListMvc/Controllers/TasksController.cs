using Microsoft.AspNetCore.Mvc;
using TodoListMvc.Models;
using TodoListMvc.Services;

namespace TodoListMvc.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de tarefas.
    /// Responsável por todas as ações relacionadas a tarefas e lembretes.
    /// </summary>
    public class TasksController : Controller
    {
        private readonly RepositorioTarefas _repositorio;
        private readonly ILogger<TasksController> _logger;

        public TasksController(RepositorioTarefas repositorio, ILogger<TasksController> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        /// <summary>
        /// GET: /tasks
        /// Exibe a lista de todas as tarefas cadastradas.
        /// </summary>
        public IActionResult Index()
        {
            var tarefas = _repositorio.ObterTodas();
            return View(tarefas);
        }

        /// <summary>
        /// GET: /tasks/create
        /// Exibe o formulário para criar nova tarefa.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /tasks/create
        /// Cria uma nova tarefa com validação.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Titulo,Descricao,LembreteEm")] Tarefa tarefa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio.Criar(tarefa.Titulo, tarefa.Descricao, tarefa.LembreteEm);
                    _logger.LogInformation("Tarefa criada com sucesso");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Titulo", ex.Message);
                _logger.LogWarning("Erro ao criar tarefa: {Mensagem}", ex.Message);
            }

            return View(tarefa);
        }

        /// <summary>
        /// GET: /tasks/{id}/edit
        /// Exibe o formulário para editar uma tarefa existente.
        /// </summary>
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var tarefa = _repositorio.ObterPorId(id.Value);
            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        /// <summary>
        /// POST: /tasks/{id}/edit
        /// Atualiza uma tarefa existente.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Titulo,Descricao,LembreteEm")] Tarefa tarefa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sucesso = _repositorio.Atualizar(id, tarefa.Titulo, tarefa.Descricao, tarefa.LembreteEm);
                    if (sucesso)
                    {
                        _logger.LogInformation("Tarefa {Id} atualizada com sucesso", id);
                        return RedirectToAction(nameof(Index));
                    }
                    return NotFound();
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Titulo", ex.Message);
                _logger.LogWarning("Erro ao atualizar tarefa: {Mensagem}", ex.Message);
            }

            return View(tarefa);
        }

        /// <summary>
        /// POST: /tasks/{id}/delete
        /// Remove uma tarefa.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var sucesso = _repositorio.Remover(id);
            if (sucesso)
            {
                _logger.LogInformation("Tarefa {Id} removida com sucesso", id);
                TempData["Sucesso"] = "Tarefa removida com sucesso!";
            }
            else
            {
                _logger.LogWarning("Tentativa de remover tarefa {Id} não encontrada", id);
                TempData["Erro"] = "Tarefa não encontrada.";
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /tasks/{id}/toggle
        /// Alterna o estado de conclusão de uma tarefa.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleComplete(Guid id)
        {
            var sucesso = _repositorio.AlternarConclusao(id);
            if (!sucesso)
            {
                _logger.LogWarning("Tarefa {Id} não encontrada para alternação", id);
                TempData["Erro"] = "Tarefa não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            _logger.LogInformation("Conclusão da tarefa {Id} alternada", id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /tasks/{id}/toggle (antigo)
        /// Mantém compatibilidade com nomes antigos.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AlternarConclusao(Guid id)
        {
            return ToggleComplete(id);
        }
    }
}
