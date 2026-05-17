# Critérios de Entrega

**Objetivo:** Definir padrões de qualidade que toda entrega (release) deve atender

---

## Requisitos Funcionais Implementados

- [ ] **US1: Cadastrar Tarefa**
  - [ ] Campo entrada com max 500 caracteres
  - [ ] Validação server-side obrigatória
  - [ ] Mensagem de erro clara em PT
  - [ ] Tarefa criada → lista atualiza
  - [ ] Campo entrada é limpo após sucesso

- [ ] **US2: Remover Tarefa**
  - [ ] Botão "Deletar" em cada tarefa
  - [ ] Dialog de confirmação obrigatório
  - [ ] Lembretes associados também removidos
  - [ ] Lista atualiza imediatamente
  - [ ] Mensagem de confirmação

- [ ] **US3: Adicionar Lembrete**
  - [ ] Campo para adicionar lembrete em cada tarefa
  - [ ] Max 1000 caracteres
  - [ ] Validação server-side
  - [ ] Lembrete aparece em badge
  - [ ] Botão "×" para remover lembrete
  - [ ] Posso adicionar múltiplos lembretes

---

## Requisitos Não-Funcionais

### Armazenamento

- [ ] **In-Memory**: Dados em `List<Tarefa>` (sem banco de dados persistente)
- [ ] **Sem Persistência**: Dados perdidos ao reiniciar app
- [ ] **Sem Migração**: Nenhum SQL, ORM ou setup de banco

### Performance

- [ ] **Tempo de Resposta**: < 200ms por ação (criar, remover, adicionar lembrete)
- [ ] **Carregamento**: Página inicial < 1s
- [ ] **Sem Lags**: Interface não trava ao clicar rápido
- [ ] **Scroll Suave**: Nenhum travamento ao scrollar

### Responsividade

- [ ] **Desktop (≥1024px)**: Layout confortável, todos elementos visíveis
- [ ] **Tablet (768-1023px)**: Layout se adapta, botões grandes
- [ ] **Mobile (<768px)**: Layout vertical, touch-friendly
- [ ] **Zoom 200%**: Interface usável, sem truncamento
- [ ] **Sem Scroll Horizontal**: Layout não requer scroll horizontal

### UX & Acessibilidade

- [ ] **Linguagem**: Tudo em português brasileiro claro
- [ ] **Mensagens**: Claras, acionáveis, sem jargão técnico
- [ ] **Labels**: Campos têm labels associados
- [ ] **Validação**: Feedback imediato de erros
- [ ] **Confirmação**: Ações destrutivas (delete) exigem confirmação
- [ ] **Contraste**: Texto legível em qualquer background
- [ ] **Teclado**: Tab funciona, campos navegáveis
- [ ] **Leitores de Tela**: Básico suportado (labels, ARIA)

### Documentação

- [ ] **Código**: XML comments em métodos públicos
- [ ] **MkDocs**: Constitution atualizada
- [ ] **MkDocs**: Boas práticas documentadas
- [ ] **MkDocs**: ADRs documentadas
- [ ] **MkDocs**: Checklists de qualidade documentados
- [ ] **Testes**: Cenários testados registrados no MkDocs
- [ ] **Links**: Todos funcionam (GitHub, Deploy, Docs)

### Segurança & Validação

- [ ] **Server-Side**: Validações obrigatórias no servidor
- [ ] **Sem Hardcode**: Valores "mágicos" são constantes
- [ ] **Sem SQL Injection**: Nenhuma concatenação SQL (não aplicável, in-memory)
- [ ] **Mensagens**: Sem informações técnicas (stack trace) para usuário
- [ ] **Tratamento de Erro**: Exceptions não travam a app

---

## Qualidade de Código

### Code Review

- [ ] **PR Checklist**: Todos os pontos verificados
- [ ] **Aprovação**: Pelo menos 1 reviewer aprovou
- [ ] **Feedback**: Comentários foram respondidos
- [ ] **Padrões**: Código segue padrões do projeto
- [ ] **Constitution**: Código respeita princípios

### Clareza

- [ ] **Nomes**: Variáveis, funções, classes têm nomes descritivos
- [ ] **Responsabilidade Única**: Funções fazem 1 coisa bem
- [ ] **DRY**: Sem duplicação de lógica
- [ ] **Comments**: Comments explicam "por quê", não "o quê"
- [ ] **Estrutura**: Código está organizado em pastas/classes lógicas

### Testes

- [ ] **Testes Manuais**: 39/39 cenários passaram (veja checklist)
- [ ] **Testes Automatizados**: Regras de negócio testadas (se aplicável)
- [ ] **Regressão**: Nenhum bug conhecido aberto
- [ ] **Performance**: Testes de performance passaram

