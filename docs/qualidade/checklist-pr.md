# Checklist de Pull Request

**Objetivo:** Garantir que toda mudança atende padrões de qualidade antes do merge

---

## Antes de Abrir PR: Checklist do Autor

- [ ] **Requisito atendido**: A mudança implementa exatamente o requisito especificado?
- [ ] **Sem duplicação**: Não há lógica duplicada que poderia ser reutilizada?
- [ ] **Responsabilidade única**: Funções/classes têm apenas uma razão para mudar?
- [ ] **Nomes descritivos**: Variáveis, funções, classes têm nomes claros?
- [ ] **Sem hardcode**: Valores "mágicos" foram movidos para constantes?
- [ ] **Lógica isolada**: Regras de negócio estão separadas da interface?
- [ ] **Testes passaram**: Testes manuais ou automatizados foram executados com sucesso?
- [ ] **Sem erros de compilação**: Código compila sem warnings?
- [ ] **Sem código comentado**: Linhas de código comentado foram removidas?
- [ ] **Documentação atualizada**: MkDocs foi atualizado se necessário?
- [ ] **Constitution respeitada**: As mudanças seguem os princípios da constitution?

---

## Criando o Pull Request

### Título

**Bom:**
- `feat: adiciona validação de lembretes`
- `fix: remove tarefa sem deletar lembretes`
- `docs: atualiza guia de performance`
- `refactor: simplifica RepositorioTarefas`

**Evitar:**
- `fix` (vago)
- `WIP` (não pronto)
- `bugfix123` (sem contexto)

### Descrição

```markdown
## Mudanças
- Adiciona validação de comprimento em lembrete
- Lembrete vazio agora retorna erro com mensagem clara
- Atualiza testes manuais correspondentes

## Contexto
Closes #123 (referência ao issue)
Relação: Constitution Princípio III (UX)

## Como Testar
1. Crio tarefa
2. Tento adicionar lembrete vazio
3. Esperado: erro "Lembrete não pode ser vazio"

## Mudanças de Comportamento
- Antes: Lembrete vazio era aceito
- Depois: Lembrete vazio é rejeitado com mensagem
```

---

## Durante a Revisão: Checklist do Revisor

### 1. Funcionalidade

- [ ] A implementação resolve o problema descrito?
- [ ] Casos extremos foram considerados (vazio, muito longo, nulo)?
- [ ] Não há regressões óbvias?
- [ ] Comportamento está correto (não apenas compila)?

### 2. Qualidade do Código

- [ ] Código segue padrões do projeto?
- [ ] Sem duplicação de lógica?
- [ ] Nomes são descritivos?
- [ ] Complexidade não aumentou desnecessariamente?
- [ ] Sem imports/variáveis não usados?

### 3. Testes

- [ ] Testes foram executados com sucesso?
- [ ] Cases críticos foram testados (sucesso + erro)?
- [ ] Não há regressões?
- [ ] Testes refletem requisito?

### 4. Documentação

- [ ] MkDocs foi atualizado se necessário?
- [ ] XML comments em métodos públicos?
- [ ] Lógica complexa tem explicação?
- [ ] Nenhuma documentação obsoleta?

### 5. Princípios

- [ ] Respeita Constitution?
- [ ] Alinha com ADRs?
- [ ] Não traz débito técnico?
- [ ] Código é simples e claro?

---

## Comentários: Feedback Construtivo

### Bom Feedback

```
Na linha 45, essa validação deveria estar em RepositorioTarefas, 
não no Controller. Assim outros clients também se beneficiam.

Referência: Constitution Princípio II (Code Clarity) - lógica de negócio isolada.

Sugestão:
```csharp
// Em Services/RepositorioTarefas.cs
private bool ValidarLembrete(string descricao)
{
    return !string.IsNullOrEmpty(descricao) && descricao.Length <= 1000;
}
```
"""

```

### Feedback a Evitar

```
❌ "Por que você fez assim?" (confrontacional)
❌ "Isso está errado" (sem explicação)
❌ "Use pattern X" (sem contexto)

✅ "Considerou usar pattern X? Assim seria mais testável."
✅ "Essa lógica seria melhor em Services para ser reutilizável."
✅ "Pode explicar essa decisão?"
```

---

## Estados e Ações

### Quando Aprovar (Approve)

- [x] Checklist completo
- [x] Sem mudanças solicitadas pending
- [x] Código está pronto para produção

### Quando Solicitar Mudanças (Request Changes)

- [ ] Problema de funcionalidade crítico
- [ ] Violação de princípios da constitution
- [ ] Regressão identificada
- [ ] Teste falhando

### Quando Comentar (Comment)

- [ ] Sugestão de melhoria (não obrigatória)
- [ ] Pergunta educacional
- [ ] Discussão de trade-offs

---

## Processo de Review

```
1. Autor abre PR com descrição clara
2. Autor valida seu checklist
3. GitHub/Sistema notifica revisor
4. Revisor: "Reviso agora" ou "Reviso em X horas"
5. Revisor executa checklist
6. Se problemas → Request Changes + comentários
7. Autor responde e refatora
8. Revisor valida novamente
9. Se OK → Approve
10. Merge automático ou manual
11. Branch deletada
12. Issue fechada (se aplicável)
```

---

## SLA (Service Level Agreement)

| Tipo de PR | Tempo Máximo | Urgência |
|-----------|-------------|----------|
| Feature | 24h | Normal |
| Hotfix | 2h | Alta |
| Docs | 48h | Baixa |
| Refactor | 24h | Normal |

---

## Exemplo Completo: Bom PR

```markdown
## Título
fix: remove tarefa sem deletar lembretes associados

## Descrição
Closes #87

## Mudanças
- Atualiza RepositorioTarefas.RemoverTarefa()
- Agora deleta lembretes da tarefa quando tarefa é removida
- Adiciona testes de regressão

## Contexto
Bug: Ao remover tarefa, lembretes ficavam órfãos

## Como Testar
1. Crio tarefa "Estudar"
2. Adiciono lembrete "Cap 3"
3. Removo tarefa
4. Verifico: lembrete também removido ✅

## Mudanças de Comportamento
Antes: Remover tarefa → lembretes continuavam na lista
Depois: Remover tarefa → lembretes também removidos

## Checklist (Autor)
- [x] Requisito atendido
- [x] Sem duplicação
- [x] Testes executados (manual)
- [x] Sem compilação warning
- [x] Constitution respeitada
```

---

## Exemplo: PR a Evitar

```markdown
## Título
fix

## Descrição
(vazia)

## Código
- Mistura 5 features diferentes
- Remove linhas aleatoriamente
- Adiciona console.log para debug

## Checklist (Autor)
- [ ] Requisito atendido (indefinido)
- [ ] Sem duplicação (não validou)
- [ ] Testes executados (não testou)
- [ ] Sem compilation warning (não compilou)
- [ ] Constitution respeitada (não leu)
```

---

## Relacionado

- [Constitution - Princípio V: Code Review](../constitution.md#v-code-review)
- [Revisão de Código](../boas-praticas/revisao-de-codigo.md)
- [Checklist de Testes](checklist-testes.md)
- [Critérios de Entrega](criterios-de-entrega.md)
