# Data Model: TODO List

**Feature**: Gerenciamento de Tarefas em TODO List  
**Date**: 2026-05-10  
**Status**: Design Complete

## Overview

O modelo de dados para TODO List é simples e direto, focando em uma única entidade principal (`Task`) com atributos mínimos necessários para cobrir todos os 5 user stories.

---

## Entity: Task (Tarefa)

### Definition

Uma `Task` representa uma atividade que um usuário deseja acompanhar. Cada tarefa possui um título, um estado de conclusão, e timestamps para auditoria.

### Attributes

| Atributo | Tipo | Obrigatório | Constraints | Descrição |
|----------|------|------------|-------------|-----------|
| `Id` | Guid | Sim | Primary Key, Generated | Identificador único da tarefa |
| `Title` | string | Sim | NOT NULL, Max 500 chars, Trimmed | Título/descrição da tarefa |
| `IsCompleted` | bool | Sim | Default: false | Estado de conclusão (true = concluída, false = pendente) |
| `DateCreated` | DateTime | Sim | Default: now (UTC), Immutable | Data/hora de criação (UTC) |
| `DateModified` | DateTime | Sim | Default: now (UTC), Auto-update | Data/hora da última modificação (UTC) |

### Entity Diagram

```
┌─────────────────────────────────┐
│           Task                  │
├─────────────────────────────────┤
│ Id (Guid) ← PK                  │
│ Title (string, max 500)         │
│ IsCompleted (bool)              │
│ DateCreated (DateTime, UTC)     │
│ DateModified (DateTime, UTC)    │
└─────────────────────────────────┘
```

### State Diagram (Task Lifecycle)

```
[Created: IsCompleted = false]
         ↓
    [Pending]
    ↙       ↘
[Complete] [Edit]
    ↘       ↙
    [Pending]
         ↓
    [Deleted/Removed]
```

---

## Validation Rules

### Title Validation

1. **Required**: Não pode ser null ou vazio
2. **Whitespace-Only Rejection**: Se contem apenas espaços/tabs/newlines, é rejeitado
3. **Auto-Trim**: Espaços ao início/fim são removidos automaticamente
4. **Max Length**: Máximo 500 caracteres (após trim)
5. **Character Set**: Qualquer caractere Unicode é permitido (exceto null byte)

**Validation Logic**:
```
IF Title == null OR Title.Length == 0 THEN error
IF Title.Trim().Length == 0 THEN error (espaços em branco)
IF Title.Length > 500 THEN error
ELSE Title = Title.Trim() AND valid
```

**Error Message** (em português):
- "O título da tarefa não pode ser vazio"
- "O título da tarefa deve ter no máximo 500 caracteres"

### IsCompleted Validation

1. **Binary**: Apenas true ou false
2. **Toggle**: Pode alternar entre states livremente
3. **No Constraints**: Não há validações quanto a título ou data

**Toggle Logic**:
- true → false: "Desmarcar como concluída"
- false → true: "Marcar como concluída"

### DateCreated & DateModified

1. **UTC Only**: Sempre em UTC, nunca em horário local
2. **Immutable (DateCreated)**: Não pode ser alterado após criação
3. **Auto-Update (DateModified)**: Atualiza em qualquer mudança (Title, IsCompleted)

**Update Logic**:
```
ON CREATE: DateCreated = NOW_UTC, DateModified = NOW_UTC
ON UPDATE: DateModified = NOW_UTC (DateCreated unchanged)
```

---

## Relationships

### Current (v1)

**None**: Task existe independentemente, sem relacionamentos com outras entidades.

Razão: MVP simples, single-user por sessão, sem categorias/tags/projetos.

### Future (v2+) - Design Considerations

Espaço reservado para futuras extensões:

```csharp
// Placeholder para v2 (single-user → multi-user)
public class Task
{
    // ... existing fields ...
    
    // Future: Multi-user support
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    // ...
    public ICollection<Task> Tasks { get; set; }
}
```

---

## Database Schema

### SQL Schema (SQL Server / PostgreSQL / SQLite)

```sql
CREATE TABLE Tasks (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title VARCHAR(500) NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    DateCreated DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    DateModified DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    
    -- Constraints
    CHECK (LEN(TRIM(Title)) > 0),
    CHECK (LEN(Title) <= 500)
);

-- Índices
CREATE INDEX IX_Tasks_DateCreated ON Tasks(DateCreated DESC);
```

### Entity Framework Core Mapping (C#)

```csharp
public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .HasDefaultValueSql("NEWID()");
        
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(500)
            .HasConversion(v => v.Trim(), v => v); // Auto-trim
        
        builder.Property(t => t.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.Property(t => t.DateCreated)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
        
        builder.Property(t => t.DateModified)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();
        
        builder.HasIndex(t => t.DateCreated)
            .IsDescending();
    }
}
```

