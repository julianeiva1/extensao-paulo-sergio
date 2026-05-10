# Implementation Plan: Gerenciamento de Tarefas em TODO List

**Branch**: `001-featurename-todo-list` | **Date**: 2026-05-10 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `specs/001-todo-list/spec.md` - TODO List com criar, listar, editar, concluir e remover tarefas

## Summary

Sistema web de gerenciamento de tarefas simples que permite ao usuário criar, visualizar, editar, marcar como concluída e remover tarefas. Frontend em React consome API REST do backend ASP.NET Core. Arquitetura separada (frontend/backend) com persistência simples de dados. MVP com 5 user stories priorizadas (2x P1 essenciais + 3x P2 complementares). Foco em UX simples, responsiva e sem jargão técnico, com validações claras e mensagens de erro em português.

## Technical Context

**Frontend Language/Version**: React 18+ (TypeScript recomendado para clareza)  
**Frontend Dependencies**: React, HTTP client (fetch ou axios), gerenciamento de estado simples (useState/useContext)  
**Backend Language/Version**: C# 11+ com .NET 7+ (ASP.NET Core Web API)  
**Backend Dependencies**: ASP.NET Core Web API, Entity Framework Core (ORM simples)  
**Storage**: Banco de dados relacional simples (SQL Server, PostgreSQL, ou SQLite para desenvolvimento)  
**Testing**: Frontend (Jest/Vitest), Backend (xUnit ou nUnit para C#)  
**Target Platform**: Web - navegadores modernos (Chrome, Firefox, Safari, Edge)  
**Project Type**: Web application (SPA + REST API)  
**Performance Goals**: 
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
