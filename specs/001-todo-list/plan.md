# Plano de Implementação: TODO List MVC

**Data**: 2026-05-10  
**Status**: Ready for Implementation  
**Arquitetura**: ASP.NET Core MVC, Armazenamento em Memória  
**Escopo**: MVP com 3 funcionalidades principais (cadastrar, remover, lembretes)

## Resumo Executivo

Refatoração do projeto anterior (backend/frontend separado) para arquitetura MVC simples em mono-repo que atenda exatamente ao enunciado da atividade. A aplicação será um ASP.NET Core MVC que permite:
1. Criar tarefas
2. Remover tarefas  
3. Adicionar lembretes às tarefas

Sem persistência de dados entre execuções (conforme requisito).

## Decisões Arquiteturais Justificadas

### Por que MVC (não API+Frontend)?
- **Requisito**: Enunciado pede aplicação simples
- **Simplicidade**: 1 projeto, 1 linguagem, deploy único
- **Custo**: Servidores gratuitos suportam bem ASP.NET Core
- **Desenvolvimento**: Mais rápido sem overhead de sincronização API/frontend
- **Manutenção**: Código centralizado, fácil encontrar bugs

### Por que em-memória (não banco de dados)?
- **Requisito**: Enunciado: "sem persistência entre execuções"
- **Simplificação**: Sem migrations, SQL, ou ORM complexity
- **Performance**: RAM é mais rápido que disco
- **Adequação**: MVP não precisa de dados persistentes
- **Escalação**: Pattern Repository permite trocar storage depois

## Stack Técnico

| Componente | Tecnologia | Por quê |
|-----------|-----------|--------|
| Framework | ASP.NET Core 8 | MVC built-in, .NET é conhecida |
| Views | Razor Templates | Tudo em C#, sem JS complexo |
| Storage | List<Tarefa> | Memória, conforme requisito |
| Banco | ❌ Nenhum | Requisito: sem persistência |
| CSS | Bootstrap 5 | Responsivo CDN, sem build |
| Deployment | Free Tier | Heroku, Railway, Azure Free |

## Estrutura do Projeto

```
TodoListMvc/
├── Controllers/
│   └── TasksController.cs        # Ações: Index, Create, Edit, Delete, Toggle, AddReminder
├── Models/
│   ├── Tarefa.cs                 # Modelo principal
│   └── Lembrete.cs               # Modelo de lembrete
├── Services/
│   └── RepositorioTarefas.cs     # Singleton in-memory repository
├── Views/
│   ├── Shared/
│   │   ├── Layout.cshtml         # Template base
│   │   └── _ValidationScriptsPartial.cshtml
│   └── Tasks/
│       ├── Index.cshtml          # Lista de tarefas
│       ├── Create.cshtml         # Criar tarefa
│       └── Edit.cshtml           # Editar tarefa
├── wwwroot/
│   ├── css/
│   │   └── site.css              # Estilos customizados
│   └── lib/                       # Bootstrap CDN (via package.json ou lib local)
├── appsettings.json              # Configurações
├── Program.cs                    # Setup da aplicação
└── TodoListMvc.csproj           # Projeto file

```

## Models

### Tarefa.cs
```csharp
public class Tarefa
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(500, ErrorMessage = "O título não pode ter mais de 500 caracteres")]
    public string Titulo { get; set; } = string.Empty;
    
    public bool Concluida { get; set; } = false;
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    public List<Lembrete> Lembretes { get; set; } = new();
}
```

### Lembrete.cs
```csharp
public class Lembrete
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string Texto { get; set; } = string.Empty;
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
```

## Repositório (In-Memory)

### RepositorioTarefas.cs (Singleton)
Armazena todas as tarefas em `List<Tarefa>` durante execução da aplicação.

Responsabilidades:
- CRUD para tarefas
- CRUD para lembretes
- Validações de negócio
- Thread-safety (lock se necessário)

Métodos principais:
- `List<Tarefa> ObterTodas()` - Retorna todas as tarefas
- `Tarefa? ObterPorId(Guid id)` - Busca tarefa específica
- `Tarefa Criar(string titulo)` - Cria nova tarefa com validação
- `bool Atualizar(Guid id, string titulo)` - Atualiza título
- `Tarefa AlternarConclusao(Guid id)` - Marca/desmarca como concluída
- `bool Remover(Guid id)` - Remove tarefa
- `Lembrete AdicionarLembrete(Guid tarefaId, string texto)` - Novo lembrete
- `bool RemoverLembrete(Guid tarefaId, Guid lembreteId)` - Remove lembrete
- `Lembrete AtualizarLembrete(Guid tarefaId, Guid lembreteId, string novoTexto)` - Edita lembrete

