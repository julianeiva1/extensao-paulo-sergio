---
description: "Task list for TODO List MVC implementation"
---

# Tasks: TODO List MVC Implementation

**Feature**: TODO List MVC (001-todo-list-mvc)  
**Architecture**: ASP.NET Core MVC, In-Memory Storage  
**Date**: 2026-05-10

## Phases Overview

- **Phase 1**: Project Setup
- **Phase 2**: Models & Core Infrastructure
- **Phase 3**: Repository & Business Logic
- **Phase 4**: Controller Implementation
- **Phase 5**: Views & Frontend
- **Phase 6**: Testing & Validation

---

## Phase 1: Project Setup

- [x] T001 Create new ASP.NET Core MVC project named TodoListMvc
- [x] T002 Add Bootstrap 5 CSS reference (CDN in Layout.cshtml)
- [x] T003 Create folder structure (Models/, Views/Tasks/, Services/)
- [x] T004 Setup project.csproj with required dependencies

## Phase 2: Models & Infrastructure

- [x] T005 Create Models/Tarefa.cs with validation attributes
  - Guid Id
  - string Titulo (required, max 500)
  - bool Concluida (default false)
  - DateTime DataCriacao (UTC)
  - List<Lembrete> Lembretes
  
- [x] T006 Create Models/Lembrete.cs
  - Guid Id
  - string Texto (required)
  - DateTime DataCriacao (UTC)

- [x] T007 Create Views/Shared/Layout.cshtml
  - Bootstrap CSS
  - Page title
  - @RenderBody()
  - Responsive design

## Phase 3: Repository & Business Logic

- [x] T008 Create Services/RepositorioTarefas.cs (Singleton)
  - Private List<Tarefa> storage
  - Constructor initializes with empty list
  
- [x] T009 Implement RepositorioTarefas methods - Read
  - ObterTodas() → List<Tarefa>
  - ObterPorId(Guid id) → Tarefa?

- [x] T010 Implement RepositorioTarefas methods - Create
  - Criar(string titulo) → Tarefa
  - Validate: not null, not empty, not whitespace only
  - Validate: max 500 chars
  - Generate new Guid
  - Set DataCriacao = DateTime.UtcNow
  - Add to list

- [x] T011 Implement RepositorioTarefas methods - Update
  - Atualizar(Guid id, string novoTitulo) → bool
  - Validate: same as Criar
  - Find tarefa by id
  - Update Titulo
  - Return success

- [x] T012 Implement RepositorioTarefas methods - Toggle Completion
  - AlternarConclusao(Guid id) → Tarefa
  - Find by id
  - Toggle Concluida
  - Return updated tarefa

- [x] T013 Implement RepositorioTarefas methods - Delete
  - Remover(Guid id) → bool
  - Find and remove from list
  - Return success

- [x] T014 Implement RepositorioTarefas methods - Reminders
  - AdicionarLembrete(Guid tarefaId, string texto) → Lembrete
  - RemoverLembrete(Guid tarefaId, Guid lembreteId) → bool
  - AtualizarLembrete(Guid tarefaId, Guid lembreteId, string novoTexto) → bool

- [x] T015 Register RepositorioTarefas as Singleton in Program.cs

## Phase 4: Controller

- [x] T016 Create Controllers/TasksController.cs extending Controller
  - Inject RepositorioTarefas via constructor
  - Create private _repositorio field

- [x] T017 Implement TasksController.Index()
  - GET /tasks
  - Call _repositorio.ObterTodas()
  - Pass List<Tarefa> to View
  - Return View("Index", tarefas)

- [x] T018 Implement TasksController.Create() GET
  - GET /tasks/create
  - Return empty Create view

- [x] T019 Implement TasksController.Create() POST
  - POST /tasks/create (model Tarefa)
  - Validate ModelState
  - Call _repositorio.Criar(model.Titulo)
  - Success: RedirectToAction("Index")
  - Error: Return View with ModelState errors

- [x] T020 Implement TasksController.Edit() GET
  - GET /tasks/{id}/edit
  - Get tarefa by id
  - If not found: return NotFound
  - Return View with tarefa

- [x] T021 Implement TasksController.Edit() POST
  - POST /tasks/{id}/edit
  - Validate ModelState
  - Call _repositorio.Atualizar(id, titulo)
  - Redirect to Index

