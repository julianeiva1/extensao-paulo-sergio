# Plano de Implementação: TODO List MVC

**Data**: 2026-05-10  
**Status**: Implementado ✅
**Arquitetura**: ASP.NET Core 8 MVC  
**Storage**: List<Tarefa> em memória

## Decisões Arquiteturais

### Por que MVC (não API+Frontend separado)?

| Aspecto | API+Frontend | MVC | Escolha |
|---------|-------------|-----|---------|
| **Complexidade** | Alta (2 projetos) | Baixa (1 projeto) | ✅ MVC |
| **Deploy** | Complexo | Simples | ✅ MVC |
| **Tempo Dev** | Longo | Curto | ✅ MVC |
| **Conhecimento** | Frontend + Backend | Só Backend | ✅ MVC |
| **Adequação** | Overkill | Perfeita | ✅ MVC |

**Conclusão**: Enunciado pede simplicidade, MVC atende 100% dos requisitos

### Por que Armazenamento em Memória (não BD)?

| Aspecto | Banco de Dados | Memória | Escolha |
|---------|---------------|---------|---------|
| **Requisito** | ❌ Viola "sem persistência" | ✅ Atende | ✅ Memória |
| **Complexidade** | Alta (migrations, SQL) | Baixa (List<T>) | ✅ Memória |
| **Performance** | Lenta (disco) | Rápida (RAM) | ✅ Memória |
| **Simplicidade** | ❌ | ✅ | ✅ Memória |

**Conclusão**: Requisito explícito do enunciado

## Stack Técnico

```
┌─────────────────────────────────────────┐
│    ASP.NET Core 8 MVC                   │
│    ├── Controllers (C# puro)            │
│    ├── Models (Tarefa, Lembrete)        │
│    ├── Views (Razor Templates)          │
│    ├── Services (RepositorioTarefas)    │
│    └── Program.cs (Dependency Injection)│
│                                         │
│    Bootstrap 5 (CSS via CDN)            │
│    List<Tarefa> (In-Memory Storage)     │
└─────────────────────────────────────────┘
```

## Arquitetura da Aplicação

```
┌──────────────────────────────────────────────────┐
│          Usuário (Browser)                       │
└─────────────┬──────────────────────────────────┘
              │ HTTP GET/POST
              ▼
┌──────────────────────────────────────────────────┐
│        TasksController                           │
│  ├── Index()           → Lista tarefas           │
│  ├── Create(GET/POST)  → Criar tarefa            │
│  ├── Edit(GET/POST)    → Editar tarefa           │
│  ├── Delete()          → Remover tarefa          │
│  ├── ToggleComplete()  → Marcar concluída        │
│  └── AdicionarLembrete() → Lembrete             │
└──────────────────┬───────────────────────────────┘
                   │ Dependency Injection
                   ▼
┌──────────────────────────────────────────────────┐
│     RepositorioTarefas (Singleton)               │
│     Private List<Tarefa> _tarefas                │
│  ├── Criar(titulo)                              │
│  ├── ObterTodas()                               │
│  ├── Atualizar(id, titulo)                      │
│  ├── Remover(id)                                │
│  ├── AlternarConclusao(id)                      │
│  ├── AdicionarLembrete(tarefaId, texto)         │
│  └── RemoverLembrete(tarefaId, lembreteId)      │
└──────────────────────────────────────────────────┘
                   │
                   ▼
        Models: Tarefa + Lembrete
           (C# POCO Classes)
```

## Estrutura de Pastas

```
TodoListMvc/
├── Controllers/
│   ├── HomeController.cs         (Opcional, não usado)
│   └── TasksController.cs         ⭐ Principal
│
├── Models/
│   ├── Tarefa.cs                 ⭐ Entidade
│   └── Lembrete.cs               ⭐ Sub-entidade
│
├── Services/
│   └── RepositorioTarefas.cs      ⭐ Business Logic
│
├── Views/
│   ├── Tasks/
│   │   ├── Index.cshtml           ⭐ Lista
│   │   ├── Create.cshtml          ⭐ Criar
│   │   └── Edit.cshtml            ⭐ Editar
│   ├── Shared/
│   │   ├── _Layout.cshtml         ⭐ Template
│   │   ├── _ValidationScriptsPartial.cshtml
│   │   └── Error.cshtml
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
│
├── wwwroot/
│   ├── css/
│   │   ├── site.css               ⭐ Customizado
│   │   └── site.css.map
│   ├── js/
│   │   └── site.js
│   └── lib/
│       ├── bootstrap/
│       ├── jquery/
│       └── jquery-validation/
│
├── Properties/
│   └── launchSettings.json
│
├── Program.cs                     ⭐ Entry point + DI
├── appsettings.json               
├── appsettings.Development.json   
└── TodoListMvc.csproj             
```

