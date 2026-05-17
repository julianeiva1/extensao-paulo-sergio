# ADR: MVC Monolítico com Armazenamento em Memória

**Status**: Aprovada | **Data**: 2026-05-17 | **Relacionada a**: Constitution, Projeto TODO List

---

## Contexto

O projeto TODO List é uma atividade acadêmica de **Spec-Driven Development (SDD)** desenvolvida para a disciplina de Engenharia de Software (Aula 09). 

**Requisitos do Escopo:**
- Aplicação web simples para gestão de tarefas
- Funcionalidades: cadastrar, remover tarefas e adicionar lembretes
- Sem persistência de dados entre execuções (conforme enunciado)
- Publicação em servidor gratuito
- Documentação com MkDocs
- Time: pequeno (1-2 pessoas), com conhecimento prévio em ASP.NET Core

---

## Problema

Como estruturar uma aplicação web simples que:
1. Seja fácil de entender e manter?
2. Permita rápida implementação sem complexidade desnecessária?
3. Sirva como exemplo educacional de boas práticas (SDD)?
4. Atenda aos requisitos sem over-engineering?

---

## Alternativas Consideradas

### 1. API REST + Frontend Separado (Node.js/React)

**Vantagens:**
- Separação clara de responsabilidades
- Frontend pode ser desenvolvido independentemente
- Escalável para múltiplos clientes

**Desvantagens:**
- Complexidade adicional: 2 projetos, 2 linguagens, 2 deploys
- Over-engineering para requisito simples
- Maior curva de aprendizado
- Mais tempo de desenvolvimento e manutenção
- Debugging complexo entre camadas

**Decisão**: ❌ Rejeitada

### 2. Monolito em Node.js (Express)

**Vantagens:**
- Construir app web rápido
- JavaScript em todo o stack

**Desvantagens:**
- Menos tipagem forte (maior risco de bugs)
- Menos built-in security
- Validações devem ser feitas manualmente
- Menos adequado para ensino de boas práticas

**Decisão**: ❌ Rejeitada

### 3. Microserviços

**Desvantagens:**
- Totalmente fora de escopo
- Complexidade distribuída
- Infra adicional necessária
- Completely inappropriate for academic project

**Decisão**: ❌ Rejeitada

### 4. **MVC Monolítico em ASP.NET Core 8** ✅