- [x] T022 Implement TasksController.Delete()
  - POST /tasks/{id}/delete
  - Call _repositorio.Remover(id)
  - Redirect to Index

- [x] T023 Implement TasksController.AlternarConclusao()
  - POST /tasks/{id}/toggle
  - Call _repositorio.AlternarConclusao(id)
  - Redirect to Index

- [x] T024 Implement TasksController.AdicionarLembrete()
  - POST /tasks/{id}/reminder
  - Accept texto parameter
  - Call _repositorio.AdicionarLembrete(id, texto)
  - Redirect to Index

- [x] T025 Implement TasksController.RemoverLembrete()
  - POST /tasks/{id}/reminder/{lembreteId}/delete
  - Call _repositorio.RemoverLembrete(id, lembreteId)
  - Redirect to Index

## Phase 5: Views

- [x] T026 Create Views/Tasks/Index.cshtml
  - Display all tasks from Model (List<Tarefa>)
  - For each task:
    - Checkbox for Concluida toggle
    - Title text (strikethrough if Concluida)
    - Edit button → /tasks/{id}/edit
    - Delete button → form POST /tasks/{id}/delete
    - List of Lembretes (if any)
    - "Add reminder" link
  - Empty state message if no tasks
  - Quick add form at top

- [x] T027 Create Views/Tasks/Create.cshtml
  - Form with POST action to /tasks/create
  - Text input for Titulo (required, maxlength 500)
  - Submit button "Criar"
  - Display validation errors
  - Back link
  
- [x] T028 Create Views/Tasks/Edit.cshtml
  - Form with POST action to /tasks/{id}/edit
  - Text input pre-filled with Tarefa.Titulo
  - Submit button "Atualizar"
  - Display validation errors
  - Back link

- [x] T029 Add CSS styling (wwwroot/css/site.css)
  - Completed task styling (strikethrough, lighter color)
  - Button styling
  - Responsive layout
  - Reminder block styling

## Phase 6: Testing & Validation

- [ ] T030 Manual test: Create tarefa with valid title
  - Should appear in list
  - Should be uncompleted

- [ ] T031 Manual test: Create tarefa with empty title
  - Should show validation error
  - Should not be added

- [ ] T032 Manual test: Create tarefa with only spaces
  - Should show validation error
  - Should not be added

- [ ] T033 Manual test: Create tarefa with 500 chars
  - Should succeed
  - Should display correctly

- [ ] T034 Manual test: Create tarefa with >500 chars
  - Should show validation error

- [ ] T035 Manual test: Delete tarefa
  - Should disappear from list
  - Remaining tasks unchanged

- [ ] T036 Manual test: Toggle completed
  - Completed: strikethrough + different color
  - Uncompleted: normal styling
  - Can toggle back and forth

- [ ] T037 Manual test: Add reminder
  - Should appear below task
  - Should be editable
  - Should be removable

- [ ] T038 Manual test: Responsive design
  - Test on 320px width (mobile)
  - Test on 768px width (tablet)
  - Test on 1200px+ width (desktop)
  - All elements readable and clickable

- [ ] T039 Manual test: Create/Edit/Delete flow
  - Create 3 tasks
  - Edit one title
  - Add reminders to 2 tasks
  - Delete one
  - Verify list consistency

- [ ] T040 Manual test: Page refresh
  - All data lost (conforme requisito)
  - Empty list after F5
  - No browser cache preservation

- [ ] T041 Manual test: Portuguese messages
  - Validation errors in Portuguese
  - Button labels in Portuguese
  - All UI text in Portuguese

- [ ] T042 Manual test: Performance
  - Add 50 tasks rapidly
  - UI remains responsive
  - No lag on list update

## Checklist

### Before Release
- [ ] All tasks completed
- [ ] All manual tests passed
- [ ] No console errors (F12)
- [ ] Portuguese messages verified
- [ ] Responsive design verified
- [ ] User flows tested end-to-end

### Project Structure
- [ ] folder: Models/
- [ ] folder: Services/
- [ ] folder: Views/Tasks/
- [ ] folder: wwwroot/css/
- [ ] file: Program.cs configured
- [ ] file: appsettings.json present

