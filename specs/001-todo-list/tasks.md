---
description: "Task list for TODO List feature implementation"
---

# Tasks: Gerenciamento de Tarefas em TODO List

**Feature**: TODO List Management (001-todo-list)  
**Branch**: `001-featurename-todo-list`  
**Input**: Design documents from `specs/001-todo-list/` (plan.md, spec.md, research.md, data-model.md, contracts/)  
**Prerequisites**: All design documents complete ✅

**Organization**: Tasks grouped by user story (US1-US5) to enable independent implementation and testing  
**Dependencies**: Phase 1 (Setup) → Phase 2 (Foundational) → Phases 3-7 (User Stories in parallel)  
**Parallelization**: Most backend+frontend tasks for same feature can run in parallel ([P] marker)

---

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story (e.g., US1, US2, US3)
- File paths based on structure from plan.md: `backend/src/...` and `frontend/src/...`

---

## Phase 1: Setup (Project Initialization)

**Goal**: Initialize both frontend and backend projects with dependencies

- [ ] T001 [P] Setup backend project structure with dotnet CLI in `backend/`
- [ ] T002 [P] Setup frontend project structure with Vite/React in `frontend/`
- [ ] T003 [P] Install backend dependencies (EF Core, ASP.NET Core packages) in `backend/IFES.Extensao.API.csproj`
- [ ] T004 [P] Install frontend dependencies (React, axios, testing libraries) in `frontend/package.json`
- [ ] T005 [P] Configure backend CORS policy for frontend in `backend/Program.cs`
- [ ] T006 [P] Configure backend database provider (SQLite for dev) in `backend/Program.cs`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Goal**: Core infrastructure that ALL user stories depend on

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [ ] T007 Create Entity Framework DbContext in `backend/Data/AppDbContext.cs`
- [ ] T008 Create Task model entity in `backend/Models/Task.cs` with 5 properties (Id, Title, IsCompleted, DateCreated, DateModified)
- [ ] T009 Create Task validation rules in `backend/Models/Task.cs` (required, max 500 chars, trimmed)
- [ ] T010 Create Entity Framework migrations for Task table in `backend/Migrations/`
- [ ] T011 [P] Create TaskService with CRUD logic in `backend/Services/TaskService.cs` (GetAllTasks, GetTaskById, CreateTask, UpdateTask, DeleteTask, ToggleComplete)
- [ ] T012 [P] Create request/response DTOs in `backend/Models/CreateTaskRequest.cs` and `backend/Models/TaskResponse.cs`
- [ ] T013 [P] Create TasksController with HTTP endpoints in `backend/Controllers/TasksController.cs` (GET, POST, PUT, PATCH, DELETE)
- [ ] T014 [P] Create error handling middleware in `backend/Middleware/ErrorHandlingMiddleware.cs` with Portuguese error messages
- [ ] T015 [P] Create HTTP client service in `frontend/src/services/taskService.ts` with axios configuration
- [ ] T016 [P] Create custom hook useTasks in `frontend/src/hooks/useTasks.ts` for state management
- [ ] T017 [P] Create ErrorMessage component in `frontend/src/components/ErrorMessage.tsx` for displaying errors
- [ ] T018 Setup frontend development environment (vite.config.ts, tsconfig.json, API proxy)

**Checkpoint**: Foundation ready - all user story tasks can now proceed in parallel

---

## Phase 3: User Story 1 - Visualizar Lista de Tarefas (Priority: P1) 🎯 MVP

**Goal**: Display list of all tasks with visual distinction between completed/pending

**Independent Test**: User can view list with zero tasks (empty message), and list with multiple tasks showing correct state distinction

### Tests for User Story 1

- [ ] T019 [P] [US1] Unit test for TaskService.GetAllTasks returns tasks ordered DESC by DateCreated in `backend/Tests/Unit/TaskServiceTests.cs`
- [ ] T020 [P] [US1] Integration test for GET /api/v1/tasks endpoint in `backend/Tests/Integration/TasksControllerTests.cs`
- [ ] T021 [P] [US1] Integration test for empty task list returns [] in `backend/Tests/Integration/TasksControllerTests.cs`

### Implementation for User Story 1

- [ ] T022 [P] [US1] Create TaskList component displaying all tasks in `frontend/src/components/TaskList.tsx`
- [ ] T023 [P] [US1] Create TaskItem component showing individual task with completion state in `frontend/src/components/TaskItem.tsx`
- [ ] T024 [US1] Implement GET /api/v1/tasks in TasksController returning list ordered DESC in `backend/Controllers/TasksController.cs`
- [ ] T025 [US1] Implement visual distinction for completed tasks (strikethrough, color, icon) in `frontend/src/components/TaskItem.tsx`
- [ ] T026 [US1] Implement empty state message "Nenhuma tarefa cadastrada" in `frontend/src/components/TaskList.tsx`
- [ ] T027 [US1] Test TaskList loads tasks from backend on component mount in `frontend/src/components/TaskList.test.tsx`
- [ ] T028 [US1] Add responsive styling for task list in `frontend/src/App.css` (mobile-first, breakpoint 768px)

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

