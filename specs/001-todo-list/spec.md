# Especificação: TODO List MVC

**Status**: Refatoração do projeto para arquitetura MVC em mono-repo  
**Data**: 2026-05-10  
**Versão**: 1.0  
**Escopo**: Atender requisitos exatos do enunciado (cadastrar, remover, adicionar lembretes)

## Resumo Executivo

Aplicação TODO List simples em ASP.NET Core MVC que permite usuários:
1. **Cadastrar tarefas** com título
2. **Remover tarefas** da lista
3. **Adicionar lembretes** às tarefas

Requisitos não-funcionais:
- Armazenamento em memória apenas (sem banco de dados)
- Dados perdidos ao reiniciar aplicação
- Publicável em servidor gratuito
- Arquitetura MVC pura

## User Stories

### US1: Cadastrar Nova Tarefa (Priority: P1 - MVP)

**Como** usuário  
**Quero** criar novas tarefas digitando um título  
**Para** organizar minhas atividades

**Critérios de Aceitação**:
1. Campo de entrada de texto para título
2. Botão "Adicionar" ou Enter para confirmar
3. Validação obrigatória: título não pode ser vazio
4. Validação obrigatória: título não pode ter apenas espaços
5. Nova tarefa aparece imediatamente na lista
6. Campo de entrada é limpo após sucesso
7. Mensagem de erro clara em português se falhar
8. Título aceita até 500 caracteres

---

### US2: Remover Tarefa (Priority: P1 - MVP)

**Como** usuário  
**Quero** deletar tarefas que não faz mais sentido  
**Para** manter minha lista organizada

**Critérios de Aceitação**:
1. Cada tarefa tem botão "Remover" ou ícone de lixeira
2. Clique em remover deleta tarefa imediatamente
3. Tarefa desaparece da tela
4. Sem diálogo de confirmação (UX simples)
5. Impossível recuperar tarefa deletada
6. Lista é atualizada automaticamente

---

### US3: Adicionar Lembrete à Tarefa (Priority: P2)

**Como** usuário  
**Quero** adicionar notas/lembretes às tarefas  
**Para** guardar detalhes importantes

**Critérios de Aceitação**:
1. Cada tarefa tem campo para adicionar lembrete
2. Campo expande ao clicar "Adicionar lembrete"
3. Lembrete é texto livre (sem limite rígido)
4. Múltiplos lembretes por tarefa são permitidos
5. Lembrete aparece associado visualmente à tarefa
6. Possibilidade de editar lembrete existente
7. Possibilidade de remover lembrete
8. Lembrete é opcional (não bloqueia criação de tarefa)

## Modelo de Dados

```csharp
public class Tarefa
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }           // obrigatório, max 500 chars
    public bool Concluida { get; set; }          // default: false
    public DateTime DataCriacao { get; set; }    // UTC
    public List<Lembrete> Lembretes { get; set; } = new();
}

public class Lembrete
{
    public Guid Id { get; set; }
    public string Texto { get; set; }            // texto livre
    public DateTime DataCriacao { get; set; }    // UTC
}
```

## Requisitos Não-Funcionais

| Requisito | Descrição |
|-----------|-----------|
| **Armazenamento** | Em-memória (List<Tarefa>) |
| **Persistência** | Nenhuma (dados perdidos ao reiniciar) |
| **Banco de Dados** | Não usar EF, SQLite, SQL |
| **Deployment** | Publicável em servidores gratuitos |
| **Performance** | <200ms para operações |
| **UX** | Responsivo, português brasileiro |
| **Browser** | Chrome, Firefox, Safari (2 versões recentes) |

## Fora do Escopo (MVP)

- ❌ Autenticação/usuários
- ❌ Persistência entre sessões
- ❌ Categorias/tags
- ❌ Prioridades
- ❌ Datas de vencimento
- ❌ Notificações
- ❌ Compartilhamento
- ❌ Múltiplos dispositivos

O usuário acessa a aplicação e vê a lista de todas as suas tarefas cadastradas. O sistema exibe claramente qual tarefa foi concluída e qual ainda precisa ser feita, permitindo ao usuário ter uma visão geral do que já fez e o que ainda falta.

**Why this priority**: Funcionalidade essencial (MVP). Sem poder visualizar tarefas, nenhuma outra funcionalidade agrega valor. É o ponto de entrada da aplicação.

**Independent Test**: Pode ser testado independentemente carregando a aplicação e verificando se as tarefas aparecem com a distinção clara entre concluídas e não concluídas.

**Acceptance Scenarios**:

