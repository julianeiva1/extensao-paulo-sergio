# Quickstart: TODO List Development

**Feature**: Gerenciamento de Tarefas em TODO List  
**Date**: 2026-05-10  
**Stack**: React 18 + ASP.NET Core Web API

## Prerequisites

### Backend (C# / ASP.NET Core)
- .NET 7 SDK ou superior [Download](https://dotnet.microsoft.com/download)
- SQL Server Express, PostgreSQL, ou SQLite (recomendado SQLite para dev local)
- Visual Studio 2022 Community ou VS Code com C# extension

### Frontend (React)
- Node.js 16+ [Download](https://nodejs.org)
- npm 8+ ou yarn
- Visual Studio Code ou outro editor

---

## Backend Setup

### 1. Criar Projeto ASP.NET Core

```bash
# Navegar até diretório raiz do projeto
cd "c:\Users\julia\OneDrive\IFES\Documentos IFES\Extensão"

# Criar solução
dotnet new globaljson --sdk-version 7.0 --roll-forward latestFeature
dotnet new sln -n IFES.Extensao

# Criar projeto Web API
dotnet new webapi -n IFES.Extensao.API -o backend

# Adicionar à solução
dotnet sln IFES.Extensao.sln add backend/IFES.Extensao.API.csproj
```

### 2. Instalar Dependências

```bash
cd backend

# Entity Framework Core (ORM)
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite  # Para dev local
# OU
# dotnet add package Microsoft.EntityFrameworkCore.SqlServer  # Para SQL Server
# dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL  # Para PostgreSQL

# Tools para migrations
dotnet add package Microsoft.EntityFrameworkCore.Tools

# CORS support (já incluído no template webapi)

# Testes
dotnet add package Microsoft.VisualStudio.TestPlatform
dotnet add package xunit
dotnet add package Moq
```

### 3. Criar Models

**File**: `backend/Models/Task.cs`
```csharp
using System;
using System.ComponentModel.DataAnnotations;

namespace IFES.Extensao.API.Models
{
    public class Task
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(500, ErrorMessage = "O título deve ter no máximo 500 caracteres")]
        public string Title { get; set; } = null!;

        public bool IsCompleted { get; set; } = false;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateModified { get; set; } = DateTime.UtcNow;
    }
}
```

**File**: `backend/Models/CreateTaskRequest.cs`
```csharp
using System.ComponentModel.DataAnnotations;

namespace IFES.Extensao.API.Models
{
    public class CreateTaskRequest
    {
        [Required]
        [StringLength(500)]
        public string Title { get; set; } = null!;
    }
}
```

### 4. Criar Entity Framework Context

**File**: `backend/Data/AppDbContext.cs`
```csharp
using Microsoft.EntityFrameworkCore;
using IFES.Extensao.API.Models;

namespace IFES.Extensao.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Task> Tasks => Set<Task>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(500);
                entity.Property(e => e.IsCompleted).HasDefaultValue(false);
                entity.Property(e => e.DateCreated).HasDefaultValue(DateTime.UtcNow);
                entity.HasIndex(e => e.DateCreated).IsDescending();
            });
        }
    }
}
```

### 5. Criar Service (Lógica de Negócio)

**File**: `backend/Services/TaskService.cs`
```csharp
using IFES.Extensao.API.Data;
using IFES.Extensao.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IFES.Extensao.API.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Task>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .OrderByDescending(t => t.DateCreated)
                .ToListAsync();
        }

        public async Task<Task?> GetTaskByIdAsync(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Task> CreateTaskAsync(string title)
        {
            ValidateTitle(title);
            
            var task = new Task { Title = title.Trim() };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> UpdateTaskAsync(Guid id, string title)
        {
            var task = await GetTaskByIdAsync(id);
            if (task == null) throw new InvalidOperationException("Tarefa não encontrada");

            ValidateTitle(title);
            task.Title = title.Trim();
            task.DateModified = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> ToggleCompleteAsync(Guid id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task == null) throw new InvalidOperationException("Tarefa não encontrada");

            task.IsCompleted = !task.IsCompleted;
            task.DateModified = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task == null) throw new InvalidOperationException("Tarefa não encontrada");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("O título da tarefa não pode ser vazio");
            
            if (title.Length > 500)
                throw new ArgumentException("O título deve ter no máximo 500 caracteres");
        }
    }
}
```

### 6. Criar Controller

**File**: `backend/Controllers/TasksController.cs`
```csharp
using Microsoft.AspNetCore.Mvc;
using IFES.Extensao.API.Models;
using IFES.Extensao.API.Services;

namespace IFES.Extensao.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _service;

        public TasksController(TaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Task>>> GetTasks()
        {
            var tasks = await _service.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<Task>> CreateTask([FromBody] CreateTaskRequest request)
        {
            try
            {
                var task = await _service.CreateTaskAsync(request.Title);
                return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message, code = "TITLE_INVALID" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Task>> UpdateTask(Guid id, [FromBody] CreateTaskRequest request)
        {
            try
            {
                var task = await _service.UpdateTaskAsync(id, request.Title);
                return Ok(task);
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { error = "Tarefa não encontrada", code = "TASK_NOT_FOUND" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message, code = "TITLE_INVALID" });
            }
        }

        [HttpPatch("{id}/complete")]
        public async Task<ActionResult<Task>> CompleteTask(Guid id)
        {
            try
            {
                var task = await _service.ToggleCompleteAsync(id);
                return Ok(task);
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { error = "Tarefa não encontrada", code = "TASK_NOT_FOUND" });
            }
        }

        [HttpPatch("{id}/incomplete")]
        public async Task<ActionResult<Task>> IncompleteTask(Guid id)
        {
            // Reutilizar mesmo endpoint (toggle)
            return await CompleteTask(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                await _service.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { error = "Tarefa não encontrada", code = "TASK_NOT_FOUND" });
            }
        }
    }
}
```

### 7. Configurar Program.cs

**File**: `backend/Program.cs`
```csharp
using Microsoft.EntityFrameworkCore;
using IFES.Extensao.API.Data;
using IFES.Extensao.API.Services;

var builder = WebApplicationBuilder.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddScoped<TaskService>();

// Entity Framework (SQLite para dev)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("DevelopmentPolicy",
        policy => {
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Middleware
app.UseCors("DevelopmentPolicy");
app.MapControllers();

// Migrations automáticas (dev only)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.Run("http://localhost:5000");
```

### 8. Executar Backend

```bash
cd backend
dotnet run

# Output: info: Microsoft.Hosting.Lifetime[14] - Now listening on: http://localhost:5000
```

### 9. Testar Backend

```bash
# GET all tasks (empty inicialmente)
curl http://localhost:5000/api/v1/tasks

# POST create task
curl -X POST http://localhost:5000/api/v1/tasks \
  -H "Content-Type: application/json" \
  -d '{"title":"Teste de tarefa"}'

# GET again to see created task
curl http://localhost:5000/api/v1/tasks
```

---

## Frontend Setup

### 1. Criar Projeto React

```bash
cd frontend
npm create vite@latest . -- --template react-ts
npm install
```

### 2. Instalar Dependências

```bash
npm install axios
npm install --save-dev @testing-library/react @testing-library/jest-dom vitest
```

### 3. Criar API Service

**File**: `frontend/src/services/taskService.ts`
```typescript
import axios from "axios";

const API_BASE = "http://localhost:5000/api/v1";

export interface Task {
  id: string;
  title: string;
  isCompleted: boolean;
  dateCreated: string;
}

const api = axios.create({
  baseURL: API_BASE,
  headers: { "Content-Type": "application/json" }
});

export const taskService = {
  async getAllTasks(): Promise<Task[]> {
    const response = await api.get("/tasks");
    return response.data;
  },

  async createTask(title: string): Promise<Task> {
    const response = await api.post("/tasks", { title });
    return response.data;
  },

  async updateTask(id: string, title: string): Promise<Task> {
    const response = await api.put(`/tasks/${id}`, { title });
    return response.data;
  },

  async completeTask(id: string): Promise<Task> {
    const response = await api.patch(`/tasks/${id}/complete`);
    return response.data;
  },

  async incompleteTask(id: string): Promise<Task> {
    const response = await api.patch(`/tasks/${id}/incomplete`);
    return response.data;
  },

  async deleteTask(id: string): Promise<void> {
    await api.delete(`/tasks/${id}`);
  }
};
```

### 4. Criar Componentes React

**File**: `frontend/src/components/TaskList.tsx`
```typescript
import React, { useEffect, useState } from "react";
import { Task, taskService } from "../services/taskService";
import TaskItem from "./TaskItem";
import TaskForm from "./TaskForm";

export default function TaskList() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    loadTasks();
  }, []);

  const loadTasks = async () => {
    try {
      const data = await taskService.getAllTasks();
      setTasks(data);
      setError(null);
    } catch (err) {
      setError("Erro ao carregar tarefas");
    } finally {
      setLoading(false);
    }
  };

  const handleAddTask = async (title: string) => {
    try {
      const newTask = await taskService.createTask(title);
      setTasks([newTask, ...tasks]); // Add to top
    } catch (err) {
      setError("Erro ao criar tarefa");
    }
  };

  const handleToggle = async (id: string) => {
    try {
      const updated = await taskService.completeTask(id);
      setTasks(tasks.map(t => t.id === id ? updated : t));
    } catch (err) {
      setError("Erro ao atualizar tarefa");
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await taskService.deleteTask(id);
      setTasks(tasks.filter(t => t.id !== id));
    } catch (err) {
      setError("Erro ao remover tarefa");
    }
  };

  if (loading) return <div>Carregando...</div>;

  return (
    <div className="todo-container">
      <h1>Minhas Tarefas</h1>
      {error && <div className="error">{error}</div>}
      
      <TaskForm onAdd={handleAddTask} />
      
      {tasks.length === 0 ? (
        <p className="no-tasks">Nenhuma tarefa cadastrada</p>
      ) : (
        <ul className="task-list">
          {tasks.map(task => (
            <TaskItem
              key={task.id}
              task={task}
              onToggle={handleToggle}
              onDelete={handleDelete}
            />
          ))}
        </ul>
      )}
    </div>
  );
}
```

### 5. Executar Frontend

```bash
cd frontend
npm run dev

# Output: VITE v4.x.x ready in X ms
# ➜  Local:   http://localhost:5173/
```

---

## End-to-End Testing

### 1. Certifique-se que Backend e Frontend estão rodando

```bash
# Terminal 1 (Backend)
cd backend && dotnet run
# http://localhost:5000

# Terminal 2 (Frontend)
cd frontend && npm run dev
# http://localhost:5173
```

### 2. Abra Browser em http://localhost:5173

### 3. Teste Fluxos

1. **Criar Tarefa**: Digite "Comprar leite" → Clique "Adicionar"
   - Esperado: Tarefa aparece no topo da lista como não concluída
   
2. **Listar Tarefas**: Recarregue página
   - Esperado: Tarefa persiste (foi salva no backend)
   
3. **Marcar Concluída**: Clique checkbox
   - Esperado: Tarefa aparece riscada/com cor diferente
   
4. **Editar**: Clique em tarefa (modo edit)
   - Esperado: Título atualizado na lista
   
5. **Remover**: Clique botão delete
   - Esperado: Tarefa desaparece da lista

---

## Troubleshooting

### Backend não conecta ao banco
```bash
cd backend
dotnet ef database drop
dotnet ef database update
dotnet run
```

### Frontend não consegue chamar Backend (CORS error)
- Verificar que Backend está em `http://localhost:5000`
- Verificar CORS policy em `Program.cs` inclui `http://localhost:3000` ou `5173`

### API retorna 404
- Verificar rota correta: `/api/v1/tasks` (não `/api/tasks`)
- Verificar que TasksController está em namespace `IFES.Extensao.API.Controllers`

---

## Next Steps

1. ✅ Backend rodando
2. ✅ Frontend rodando
3. ⏳ Adicionar testes automatizados (unit + integration)
4. ⏳ Melhorar UI/styling
5. ⏳ Testar em múltiplos navegadores e tamanhos de tela