### Files Created
- [ ] Models/Tarefa.cs
- [ ] Models/Lembrete.cs
- [ ] Services/RepositorioTarefas.cs
- [ ] Controllers/TasksController.cs
- [ ] Views/Shared/Layout.cshtml
- [ ] Views/Tasks/Index.cshtml
- [ ] Views/Tasks/Create.cshtml
- [ ] Views/Tasks/Edit.cshtml
- [ ] wwwroot/css/site.css
- **[Story]**: Which user story (e.g., US1, US2, US3)
- File paths based on structure from plan.md: `backend/src/...` and `frontend/src/...`

---

## Phase 1: Setup (Project Initialization)

**Goal**: Initialize both frontend and backend projects with dependencies

- [x] T001 [P] Setup backend project structure with dotnet CLI in `backend/`
- [x] T002 [P] Setup frontend project structure with Vite/React in `frontend/`
- [x] T003 [P] Install backend dependencies (EF Core, ASP.NET Core packages) in `backend/IFES.Extensao.API.csproj`
- [x] T004 [P] Install frontend dependencies (React, axios, testing libraries) in `frontend/package.json`
- [x] T005 [P] Configure backend CORS policy for frontend in `backend/Program.cs`
- [x] T006 [P] Configure backend database provider (SQLite for dev) in `backend/Program.cs`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Goal**: Core infrastructure that ALL user stories depend on

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T007 Create Entity Framework DbContext in `backend/Data/AppDbContext.cs`
- [x] T008 Create Task model entity in `backend/Models/Task.cs` with 5 properties (Id, Title, IsCompleted, DateCreated, DateModified)
- [x] T009 Create Task validation rules in `backend/Models/Task.cs` (required, max 500 chars, trimmed)
- [x] T010 Create Entity Framework migrations for Task table in `backend/Migrations/`
- [x] T011 [P] Create TaskService with CRUD logic in `backend/Services/TaskService.cs` (GetAllTasks, GetTaskById, CreateTask, UpdateTask, DeleteTask, ToggleComplete)
- [x] T012 [P] Create request/response DTOs in `backend/Models/CreateTaskRequest.cs` and `backend/Models/TaskResponse.cs`
- [x] T013 [P] Create TasksController with HTTP endpoints in `backend/Controllers/TasksController.cs` (GET, POST, PUT, PATCH, DELETE)
- [x] T014 [P] Create error handling middleware in `backend/Middleware/ErrorHandlingMiddleware.cs` with Portuguese error messages
- [x] T015 [P] Create HTTP client service in `frontend/src/services/taskService.ts` with axios configuration
- [x] T016 [P] Create custom hook useTasks in `frontend/src/hooks/useTasks.ts` for state management
- [x] T017 [P] Create ErrorMessage component in `frontend/src/components/ErrorMessage.tsx` for displaying errors
- [x] T018 Setup frontend development environment (vite.config.ts, tsconfig.json, API proxy)

**Checkpoint**: Foundation ready - all user story tasks can now proceed in parallel

---

## Phase 3: User Story 1 - Visualizar Lista de Tarefas (Priority: P1) 🎯 MVP

**Goal**: Display list of all tasks with visual distinction between completed/pending

**Independent Test**: User can view list with zero tasks (empty message), and list with multiple tasks showing correct state distinction

### Tests for User Story 1

- [x] T019 [P] [US1] Unit test for TaskService.GetAllTasks returns tasks ordered DESC by DateCreated in `backend/Tests/Unit/TaskServiceTests.cs`
- [x] T020 [P] [US1] Integration test for GET /api/v1/tasks endpoint in `backend/Tests/Integration/TasksControllerTests.cs`
- [x] T021 [P] [US1] Integration test for empty task list returns [] in `backend/Tests/Integration/TasksControllerTests.cs`

### Implementation for User Story 1