---

## Mapping to User Stories

| User Story | Entidades Envolvidas | Atributos Usados |
|-----------|----------------------|------------------|
| **US1: Visualizar Lista** | Task | Id, Title, IsCompleted, DateCreated |
| **US2: Criar Tarefa** | Task | Title (input), IsCompleted (default false), DateCreated (auto) |
| **US3: Marcar Concluída** | Task | IsCompleted (toggle), DateModified (auto-update) |
| **US4: Editar Título** | Task | Title (input), DateModified (auto-update) |
| **US5: Remover Tarefa** | Task | Id (delete) |

---

## Performance Considerations

### Índices

1. **IX_Tasks_DateCreated** (DESC)
   - Suporta ordenação padrão (FIFO desc)
   - Usado em `SELECT * FROM Tasks ORDER BY DateCreated DESC`

### Query Optimization

**GET List** (US1):
```sql
SELECT Id, Title, IsCompleted, DateCreated 
FROM Tasks 
ORDER BY DateCreated DESC
-- Performance: O(n) com índice, típico < 100ms para 1000 tasks
```

**POST Create** (US2):
```sql
INSERT INTO Tasks (Title, IsCompleted) 
VALUES (@title, 0)
-- Performance: O(1), < 50ms
```

**UPDATE Complete** (US3):
```sql
UPDATE Tasks 
SET IsCompleted = ~IsCompleted, DateModified = GETUTCDATE() 
WHERE Id = @id
-- Performance: O(1), < 50ms
```

**UPDATE Title** (US4):
```sql
UPDATE Tasks 
SET Title = TRIM(@title), DateModified = GETUTCDATE() 
WHERE Id = @id
-- Performance: O(1), < 50ms
```

**DELETE** (US5):
```sql
DELETE FROM Tasks WHERE Id = @id
-- Performance: O(1), < 50ms
```

---

## Assumptions & Constraints

### Assumptions

1. **Single-User Session**: Cada sessão é um usuário anônimo (sem autenticação)
2. **Persistent Storage**: Dados são persistidos em banco (não apenas em memória)
3. **UTC Time**: Sistema usa UTC internamente (frontend converte para local conforme necessário)
4. **300 tarefas típicas**: Assume-se que usuário típico tem < 300 tarefas (testes até 1000)

### Constraints

1. **Sem Soft Deletes (v1)**: Quando deletado, é removido do banco (não marcado como deleted)
2. **Sem Auditoria Completa**: Só DateCreated/Modified são auditadas (sem quem criou/modificou)
3. **Sem Versionamento**: Não é possível ver histórico de mudanças de título

---

## Testing Strategy

### Unit Tests (Validações)

```csharp
[TestClass]
public class TaskValidationTests
{
    [TestMethod]
    public void Title_Empty_ThrowsException()
    {
        var task = new Task { Title = "" };
        Assert.ThrowsException<ValidationException>(() => ValidateTask(task));
    }
    
    [TestMethod]
    public void Title_WhitespaceOnly_ThrowsException()
    {
        var task = new Task { Title = "   " };
        Assert.ThrowsException<ValidationException>(() => ValidateTask(task));
    }
    
    [TestMethod]
    public void Title_500Chars_Accepted()
    {
        var task = new Task { Title = new string('a', 500) };
        ValidateTask(task); // Should not throw
    }
    
    [TestMethod]
    public void Title_501Chars_ThrowsException()
    {
        var task = new Task { Title = new string('a', 501) };
        Assert.ThrowsException<ValidationException>(() => ValidateTask(task));
    }
    
    [TestMethod]
    public void IsCompleted_Toggle_Works()
    {
        var task = new Task { IsCompleted = false };
        task.IsCompleted = true;
        Assert.IsTrue(task.IsCompleted);
    }
}
```

### Integration Tests (EF Core)

```csharp
[TestClass]
public class TaskRepositoryTests
{
    [TestMethod]
    public async Task GetTasks_ReturnsOrderedByDateDesc()
    {
        // Arrange: Criar 3 tarefas
        var task1 = CreateTask("Task 1");
        var task2 = CreateTask("Task 2");
        var task3 = CreateTask("Task 3");
        
        // Act
        var tasks = await _context.Tasks.OrderByDescending(t => t.DateCreated).ToListAsync();
        
        // Assert: Order should be 3, 2, 1 (reverse)
        Assert.AreEqual(task3.Id, tasks[0].Id);
        Assert.AreEqual(task2.Id, tasks[1].Id);
        Assert.AreEqual(task1.Id, tasks[2].Id);
    }
}
```

