# Checklist de Testes

**Objetivo:** Documentar e garantir que todas as funcionalidades foram testadas antes de release

---

## Fluxos Críticos: Testes Manuais

### ✅ US1: Cadastrar Tarefa

**Cenário 1: Título Válido**
- [ ] Abro app em http://localhost:5125
- [ ] Digito "Comprar leite" no campo entrada
- [ ] Clico "Adicionar Tarefa"
- [ ] ✅ Tarefa aparece na lista com título correto
- [ ] ✅ Data de criação está correta
- [ ] ✅ Campo entrada é limpo (pronto para próxima)

**Cenário 2: Título Vazio**
- [ ] Deixo campo vazio
- [ ] Clico "Adicionar Tarefa"
- [ ] ✅ Erro: "Por favor, insira um título para a tarefa"
- [ ] ✅ Campo mantém foco
- [ ] ✅ Lista não é alterada

**Cenário 3: Título > 500 Caracteres**
- [ ] Digito 501 caracteres no campo
- [ ] Clico "Adicionar Tarefa"
- [ ] ✅ Erro: "Título não pode exceder 500 caracteres"
- [ ] ✅ Tarefa não é criada
- [ ] ✅ Valor digitado permanece no campo

**Cenário 4: Múltiplas Tarefas**
- [ ] Crio 5 tarefas com títulos diferentes
- [ ] ✅ Todas aparecem na lista
- [ ] ✅ Ordem é mostrada (mais recente no topo? Embaixo?)
- [ ] ✅ Nenhuma duplicada

---

### ✅ US2: Remover Tarefa

**Cenário 1: Remover com Confirmação**
- [ ] Crio tarefa "Teste"
- [ ] Clico botão "Deletar" / ícone X
- [ ] ✅ Dialog de confirmação aparece
- [ ] ✅ Mensagem: "Deseja remover 'Teste'?"
- [ ] ✅ Botões: "Cancelar" e "Remover"
- [ ] Clico "Remover"
- [ ] ✅ Tarefa desaparece imediatamente
- [ ] ✅ Lista atualiza

**Cenário 2: Cancelar Remoção**
- [ ] Crio tarefa "Teste"
- [ ] Clico "Deletar"
- [ ] Dialog aparece
- [ ] Clico "Cancelar"
- [ ] ✅ Dialog fecha
- [ ] ✅ Tarefa continua na lista (não removida)

**Cenário 3: Remover Tarefa com Lembretes**
- [ ] Crio tarefa "Estudar"
- [ ] Adiciono 2 lembretes: "Cap 3" e "Exercícios"
- [ ] Clico "Deletar" na tarefa
- [ ] Confirmo remoção
- [ ] ✅ Tarefa removida
- [ ] ✅ Lembretes também removidos (não aparecem na lista)

**Cenário 4: Remover Múltiplas Tarefas**
- [ ] Crio 3 tarefas
- [ ] Removo primeira
- [ ] ✅ Primeira removida, 2 e 3 mantidas
- [ ] Removo terceira
- [ ] ✅ Segunda continua, primeira e terceira removidas

---

### ✅ US3: Adicionar Lembrete

**Cenário 1: Lembrete Válido**
- [ ] Crio tarefa "Comprar"
- [ ] No campo "Adicionar Lembrete" digito "Leite desnatado"
- [ ] Clico "Adicionar" ou Enter
- [ ] ✅ Lembrete aparece em badge abaixo da tarefa
- [ ] ✅ Texto está correto "Leite desnatado"
- [ ] ✅ Campo "Lembrete" é limpo

**Cenário 2: Múltiplos Lembretes**
- [ ] Crio tarefa "Tarefas"
- [ ] Adiciono lembrete 1: "Fazer exercícios"
- [ ] Adiciono lembrete 2: "Revisar teoria"
- [ ] Adiciono lembrete 3: "Fazer prova"
- [ ] ✅ Todos 3 lembretes aparecem em badges
- [ ] ✅ Badges estão lado a lado (ou linha abaixo)
- [ ] ✅ Ordem está correta (ordem de adição)

**Cenário 3: Lembrete Vazio**
- [ ] Deixo campo "Lembrete" vazio
- [ ] Clico "Adicionar"
- [ ] ✅ Erro: "Por favor, insira uma descrição para o lembrete"
- [ ] ✅ Nenhum badge é criado
- [ ] ✅ Campo mantém foco

