# Research: TODO List Implementation

**Date**: 2026-05-10  
**Feature**: Gerenciamento de Tarefas em TODO List  
**Status**: Completed - All ambiguities resolved

## Research Summary

Esta seção documenta as ambiguidades identificadas na especificação e as decisões de design tomadas para resolvê-las, resultando em um plano de implementação claro e viável.

---

## Ambiguidade 1: Ordenação de Tarefas na Lista

### Problem Statement
A especificação deixa em aberto a ordem em que tarefas aparecem na lista: "A ordem das tarefas é mantida conforme inserção (FIFO) ou pode ser ordenada por estado (concluídas vs não concluídas) - decidir durante planning".

Esta decisão impacta:
- **Backend**: Query SQL (ORDER BY)
- **Frontend**: Posição de novos items (topo vs final)
- **Tests**: Validação de ordem em testes de aceitação

### Decision: FIFO com Tarefas Mais Recentes no Topo

**Chosen**: Tarefas mais recentes aparecem no topo (`ORDER BY DateCreated DESC`)

### Rationale

1. **Padrão da Indústria**: Grandes TODO lists (Todoist, Microsoft To Do, Google Tasks) usam "Most Recent First"
2. **Melhor UX**: Usuário vê imediatamente o que acabou de criar
3. **Simples de Implementar**: Query única `ORDER BY DateCreated DESC`, sem sorting complexo
4. **Alinha com Princípio VI (Pragmatism)**: Solução direta sem over-engineering
5. **Compatível com Responsivo**: Funciona bem em celular (scroll natural)

### Implementation Details