1. **Given** que não há tarefas cadastradas, **When** o usuário acessa a lista, **Then** o sistema exibe uma mensagem indicando que não há tarefas
2. **Given** que existem tarefas cadastradas, **When** o usuário acessa a lista, **Then** o sistema exibe todas as tarefas
3. **Given** que existem tarefas concluídas e não concluídas, **When** o usuário visualiza a lista, **Then** o sistema distingue visualmente as tarefas concluídas das não concluídas (ex: riscado, cor diferente, ícone)

---

### User Story 2 - Criar Nova Tarefa com Validação (Priority: P1)

O usuário deseja adicionar uma nova tarefa à lista. O sistema oferece um campo para digitar o título da tarefa e um botão para confirmar a criação. Antes de adicionar, o sistema valida se o usuário digitou algo.

**Why this priority**: Funcionalidade essencial (MVP). Permite ao usuário começar a usar a aplicação, criando suas primeiras tarefas.

**Independent Test**: Pode ser testado independentemente criando uma tarefa com título válido e verificando se aparece na lista, e também tentando criar sem título para validar mensagem de erro.

**Acceptance Scenarios**:

1. **Given** que o usuário está na tela de criação, **When** digita um título válido e confirma, **Then** a tarefa é adicionada à lista como não concluída
2. **Given** que o usuário tenta criar uma tarefa sem título, **When** confirma a criação, **Then** o sistema exibe uma mensagem de erro clara (ex: "O título da tarefa não pode ser vazio")
3. **Given** que uma tarefa foi criada com sucesso, **When** o usuário retorna à lista, **Then** a nova tarefa aparece imediatamente no topo ou final da lista
4. **Given** que o usuário acabou de criar uma tarefa, **When** visualiza a lista, **Then** a tarefa está marcada como não concluída (não pode estar pré-marcada como concluída)

---

### User Story 3 - Marcar Tarefa como Concluída/Não Concluída (Priority: P2)

O usuário deseja marcar uma tarefa como concluída quando termina de executá-la, e poder desmarcar se mudar de ideia. O sistema deve refletir imediatamente essa mudança na lista.

**Why this priority**: Funcionalidade importante para rastreamento de progresso. Permite usuário visualizar o que já conquistou, mas não impede que outras tarefas sejam criadas ou editadas.

**Independent Test**: Pode ser testado independentemente alternando o estado de conclusão de uma tarefa e verificando que a mudança aparece imediatamente.

**Acceptance Scenarios**:

1. **Given** que existe uma tarefa não concluída, **When** o usuário marca como concluída, **Then** a tarefa aparece como concluída na lista (ex: riscada ou com cor diferentes)
2. **Given** que existe uma tarefa concluída, **When** o usuário desmarca a conclusão, **Then** a tarefa volta ao estado não concluído
3. **Given** que o usuário marca/desmarca uma tarefa, **When** a ação é confirmada, **Then** a mudança é refletida imediatamente na lista (sem necessidade de recarregar)

---

### User Story 4 - Editar Título de Tarefa Existente (Priority: P2)

O usuário deseja corrigir ou atualizar o título de uma tarefa existente sem precisar deletá-la e criar novamente. O sistema oferece uma forma de editar o título e valida se o novo título é válido.

**Why this priority**: Funcionalidade importante para correção e melhorias. Complementa a criação e visualização, mas outras tarefas podem ser criadas/concluídas sem isso.

**Independent Test**: Pode ser testado independentemente editando uma tarefa com novo título válido e também tentando deixar vazia para validar.

**Acceptance Scenarios**:

1. **Given** que existe uma tarefa cadastrada, **When** o usuário edita o título com um valor válido, **Then** o novo título é exibido na lista
2. **Given** que o usuário tenta editar uma tarefa deixando o título vazio, **When** confirma a edição, **Then** o sistema impede a alteração e exibe mensagem de erro (ex: "O título da tarefa não pode ser vazio")
3. **Given** que o usuário edita uma tarefa com sucesso, **When** visualiza a lista, **Then** o novo título aparece imediatamente
4. **Given** que uma tarefa está marcada como concluída, **When** o usuário edita o título, **Then** a tarefa mantém seu estado de conclusão após a edição

---

### User Story 5 - Remover Tarefa (Priority: P2)

O usuário deseja deletar uma tarefa que não faz mais sentido ou que foi criada por acidente. O sistema remove a tarefa da lista imediatamente.

**Why this priority**: Funcionalidade desejável para limpeza e organização. Complementa outras funcionalidades mas não é bloqueadora do MVP.