**Vantagens:**
- ✅ 1 projeto, 1 linguagem, 1 deploy
- ✅ Tipagem forte (C#) reduz bugs
- ✅ MVC é padrão bem conhecido e educacional
- ✅ ASP.NET Core tem MVC built-in, menos dependências
- ✅ Validação server-side integrada
- ✅ Razor Templates permitem lógica de template em C#
- ✅ Performance excelente
- ✅ Segurança por padrão
- ✅ Pragmático para requisito simples

**Desvantagens:**
- Menos escalável se projeto crescesse significativamente (mas não é requisito)
- Coupling entre frontend e backend (adequado para projeto simples)

**Decisão**: ✅ Adotada

---

## Decisão

**Usar ASP.NET Core 8 MVC como arquitetura principal do projeto.**

### Arquitetura: MVC Monolítico

- **1 projeto** TodoListMvc.csproj
- **Controllers**: TasksController.cs (API de tarefas)
- **Models**: Tarefa.cs, Lembrete.cs (domínio)
- **Services**: RepositorioTarefas.cs (lógica de negócio)
- **Views**: Razor Templates (HTML + C#)
- **Responsivo**: Bootstrap 5 via CDN

### Storage: In-Memory

**Por quê?**
- Requisito: *"Armazenamento em memória (sem banco de dados persistente)"*
- Simplificação: Sem migrations, SQL, ORM ou DBMS
- Performance: RAM é mais rápido que disco
- Educação: Foco no MVC e boas práticas, não em SQL

**Estrutura de Dados:**
```csharp
private static List<Tarefa> tarefas = new();
```

**Implicação:** Dados são perdidos ao reiniciar a aplicação (conforme requisito).

### Framework: ASP.NET Core 8

**Razões Técnicas:**
- **Tipagem forte**: C# compilado previne muitos bugs em tempo de compilação
- **Built-in MVC**: Controllers, Actions, Model Binding sem dependências externas
- **Validação integrada**: Data Annotations, ModelState automático
- **Performance**: Um dos frameworks web mais rápidos disponíveis
- **Segurança**: CSRF tokens, sanitização, autorização integrada
- **Comunidade**: Bem suportado e documentado

**Alternativas:**
- Django (Python): time prefere C#, menos familiar
- Express (Node.js): menos tipagem, validação manual
- Laravel (PHP): menos familiar ao time

### Views: Razor Templates

**Por quê?**
- Lógica em C# (mesma linguagem do backend)
- Server-side rendering: sem JavaScript complexo
- Binding automático: `@Model.Titulo` no HTML
- Validação server-side integrada
- Sem framework JavaScript desnecessário

**Exemplo:**
```html
<form asp-action="Create" asp-controller="Tasks">
    <input asp-for="Titulo" required/>
    <span asp-validation-for="Titulo"></span>
</form>
```

### CSS: Bootstrap 5 via CDN

**Por quê?**
- Responsivo por padrão (mobile, tablet, desktop)
- Sem build tools (npm, webpack) necessários
- CDN garante download rápido
- Componentes prontos (buttons, forms, grid)
- Grande comunidade, muita documentação

**Alternativas Consideradas:**
- Tailwind CSS: Requer npm, build tools, mais complexo
- CSS puro: Responsividade manual, trabalhoso
- SCSS: Build tools adicionais

### Documentação: MkDocs

**Por quê?**
- Estática (publicável em GitHub Pages gratuitamente)
- Markdown versionável no repo
- Tema Material bonito por padrão
- Fácil de manter e atualizar
- Histórico no Git

---

## Consequências

### Positivas

✅ **Simplicidade**: 1 projeto, fácil de entender e manter
✅ **Rapidez**: Desenvolvimento rápido sem complexidade desnecessária
✅ **Segurança**: Tipagem forte e built-ins de segurança do ASP.NET
✅ **Performance**: Excelente performance para requisito simples
✅ **Educação**: Exemplifica bem padrão MVC e Spec-Driven Development
✅ **Deploy**: Docker + Render gratuito, deploy simples
✅ **Manutenção**: Um único codebase fácil de revisar e corrigir

### Trade-offs / Limitações

⚠️ **Escalabilidade Limitada**: Se crescer para milhões de usuários, monolito pode não escalar
   - **Nota**: Não é requisito atual

⚠️ **Dados Não Persistem**: Reiniciar app = perder dados
   - **Nota**: Conforme requisito ("sem banco de dados persistente")

⚠️ **Sem Reuso de API**: Se frontend precisasse de outro dispositivo, API estaria acoplada às Views
   - **Nota**: Fora de escopo; requisito é app web única

⚠️ **Frontend Limitado**: Sem JavaScript complexo (por design)
   - **Nota**: Alinha com princípio de Simplicity; futuras features podem adicionar JS se necessário

---

## Relação com a Constitution

Esta ADR é uma aplicação específica dos princípios da constitution:

### Pragmatism & Simplicity
✅ Monolito MVC é a solução *mais simples* que atende requisitos
✅ Sem abstrações desnecessárias (API, microserviços, etc.)

### Code Clarity
✅ C# com tipagem forte é claro
✅ MVC padrão: Controllers, Services, Models bem definidos
✅ Nomes em português descritivo

### UX & Acessibilidade
✅ Razor Templates permitem interface clara
✅ Bootstrap garante responsividade
✅ Server-side rendering evita complexidade no browser

### Testing Discipline
✅ Validações server-side testáveis
✅ Lógica de negócio isolada em Services
✅ Models com regras de validação

### Performance
✅ In-memory: respostas sub-200ms
✅ ASP.NET Core: performance excelente

### Living Documentation
✅ Esta ADR documenta decisões técnicas
✅ MkDocs explica stack e justificativas

---

## Próximos Passos

Se o projeto evoluir em escopo:

1. **Persistência em Banco**: Migrar in-memory para SQL (PostgreSQL/SQLite)
   - Manter Models e Business Logic
   - Substituir RepositorioTarefas.cs para usar Entity Framework

2. **Separação de Frontend**: Criar API REST separada se necessário multiple UIs
   - Controllers atuais viram WebAPI
   - Frontend React/Vue consome API

3. **Cache Distribuído**: Se performance degradar
   - Adicionar Redis para cache
   - Manter lógica em Services intacta

4. **Testes Automatizados**: Conforme projeto cresce
   - Unit tests para Services
   - Integration tests para Controllers
   - E2E tests com Selenium

---

## Referências

- [Constitution do Projeto](../constitution.md)
- [Plano de Arquitetura](../plan.md)
- [Especificação](../spec.md)
- [ASP.NET Core Docs](https://docs.microsoft.com/en-us/aspnet/core/)
- [Razor Template Syntax](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor)

---

**ADR Aprovada** ✅
