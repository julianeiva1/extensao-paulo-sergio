# TODO List - Atividade Acadêmica de Spec-Driven Development

## 📋 Visão Geral

Bem-vindo à documentação do projeto **TODO List**, uma aplicação MVC desenvolvida com **Spec-Driven Development** (SDD) conforme metodologia ensinada na disciplina de Engenharia de Software (Aula 09).

Este projeto demonstra os princípios de SDD na prática: especificação clara, arquitetura pragmática e implementação orientada por requisitos.

## 🎯 Objetivo da Atividade

Criar uma aplicação web TODO List que permita:
- ✓ Cadastrar novas tarefas
- ✓ Remover tarefas
- ✓ Adicionar lembretes às tarefas

Com requisitos não-funcionais:
- Armazenamento em memória (sem banco de dados persistente)
- Implantação em servidor gratuito
- Documentação com MkDocs
- Todo o código no GitHub

## 🛠️ Stack Técnico

| Componente | Tecnologia | Razão |
|-----------|-----------|-------|
| **Framework Web** | ASP.NET Core 8 MVC | Simplicidade, conhecimento prévio, MVC padrão educacional |
| **Linguagem** | C# | Tipada, segura, bom para grandes projetos |
| **Views** | Razor Templates | Sem JavaScript complexo, bindings automáticos |
| **Armazenamento** | List<Tarefa> em memória | Conforme requisito do enunciado |
| **Banco de Dados** | ❌ Nenhum | Dados perdidos ao reiniciar (conforme enunciado) |
| **Frontend CSS** | Bootstrap 5 | CDN, responsivo, sem build tools |
| **Documentação** | MkDocs | Estática, publicável em GitHub Pages |

## 📁 Estrutura do Repositório

```
.
├── .specify/                    # Artefatos do Spec-Kit
│   ├── extensions.yml
│   ├── memory/
│   │   └── constitution.md     # Princípios do projeto
│   └── scripts/
├── docs/                        # Documentação MkDocs
│   ├── index.md
│   ├── aula-09-sdd.md
│   ├── spec.md
│   ├── plan.md
│   ├── tasks.md
│   ├── constitution.md
│   └── deploy.md
├── specs/
│   └── 001-todo-list/          # Artefatos SDD
│       ├── spec.md             # Especificação (user stories)
│       ├── plan.md             # Plano arquitetural
│       ├── data-model.md       # Modelo de dados
│       ├── tasks.md            # Tarefas de implementação
│       ├── research.md         # Pesquisa técnica
│       ├── contracts/          # Contratos de API/testes
│       └── checklists/         # Checklists de qualidade
├── TodoListMvc/                 # Aplicação MVC
│   ├── Controllers/
│   │   └── TasksController.cs
│   ├── Models/
│   │   ├── Tarefa.cs
│   │   └── Lembrete.cs
│   ├── Services/
│   │   └── RepositorioTarefas.cs
│   ├── Views/
│   │   ├── Tasks/
│   │   │   ├── Index.cshtml
│   │   │   ├── Create.cshtml
│   │   │   └── Edit.cshtml
│   │   └── Shared/
│   │       └── _Layout.cshtml
│   ├── wwwroot/
│   │   └── css/
│   │       └── site.css
│   ├── Program.cs
│   ├── appsettings.json
│   └── TodoListMvc.csproj
├── mkdocs.yml                   # Configuração MkDocs
├── ENTREGA.md                   # Placeholders de entrega
└── README.md                    # README do projeto
```

## 🚀 Como Executar Localmente

### Pré-requisitos
- .NET 8 SDK instalado ([baixar](https://dotnet.microsoft.com/download))
- Visual Studio Code (opcional) ou Visual Studio

### Passos

1. **Clone o repositório**
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. **Navegue até o projeto MVC**
   ```bash
   cd TodoListMvc
   ```

3. **Execute a aplicação**
   ```bash
   dotnet run
   ```

4. **Abra no navegador**
   ```
   http://localhost:5125
   ```

A aplicação será disponibilizada automaticamente.

## 📖 Documentação

- [Atividade SDD](aula-09-sdd.md) - Descrição da atividade e metodologia SDD
- [Especificação](spec.md) - User stories e requisitos
- [Plano](plan.md) - Arquitetura e decisões técnicas
- [Constitution](constitution.md) - Princípios governantes do projeto
- [Tasks](tasks.md) - Lista de tarefas de implementação
- [Deploy](deploy.md) - Como publicar em servidor gratuito

## ✅ Funcionalidades Implementadas

### US1: Cadastrar Nova Tarefa
- Campo de entrada para título (máx 500 caracteres)
- Validação server-side obrigatória
- Mensagens de erro em português
- Redirecionamento automático para lista

### US2: Remover Tarefa
- Botão "Deletar" em cada tarefa
- Confirmação antes de remover
- Remoção imediata da lista
- Dados dos lembretes também removidos

### US3: Adicionar Lembrete à Tarefa
- Campo de texto para adicionar lembrete
- Cada tarefa pode ter múltiplos lembretes
- Lembretes aparecem em badge abaixo da tarefa
- Botão "✕" para remover lembrete
- Validação de texto não vazio

### Funcionalidades Adicionais
- Marcar tarefa como concluída (checkbox)
- Editar título de tarefa existente
- Exibição de data de criação
- Interface responsiva (mobile, tablet, desktop)
- Layout limpo com Bootstrap 5

## 🧪 Testando a Aplicação

A aplicação está completa e pronta para testes. Sugestões:

1. **Criar tarefas**: Teste adicionar tarefas com títulos diferentes
2. **Validar**: Tente criar tarefa vazia ou com >500 caracteres (deve falhar)
3. **Lembretes**: Adicione lembretes a uma tarefa
4. **Editar**: Clique "Editar" e altere título
5. **Concluir**: Marque checkbox para marcar como concluída
6. **Remover**: Delete tarefas (confirmação obrigatória)
7. **Responsivo**: Teste em tela pequena (F12 → Device Emulation)
8. **Refresh**: Pressione F5 - dados devem desaparecer (conforme requisito)

## 📝 Metodologia: Spec-Driven Development

Este projeto foi desenvolvido seguindo a metodologia de **Spec-Driven Development (SDD)**:

### Fase 1: Especificação
- Escrita de user stories em linguagem clara (português)
- Identificação de critérios de aceitação mensuráveis
- Definição do modelo de dados
- Documentação de requisitos e restrições

### Fase 2: Planejamento
- Decisões arquiteturais justificadas (WCAG)
- Definição do stack técnico
- Estimativa de esforço
- Identificação de riscos

### Fase 3: Implementação
- Divisão em tarefas incrementais
- Code review obrigatório
- Testes manuais de cada funcionalidade
- Documentação viva

### Fase 4: Validação
- Testes de aceitação contra especificação
- Verificação de requisitos não-funcionais
- Checklist de qualidade
- Documentação final

## 🔗 Links da Entrega

Veja [ENTREGA.md](../ENTREGA.md) para links de:
- Repositório GitHub
- Aplicação publicada
- Documentação online

## 📧 Suporte

Para dúvidas sobre o projeto, consulte:
- [Especificação](spec.md)
- [Plano](plan.md)
- [Constitution]( constitution.md)
- Código comentado em [TodoListMvc/](../TodoListMvc)

---

**Desenvolvido com ❤️ utilizando Spec-Driven Development**
