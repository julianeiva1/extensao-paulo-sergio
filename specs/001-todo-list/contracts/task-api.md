# API Contracts: TODO List

**Feature**: Gerenciamento de Tarefas em TODO List  
**Date**: 2026-05-10  
**Status**: Contracts Defined

## Overview

Este documento define os contratos de API REST que o frontend consome do backend ASP.NET Core. Todos os endpoints retornam JSON, com mensagens de erro em português.

---

## Base Configuration

**Base URL**: `http://localhost:5000/api/v1` (desenvolvimento)  
**Content-Type**: `application/json`  
**Charset**: UTF-8  
**Error Format**: `{ "error": "Mensagem em português", "code": "ERROR_CODE" }`  

---

## Endpoints

### 1. GET /tasks - Listar Todas as Tarefas

**Description**: Retorna a lista completa de tarefas, ordenadas por data de criação (mais recentes primeiro).

**HTTP Method**: GET  
**Path**: `/api/v1/tasks`  
**Authentication**: Não requerida (v1)

**Query Parameters**: Nenhum (v1 - sem paginação)

**Response Success**:
- **Status Code**: 200 OK
- **Content-Type**: `application/json`
- **Body**:
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "title": "Comprar leite",
    "isCompleted": false,
    "dateCreated": "2026-05-10T14:30:00Z"
  },
  {
    "id": "6ba7b810-9dad-11d1-80b4-00c04fd430c8",
    "title": "Fazer exercício",
    "isCompleted": true,
    "dateCreated": "2026-05-10T14:00:00Z"
  }
]
```

**Response Empty**:
```json
[]
```

**Performance SLA**: < 1 segundo para até 1000 tarefas

**Example (cURL)**:
```bash
curl -X GET "http://localhost:5000/api/v1/tasks" \
  -H "Content-Type: application/json"
```

**Example (JavaScript/fetch)**:
```typescript
const response = await fetch("http://localhost:5000/api/v1/tasks");
const tasks = await response.json();
console.log(tasks);
```

---

### 2. POST /tasks - Criar Nova Tarefa

**Description**: Cria uma nova tarefa com o título fornecido. Inicialmente marcada como não concluída.

**HTTP Method**: POST  
**Path**: `/api/v1/tasks`  
**Authentication**: Não requerida (v1)  
**Content-Type**: `application/json`

**Request Body**:
```json
{
  "title": "Nova tarefa"
}
```

**Validation**:
- `title` é obrigatório
- `title` não pode ser vazio ou apenas espaços em branco
- `title` máximo 500 caracteres
- `title` é automaticamente trimmed (espaços ao início/fim removidos)

**Response Success**:
- **Status Code**: 201 Created
- **Body**:
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440001",
  "title": "Nova tarefa",
  "isCompleted": false,
  "dateCreated": "2026-05-10T15:00:00Z"
}
```

**Response Errors**:

**400 Bad Request** - Título vazio:
```json
{
  "error": "O título da tarefa não pode ser vazio",
  "code": "TITLE_REQUIRED"
}
```

**400 Bad Request** - Título muito longo:
```json
{
  "error": "O título da tarefa deve ter no máximo 500 caracteres",
  "code": "TITLE_TOO_LONG"
}
```

**Performance SLA**: < 500ms

**Example (cURL)**:
```bash
curl -X POST "http://localhost:5000/api/v1/tasks" \
  -H "Content-Type: application/json" \
  -d '{"title":"Estudar React"}'
```

**Example (JavaScript)**:
```typescript
const response = await fetch("http://localhost:5000/api/v1/tasks", {
  method: "POST",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify({ title: "Estudar React" })
});
const newTask = await response.json();
```

---

### 3. PUT /tasks/{id} - Editar Título da Tarefa

**Description**: Atualiza o título de uma tarefa existente. Estado de conclusão é preservado.

**HTTP Method**: PUT  
**Path**: `/api/v1/tasks/{id}`  
**Authentication**: Não requerida (v1)  
**Content-Type**: `application/json`

**Path Parameters**:
- `id` (Guid): Identificador único da tarefa

**Request Body**:
```json
{
  "title": "Título atualizado"
}
```

**Validation**: Mesma do POST (obrigatório, não vazio, max 500 chars, auto-trim)

**Response Success**:
- **Status Code**: 200 OK
- **Body**:
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "title": "Título atualizado",
  "isCompleted": false,
  "dateCreated": "2026-05-10T14:30:00Z"
}
```

**Response Errors**:

**404 Not Found** - Tarefa não existe:
```json
{
  "error": "Tarefa não encontrada",
  "code": "TASK_NOT_FOUND"
}
```

**400 Bad Request** - Título inválido:
```json
{
  "error": "O título da tarefa não pode ser vazio",
  "code": "TITLE_REQUIRED"
}
```

**Performance SLA**: < 500ms

**Example (cURL)**:
```bash
curl -X PUT "http://localhost:5000/api/v1/tasks/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json" \
  -d '{"title":"Novo título"}'
