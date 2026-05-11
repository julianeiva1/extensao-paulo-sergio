# Especificação: TODO List MVC

**Status**: Completo ✅
**Data**: 2026-05-10  
**Versão**: 1.0

## Resumo Executivo

Aplicação TODO List em ASP.NET Core MVC que permite usuários:
1. **Cadastrar tarefas** com título
2. **Remover tarefas** da lista
3. **Adicionar lembretes** às tarefas

Requisitos não-funcionais:
- Armazenamento em memória apenas (sem banco de dados)
- Dados perdidos ao reiniciar aplicação
- Interface responsiva
- Mensagens em português

## User Stories

### US1: Cadastrar Nova Tarefa

**Como** usuário  
**Quero** criar novas tarefas digitando um título  
**Para** organizar minhas atividades

#### Critérios de Aceitação
- ✅ Campo de entrada de texto para título
- ✅ Botão "Criar" para confirmar
- ✅ Validação: título não pode ser vazio
- ✅ Validação: título não pode ter apenas espaços
- ✅ Nova tarefa aparece imediatamente na lista
- ✅ Campo é limpo após sucesso
- ✅ Mensagens de erro em português
- ✅ Título aceita até 500 caracteres

#### Cenários de Aceitação
1. Criar com título válido → Tarefa aparece na lista
2. Criar com título vazio → Erro: "O título não pode ser vazio"
3. Criar com 500 caracteres → Sucesso
4. Criar com 501+ caracteres → Erro: "Máximo 500 caracteres"

---

### US2: Remover Tarefa

**Como** usuário  
**Quero** deletar tarefas que não faz mais sentido  
**Para** manter minha lista organizada

#### Critérios de Aceitação
- ✅ Cada tarefa tem botão "Deletar"
- ✅ Clique em deletar remove tarefa imediatamente
- ✅ Tarefa desaparece da tela
- ✅ Confirmação obrigatória (para evitar cliques acidentais)
- ✅ Impossível recuperar tarefa deletada
- ✅ Lista é atualizada automaticamente

#### Cenários de Aceitação
1. Confirmar delete → Tarefa removida
2. Cancelar delete → Tarefa permanece
3. Delete múltiplas tarefas → Todas removidas corretamente

---

### US3: Adicionar Lembrete à Tarefa

**Como** usuário  
**Quero** adicionar notas/lembretes às tarefas  
**Para** guardar detalhes importantes

#### Critérios de Aceitação
- ✅ Cada tarefa tem campo para adicionar lembrete
- ✅ Lembrete é texto livre (sem limite rígido)
- ✅ Múltiplos lembretes por tarefa são permitidos
- ✅ Lembrete aparece em badge abaixo da tarefa
- ✅ Possibilidade de remover lembrete
- ✅ Lembrete é opcional (não bloqueia tarefa)

#### Cenários de Aceitação
1. Adicionar lembrete a tarefa → Aparece em badge
2. Adicionar múltiplos lembretes → Todos aparecem
3. Remover lembrete → Desaparece da tela
4. Remover tarefa → Todos os lembretes removidos

---

### Funcionalidades Adicionais

#### Marcar Tarefa como Concluída
- Checkbox em cada tarefa
- Concluída: strikethrough + cor muted
- Possibilidade de desmarcar
- Mudança imediata na interface

#### Editar Tarefa
- Botão "Editar" em cada tarefa
- Formulário pré-preenchido com título atual
- Mesmas validações de criação
- Redirect para lista após sucesso

## Modelo de Dados

### Entidade: Tarefa
```csharp
public class Tarefa
{
    public Guid Id { get; set; }                    // PK, gerado automaticamente
    public string Titulo { get; set; }              // Obrigatório, max 500 chars
    public bool Concluida { get; set; }             // Default: false
    public DateTime DataCriacao { get; set; }       // UTC, automático
    public DateTime DataModificacao { get; set set; } // UTC, atualizado em edições
    public List<Lembrete> Lembretes { get; set; }   // Coleção
}
```

### Entidade: Lembrete
```csharp
public class Lembrete
{
    public Guid Id { get; set; }                    // PK, gerado automaticamente
    public string Texto { get; set; }               // Obrigatório
    public DateTime DataCriacao { get; set; }       // UTC, automático
}
```

## Requisitos Não-Funcionais

| Requisito | Descrição | Status |
|-----------|-----------|--------|
| **Armazenamento** | Em-memória (List<Tarefa>) | ✅ |
| **Persistência** | Nenhuma entre sessões | ✅ |
| **Performance** | <200ms por operação | ✅ |
| **Responsivo** | Mobile, Tablet, Desktop | ✅ |
| **Idioma** | Português Brasileiro | ✅ |
| **Browser** | Chrome, Firefox, Safari | ✅ |
| **Deployment** | Servidor gratuito | ⏳ |

## Fora do Escopo (MVP)

- ❌ Autenticação/Usuários
- ❌ Persistência em banco de dados
- ❌ Categorias/Tags
- ❌ Prioridades
- ❌ Datas de vencimento
- ❌ Notificações
- ❌ Compartilhamento
- ❌ Integração com calendários

---

**Especificação validada contra Constitution ✅**