**Backend (C#)**:
```csharp
var tasks = await _context.Tasks
    .OrderByDescending(t => t.DateCreated)
    .ToListAsync();
```

**Frontend (React)**:
- Novos items adicionados ao topo (`unshift` em state)
- Reflexo imediato (otimistic update)

**Tests**:
- Validar que GET /api/v1/tasks retorna tarefas em ordem DESC
- Validar que nova tarefa aparece no topo da lista

### Alternatives Considered & Rejected

| Opção | Motivo Rejeição |
|-------|-----------------|
| **Agrupar por Estado** (não concluídas primeiro) | Adiciona complexidade de sorting duplo; requer lógica condicional; padrão menos comum |
| **Permitir Reordenação Manual** | Out of scope para MVP; requer UI complexa (drag-drop); não necessário para primeiros usuários |
| **Ordem Alfabética** | Menos intuitivo para tarefas diárias; usuário precisa procurar por data |

---

## Ambiguidade 2: Limite de Caracteres para Título

### Problem Statement
Edge case identifica que "O que acontece se o usuário tenta criar/editar tarefa com título muito longo?" Specification não define limite máximo.

Esta decisão impacta:
- **Database**: `varchar(n)` ou `text`
- **Backend**: Validação `Title.Length <= X`
- **Frontend**: Contador visual, feedback UX
- **Tests**: Edge cases com tamanho limite

### Decision: Máximo 500 Caracteres

**Chosen**: `Title` é `varchar(500)` com validação em backend e feedback em frontend

### Rationale

1. **Limite Prático**: 500 caracteres é suficiente para descrição detalhada de uma tarefa (típica: 50-150 caracteres)
2. **UX em Mobile**: Evita text wrapping excessivo em telas pequenas (celular com 320px)
3. **Performance**: Índices em Title coluna funcionam bem até 500 chars
4. **Consistente**: Padrão em sistemas similares (Todoist: 500 chars, Microsoft To Do: ilimitado mas ~1000 na prática)
5. **Fácil Validação**: Regra simples sem lógica complexa

### Implementation Details

**Backend (C#)**:
```csharp
public class Task
{
    [StringLength(500, ErrorMessage = "O título deve ter no máximo 500 caracteres")]
    public string Title { get; set; }
}
```

**Frontend (React)**:
- Campo `<textarea>` com `maxLength={500}`
- Contador opcional: "0/500 caracteres"
- Validação ao blur/submit

**Tests**:
- Teste com 500 caracteres: deve aceitar
- Teste com 501 caracteres: deve rejeitar com erro "O título deve ter no máximo 500 caracteres"

### Edge Case: Caracteres Especiais
**Observation**: 500 caracteres inclui Unicode (emojis, acentos, caracteres especiais)
**Implementation**: JavaScript `.length` e C# `string.Length` contam unidades de código (válido para nosso caso)

---

## Ambiguidade 3: Validação de Espaços em Branco

### Problem Statement
Edge case: "O que acontece se o usuário tenta criar uma tarefa com apenas espaços em branco?" Specification assume que espaço vazio = erro, mas não clarifica se " " (um espaço) ou "  \t  " (espaços/tabs) devem ser rejeitados.

Esta decisão impacta:
- **Backend**: Validação `IsNullOrWhiteSpace` vs `IsNullOrEmpty`
- **Frontend**: Trim automático durante digitação
- **UX**: Feedback visual e mensagens de erro
- **Tests**: Casos como " ", "\t", "\n"

### Decision: Trim Automático + Validação Rigorosa

**Chosen**: (1) Trim automático na entrada; (2) Rejeitar se vazio ou só espaços em branco

### Rationale

1. **Evita Tarefas Inúteis**: " " não é uma tarefa válida
2. **Melhora UX**: Trim automático remove erros acidentais do usuário (espaço antes/depois)
3. **Alinha com Regra de Negócio**: "Uma tarefa deve ter obrigatoriamente um título" implica título com conteúdo, não espaços
4. **Implementação Simples**: `IsNullOrWhiteSpace` em C#, `trim()` + check em TypeScript

### Implementation Details

**Backend (C#)**:
```csharp
if (string.IsNullOrWhiteSpace(title))
    throw new ValidationException("O título da tarefa não pode ser vazio");

// Trim e armazena
task.Title = title.Trim();
```

**Frontend (TypeScript)**:
```typescript
const handleSubmit = (title: string) => {
  const trimmed = title.trim();
  if (!trimmed) {
    setError("O título da tarefa não pode ser vazio");
    return;
  }
  // Criar tarefa
};
```

**Tests**:
- Teste com `" "` (espaço único): deve rejeitar
- Teste com `"  \t\n  "` (espaços/tabs/newlines): deve rejeitar
- Teste com `"  Tarefa Válida  "` (espaços ao redor): deve aceitar como "Tarefa Válida"

---

## Ambiguidade 4: Responsividade & Breakpoints

### Problem Statement
Specification menciona "O sistema deve funcionar bem em notebook e celular (responsivo)" mas não define breakpoints específicos ou comportamentos.

Esta decisão impacta:
- **Frontend**: CSS media queries, layout flexível
- **Tests**: Validar em múltiplos tamanhos
- **UX**: Botões maiores em mobile, densidade de informação

### Decision: Mobile-First com 2 Breakpoints Principais

**Chosen**: 
- Mobile (< 768px): Single column, botões grandes
- Desktop (≥ 768px): Otimizado para desktop

### Rationale

1. **Mobile-First**: Maioria dos usuários em smartphones (contexto do projeto: usuários IFES)
2. **Simples de Implementar**: 1 media query é suficiente para este MVP
3. **Padrão Atual**: Bootstrap/Tailwind usam 768px como breakpoint `md` padrão
4. **Testável**: Fácil validar com Browser DevTools

### Implementation Details

**Frontend (CSS/Tailwind)**:
```css
/* Mobile: 100% width, stacked */
@media (max-width: 767px) {
  .task-item { padding: 12px; font-size: 14px; }
  .button { padding: 12px 16px; font-size: 16px; } /* Bigger touch target */
}

/* Desktop: Constrained width, horizontal layout */
@media (min-width: 768px) {
  .task-item { padding: 8px; font-size: 14px; }
  .button { padding: 8px 12px; font-size: 14px; }
}
```

**Tests**:
- Chrome DevTools: 320px (iPhone SE) - validar layout
- Chrome DevTools: 768px (iPad) - validar transição
- Chrome DevTools: 1200px (Desktop) - validar desktop

---

## Summary Table: Decisions Made

| Ambiguidade | Decision | Impact | Confidence |
|-------------|----------|--------|------------|
| Ordenação | FIFO Desc | Backend query, Frontend state | 🟢 High |
| Limite Título | 500 chars | DB schema, Validações | 🟢 High |
| Espaços em Branco | Trim + Rejeitar | Backend/Frontend validation | 🟢 High |
| Responsividade | Mobile-first 768px breakpoint | CSS, Testes | 🟢 High |

**All ambiguities resolved** ✅

---

## Testing Strategy (Informed by Research)

### Unit Tests (Backend)
- Validação de título vazio
- Validação de espaços em branco
- Validação de limite 500 chars
- Toggle de estado (complete/incomplete)

### Integration Tests (API)
- GET /tasks retorna ordem DESC por DateCreated
- POST /tasks trim título antes de armazenar
- PUT /tasks rejeita título vazio

### UI Tests (Frontend)
- Botão "Adicionar" desabilitado se título vazio
- Contador de caracteres atualiza (opcional)
- Layout responsivo em 320px, 768px, 1200px

### Manual Tests (Before Release)
- Criar, listar, editar, concluir, remover tarefas
- Testar em navegadores (Chrome, Firefox, Safari, Edge)
- Testar em celular real (se possível)

