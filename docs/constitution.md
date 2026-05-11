# Constitution: Princípios do Projeto

**Versão**: 1.0.0 | **Aprovado**: 2026-05-10

## Propósito

Este documento estabelece os princípios governantes do projeto TODO List, garantindo consistência, qualidade e manutenibilidade em todas as decisões técnicas e de design.

## Princípios Centrais

### I. Pragmatism & Simplicity
Priorizar soluções simples que atendem requisitos, sobre complexidade arquitetural desnecessária.

**Implementação:**
- ✅ MVC simples vs Backend+Frontend separado
- ✅ In-Memory vs Banco de dados
- ✅ Bootstrap 5 via CDN vs Framework de build

### II. Code Clarity
Código deve ser claro, bem nomeado e fácil de entender para qualquer desenvolvedor.

**Implementação:**
- ✅ Nomes em português descritivos
- ✅ Controllers com responsabilidades definidas
- ✅ Services com lógica de negócio isolada
- ✅ XML comments em métodos públicos

### III. User Experience Focus
Interface deve ser simples, responsiva e em português claro.

**Implementação:**
- ✅ Layout responsivo (Bootstrap Grid)
- ✅ Mensagens de erro claras
- ✅ Validações server-side
- ✅ Interface testada em mobile/tablet/desktop

### IV. Testing Discipline
Todas as funcionalidades devem ser testadas antes de release.

**Implementação:**
- ✅ Testes manuais contra acceptance criteria
- ✅ Checklist de validação para cada feature
- ✅ Validação de requisitos não-funcionais (responsivo, performance)

### V. Code Review
Todo código passa por revisão antes de integração.

**Implementação:**
- ✅ Pull requests obrigatórios
- ✅ Validação de compilação
- ✅ Testes executados
- ✅ Aderência à constitution verificada

## Decisões Arquiteturais Principais

### Arquitetura: MVC Monolítica

**Por quê?**
- Requisito: "aplicação web simples"
- Simplificação: 1 projeto, 1 linguagem, deploy único
- Educação: Padrão MVC bem conhecido

**Alternativas Rejeitadas:**
- ❌ API + Frontend separado (over-engineering)
- ❌ Microserviços (fora de escopo)
- ❌ Monolito em Node.js (menos segurança)

### Storage: In-Memory

**Por quê?**
- Requisito: "sem persistência entre execuções"
- Simplificação: sem migrations, SQL ou ORM
- Performance: RAM é mais rápido

**Alternativas Rejeitadas:**
- ❌ SQLite/SQL (viola requisito)
- ❌ NoSQL (desnecessário)

### Framework: ASP.NET Core 8

**Por quê?**
- Tipagem forte (segurança)
- Built-in MVC (menos dependências)
- Performance (rápido)

**Alternativas Rejeitadas:**
- ❌ Node.js (menos segurança)
- ❌ Django (linguagem diferente do grupo)

### Views: Razor Templates

**Por quê?**
- Tudo em C# (menos contexto switching)
- Validação server-side
- Sem JavaScript complexo

**Alternativas Rejeitadas:**
- ❌ Vue/React (frontend framework desnecessário)
- ❌ Blazor (complexidade adicionada)

### CSS: Bootstrap 5 via CDN

**Por quê?**
- Sem build tools adicionais
- Responsivo por padrão
- Reduz complexidade

**Alternativas Rejeitadas:**
- ❌ Tailwind (requer npm)
- ❌ CSS puro (responsividade manual)
- ❌ SCSS (build tools necessários)

### Documentação: MkDocs

**Por quê?**
- Estática (GitHub Pages gratuito)
- Markdown versionável no repo
- Fácil de manter

**Alternativas Rejeitadas:**
- ❌ Confluence (pago, fora do repo)
- ❌ Wiki GitHub (sem temas)
- ❌ Sphinx (Python, complexidade)

## Quality Standards

| Aspecto | Padrão |
|---------|--------|
| **Code Review** | Obrigatório para todo merge |
| **Tests** | Manual: 100% das features testadas |
| **Performance** | <200ms por ação |
| **Responsivo** | Mobile, Tablet, Desktop |
| **Linguagem** | Português Brasileiro |
| **Validações** | Server-side obrigatório |
| **Erros** | Mensagens claras em PT |

## UX Standards

- ✅ Interface em português
- ✅ Botões e links com texto claro
- ✅ Validações com feedback imediato
- ✅ Responsivo em todos tamanhos
- ✅ Sem diálogos confusos
- ✅ Confirmação para ações destrutivas

## Performance Standards

- ✅ Carregar página: <1s
- ✅ Criar/Editar/Remover: <200ms
- ✅ Sem carregamentos desnecessários
- ✅ Sem N+1 queries (não aplicável, in-memory)

## Development Workflow

1. **Especificar**: User stories em português
2. **Planejar**: Arquitetura, stack, tarefas
3. **Implementar**: Fase a fase, commit incrementais
4. **Testar**: Manual contra acceptance criteria
5. **Validar**: Checklist de qualidade
6. **Documentar**: MkDocs + comments no código
7. **Deploy**: Servidor gratuito com link verificado

## Compliance Checklist

Antes de cada release, validar:

- [ ] Todas as user stories implementadas
- [ ] Testes manuais passaram
- [ ] Interface responsiva testada
- [ ] Mensagens em português
- [ ] Sem erros de compilação
- [ ] Code review realizada
- [ ] Constitution verificada
- [ ] MkDocs atualizada
- [ ] Links funcionando (GitHub, Deploy, Docs)

---

**Constitution aprovada e ratificada pelo time** ✅