## Controller

### TasksController.cs

Ações HTTP:

1. **Index()** - GET /tasks
   - Lista todas as tarefas
   - Model: `List<Tarefa>`
   - View: Index.cshtml

2. **Create()** - GET /tasks/create
   - Renderiza formulário em branco
   - View: Create.cshtml

3. **Create(Tarefa)** - POST /tasks/create
   - Valida modelo (server-side)
   - Chama Repositorio.Criar()
   - Sucesso: Redirect /tasks
   - Erro: Re-renderiza Create.cshtml com erros

4. **Edit(Guid id)** - GET /tasks/{id}/edit
   - Busca tarefa e renderiza formulário
   - View: Edit.cshtml

5. **Edit(Guid id, Tarefa)** - POST /tasks/{id}/edit
   - Valida modelo
   - Chama Repositorio.Atualizar()
   - Redirect /tasks

6. **Delete(Guid id)** - POST /tasks/{id}/delete
   - Remove tarefa
   - Redirect /tasks

7. **AlternarConclusao(Guid id)** - POST /tasks/{id}/toggle
   - Marca/desmarca como concluída
   - Redirect /tasks (ou AJAX)

8. **AdicionarLembrete(Guid id, string texto)** - POST /tasks/{id}/reminder
   - Valida texto
   - Chama Repositorio.AdicionarLembrete()
   - Redirect /tasks

9. **RemoverLembrete(Guid id, Guid lembreteId)** - POST /tasks/{id}/reminder/{lembreteId}/delete
   - Chama Repositorio.RemoverLembrete()
   - Redirect /tasks

## Views (Razor Templates)

### Layout.cshtml
Estrutura base com:
- Header com "TODO List"
- Bootstrap CSS
- Responsivo
- Footer (opcional)

### Index.cshtml
- Lista de todas as tarefas
- Cada tarefa exibe:
  - [ ] Checkbox (toggle concluída)
  - Título (com strikethrough se concluída)
  - Botão "Editar"
  - Botão "Remover"
  - Lembretes (se houver)
  - Link "Adicionar lembrete"
- Formulário rápido no topo para nova tarefa
- Mensagem se vazio: "Nenhuma tarefa cadastrada"

### Create.cshtml
- Formulário para criar tarefa
- Campo: Titulo (required, max 500)
- Validação server-side
- Mensagens de erro em português
- Botão: "Criar"
- Link: "Voltar"

### Edit.cshtml
- Formulário para editar tarefa
- Campo: Titulo (pré-preenchido)
- Botão: "Atualizar"
- Link: "Voltar"

## Configuração (Program.cs)

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to DI container
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<RepositorioTarefas>();

var app = builder.Build();

// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();
```

## Implementação Sequencial

### Fase 1: Setup Projeto
- [ ] Criar novo projeto ASP.NET Core MVC
- [ ] Adicionar Bootstrap CSS (CDN)
- [ ] Setup layout base

### Fase 2: Models & Repositório
- [ ] Criar Tarefa.cs com validações
- [ ] Criar Lembrete.cs
- [ ] Criar RepositorioTarefas.cs (Singleton)

### Fase 3: Controller & Views
- [ ] Criar TasksController.cs
- [ ] Criar Index.cshtml
- [ ] Criar Create.cshtml
- [ ] Criar Edit.cshtml

### Fase 4: Funcionalidades
- [ ] Criar tarefa (validação)
- [ ] Remover tarefa
- [ ] Adicionar lembrete
- [ ] Editar lembrete
- [ ] Remover lembrete
- [ ] Marcar/desmarcar concluída

### Fase 5: Testes & Polish
- [ ] Testes manuais de fluxos críticos
- [ ] Responsividade em múltiplos tamanhos
- [ ] Mensagens de erro em português
- [ ] UX review

## Validações

### Server-Side (Obrigatório)
```csharp
// Titulo obrigatório
if (string.IsNullOrWhiteSpace(tarefa.Titulo))
    throw new ArgumentException("Título não pode ser vazio");

// Titulo max 500
if (tarefa.Titulo.Length > 500)
    throw new ArgumentException("Título max 500 caracteres");
