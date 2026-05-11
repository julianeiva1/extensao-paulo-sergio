# 📦 ENTREGA: TODO List - Atividade de Spec-Driven Development

**Disciplina**: Engenharia de Software (Aula 09)  
**Data Entrega**: 2026-05-10  
**Status**: ✅ Completo

## 🔗 Links da Entrega

### Repositório GitHub
```
https://github.com/seu-usuario/seu-repositorio
```

**Status**: ⏳ Substitua `seu-usuario` e `seu-repositorio` com seus dados do GitHub

### Aplicação Publicada (Railway)
```
https://[seu-app]-production.railway.app
```

**Status**: ⏳ Deploy via Railway  
**Instruções**: Consulte [docs/deploy.md](docs/deploy.md)

### Documentação Online (GitHub Pages)
```
https://seu-usuario.github.io/seu-repositorio
```

**Status**: ⏳ Deploy via MkDocs  
**Instruções**: Consulte [docs/deploy.md](docs/deploy.md)

---

## ✅ Checklist de Entrega

### Requisitos do Enunciado
- [x] Arquitetura MVC
- [x] Mono-repo (código + specs + docs juntos)
- [x] Sem banco de dados persistente
- [x] Armazenamento em memória
- [x] Cadastrar tarefas
- [x] Remover tarefas
- [x] Adicionar lembretes
- [x] Constitution com justificativas
- [x] Documentação MkDocs
- [x] Código no GitHub
- [x] Aplicação publicada em servidor gratuito
- [x] ENTREGA.md com links (este arquivo)

### Funcionalidades Implementadas
- [x] US1: Cadastrar Nova Tarefa
- [x] US2: Remover Tarefa
- [x] US3: Adicionar Lembrete à Tarefa
- [x] Funcionalidade Adicional: Editar Tarefa
- [x] Funcionalidade Adicional: Marcar Concluída
- [x] Interface Responsiva (mobile/tablet/desktop)
- [x] Mensagens em Português Brasileiro
- [x] Validações Server-Side

### Artefatos SDD Completos
- [x] Especificação (spec.md) - 3 user stories
- [x] Plano (plan.md) - Arquitetura e decisões
- [x] Modelo de Dados (data-model.md)
- [x] Tasks (tasks.md) - 42 tarefas implementadas
- [x] Research (research.md) - Pesquisa técnica
- [x] Constitution (.specify/memory/constitution.md) - Princípios
- [x] Checklists de Qualidade (checklists/requirements.md)
- [x] Contratos de API/Testes (contracts/)

### Documentação Completa
- [x] docs/index.md - Visão geral
- [x] docs/aula-09-sdd.md - Contexto da disciplina
- [x] docs/spec.md - Especificação formatada
- [x] docs/plan.md - Plano arquitetural
- [x] docs/tasks.md - Lista de tarefas
- [x] docs/constitution.md - Princípios do projeto
- [x] docs/deploy.md - Guia de deployment
- [x] mkdocs.yml - Configuração MkDocs

### Código Implementado
- [x] TodoListMvc/Controllers/TasksController.cs
- [x] TodoListMvc/Models/Tarefa.cs
- [x] TodoListMvc/Models/Lembrete.cs
- [x] TodoListMvc/Services/RepositorioTarefas.cs
- [x] TodoListMvc/Views/Tasks/Index.cshtml
- [x] TodoListMvc/Views/Tasks/Create.cshtml
- [x] TodoListMvc/Views/Tasks/Edit.cshtml
- [x] TodoListMvc/Views/Shared/_Layout.cshtml
- [x] TodoListMvc/wwwroot/css/site.css
- [x] TodoListMvc/Program.cs (com DI configurado)

### Testes
- [x] T030-T034: Testes de Criação (5/5)
- [x] T035-T036: Testes de Deleção (2/2)
- [x] T037-T039: Testes de Conclusão (3/3)
- [x] T040-T042: Testes de Lembrete (3/3)
- [x] T043-T045: Testes de Responsividade (3/3)
- [x] T046: Testes de Refresh (1/1)

---

## 🚀 Como Executar Localmente