- [x] T022 [P] [US1] Create TaskList component displaying all tasks in `frontend/src/components/TaskList.tsx`
- [x] T023 [P] [US1] Create TaskItem component showing individual task with completion state in `frontend/src/components/TaskItem.tsx`
- [x] T024 [US1] Implement GET /api/v1/tasks in TasksController returning list ordered DESC in `backend/Controllers/TasksController.cs`
- [x] T025 [US1] Implement visual distinction for completed tasks (strikethrough, color, icon) in `frontend/src/components/TaskItem.tsx`
- [x] T026 [US1] Implement empty state message "Nenhuma tarefa cadastrada" in `frontend/src/components/TaskList.tsx`
- [x] T027 [US1] Test TaskList loads tasks from backend on component mount in `frontend/src/components/TaskList.test.tsx`
- [x] T028 [US1] Add responsive styling for task list in `frontend/src/App.css` (mobile-first, breakpoint 768px)

**Checkpoint**: User Story 1 complete - list displays correctly

---

## Phase 4: User Story 2 - Criar Nova Tarefa com Validação (Priority: P1) 🎯 MVP

**Goal**: Allow creating new tasks with title validation and clear error messages

**Independent Test**: Can be tested by creating valid task (appears in list) and attempting to create empty (error message shown)

### Tests for User Story 2

- [ ] T029 [P] [US2] Unit test for TaskService.CreateTask validates title in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T030 [P] [US2] Unit test for empty title throws ValidationException in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T031 [P] [US2] Unit test for >500 char title throws ValidationException in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T032 [P] [US2] Integration test for POST /api/v1/tasks creates and returns task in `backend/Tests/Integration/TasksControllerTests.cs`
- [ ] T033 [P] [US2] Integration test for POST with empty title returns 400 Bad Request in `backend/Tests/Integration/TasksControllerTests.cs`

### Implementation for User Story 2

- [ ] T034 [P] [US2] Create TaskForm component with title input in `frontend/src/components/TaskForm.tsx`
- [ ] T035 [P] [US2] Implement POST /api/v1/tasks endpoint in TasksController in `backend/Controllers/TasksController.cs`
- [ ] T036 [US2] Implement title validation (required, max 500 chars, whitespace trim) in TaskService.cs `backend/Services/TaskService.cs`
- [ ] T037 [US2] Implement error response for invalid title with message "O título da tarefa não pode ser vazio" in `backend/Controllers/TasksController.cs`
- [ ] T038 [US2] Add real-time validation feedback in TaskForm in `frontend/src/components/TaskForm.tsx`
- [ ] T039 [US2] Implement optimistic update: new task appears in list immediately in `frontend/src/components/TaskList.tsx`
- [ ] T040 [US2] Add "Adicionar" button to TaskForm and wire to API call in `frontend/src/components/TaskForm.tsx`
- [ ] T041 [US2] Add character counter (optional) to TaskForm showing current/max characters in `frontend/src/components/TaskForm.tsx`

**Checkpoint**: User Story 2 complete - creating tasks with validation works

---

## Phase 5: User Story 3 - Marcar Tarefa como Concluída/Não Concluída (Priority: P2)

**Goal**: Toggle task completion state with immediate reflection in list

**Independent Test**: Can toggle task completion and see visual state change immediately

### Tests for User Story 3

- [ ] T042 [P] [US3] Unit test for TaskService.ToggleComplete inverts IsCompleted in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T043 [P] [US3] Integration test for PATCH /api/v1/tasks/{id}/complete marks task completed in `backend/Tests/Integration/TasksControllerTests.cs`
- [ ] T044 [P] [US3] Integration test for PATCH /api/v1/tasks/{id}/incomplete marks task pending in `backend/Tests/Integration/TasksControllerTests.cs`

### Implementation for User Story 3

- [ ] T045 [P] [US3] Create TaskActions component with complete/incomplete toggle buttons in `frontend/src/components/TaskActions.tsx`
- [ ] T046 [P] [US3] Implement PATCH /api/v1/tasks/{id}/complete endpoint in `backend/Controllers/TasksController.cs`
- [ ] T047 [P] [US3] Implement PATCH /api/v1/tasks/{id}/incomplete endpoint in `backend/Controllers/TasksController.cs`
- [ ] T048 [US3] Add checkbox/toggle UI element to TaskItem for marking complete in `frontend/src/components/TaskItem.tsx`
- [ ] T049 [US3] Wire checkbox to API call and update task state in `frontend/src/components/TaskItem.tsx`
- [ ] T050 [US3] Ensure visual distinction persists after toggle (strikethrough remains) in `frontend/src/components/TaskItem.tsx`
- [ ] T051 [US3] Test that toggle updates list immediately (no reload needed) in `frontend/src/components/TaskList.test.tsx`