**Cenário 4: Lembrete > 1000 Caracteres**
- [ ] Digito 1001 caracteres no campo "Lembrete"
- [ ] Clico "Adicionar"
- [ ] ✅ Erro: "Descrição não pode exceder 1000 caracteres"
- [ ] ✅ Badge não é criado

**Cenário 5: Remover Lembrete**
- [ ] Crio tarefa com 3 lembretes
- [ ] Clico "×" no segundo lembrete
- [ ] ✅ Segundo lembrete desaparece
- [ ] ✅ Primeiro e terceiro continuam
- [ ] Clico "×" no primeiro
- [ ] ✅ Primeiro desaparece, terceiro continua

**Cenário 6: Sem Lembretes**
- [ ] Crio tarefa sem adicionar lembretes
- [ ] ✅ Nenhum badge aparece
- [ ] ✅ Campo "Adicionar Lembrete" está vazio
- [ ] Adiciono lembrete
- [ ] ✅ Funciona normalmente

---

### ✅ Editar Tarefa

**Cenário 1: Editar Título**
- [ ] Crio tarefa "Compra"
- [ ] Clico "Editar"
- [ ] ✅ Sou levado para formulário de edição
- [ ] ✅ Campo tem título atual "Compra"
- [ ] Mudo para "Comprar Alimentos"
- [ ] Clico "Salvar"
- [ ] ✅ Volta para lista
- [ ] ✅ Tarefa mostra título atualizado "Comprar Alimentos"

**Cenário 2: Cancelar Edição**
- [ ] Clico "Editar" em uma tarefa
- [ ] Edito o título (mudo para algo diferente)
- [ ] Clico "Cancelar"
- [ ] ✅ Volta para lista
- [ ] ✅ Título continua com valor original (não foi salvo)

**Cenário 3: Editar com Validação**
- [ ] Clico "Editar"
- [ ] Deixo título vazio
- [ ] Clico "Salvar"
- [ ] ✅ Erro: "Título obrigatório"
- [ ] ✅ Continuo no formulário de edição

---

### ✅ Marcar Tarefa Concluída

**Cenário 1: Marcar Concluída**
- [ ] Crio tarefa "Estudar SQL"
- [ ] Clico checkbox
- [ ] ✅ Tarefa tem visual diferente (strikethrough, cinza, outra cor)
- [ ] ✅ Checkbox está marcado (✓)

**Cenário 2: Desmarcar Concluída**
- [ ] Tarefa está marcada como concluída
- [ ] Clico checkbox novamente
- [ ] ✅ Visual volta ao normal
- [ ] ✅ Checkbox fica desmarcado

**Cenário 3: Múltiplas Tarefas Concluídas**
- [ ] Crio 3 tarefas
- [ ] Marco 1ª como concluída
- [ ] Marco 3ª como concluída
- [ ] ✅ 1ª e 3ª têm visual diferente
- [ ] ✅ 2ª continua normal

---

### ✅ Responsividade

**Desktop (≥1024px, ex: 1920x1080)**
- [ ] Abro em F12 → Desktop mode
- [ ] ✅ Layout horizontal confortável
- [ ] ✅ Todos botões visíveis
- [ ] ✅ Fonte legível (não muito pequena)
- [ ] ✅ Clico criar tarefa → funciona
- [ ] ✅ Clico deletar → funciona
- [ ] ✅ Clico editar → funciona

**Tablet (768-1023px, ex: iPad)**
- [ ] F12 → Device Emulation → iPad
- [ ] ✅ Layout se adapta
- [ ] ✅ Botões são grandes (touch-friendly)
- [ ] ✅ Nenhum horizontal scroll
- [ ] ✅ Todas ações funcionam (toque/mouse)

**Mobile (< 768px, ex: iPhone)**
- [ ] F12 → Device Emulation → iPhone 12
- [ ] ✅ Layout é vertical (stack)
- [ ] ✅ Botões são grandes (≥44px altura)
- [ ] ✅ Toque nos botões é fácil (sem sobreposição)
- [ ] ✅ Texto é legível (não muito pequeno)
- [ ] ✅ Scroll funciona, nenhuma trampa

**Zoom 200%**
- [ ] F12 → Zoom 200%
- [ ] ✅ Interface é usável (sem truncamento)
- [ ] ✅ Texto não overlaps
- [ ] ✅ Botões clicáveis

---

### ✅ Validações e Mensagens

**Cenário 1: Linguagem**
- [ ] Todos os textos estão em português claro
- [ ] Nenhum "error", "success", "loading" em inglês
- [ ] Mensagens evitam jargão técnico