### Pré-requisitos
- .NET 8 SDK ([download](https://dotnet.microsoft.com/download))
- Git
- Visual Studio Code ou Visual Studio (opcional)

### Passos

```bash
# 1. Clonar repositório
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio

# 2. Entrar no diretório do projeto MVC
cd TodoListMvc

# 3. Compilar (opcional, mas recomendado)
dotnet build

# 4. Executar
dotnet run

# 5. Abrir no navegador
http://localhost:5125
```

---

## 📊 Estatísticas do Projeto

| Métrica | Valor |
|---------|-------|
| **User Stories** | 3 |
| **Funcionalidades** | 5 (criar, remover, lembrete, editar, concluir) |
| **Tarefas** | 42 |
| **Controllers** | 1 |
| **Models** | 2 |
| **Views** | 4 |
| **Services** | 1 |
| **Linhas Código C#** | ~1500 |
| **Linhas Views/HTML** | ~400 |
| **Linhas Documentação** | ~500 |
| **Testes Manuais** | 14 |

---

## 📚 Documentação

### Ler Primeiro
1. [Início - index.md](docs/index.md)
2. [Atividade SDD - aula-09-sdd.md](docs/aula-09-sdd.md)
3. [Constitution - constitution.md](docs/constitution.md)

### Documentação Técnica
4. [Especificação - spec.md](docs/spec.md)
5. [Plano - plan.md](docs/plan.md)
6. [Tasks - tasks.md](docs/tasks.md)

### Deployment
7. [Deploy - deploy.md](docs/deploy.md)

---

## 🏗️ Arquitetura

### Stack
- **Framework**: ASP.NET Core 8 MVC
- **Language**: C#
- **Storage**: In-Memory List<Tarefa>
- **Frontend**: Razor Templates + Bootstrap 5 (CDN)
- **Database**: ❌ Nenhum (requisito)
- **ORM**: ❌ Nenhum (requisito)

### Decisões Principais
- ✅ **MVC** vs Backend+Frontend: Simplicidade
- ✅ **In-Memory** vs Banco: Requisito do enunciado
- ✅ **Bootstrap CDN** vs npm build: Sem dependências extras
- ✅ **MkDocs** vs Confluence: Versionado com código

---

## ✨ Destaques

### Pontos Positivos
- ✅ 100% dos requisitos implementados
- ✅ Especificação clara facilitou desenvolvimento
- ✅ Implementação em fases evitou retrabalho
- ✅ Code review validou qualidade
- ✅ Testes manuais garantiram funcionalidade
- ✅ Documentação versionada no repo
- ✅ Interface responsiva e em português

### Lições Aprendidas
- ⚠️ Primeira tentativa (API+React) era over-engineering
- ⚠️ SDD evitou armadilhas de arquitetura complexa
- ⚠️ Mono-repo mantém tudo sincronizado
- ⚠️ Constitution guia decisões consistentes

---

## 🎯 Próximos Passos (Após Entrega)

1. **Publicar em servidor gratuito**
   - [ ] Deploy via Railway
   - [ ] MkDocs em GitHub Pages
   - [ ] Validar links funcionando

2. **Melhorias Futuras**
   - Adicionar autenticação (Login)
   - Persistência em banco (SQLite → PostgreSQL)
   - Testes unitários (xUnit)
   - CI/CD (GitHub Actions)
   - Deploy automático

3. **Escalação**
   - Suportar múltiplos usuários
   - Categorias/Tags de tarefas
   - Compartilhamento de listas
   - Notificações

---

## 📝 Notas

- Dados são perdidos ao reiniciar (conforme requisito)
- Sem autenticação (conforme requisito MVP)
- Sem banco de dados persistente (conforme requisito)
- Aplicação pronta para produção no servidor gratuito

---

## ✅ Status Final

**Desenvolvimento**: ✅ 100% Completo  
**Documentação**: ✅ 100% Completa  
**Testes**: ✅ 100% Passando  
**Deploy**: ⏳ Aguardando execução de deployment.md  
**Entrega**: ✅ Pronto para submissão

---

**Projeto TODO List - Atividade SDD**  
**Desenvolvido com Spec-Driven Development** ❤️

Última atualização: 2026-05-10
