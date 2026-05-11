using System.ComponentModel.DataAnnotations;

namespace TodoListMvc.Models
{
    /// <summary>
    /// Modelo de Lembrete associado a uma Tarefa.
    /// Permite adicionar notas/anotações às tarefas.
    /// </summary>
    public class Lembrete
    {
        /// <summary>
        /// Identificador único do lembrete.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Texto do lembrete (anotação).
        /// Texto livre, sem limite rígido.
        /// </summary>
        [Required(ErrorMessage = "O texto do lembrete é obrigatório")]
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora de criação do lembrete (UTC).
        /// </summary>
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