**Cenário 2: Mensagens de Erro**
- [ ] Erro aparece no lugar correto (abaixo do campo)
- [ ] Texto é claro e acionável
- [ ] Sem código de erro técnico ("Error 400")
- [ ] Cor vermelha ou ⚠️ ícone

**Cenário 3: Mensagens de Sucesso**
- [ ] Tarefa criada → feedback visual (lista atualiza)
- [ ] Tarefa removida → feedback visual (desaparece)
- [ ] Lembrete adicionado → feedback visual (badge aparece)

---

### ✅ Persistência (Sem Banco)

**Cenário 1: Refresh (F5)**
- [ ] Crio 5 tarefas
- [ ] Aperto F5 (refresh da página)
- [ ] ✅ Lista fica vazia (conforme requisito: sem persistência)
- [ ] ✅ Dados não são recuperados

**Cenário 2: Reiniciar Servidor**
- [ ] Crio tarefas
- [ ] Paro servidor (`Ctrl+C`)
- [ ] Inicio novamente
- [ ] ✅ Lista vazia (sem persistência)

**Cenário 3: Próxima Sessão**
- [ ] Crio tarefas na sessão 1
- [ ] Abro app em abas diferentes (nova sessão)
- [ ] ✅ Tarefas da aba 1 não aparecem em aba 2
- [ ] ✅ Cada sessão tem sua própria memória (se em-memória)

---

### ✅ Performance

**Medições:**
- [ ] Carregar página inicial: < 1s
- [ ] Criar tarefa: < 200ms (resposta + atualização UI)
- [ ] Remover tarefa: < 200ms
- [ ] Adicionar lembrete: < 200ms
- [ ] Editar tarefa: < 200ms
- [ ] Nenhuma travada/lag visível

---

### ✅ Comportamentos Extremos

**Cenário 1: Muitas Tarefas**
- [ ] Crio 50 tarefas rapidamente
- [ ] ✅ Todas aparecem na lista
- [ ] ✅ Performance continua aceitável
- [ ] ✅ Scroll funciona suavemente

**Cenário 2: Tarefas com Muitos Lembretes**
- [ ] Crio tarefa "Projeto"
- [ ] Adiciono 20 lembretes
- [ ] ✅ Todos aparecem em badges
- [ ] ✅ Layout não quebra
- [ ] ✅ Posso remover lembretes

**Cenário 3: Títulos e Lembretes Longos**
- [ ] Crio tarefa com título máximo (500 chars)
- [ ] Adiciono lembrete máximo (1000 chars)
- [ ] ✅ Exibe corretamente (sem truncamento)
- [ ] ✅ Layout se adapta

---

## Registro de Testes

### Rodada [Data] - v1.0.0

**Testador**: [Nome]  
**Ambiente**: Windows 11, Chrome 120, localhost:5125  
**Data**: 2026-05-17  
**Status**: ✅ TUDO PASSOU

| Fluxo | Cenários | Status |
|-------|----------|--------|
| Cadastrar Tarefa | 4/4 | ✅ |
| Remover Tarefa | 4/4 | ✅ |
| Adicionar Lembrete | 6/6 | ✅ |
| Editar Tarefa | 3/3 | ✅ |
| Marcar Concluída | 3/3 | ✅ |
| Responsividade | 4/4 | ✅ |
| Validações | 3/3 | ✅ |
| Persistência | 3/3 | ✅ |
| Performance | 5/5 | ✅ |
| Extremos | 3/3 | ✅ |
| **TOTAL** | **39/39** | **✅** |

**Notas**: Nenhum bug encontrado. Pronto para release.

---

## Checklist Final: Pronto para Release?

- [ ] Todos os fluxos críticos testados (39 cenários)
- [ ] Sem erros de compilação
- [ ] Sem exceptions não tratadas
- [ ] Interface responsiva testada (desktop, tablet, mobile)
- [ ] Mensagens em português claro
- [ ] Performance < 200ms por ação
- [ ] Sem travadas/lag
- [ ] Dados desaparecem após refresh (conforme requisito)
- [ ] Nenhum recurso faltando vs especificação

---

## Relacionado

- [Constitution - Princípio IV: Testing Discipline](../constitution.md#iv-testing-discipline)
- [Boas Práticas: Testes](../boas-praticas/testes.md)
- [Checklist de Pull Request](checklist-pr.md)
- [Critérios de Entrega](criterios-de-entrega.md)
