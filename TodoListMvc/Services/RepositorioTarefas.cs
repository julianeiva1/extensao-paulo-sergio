using TodoListMvc.Models;

namespace TodoListMvc.Services
{
    /// <summary>
    /// Repositório em memória para gerenciamento de tarefas.
    /// Armazena todas as tarefas em List<Tarefa> durante a execução da aplicação.
    /// Registrado como Singleton no DI container.
    /// 
    /// NOTA: Dados são perdidos ao reiniciar a aplicação (conforme requisito do enunciado).
    /// </summary>
    public class RepositorioTarefas
    {
        private readonly List<Tarefa> _tarefas = new();
        private readonly object _lock = new object();

        /// <summary>
        /// Obtém todas as tarefas cadastradas.
        /// Retorna cópia ordenada pela data de criação (mais recentes primeiro).
        /// </summary>
        public List<Tarefa> ObterTodas()
        {
            lock (_lock)
            {
                return _tarefas
                    .OrderByDescending(t => t.DataCriacao)
                    .ToList();
            }
        }

        /// <summary>
        /// Obtém uma tarefa específica pelo ID.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <returns>Tarefa encontrada ou null</returns>
        public Tarefa? ObterPorId(Guid id)
        {
            lock (_lock)
            {
                return _tarefas.FirstOrDefault(t => t.Id == id);
            }
        }

        /// <summary>
        /// Cria uma nova tarefa com validação do título.
        /// </summary>
        /// <param name="titulo">Título da tarefa</param>
        /// <param name="descricao">Descrição opcional da tarefa</param>
        /// <param name="lembreteEm">Data/hora opcional do lembrete</param>
        /// <returns>Tarefa criada</returns>
        /// <exception cref="ArgumentException">Se título inválido</exception>
        public Tarefa Criar(string titulo, string? descricao = null, DateTime? lembreteEm = null)
        {
            ValidarTitulo(titulo);

            var tarefa = new Tarefa
            {
                Id = Guid.NewGuid(),
                Titulo = titulo.Trim(),
                Descricao = string.IsNullOrWhiteSpace(descricao) ? null : descricao.Trim(),
                LembreteEm = lembreteEm,
                Concluida = false,
                DataCriacao = DateTime.UtcNow,
                DataModificacao = DateTime.UtcNow,
                Lembretes = new()
            };

            lock (_lock)
            {
                _tarefas.Add(tarefa);
            }

            return tarefa;
        }

        /// <summary>
        /// Atualiza os dados de uma tarefa existente.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <param name="novoTitulo">Novo título</param>
        /// <param name="novaDescricao">Nova descrição (opcional)</param>
        /// <param name="novoLembreteEm">Novo lembrete (opcional)</param>
        /// <returns>true se atualizada, false se não encontrada</returns>
        /// <exception cref="ArgumentException">Se título inválido</exception>
        public bool Atualizar(Guid id, string novoTitulo, string? novaDescricao = null, DateTime? novoLembreteEm = null)
        {
            ValidarTitulo(novoTitulo);

            lock (_lock)
            {
                var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
                if (tarefa == null)
                    return false;

                tarefa.Titulo = novoTitulo.Trim();
                tarefa.Descricao = string.IsNullOrWhiteSpace(novaDescricao) ? null : novaDescricao.Trim();
                tarefa.LembreteEm = novoLembreteEm;
                tarefa.DataModificacao = DateTime.UtcNow;
                return true;
            }
        }

        /// <summary>
        /// Alterna o estado de conclusão de uma tarefa.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <returns>true se alternado, false se não encontrada</returns>
        public bool AlternarConclusao(Guid id)
        {
            lock (_lock)
            {
                var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
                if (tarefa == null)
                    return false;

                tarefa.Concluida = !tarefa.Concluida;
                tarefa.DataModificacao = DateTime.UtcNow;
                return true;
            }
        }

        /// <summary>
        /// Remove uma tarefa e todos os seus lembretes.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <returns>true se removida, false se não encontrada</returns>
        public bool Remover(Guid id)
        {
            lock (_lock)
            {
                var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
                if (tarefa == null)
                    return false;

                _tarefas.Remove(tarefa);
                return true;
            }
        }

        /// <summary>
        /// Adiciona um lembrete a uma tarefa.
        /// </summary>
        /// <param name="tarefaId">ID da tarefa</param>
        /// <param name="texto">Texto do lembrete</param>
        /// <returns>Lembrete criado</returns>
        /// <exception cref="ArgumentException">Se tarefa não encontrada ou texto inválido</exception>
        public Lembrete AdicionarLembrete(Guid tarefaId, string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("O texto do lembrete não pode ser vazio");

            lock (_lock)
            {
                var tarefa = _tarefas.FirstOrDefault(t => t.Id == tarefaId);
                if (tarefa == null)
                    throw new ArgumentException("Tarefa não encontrada");

                var lembrete = new Lembrete
                {
                    Id = Guid.NewGuid(),
                    Texto = texto.Trim(),
                    DataCriacao = DateTime.UtcNow
                };

                tarefa.Lembretes.Add(lembrete);
                tarefa.DataModificacao = DateTime.UtcNow;
                return lembrete;
            }
        }

        /// <summary>
        /// Remove um lembrete de uma tarefa.
        /// </summary>
        /// <param name="tarefaId">ID da tarefa</param>
        /// <param name="lembreteId">ID do lembrete</param>
        /// <returns>true se removido, false se não encontrado</returns>
        public bool RemoverLembrete(Guid tarefaId, Guid lembreteId)
        {
            lock (_lock)
            {
                var tarefa = _tarefas.FirstOrDefault(t => t.Id == tarefaId);
                if (tarefa == null)
                    return false;

                var lembrete = tarefa.Lembretes.FirstOrDefault(l => l.Id == lembreteId);
                if (lembrete == null)
                    return false;

                tarefa.Lembretes.Remove(lembrete);
                tarefa.DataModificacao = DateTime.UtcNow;
                return true;
            }
        }

        /// <summary>
        /// Atualiza o texto de um lembrete existente.
        /// </summary>
        /// <param name="tarefaId">ID da tarefa</param>
        /// <param name="lembreteId">ID do lembrete</param>
        /// <param name="novoTexto">Novo texto</param>
        /// <returns>true se atualizado, false se não encontrado</returns>
        public bool AtualizarLembrete(Guid tarefaId, Guid lembreteId, string novoTexto)
        {
            if (string.IsNullOrWhiteSpace(novoTexto))
                return false;

            lock (_lock)
            {
                var tarefa = _tarefas.FirstOrDefault(t => t.Id == tarefaId);
                if (tarefa == null)
                    return false;

                var lembrete = tarefa.Lembretes.FirstOrDefault(l => l.Id == lembreteId);
                if (lembrete == null)
                    return false;

                lembrete.Texto = novoTexto.Trim();
                tarefa.DataModificacao = DateTime.UtcNow;
                return true;
            }
        }

        /// <summary>
        /// Valida o título de uma tarefa.
        /// </summary>
        /// <param name="titulo">Título a validar</param>
        /// <exception cref="ArgumentException">Se título inválido</exception>
        private static void ValidarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título da tarefa não pode ser vazio");

            if (titulo.Length > 500)
                throw new ArgumentException("O título não pode ter mais de 500 caracteres");
        }
    }
}