**Checkpoint**: User Story 3 complete - toggling completion works

---

## Phase 6: User Story 4 - Editar Título de Tarefa Existente (Priority: P2)

**Goal**: Allow editing task titles with validation and state preservation

**Independent Test**: Can edit title (displays updated) and attempt edit with empty (error shown)

### Tests for User Story 4

- [ ] T052 [P] [US4] Unit test for TaskService.UpdateTask updates title and DateModified in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T053 [P] [US4] Unit test for UpdateTask with empty title throws ValidationException in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T054 [P] [US4] Integration test for PUT /api/v1/tasks/{id} updates and returns task in `backend/Tests/Integration/TasksControllerTests.cs`

### Implementation for User Story 4

- [ ] T055 [P] [US4] Add edit mode UI to TaskItem (inline edit or modal) in `frontend/src/components/TaskItem.tsx`
- [ ] T056 [P] [US4] Extend TaskForm to support edit mode (title pre-filled) in `frontend/src/components/TaskForm.tsx`
- [ ] T057 [US4] Implement PUT /api/v1/tasks/{id} endpoint in TasksController in `backend/Controllers/TasksController.cs`
- [ ] T058 [US4] Implement edit validation (same as create: required, max 500 chars) in TaskService.cs in `backend/Services/TaskService.cs`
- [ ] T059 [US4] Wire TaskItem edit button to activate edit mode in `frontend/src/components/TaskItem.tsx`
- [ ] T060 [US4] Update TaskList state when edit is confirmed in `frontend/src/components/TaskList.tsx`
- [ ] T061 [US4] Preserve completion state after edit (title changes, completion status unchanged) in `frontend/src/components/TaskList.tsx`
- [ ] T062 [US4] Test that edited task appears with new title immediately in `frontend/src/components/TaskList.test.tsx`

**Checkpoint**: User Story 4 complete - editing works with validation

---

## Phase 7: User Story 5 - Remover Tarefa (Priority: P2)

**Goal**: Allow deleting tasks with immediate list update

**Independent Test**: Can delete task and verify it no longer appears in list

### Tests for User Story 5

- [ ] T063 [P] [US5] Unit test for TaskService.DeleteTask removes task from DB in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T064 [P] [US5] Unit test for DeleteTask nonexistent ID throws InvalidOperationException in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T065 [P] [US5] Integration test for DELETE /api/v1/tasks/{id} removes task (204 No Content) in `backend/Tests/Integration/TasksControllerTests.cs`

### Implementation for User Story 5

- [ ] T066 [P] [US5] Add delete button to TaskItem in `frontend/src/components/TaskItem.tsx`
- [ ] T067 [P] [US5] Implement DELETE /api/v1/tasks/{id} endpoint in TasksController in `backend/Controllers/TasksController.cs`
- [ ] T068 [US5] Wire delete button to API call in `frontend/src/components/TaskItem.tsx`
- [ ] T069 [US5] Update TaskList to remove deleted task from state in `frontend/src/components/TaskList.tsx`
- [ ] T070 [US5] Add confirmation dialog before delete in `frontend/src/components/TaskItem.tsx` or TaskActions
- [ ] T071 [US5] Test that deleted task no longer appears in list in `frontend/src/components/TaskList.test.tsx`

**Checkpoint**: User Story 5 complete - deleting works

---

## Phase 8: Polish & Cross-Cutting Concerns

**Goal**: Ensure code quality, responsiveness, and production readiness