```

---

### 4. PATCH /tasks/{id}/complete - Marcar Tarefa como Concluída

**Description**: Marca uma tarefa como concluída (IsCompleted = true). Sem request body.

**HTTP Method**: PATCH  
**Path**: `/api/v1/tasks/{id}/complete`  
**Authentication**: Não requerida (v1)

**Request Body**: Vazio (nenhum)

**Response Success**:
- **Status Code**: 200 OK
- **Body**:
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "title": "Comprar leite",
  "isCompleted": true,
  "dateCreated": "2026-05-10T14:30:00Z"
}
```

**Response Errors**:

**404 Not Found**:
```json
{
  "error": "Tarefa não encontrada",
  "code": "TASK_NOT_FOUND"
}
```

**Performance SLA**: < 500ms

**Idempotency**: Chamando múltiplas vezes não causa problemas (idempotente)

**Example**:
```bash
curl -X PATCH "http://localhost:5000/api/v1/tasks/550e8400-e29b-41d4-a716-446655440000/complete"
```

---

### 5. PATCH /tasks/{id}/incomplete - Marcar Tarefa como Não Concluída

**Description**: Marca uma tarefa como não concluída (IsCompleted = false).

**HTTP Method**: PATCH  
**Path**: `/api/v1/tasks/{id}/incomplete`  
**Authentication**: Não requerida (v1)

**Request Body**: Vazio (nenhum)

**Response Success**:
- **Status Code**: 200 OK
- **Body**:
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "title": "Comprar leite",
  "isCompleted": false,
  "dateCreated": "2026-05-10T14:30:00Z"
}
```

**Response Errors**: Mesmo do `/complete`

**Performance SLA**: < 500ms

**Example**:
```bash
curl -X PATCH "http://localhost:5000/api/v1/tasks/550e8400-e29b-41d4-a716-446655440000/incomplete"
```

---

### 6. DELETE /tasks/{id} - Remover Tarefa

**Description**: Remove uma tarefa do banco de dados. A tarefa não aparece mais na lista.

**HTTP Method**: DELETE  
**Path**: `/api/v1/tasks/{id}`  
**Authentication**: Não requerida (v1)

**Request Body**: Vazio (nenhum)

**Response Success**:
- **Status Code**: 204 No Content
- **Body**: Vazio (nenhum)

**Response Errors**:

**404 Not Found**:
```json
{
  "error": "Tarefa não encontrada",
  "code": "TASK_NOT_FOUND"
}
```

**Performance SLA**: < 500ms

**Idempotency**: Chamando múltiplas vezes retorna 204 na primeira, 404 nas subsequentes (esperado)

**Example**:
```bash
curl -X DELETE "http://localhost:5000/api/v1/tasks/550e8400-e29b-41d4-a716-446655440000"
# Response: HTTP 204 No Content (sem body)
```

---

## Error Handling

### Error Response Format

Todos os erros retornam um objeto JSON estruturado:

```json
{
  "error": "Descrição do erro em português",
  "code": "ERROR_CODE",
  "details": {} // Opcional, conforme contexto
}
```

### Error Codes

| Code | HTTP Status | Descrição |
|------|-------------|-----------|
| `TITLE_REQUIRED` | 400 | Título é obrigatório |
| `TITLE_TOO_LONG` | 400 | Título excede 500 caracteres |
| `TASK_NOT_FOUND` | 404 | Tarefa com ID não existe |
| `INTERNAL_ERROR` | 500 | Erro interno do servidor |

### Error Messages (em Português)

- "O título da tarefa não pode ser vazio"
- "O título da tarefa deve ter no máximo 500 caracteres"
- "Tarefa não encontrada"
- "Erro ao processar requisição. Tente novamente."

---

## Response Data Structures

### TaskResponse

Estrutura padrão retornada em GET, POST, PUT, PATCH:

```json
{
  "id": "string (Guid)",
  "title": "string (1-500 chars)",
  "isCompleted": "boolean",
  "dateCreated": "string (ISO 8601 UTC)"
}
```

**Field Descriptions**:
- `id`: Identificador único (UUID v4)
- `title`: Título da tarefa (trimmed)
- `isCompleted`: true = concluída, false = pendente
- `dateCreated`: Timestamp UTC (formato: `2026-05-10T15:30:45.123Z`)

---

## API Workflow Examples

### Criar e Marcar como Concluída

```
1. POST /tasks { "title": "Comprar leite" }
   → 201 { "id": "...", "title": "Comprar leite", "isCompleted": false, ... }

2. PATCH /tasks/{id}/complete
   → 200 { "id": "...", "title": "Comprar leite", "isCompleted": true, ... }

3. GET /tasks
   → 200 [{ "id": "...", "title": "Comprar leite", "isCompleted": true, ... }]
```

### Editar e Remover

```
1. PUT /tasks/{id} { "title": "Novo título" }
   → 200 { "id": "...", "title": "Novo título", ... }

2. DELETE /tasks/{id}
   → 204 (No Content)

3. GET /tasks
   → 200 [] (tarefa removida)
```

---

## CORS & Security (v1)

**CORS**: Habilitado para `http://localhost:3000` em desenvolvimento

```csharp
builder.Services.AddCors(options => {
    options.AddPolicy("DevelopmentPolicy",
        policy => {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
```

**Rate Limiting**: Não implementado na v1 (added para v2+)

**Authentication**: Não implementado na v1 (single-user)

