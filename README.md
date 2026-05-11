# TODO List - Aplicação MVC com Spec-Driven Development

## 📋 Sobre o Projeto

Aplicação **TODO List** em **ASP.NET Core 8 MVC** desenvolvida com metodologia de **Spec-Driven Development (SDD)** como atividade da disciplina de Engenharia de Software (Aula 09).

**Objetivo**: Demonstrar na prática os princípios de SDD através de uma aplicação funcional que segue especificação clara, arquitetura pragmática, implementação orientada por tarefas e documentação estruturada.

**Status**: ✅ **COMPLETO** - Funcionalidades de MVP implementadas e testadas.

---

## ✨ Funcionalidades

- ✅ **Listar Tarefas**: Visualizar todas as tarefas com descrição e lembrete
- ✅ **Cadastrar Tarefa**: Criar tarefa com título obrigatório (máx 500 chars) e descrição opcional
- ✅ **Remover Tarefa**: Deletar tarefa com confirmação
- ✅ **Lembrete Opcional**: Campo de data/hora para lembretes
- ✅ **Exibir Lembrete**: Lembrete formatado exibido na listagem
- ✅ **Editar Tarefa**: Alterar título, descrição e lembrete
- ✅ **Marcar Concluída**: Checkbox para marcar/desmarcar conclusão
- ✅ **Validações**: Título obrigatório, máximos de caracteres
- ✅ **Interface Responsiva**: Bootstrap 5 - Desktop, Tablet, Mobile
- ✅ **Armazenamento em Memória**: Sem banco de dados persistente

---

## 🛠️ Stack Técnico

| Componente | Tecnologia | Motivo |
|-----------|-----------|---------|
| **Linguagem** | C# 12 | Tipagem forte, segurança de tipos |
| **Framework** | ASP.NET Core 8 MVC | MVC nativo, segurança, performance |
| **Views** | Razor Templates | Binding automático, sem JS complexo |
| **Armazenamento** | List<Tarefa> (In-Memory) | Requisito: sem persistência |
| **Frontend** | Bootstrap 5 CDN | Responsivo, sem npm necessário |
| **Documentação** | MkDocs + Material Theme | GitHub Pages, versionada |
| **Versionamento** | Git + GitHub | Control de versão, CI/CD ready |

---

## 🏗️ Arquitetura

### Decisão Principal: MVC Monolítico

**Por que MVC e não API+React?**
- Simplicidade: 1 projeto, 1 deploy, 1 build
- Alinhamento: Requisito especificava "arquitetura MVC"
- Educação: Padrão MVC real usado em produção
- Adequação: Funcionalidades simples não justificam separação

### Estrutura MVC

```
ASP.NET Core 8 MVC
├── Models/Tarefa.cs
├── Controllers/TasksController.cs
├── Services/RepositorioTarefas.cs (Singleton In-Memory)
└── Views/Tasks/ (Razor Templates)
    ├── Index.cshtml
    ├── Create.cshtml
    └── Edit.cshtml
```

---

## 🚀 Como Executar Localmente