```

### Client-Side (HTML5)
```html
<input type="text" name="Titulo" required maxlength="500" />
```

## Deployment

### Local Development
```bash
dotnet new mvc -n TodoListMvc
cd TodoListMvc
dotnet run
# Acessa http://localhost:5000
```

### Production (Free Server)
1. Heroku: `git push heroku main`
2. Railway: Conectar repo, auto-deploy
3. Azure Free Tier: Web App .NET Core

## Requisitos Não-Funcionais

| Requisito | Implementação |
|-----------|--------------|
| **Responsivo** | Bootstrap grid + media queries |
| **Português** | Todas as mensagens, labels, validações |
| **Performance** | <100ms para operações (em-memória é rápido) |
| **Segurança** | CSRF tokens (ASP.NET Core automático), HTML encoding |
| **Acessibilidade** | Labels associadas, contrast mínimo AA |
| **Browser** | Chrome, Firefox, Safari (2 versões recentes) |

## Próximas Etapas

1. ✅ Definir arquitetura
2. ⏳ Criar projeto
3. ⏳ Implementar Models
4. ⏳ Implementar Repositório
5. ⏳ Implementar Controller & Views
6. ⏳ Testes manuais
7. ⏳ Deploy 
  - Ações principais: < 1 segundo (criar, editar, remover)
  - Reflexo de mudanças: < 500ms (conforme constitution)
  - Compatível com conexão instável  

**Constraints**:
  - Baixo custo (sem infraestrutura premium)
  - Prazo: viável em disciplina
  - Responsivo em mobile e desktop
  - Sem autenticação na v1 (single-user por sessão)

**Scale/Scope**: 
  - Usuários: 1 por sessão (no escopo)
  - Tarefas: 100-1000 por lista (razoável para MVP)
  - Screens: 1 principal (lista) + modal/inline para criar/editar

## Constitution Check - GATE: Phase 0 Entry

*Validando aderência aos 6 princípios da constitution IFES Extensão*

| Princípio | Validação | Status |
|-----------|-----------|--------|
| **I. Code Review** | API e componentes revisáveis em PRs; estrutura clara para review | ✅ PASS |
| **II. Code Clarity** | Nomes em português; separação frontend/backend/logic; sem duplicação | ✅ PASS |
| **III. Testing Discipline** | Testes unitários para validações (backend); testes manuais documentados | ✅ PASS |
| **IV. UX Focus** | Interface simples, responsiva, mensagens em português claro, sem jargão | ✅ PASS |
| **V. Performance** | < 1s para ações principais, < 500ms para reflexo, paginação se necessário | ✅ PASS |
| **VI. Pragmatism** | Stack conhecido (React + ASP.NET), solução simples, documentação mínima | ✅ PASS |

**GATE STATUS**: 🟢 **PASSED** - Todos os princípios alinhados. Prosseguindo para Phase 0.

---

## Project Structure

### Documentation (this feature)

```text
specs/001-todo-list/
├── plan.md              # Este arquivo
├── research.md          # Phase 0 output - decisões de design
├── data-model.md        # Phase 1 output - modelo de dados
├── quickstart.md        # Phase 1 output - guia rápido
├── contracts/           # Phase 1 output - contratos de API
│   ├── task-api.md      # Endpoints da API
│   └── task-ui-model.md # Contrato de dados frontend
└── checklists/
    └── requirements.md  # Checklist de qualidade
```

### Source Code (repository root)

```text
backend/
├── src/
│   ├── Models/
│   │   ├── Task.cs            # Entidade Task (EF Core)
│   │   └── CreateTaskRequest.cs
│   ├── Services/
│   │   └── TaskService.cs     # Lógica de negócio (validações, CRUD)
│   ├── Controllers/
│   │   └── TasksController.cs # Endpoints HTTP
│   ├── Data/
│   │   └── AppDbContext.cs    # Entity Framework context
│   └── Program.cs             # Configuração e DI
└── Tests/
    ├── Unit/
    │   └── TaskServiceTests.cs
    └── Integration/
        └── TasksControllerTests.cs

