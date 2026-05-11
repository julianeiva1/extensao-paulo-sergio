using System.ComponentModel.DataAnnotations;

namespace TodoListMvc.Models
{
    /// <summary>
    /// Modelo de Tarefa para o TODO List.
    /// Representa uma atividade individual que pode ter lembretes associados.
    /// </summary>
    public class Tarefa
    {
        /// <summary>
        /// Identificador único da tarefa.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Título descritivo da tarefa.
        /// Obrigatório, máximo 500 caracteres.
        /// </summary>
        [Required(ErrorMessage = "O título da tarefa é obrigatório")]
        [StringLength(500, ErrorMessage = "O título não pode ter mais de 500 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da tarefa.
        /// Opcional, máximo 2000 caracteres.
        /// </summary>
        [StringLength(2000, ErrorMessage = "A descrição não pode ter mais de 2000 caracteres")]
        public string? Descricao { get; set; }

        /// <summary>
        /// Data e hora de um lembrete opcional para a tarefa.
        /// Nullable porque é opcional.
        /// </summary>
        public DateTime? LembreteEm { get; set; }

        /// <summary>
        /// Indica se a tarefa foi concluída.
        /// Default: false (não concluída).
        /// </summary>
        public bool Concluida { get; set; } = false;

        /// <summary>
        /// Data e hora de criação da tarefa (UTC).
        /// Definida automaticamente ao criar.
        /// </summary>
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data e hora da última modificação (UTC).
        /// </summary>
        public DateTime DataModificacao { get; set; } = DateTime.UtcNow;
    }
}
