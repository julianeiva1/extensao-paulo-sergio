# Atividade Acadêmica: Spec-Driven Development

## 📚 Contexto

Esta atividade faz parte da disciplina de **Engenharia de Software** (Aula 09), que aborda a metodologia de **Spec-Driven Development (SDD)** como prática de qualidade no desenvolvimento de software.

## 🎓 Objetivo da Disciplina

Aplicar na prática os conceitos de SDD:
- Especificação clara antes de codificar
- Arquitetura pragmática baseada em requisitos
- Implementação orientada por tarefas
- Documentação versionada com código
- Validação contra requisitos (testes)

## 📋 Requisitos Obrigatórios do Enunciado

1. ✅ **Arquitetura MVC**: Aplicação web MVC monolítica
2. ✅ **Mono-repo**: Código, specs e docs em um repositório
3. ✅ **Sem banco persistente**: Armazenamento em memória apenas
4. ✅ **Funcionalidades**:
   - Cadastrar tarefas
   - Remover tarefas
   - Adicionar lembretes
5. ✅ **Constitution**: Documento justificando escolhas técnicas
6. ✅ **Documentação**: MkDocs versionada
7. ✅ **GitHub**: Todo código publicado
8. ✅ **Deploy**: Publicado em servidor gratuito
9. ✅ **ENTREGA.md**: Arquivo com links de entrega

## 🔄 Processo de Desenvolvimento SDD

### 1. Especificação (Phase: Spec)
```
Artefatos criados:
├── specs/001-todo-list/spec.md         # User stories
├── specs/001-todo-list/data-model.md   # Modelo de dados
└── specs/001-todo-list/checklists/     # Validação de qualidade
```

**Ações realizadas:**
- Escrita de 3 user stories (US1, US2, US3)
- Definição do modelo: Tarefa + Lembrete
- Critérios de aceitação testáveis
- Validação contra constitution (checklist)

### 2. Planejamento (Phase: Plan)
```
Artefatos criados:
├── specs/001-todo-list/plan.md         # Decisões arquiteturais
├── specs/001-todo-list/research.md     # Pesquisa técnica
└── .specify/memory/constitution.md     # Princípios do projeto
```

**Ações realizadas:**
- Justificativa WCAG para MVC + Memória
- Alternativas avaliadas
- Stack técnico escolhido
- Alinhamento com constitution

### 3. Tarefas (Phase: Tasks)
```
Artefatos criados:
└── specs/001-todo-list/tasks.md        # Tarefas incrementais
```

**Ações realizadas:**
- Divisão em 42 tarefas
- Agrupadas em 6 fases:
  - Phase 1: Project Setup
  - Phase 2: Models & Infrastructure
  - Phase 3: Repository & Business Logic
  - Phase 4: Controller Implementation
  - Phase 5: Views & Frontend
  - Phase 6: Testing & Validation

### 4. Implementação (Phase: Implement)
```
Código criado:
└── TodoListMvc/
    ├── Controllers/TasksController.cs
    ├── Models/Tarefa.cs + Lembrete.cs
    ├── Services/RepositorioTarefas.cs
    ├── Views/Tasks/Index.cshtml + Create.cshtml + Edit.cshtml
    ├── Views/Shared/_Layout.cshtml
    └── wwwroot/css/site.css
```

**Ações realizadas:**
- Implementação fase por fase
- Code review (compilação sem erros)
- Testes manuais iterativos
- Documentação inline (XML comments)

### 5. Documentação (Phase: Docs)
```
Documentação MkDocs:
├── mkdocs.yml
└── docs/
    ├── index.md                 # Visão geral
    ├── aula-09-sdd.md          # Esta página
    ├── spec.md                 # Especificação
    ├── plan.md                 # Plano
    ├── constitution.md         # Constitution
    ├── tasks.md                # Tasks
    └── deploy.md               # Deploy
```

**Ações realizadas:**
- Documentação de processo SDD
- Especificação formatada para MkDocs
- Guias de execução e deploy
- Links versionados

### 6. Validação (Phase: Validate)
```
Checklists:
├── specs/001-todo-list/checklists/requirements.md
└── Testes manuais contra acceptance criteria
```

**Ações realizadas:**
- Validação de requirements.md (100% OK)
- Testes de cada user story
- Testes de validação
- Testes responsivos

## 📊 Métricas do Projeto

| Métrica | Valor |
|---------|-------|
| **User Stories** | 3 (criação, deleção, lembretes) |
| **Tarefas** | 42 (organizadas em 6 fases) |
| **Models** | 2 (Tarefa, Lembrete) |
| **Controllers** | 1 (TasksController com 9 ações) |
| **Views** | 3 (Index, Create, Edit) |
| **Métodos Repositório** | 8 (CRUD + lembretes) |
| **Linhas de Código** | ~1500 (sem contar comentários) |
| **Linhas de Testes** | Manual (42 casos testados) |
| **Linhas de Documentação** | ~500 (MkDocs + specs) |

## 🎯 Decisões Arquiteturais Principais

### Por quê MVC e não API + Frontend?
- **Simplicidade**: 1 projeto, 1 linguagem, deploy único
- **Adequação**: Requisitos simples não justificam separação
- **Educação**: Demonstra padrão MVC real em produção
- **Deployment**: Servidores gratuitos suportam melhor

### Por quê em-memória e não banco de dados?
- **Requisito**: Enunciado: "sem persistência entre execuções"
- **Simplificação**: Sem migrations, SQL ou ORM
- **Performance**: RAM é mais rápido que disco
- **Escalação**: Pattern Repository permite trocar depois

### Por quê Bootstrap 5 e não Tailwind?
- **Simplicidade**: CDN, sem build tools
- **Responsive**: Mobile-first por padrão
- **Conhecimento**: Bootstrap é mais comum em educação
- **Sem dependências**: Não requer npm no projeto .NET

## 💡 Lições Aprendidas

### Sucessos
✅ Especificação clara facilitou implementação
✅ Tarefas incrementais evitaram retrabalho
✅ Constitution guiou decisões consistentes
✅ MVC simples provou ser adequado ao escopo
✅ Documentação versionada no repo evitou desincronização

### Divergências / Melhorias Futuras
⚠️ Primeiramente tentou-se arquitetura API+React (over-engineering)
⚠️ Avisos de null reference em Views (ignorar para app acadêmica)
⚠️ Sem autenticação (enunciado permitiu, poderia ser adicionado depois)
⚠️ Dados em memória (adequado ao requisito, mas limita escalação)

**Valor da atividade**: Demonstrou importância de SDD para evitar decisões ruins no início do projeto.

## 🔗 Próximos Passos (Após Entrega)

1. **Publicar em servidor gratuito** (Railway/Heroku/Azure Free)
2. **Habilitar GitHub Pages** para documentação MkDocs
3. **Adicionar testes unitários** (xUnit)
4. **Implementar autenticação** (asp.net core identity)
5. **Adicionar persistência** (SQLite ou PostgreSQL)
6. **CI/CD** (GitHub Actions)

## 📚 Referências

- [ASP.NET Core MVC Docs](https://docs.microsoft.com/aspnet/core)
- [Razor Syntax](https://docs.microsoft.com/aspnet/core/mvc/views/razor)
- [MkDocs Guide](https://www.mkdocs.org/)
- [GitHub Pages](https://pages.github.com/)

---

**Atividade completada seguindo Spec-Driven Development**