## Fluxo de Dados

### Criar Tarefa
```
1. Usuário preenche formulário Create.cshtml
2. POST /tasks/create → TasksController.Create(Tarefa)
3. Validação server-side (ModelState)
4. RepositorioTarefas.Criar(titulo)
   ├── ValidarTitulo(titulo)
   ├── Gerar novo Guid
   ├── Adicionar à List<Tarefa>
   └── Retornar Tarefa criada
5. Redirect("Index")
6. GET /tasks → TasksController.Index()
   └── Return View(_repositorio.ObterTodas())
7. Index.cshtml renderiza com nova tarefa
```

### Remover Tarefa
```
1. Usuário clica botão "Deletar" em Index.cshtml
2. Confirmação JavaScript
3. POST /tasks/{id}/delete → TasksController.Delete(Guid id)
4. RepositorioTarefas.Remover(id)
   ├── Encontrar Tarefa por id
   ├── Remover da List<Tarefa>
   └── Retornar sucesso
5. Redirect("Index")
6. Nova lista renderizada sem tarefa
```

### Adicionar Lembrete
```
1. Usuário digita texto em "Novo lembrete" (Index.cshtml)
2. POST /tasks/{tarefaId}/reminder → TasksController.AdicionarLembrete(tarefaId, texto)
3. RepositorioTarefas.AdicionarLembrete(tarefaId, texto)
   ├── Validar texto
   ├── Encontrar Tarefa
   ├── Criar novo Lembrete(Guid, texto, DataCriacao)
   ├── Adicionar à tarefa.Lembretes
   └── Retornar Lembrete
4. Redirect("Index")
5. Tarefa exibe novo lembrete em badge
```

## Validações Implementadas

### Validação de Tarefa
- ✅ Título obrigatório
- ✅ Título não pode ser vazio
- ✅ Título não pode ter só espaços
- ✅ Máximo 500 caracteres
- ✅ Trim automático

### Validação de Lembrete
- ✅ Texto obrigatório
- ✅ Texto não pode ser vazio
- ✅ Texto não pode ter só espaços
- ✅ Trim automático

### Thread Safety
- ✅ Uso de `lock` no repositório
- ✅ Thread-safe para múltiplas requisições simultâneas

## Dependency Injection

No `Program.cs`:
```csharp
builder.Services.AddSingleton<RepositorioTarefas>();
```

- **Singleton**: Uma única instância durante toda a vida da aplicação
- **Vantagem**: Todos os controllers compartilham mesmas tarefas
- **Consequência**: Dados perdidos ao reiniciar app (conforme requisito)

## Roteamento

Default em `Program.cs`:
```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");
```

Rotas disponíveis:
- `GET /` → TasksController.Index()
- `GET /tasks` → TasksController.Index()
- `GET /tasks/create` → TasksController.Create()
- `POST /tasks/create` → TasksController.Create(model)
- `GET /tasks/{id}/edit` → TasksController.Edit(id)
- `POST /tasks/{id}/edit` → TasksController.Edit(id, model)
- `POST /tasks/{id}/delete` → TasksController.Delete(id)
- `POST /tasks/{id}/toggle` → TasksController.ToggleComplete(id)
- `POST /tasks/{tarefaId}/reminder` → TasksController.AdicionarLembrete(tarefaId, texto)
- `POST /tasks/{tarefaId}/reminder/{lembreteId}/delete` → TasksController.RemoverLembrete(tarefaId, lembreteId)

## Responsividade (Bootstrap)

- ✅ Breakpoint mobile: <576px
- ✅ Breakpoint tablet: 576px-768px
- ✅ Breakpoint desktop: >768px
- ✅ Grid system 12 colunas
- ✅ Componentes automáticos: btn, form, alert, badge

---

**Plano aprovado e implementado com sucesso ✅**