---

## Compilação & Build

- [ ] **Sem Erros**: Compila sem erros
- [ ] **Sem Warnings**: Nenhum warning de compilação
- [ ] **Dependências**: Todas as dependências estão listadas
- [ ] **Versão**: Versão está documentada em arquivo
- [ ] **Build Reproducível**: `dotnet build` produz output consistente

---

## Publicação & Deploy

- [ ] **Servidor**: App publicada em servidor (ex: Render, Heroku)
- [ ] **URL Funcional**: Link acessa aplicação sem erros
- [ ] **HTTPS**: Certificado SSL/TLS válido
- [ ] **Redirect**: URL redireciona corretamente (ex: www vs sem www)
- [ ] **Healthcheck**: App responde a requisições (não 500 error)

---

## Documentação & Entrega

- [ ] **README**: Instruções de como rodar localmente
- [ ] **MkDocs**: Site gerado corretamente
- [ ] **GitHub Pages**: Docs publicadas e acessíveis
- [ ] **ENTREGA.md**: Links verificados e funcionando
- [ ] **Version Tag**: Git tem tag para release (ex: v1.0.0)
- [ ] **Changelog**: Mudanças documentadas para usuário

---

## Requisitos Específicos (Projeto TODO List)

### Funcionalidades Adicionais (Bônus)

- [ ] **Editar Tarefa**: Posso mudar título de tarefa criada
- [ ] **Marcar Concluída**: Checkbox para marcar tarefa como concluída
- [ ] **Data de Criação**: Tarefa mostra quando foi criada

### Integração

- [ ] **Git**: Histórico de commits é limpo e descritivo
- [ ] **GitHub**: Repositório é público e acessível
- [ ] **Actions**: CI/CD passa (se configurado)

### Requisitos de Escopo (Acadêmico)

- [ ] **SDD**: Projeto segue Spec-Driven Development
- [ ] **Constitution**: Princípios aplicados em prática
- [ ] **ADRs**: Decisões técnicas documentadas
- [ ] **Checklists**: Qualidade documentada e verificável
- [ ] **MkDocs**: Boas práticas educacionais expostas

---

## Checklist Final: Pronto para Entregar?

### Funcionalidade

- [ ] Todas 3 user stories implementadas
- [ ] Funcionalidades adicionais (editar, concluir) funcionam
- [ ] 39/39 cenários de teste passaram

### Qualidade

- [ ] Code review completada
- [ ] Sem erros de compilação
- [ ] Sem warnings
- [ ] Constitution respeitada
- [ ] Documentação atualizada

### Deploy

- [ ] App publicada em servidor
- [ ] URL funciona sem erros
- [ ] Performance está dentro dos limites
- [ ] Dados não persistem entre reinicializações (conforme requisito)

### Documentação

- [ ] MkDocs gerado e publicado
- [ ] GitHub acessível
- [ ] ENTREGA.md tem todos os links
- [ ] README tem instruções

### Académico (SDD)

- [ ] Spec foi seguida
- [ ] Plan foi executado
- [ ] Tasks foram completadas
- [ ] Constitution foi respeitada
- [ ] Boas práticas foram aplicadas

---

## Status de Entrega

| Aspecto | Status | Data | Responsável |
|---------|--------|------|-------------|
| Funcionalidade | ✅ | 2026-05-17 | [Nome] |
| Qualidade | ✅ | 2026-05-17 | [Nome] |
| Deploy | ✅ | 2026-05-17 | [Nome] |
| Documentação | ✅ | 2026-05-17 | [Nome] |
| Acadêmico | ✅ | 2026-05-17 | [Nome] |
| **ENTREGA PRONTA** | **✅** | **2026-05-17** | **[Nome]** |

---

## Pós-Entrega

### Feedback & Iteração

- [ ] Feedback recebido? Qual?
- [ ] Bugs identificados? Quais?
- [ ] Melhorias sugeridas?

### Próximos Passos (Futuro)

Se projeto continuar:

- [ ] Persistência em banco (PostgreSQL)
- [ ] API REST separada
- [ ] Frontend em React/Vue
- [ ] Testes automatizados (xUnit)
- [ ] CI/CD pipeline

---

## Referências

- [Constitution](../constitution.md)
- [ADRs](../decisoes-arquiteturais/)
- [Boas Práticas](../boas-praticas/)
- [Checklist de Testes](checklist-testes.md)
- [Checklist de PR](checklist-pr.md)
- [Especificação](../spec.md)
- [Plano](../plan.md)