frontend/
├── src/
│   ├── components/
│   │   ├── TaskList.tsx       # Componente de lista (US1)
│   │   ├── TaskItem.tsx       # Componente de item individual
│   │   ├── TaskForm.tsx       # Componente criar/editar (US2, US4)
│   │   ├── TaskActions.tsx    # Botões marcar/remover (US3, US5)
│   │   └── ErrorMessage.tsx   # Componente de mensagens de erro
│   ├── services/
│   │   └── taskService.ts     # Client HTTP para API backend
│   ├── hooks/
│   │   └── useTasks.ts        # Hook custom para gerenciar estado
│   ├── App.tsx
│   └── App.css                # Estilos (responsivo)
└── Tests/
    ├── unit/
    │   └── TaskForm.test.tsx
    └── integration/
        └── TaskList.integration.test.tsx
```

**Structure Decision**: Opção 2 (Web application). Frontend e backend separados permite desenvolvimento independente, deploy flexível e segue convention do stack React + ASP.NET Core. Facilita testes de contrato (API) e testes de UI isolados.

---

## Phase 0: Research & Clarification

**Status**: Resolvendo ambiguidades da especificação

### Research Tasks Completed

#### Decisão 1: Ordenação de Tarefas

**Ambiguidade**: Specification menciona que "A ordem das tarefas é mantida conforme inserção (FIFO) ou pode ser ordenada por estado".

**Decision**: **FIFO (Most Recent First)**
- Tarefas novas aparecem no topo da lista
- Ordem mantida por `DateCreated DESC`

**Rationale**:
- (1) Padrão em TODO lists modernas (Todoist, Microsoft To Do)
- (2) Mais simples de implementar (sem sorting complexo)
- (3) Melhor UX: usuário vê o que acabou de criar imediatamente no topo
- (4) Alinha com constraint de simplicidade (Princípio VI)

**Impact**: 
- Backend: `ORDER BY DateCreated DESC` em query de lista
- Frontend: Refletir ordem descendente, adicionar novos items no topo
- Tests: Validar que ordem é mantida corretamente

#### Decisão 2: Limite de Caracteres para Título

**Ambiguidade**: Edge case pergunta "O que acontece se o usuário tenta criar/editar tarefa com título muito longo?"

**Decision**: **Máximo 500 caracteres**

**Rationale**:
- (1) Suficiente para descrições detalhadas (sem ser excessivo)
- (2) Limite prático para UX em celular (evita text wrapping excessivo)
- (3) Fácil de validar (constraint simples no backend)
- (4) Padrão em sistemas similares

**Impact**:
- Backend: Validação `Title.Length <= 500`
- Frontend: Contador visual opcional, placeholder de limite
- Tests: Teste edge case com 500 e 501 caracteres

#### Decisão 3: Trimming de Espaços em Branco

**Ambiguidade**: Edge case "O que acontece se o usuário tenta criar uma tarefa com apenas espaços em branco?"

**Decision**: **Trim + validação como vazio**

**Rationale**:
- (1) Evita tarefas com títulos "inúteis" (só espaços)
- (2) Trim automático melhora UX (entrada acidental)
- (3) Consistente com regra de negócio "título obrigatório"

**Implementation**:
```csharp
// Backend (C#)
if (string.IsNullOrWhiteSpace(title) || title.Trim().Length == 0)
    throw new ValidationException("O título da tarefa não pode ser vazio");
Title = title.Trim();
```

**Impact**:
- Frontend: Trim na entrada (feedback visual)
- Backend: Trim + validação
- Tests: Teste com " ", "  \t  ", etc.

---

## Phase 1: Design & Contracts

**Prerequisites**: research.md (✅ Acima) e Constitution Check (✅ PASS)

### 1. Data Model (data-model.md)

Entity `Task`:

```
Task
├── Id: Guid (primary key, generated)
├── Title: string (required, max 500 chars, trimmed)
├── IsCompleted: bool (default: false)
├── DateCreated: DateTime (auto-set, UTC)
├── DateModified: DateTime (auto-update, UTC)
└── [Future] UserId: string (out of scope v1, placeholder)

Validation Rules:
- Title: required, not null, not empty, max 500 chars, trimmed
- IsCompleted: boolean toggle, default false
- DateCreated: auto-set on creation, immutable
- DateModified: auto-update on any change
```

Relationships:
- No relationships in v1 (single user, no categories/tags)
- Future: UserId FK para suportar multi-user

### 2. API Contracts (contracts/task-api.md)

```
Base URL: /api/v1/tasks

GET /api/v1/tasks
  Description: Lista todas as tarefas
  Response: 200 OK
    Body: List<TaskResponse>
    [{"id": "...", "title": "...", "isCompleted": false, "dateCreated": "..."}]
  Performance: < 1 second