**Independent Test**: Pode ser testado independentemente deletando uma tarefa e verificando que não aparece mais na lista.

**Acceptance Scenarios**:

1. **Given** que existe uma tarefa cadastrada, **When** o usuário remove a tarefa, **Then** ela não aparece mais na lista
2. **Given** que o usuário confirma a remoção de uma tarefa, **When** visualiza a lista, **Then** a tarefa foi removida imediatamente
3. **Given** que existem múltiplas tarefas na lista, **When** o usuário remove uma tarefa, **Then** as demais tarefas permanecem na lista sem mudanças

---

### Edge Cases

- O que acontece se o usuário tenta criar uma tarefa com apenas espaços em branco? (Deve ser tratado como vazio)
- O que acontece se o usuário edita uma tarefa rapidamente consecutivas vezes? (Todas as edições devem ser refletidas)
- O que acontece se o usuário tenta criar/editar tarefa com título muito longo? (Sistema deve aceitar ou indicar limite)
- O que acontece com a lista se o usuário faz múltiplas operações rapidamente (criar, editar, remover)? (Deve refletir todas as mudanças corretamente)

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Sistema DEVE exibir todas as tarefas cadastradas em uma lista
- **FR-002**: Sistema DEVE permitir ao usuário criar uma nova tarefa informando um título
- **FR-003**: Sistema DEVE validar que o título da tarefa não está vazio antes de confirmar a criação
- **FR-004**: Sistema DEVE exibir mensagem de erro clara quando usuário tenta criar tarefa sem título (mensagem: "O título da tarefa não pode ser vazio")
- **FR-005**: Sistema DEVE exibir mensagem de erro clara quando usuário tenta salvar edição sem título (mensagem: "O título da tarefa não pode ser vazio")
- **FR-006**: Sistema DEVE permitir ao usuário marcar uma tarefa como concluída
- **FR-007**: Sistema DEVE permitir ao usuário desmarcar uma tarefa concluída, voltando ao estado não concluído
- **FR-008**: Sistema DEVE exibir visualmente a distinção entre tarefas concluídas e não concluídas
- **FR-009**: Sistema DEVE permitir ao usuário editar o título de uma tarefa existente
- **FR-010**: Sistema DEVE validar que o novo título não está vazio durante a edição
- **FR-011**: Sistema DEVE permitir ao usuário remover uma tarefa da lista
- **FR-012**: Sistema DEVE refletir imediatamente todas as mudanças (criar, editar, marcar como concluída, remover) na lista sem necessidade de recarregar

### Key Entities

- **Task/Tarefa**: Representa uma atividade a ser executada
  - Attributes: título (string, obrigatório), estado de conclusão (booleano, padrão: false), data de criação (timestamp)
  - Relações: nenhuma (escopo MVP simples)

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Usuário consegue visualizar a lista de tarefas imediatamente ao abrir a aplicação
- **SC-002**: Usuário consegue criar uma nova tarefa e vê-la aparecer na lista em menos de 1 segundo
- **SC-003**: Usuário consegue distinguir visualmente entre tarefas concluídas e não concluídas sem ambiguidade
- **SC-004**: Todas as validações (título vazio) exibem mensagens de erro claras em português
- **SC-005**: Operações de edição, remoção e marcação como concluída refletem-se na lista imediatamente (menos de 500ms)
- **SC-006**: A aplicação funciona corretamente em navegadores modernos (Chrome, Firefox, Safari, Edge)
- **SC-007**: A aplicação é responsiva e utilizável em telas pequenas (celular) e grandes (notebook)
- **SC-008**: Usuário consegue executar todas as 5 user stories (criar, visualizar, concluir, editar, remover) sem recarregar a página

## Assumptions

- Autenticação de usuários está fora do escopo (cada sessão é um usuário anônimo ou single-user)
- Persistência de dados: Assume-se que o sistema persiste tarefas (em banco de dados, localStorage, ou outro meio) para sobreviver a recarregamentos
- Usuário tem conexão com a internet estável (conforme contexto do projeto)
- Interface é baseada em web (não especifica desktop, mobile app, etc)
- Tarefas não têm prioridades, datas ou categorias (escopo definido como fora)
- Não há sincronização em nuvem ou compartilhamento de tarefas entre usuários
- O título de uma tarefa pode conter qualquer caractere Unicode exceto null
- Título máximo razoável: 500 caracteres (limite prático para UX)
- A ordem das tarefas é mantida conforme inserção (FIFO) ou pode ser ordenada por estado (concluídas vs não concluídas) - decidir durante planning