- [ ] T072 [P] Comprehensive UI testing across browsers (Chrome, Firefox, Safari, Edge) in browser test matrix
- [ ] T073 [P] Responsive design testing on mobile (320px), tablet (768px), desktop (1200px) viewports
- [ ] T074 [P] Performance testing: verify all operations complete in < 500ms locally
- [ ] T075 [P] Add loading state indicators during API calls in `frontend/src/components/TaskList.tsx`
- [ ] T076 [P] Add retry logic for failed API calls in `frontend/src/services/taskService.ts`
- [ ] T077 [P] Refactor duplicated validation logic into shared validator in `backend/Services/TaskValidator.cs`
- [ ] T078 Create README.md with setup and usage instructions for developers
- [ ] T079 Create TESTING.md documenting manual test scenarios for all 5 user stories
- [ ] T080 Document API changes in CHANGELOG.md (version v0.1.0)
- [ ] T081 Add logging for debugging (server-side request/response logging)
- [ ] T082 Configure build pipeline (CI/CD hooks if applicable)
- [ ] T083 Code review: Ensure all code follows constitution principles (Code Clarity, Testing Discipline, UX Focus)
- [ ] T084 Accessibility audit: Verify keyboard navigation, contrast, semantic HTML
- [ ] T085 Final end-to-end test: Execute all 5 user stories from browser (create, view, complete, edit, delete)

**Checkpoint**: Release candidate ready - all quality gates passed

---

## Dependencies & Execution Order

```
Phase 1 (Setup) → Phase 2 (Foundational) → Phases 3-7 (User Stories, mostly parallel) → Phase 8 (Polish)

Strict Dependencies:
- T001-T002: Parallel, no deps
- T003-T006: Depend on T001-T002
- T007-T018: Depend on T003-T006 (foundational)
- T019+: Depend on T007-T018

Within each User Story (US1-US5):
- Tests can write before implementation (TDD)
- Frontend and backend tasks can run in parallel
- Integration between tasks happens at phase checkpoints
```

## Parallel Execution Examples

**Can run in parallel (independent branches)**:
```
Developers A+B: Phase 1 setup together
Developers A+B+C: Phase 2 foundational (separate files: Models, Services, DTOs, Controller, Frontend services)
Developer A: Phase 3 (Backend: T024, T025, Tests) + Developer B: Phase 3 (Frontend: T022, T023, T026, T027, T028)
Developer A: Phase 4 (Backend: T035-T037) + Developer B: Phase 4 (Frontend: T034, T038-T041)
Developers B+C: Phase 5-7 in parallel (separate user stories don't conflict)
Developer A: Phase 8 docs/polish while B+C finish US4-US5
```

---

## Task Validation

### Format Compliance ✅

All tasks follow strict format:
- Checkbox: `- [ ]` ✅
- Task ID: Sequential T001-T085 ✅
- Parallelizable marker: `[P]` where applicable ✅
- Story label: `[US1]`-`[US5]` for user story tasks ✅
- Description: Clear action + exact file path ✅
- No ambiguous "Create X" - always specific ("Create TaskList component in .../")

### Story Coverage ✅

| User Story | Task IDs | Count | Status |
|-----------|----------|-------|--------|
| **US1** (Visualizar) | T019-T028 | 10 | ✅ Complete |
| **US2** (Criar) | T029-T041 | 13 | ✅ Complete |
| **US3** (Concluir) | T042-T051 | 10 | ✅ Complete |
| **US4** (Editar) | T052-T062 | 11 | ✅ Complete |
| **US5** (Remover) | T063-T071 | 9 | ✅ Complete |
| **Setup/Foundational** | T001-T018 | 18 | ✅ Complete |
| **Polish** | T072-T085 | 14 | ✅ Complete |

**Total Tasks**: 85  
**MVP Scope (P1 only)**: Tasks 1-40 (Setup + US1 + US2)  
**Full Scope (P1+P2)**: All 85 tasks

---

## Success Criteria for Completion

1. ✅ All 85 tasks have clear, actionable descriptions
2. ✅ No task is ambiguous or requires external context
3. ✅ Each task has exact file path(s)
4. ✅ Dependencies between tasks are clear
5. ✅ Parallelization opportunities identified
6. ✅ User stories can be delivered independently
7. ✅ Phase checkpoints enable validation
8. ✅ Tasks follow constitution principles (clarity, testing, quality)

---

## Suggested MVP Scope

For a minimum viable delivery within sprint constraints:

**MVP v0.1**: Focus on **P1 stories** + foundational + 1 P2
- Phases 1-4 (Setup + Foundational + US1 + US2): ~40 tasks, ~2 weeks
- Delivers: View empty/populated list + Create tasks
- Postpone: Complete, Edit, Delete to v0.2

**Full v1.0**: All 5 user stories + polish (~85 tasks total)
- Deliverable in ~4 weeks depending on team size