POST /api/v1/tasks
  Description: Criar nova tarefa
  Request: CreateTaskRequest { title: string }
  Validation: title required, max 500 chars
  Response: 201 Created
    Body: TaskResponse
  Error: 400 Bad Request (title vazio, muito longo)
    Body: { "error": "O título da tarefa não pode ser vazio" }

PUT /api/v1/tasks/{id}
  Description: Atualizar título de tarefa existente
  Request: UpdateTaskRequest { title: string }
  Response: 200 OK | 404 Not Found | 400 Bad Request

PATCH /api/v1/tasks/{id}/complete
  Description: Marcar tarefa como concluída
  Response: 200 OK | 404 Not Found

PATCH /api/v1/tasks/{id}/incomplete
  Description: Marcar tarefa como não concluída
  Response: 200 OK | 404 Not Found

DELETE /api/v1/tasks/{id}
  Description: Remover tarefa
  Response: 204 No Content | 404 Not Found
```

### 3. UI Data Model (contracts/task-ui-model.md)

```typescript
// Frontend TypeScript interfaces
interface Task {
  id: string;
  title: string;
  isCompleted: boolean;
  dateCreated: string; // ISO 8601
}

interface TaskFormData {
  title: string; // Validado no frontend (required, max 500)
}

interface ErrorMessage {
  message: string;
  field?: string; // Opcional: qual campo causou erro
}
```

### 4. Quickstart (quickstart.md)

**Para Desenvolvedor Backend**:
1. Clone projeto
2. Abra `backend/` em Visual Studio
3. Restaure dependências: `dotnet restore`
4. Execute migrations: `dotnet ef database update`
5. Inicie API: `dotnet run` (padrão: http://localhost:5000)
6. Teste endpoint: `curl http://localhost:5000/api/v1/tasks`

**Para Desenvolvedor Frontend**:
1. Clone projeto
2. Navegue até `frontend/`: `cd frontend`
3. Instale dependências: `npm install`
4. Inicie dev server: `npm start`
5. Browser abre em http://localhost:3000
6. API é chamada automaticamente em http://localhost:5000/api/v1/tasks

**Para Testar End-to-End**:
1. Inicie backend + frontend (acima)
2. Crie tarefa: Digite título → Clique "Adicionar"
3. Marque como concluída: Clique checkbox
4. Edite: Clique tarefa → Edite título
5. Remova: Clique botão delete

---

## Phase 1 - Constitution Check Re-evaluation

*Validando design contra 6 princípios após Phase 1*

| Princípio | Validação Design | Status |
|-----------|------------------|--------|
| **I. Code Review** | Frontend (components) e Backend (services) separados em arquivos revisáveis; contratos de API claros | ✅ PASS |
| **II. Code Clarity** | Nomes em português (TaskForm, TaskService, validarTítulo); separação: services (lógica) vs components (UI) | ✅ PASS |
| **III. Testing** | Testes unitários em TaskService (validações); testes de UI em TaskForm (integração) | ✅ PASS |
| **IV. UX** | UI simples (lista + form), responsiva, mensagens em português português claro | ✅ PASS |
| **V. Performance** | API GET < 1s (lista simples), POST < 500ms (inserção), sem queries N+1 | ✅ PASS |
| **VI. Pragmatism** | Stack simples (React + ASP.NET), sem patterns excessivos (Redux, etc), documentação mínima | ✅ PASS |

**GATE STATUS**: 🟢 **PASSED** - Design alinhado com todos os princípios. Pronto para Phase 2 (Tasks).

---

## Complexity Tracking

**Violations**: Nenhuma 🟢

Projeto está dentro do escopo pragmático definido pela constitution. Stack é simples, arquitetura é direta (Frontend/Backend/DB), sem over-engineering.

---

## Next Steps

1. ✅ **Phase 0 & 1 Complete**: Research e Design finalizados
2. ⏳ **Phase 2**: Executar `/speckit.tasks` para gerar task list baseada em design
3. ⏳ **Implementation**: Equipe implementa tasks conforme schedule
4. ⏳ **Testing**: Testes manuais (fluxos críticos) + testes automatizados (validações)
5. ⏳ **Review & Merge**: PRs revisadas conforme Code Review Principle

**Artifacts Generated**:
- ✅ plan.md (este arquivo)
- ✅ research.md (decisões)
- ✅ data-model.md (Task entity)
- ✅ contracts/ (API + UI models)
- ✅ quickstart.md (dev setup)