### Pré-requisitos
- **.NET 8 SDK** ([download](https://dotnet.microsoft.com/download/dotnet/8.0))
- **Git**

### Passos

```bash
# 1. Clonar repositório
git clone https://github.com/julianeiva1/extensao-paulo-sergio.git
cd extensao-paulo-sergio

# 2. Entrar na pasta da aplicação MVC
cd TodoListMvc

# 3. Compilar
dotnet build

# 4. Executar
dotnet run

# 5. Abrir no navegador
# http://localhost:5125
```

---

## 📁 Estrutura do Repositório

```
extensao-paulo-sergio/
├── .specify/                          # Artefatos Spec-Kit
│   └── memory/constitution.md
├── docs/                              # Documentação MkDocs
│   ├── index.md
│   ├── aula-09-sdd.md
│   ├── arquitetura.md
│   ├── uso.md
│   ├── spec-driven-development.md
│   ├── deploy.md
│   ├── spec.md
│   ├── plan.md
│   ├── tasks.md
│   └── constitution.md
├── specs/001-todo-list/               # Artefatos SDD (Spec-Kit)
│   ├── spec.md
│   ├── plan.md
│   ├── tasks.md
│   ├── data-model.md
│   ├── research.md
│   ├── quickstart.md
│   ├── contracts/task-api.md
│   └── checklists/requirements.md
├── TodoListMvc/                       # Aplicação ASP.NET Core MVC
│   ├── Controllers/TasksController.cs
│   ├── Models/Tarefa.cs
│   ├── Services/RepositorioTarefas.cs
│   ├── Views/Tasks/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   └── Edit.cshtml
│   ├── Program.cs
│   └── TodoListMvc.csproj
├── mkdocs.yml
├── ENTREGA.md
└── README.md
```

---

## ✅ Checklist de Conformidade

- [x] Arquitetura MVC
- [x] Mono-repo
- [x] Armazenamento em memória
- [x] Sem banco de dados
- [x] Sem Entity Framework
- [x] Sem SQLite
- [x] Cadastrar tarefas
- [x] Remover tarefas
- [x] Lembretes
- [x] Constitution justificando
- [x] Documentação MkDocs
- [x] Código no GitHub
- [x] ENTREGA.md com links
- [ ] Aplicação publicada em servidor gratuito

---

## 📊 Métricas

| Métrica | Valor |
|---------|-------|
| User Stories | 3 |
| Tarefas Implementadas | 42 |
| Controllers | 1 |
| Models | 1 |
| Views | 3 |
| Services | 1 |
| Linhas Código | ~600 |

---

## 🔗 Links Importantes

- 📖 [Documentação MkDocs](docs/index.md)
- 🎓 [Atividade SDD](docs/aula-09-sdd.md)
- 🏗️ [Arquitetura](docs/arquitetura.md)
- 🚀 [Deploy](docs/deploy.md)
- 📋 [ENTREGA.md](ENTREGA.md)

---

## 🎓 Metodologia: Spec-Driven Development

1. **Especificação**: [specs/001-todo-list/spec.md](specs/001-todo-list/spec.md)
2. **Planejamento**: [specs/001-todo-list/plan.md](specs/001-todo-list/plan.md)
3. **Tarefas**: [specs/001-todo-list/tasks.md](specs/001-todo-list/tasks.md)
4. **Implementação**: Código em TodoListMvc/
5. **Validação**: Testes manuais
6. **Documentação**: MkDocs + Constitution

---

## ⚖️ Decisões Arquiteturais

**Por que In-Memory?** - Requisito especificava sem banco persistente

**Por que MVC?** - Requisito especificava arquitetura MVC

**Por que Bootstrap CDN?** - Sem dependências npm necessárias

**Por que Lembrete Simples?** - Reduz complexidade, usa `DateTime? LembreteEm`

---

## 🛡️ Validações Implementadas

- Título obrigatório (max 500 chars)
- Descrição opcional (max 2000 chars)
- Lembrete opcional (DateTime)
- Anti-forgery tokens (CSRF protection)
- Thread-safe (lock no Repository)

---

## 🔗 Links da Entrega

- **Repositório GitHub**: https://github.com/julianeiva1/extensao-paulo-sergio
- **Documentação Online**: https://julianeiva1.github.io/extensao-paulo-sergio/
- **Aplicação Publicada**: https://extensao-paulo-sergio.onrender.com

---

## ✨ Desenvolvido com ❤️ utilizando Spec-Driven Development

Para mais informações, consulte:
- [ENTREGA.md](ENTREGA.md) - Links e instruções
- [docs/index.md](docs/index.md) - Documentação completa
- [specs/001-todo-list/spec.md](specs/001-todo-list/spec.md) - Especificação técnica

**Última atualização**: 10 de maio de 2026  
**Status**: ✅ Pronto para Entrega
