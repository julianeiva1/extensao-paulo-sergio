<!-- CONSTITUTION SYNC IMPACT REPORT
Version: 1.0.0 (Initial)
Status: Created from user requirements
Ratified: 2026-05-10
Modified Principles: N/A (Initial creation)
Added Sections: Core Principles, Quality Standards, UX Standards, Performance Standards, Project Constraints, Development Workflow, Governance
Templates Updated: ✅ templates/spec-template.md, ✅ templates/plan-template.md, ✅ templates/tasks-template.md
-->

# IFES Extensão - Constitution

## Propósito

Este documento estabelece os princípios governantes do projeto IFES Extensão, um sistema de suporte à organização e execução de atividades de extensão desenvolvido para usuários reais do IFES com foco em simplicidade, clareza e manutenção futura.

## Core Principles

### I. Code Review & Collaboration (Revisão de Código Obrigatória)

Todo código DEVE ser revisado e aprovado antes de ser incorporado à branch principal. Nenhum commit deve ir para a produção sem revisão de pelo menos um membro do grupo. Isto garante qualidade consistente, conhecimento compartilhado do código e previne regressões.

**Expectativa**: Pull requests DEVEM incluir descrição clara da mudança, contexto do negócio e lista de testes realizados.

### II. Code Clarity & Maintainability (Clareza no Código)

O código DEVE usar nomes descritivos e autoexplicativos para arquivos, funções, componentes e variáveis. Duplicação de código DEVE ser evitada através de reutilização e abstração adequada. Regras de negócio DEVEM ser separadas da interface de usuário sempre que possível, criando arquivos dedicados a lógica de negócio (services, utils, models).

**Expectativa**: Nomes de variáveis em português, funções nomeadas conforme ação que executam (verbo + substantivo), componentes reutilizáveis sempre que possível.

### III. Testing Discipline (Testes Obrigatórios em Lógica de Negócio)

Funcionalidades que implementem regra de negócio DEVEM ter testes unitários. Fluxos críticos DEVEM ser testados manualmente pelo grupo antes da entrega em produção. Correções de bugs DEVEM incluir testes de regressão para validar que o problema não ressurja. O grupo DEVE manter registro documentado dos cenários testados.

**Expectativa**: Teste automatizado para lógica de negócio, teste manual documentado para fluxos críticos, testes de regressão para toda correção de bug.

### IV. User Experience & Accessibility (UX Focada em Usuários Reais)

Telas DEVEM ser simples, objetivas e adequadas para usuários com pouca experiência técnica. Mensagens de erro DEVEM ser claras e apontarem ações específicas. Usuários DEVEM conseguir compreender o que fazer em cada tela sem depender de explicação externa. O sistema DEVE funcionar bem em notebooks e celulares (responsivo).

**Expectativa**: Validações em tempo real, mensagens amigáveis em português, layout responsivo testado em múltiplos tamanhos, linguagem simples sem jargão técnico.

### V. Performance & Efficiency (Performance Responsiva)

Principais ações do usuário (carregar lista, enviar formulário, processar ação) DEVEM responder em até 2 segundos em condições normais de conexão. O sistema DEVE evitar carregamentos desnecessários e consultas que tragam dados não utilizados. Paginação e lazy loading DEVEM ser usados para listas grandes.

**Expectativa**: Monitoramento de tempo de resposta, uso de paginação, índices em banco de dados, cache onde apropriado.

### VI. Pragmatism & Simplicity (Simplicidade Sobre Complexidade)

O projeto DEVE priorizar soluções simples, mantíveis e viáveis dentro do prazo da disciplina. Não DEVE adotar padrões, bibliotecas ou arquiteturas desnecessariamente complexas. Stack técnico DEVE ser escolhido pela clareza e manutenibilidade, não por trends. Documentação DEVE ser mínima mas suficiente (MkDocs para conceitos críticos).

**Expectativa**: Evitar over-engineering, escolher ferramentas conhecidas pelo grupo, documentação concisa, reavaliação contínua do que realmente agrega valor.

## Arquitetura Decisão: MVC Mono-Repo com Armazenamento em Memória

### Decisão
Arquitetura: **ASP.NET Core MVC** em **mono-repo** com **armazenamento em memória** (sem banco de dados persistente).

### Justificativa (WCAG - Why, Context, Alternatives Considered, Good choice)

**Why (Por quê)**:
- Enunciado exige: "A aplicação não requer persistência de dados entre execuções"
- Contexto acadêmico: simplicidade é mais importante que escalabilidade
- Requisitos cobrem apenas: cadastrar tarefas, remover tarefas, adicionar lembretes
- Atividade de ensino em Spec-Driven Development deve focar no processo, não em complexidade técnica

**Context (Contexto)**:
- Implementação anterior tentava separação backend (ASP.NET Core Web API) + frontend (React)
- Resultado: over-engineering, complexidade desnecessária, dificuldade de deployment
- Feedback do enunciado: projeto deve atender requisitos específicos sem adicionar funcionalidades
- Mono-repo permite manter código, especificações e documentação juntos

**Alternatives Considered (Alternativas)**:
1. ❌ Backend/Frontend separado (API + React): Over-engineering, custo operacional, complexidade
2. ❌ Banco de dados persistente (SQLite/SQL): Não atende requisito "sem persistência"
3. ❌ Frontend framework (Vue/Angular): Desnecessário para requisitos simples
4. ✅ **MVC Mono-Repo em Memória**: Atende todos os requisitos com máxima simplicidade

**Good Choice (Boa Escolha)**:
- Atende 100% dos requisitos do enunciado
- Minimiza complexidade: 1 projeto, 1 linguagem, deploy simples
- Mantém aderência à constitution (Pragmatism & Simplicity)
- Facilita o entendimento e manutenção do código

### Decisões Técnicas Secundárias

#### 1. MVC (não WebForms, nem Pages)
- MVC é a arquitetura padrão educacional para .NET
- Controllers e Views seguem padrão bem conhecido
- Razor templates simplificam bindings sem exigir JavaScript
- Models naturais para validação server-side

#### 2. Armazenamento em Memória (não Entity Framework)
- Requisito: "sem banco de dados persistente"
- Repositório simples com `List<Tarefa>` em uma classe Singleton
- Validações centralizadas no repositório
- Dados perdidos ao reiniciar (conforme enunciado)
- Permite migração futura para banco se necessário

#### 3. Mono-Repo (não multi-repo)
- Repositório único no GitHub com: `/specs`, `/TodoListMvc`, `/docs`
- Simplifica CI/CD e deployment
- Mantém artefatos SDD, código e documentação sincronizados
- Adequado para projeto acadêmico de tamanho pequeno/médio

#### 4. Bootstrap 5 via CDN (não npm/build)
- Sem dependência de Node.js ou build tools adicionais
- Responsivo por padrão (mobile-first)
- Reduz complexidade do projeto .NET
- CSS rápido e testado em produção

#### 5. MkDocs para Documentação
- Geração estática (sem banco de dados)
- Publicável em GitHub Pages gratuitamente
- Markdown versionável no repositório
- Foco em documentação de processo SDD, não code generation

#### 6. Deployment em Servidor Gratuito (Railway/Heroku/Azure Free)
- Requisito do enunciado: "publicável em servidor gratuito"
- ASP.NET Core roda em Linux containers
- Não requer recursos especiais (sem banco persistente)
- Deploy direto do GitHub simplifica processo

### Alinhamento com Constitution

| Princípio | Como Aplicado |
|-----------|---------------|
| **Pragmatism** | Escolhemos a solução mais simples que atende ao enunciado |
| **Code Clarity** | Nomes em português, Controllers e Services com responsabilidades claras |
| **Code Review** | Todos os commits passam por validação (compilação e testes) |
| **Testing** | Testes manuais de todos os user stories antes de release |
| **UX Focus** | Interface simples em português, validações claras em tempo real |
| **Simplicity** | 1 projeto, 1 linguagem, sem frameworks desnecessários |
- Publicável em servidores gratuitos (Heroku, Railway, Azure Free)
- Teste e debug facilitados por tudo estar centralizado
- Preparado para evolução: Repositório Pattern permite trocar storage depois

### Stack Técnico - Justificado

| Componente | Escolha | Por Quê |
|---|---|---|
| Framework | ASP.NET Core MVC | MVC built-in, .NET é conhecido pelo grupo, segurança e performance |
| Views | Razor Templates | Tudo em C#, sem frontend framework desnecessário |
| Storage | In-Memory List | Requisito: dados não persistem, mais rápido, sem migrations SQL |
| ORM | ❌ Nenhum | Não precisa com in-memory storage, simplifica código |
| Database | ❌ Nenhum | Requisito: sem persistência entre execuções |
| Deployment | Free Tier Servers | Heroku, Railway, Azure Free (ASP.NET Core é leve) |

## Quality Standards

### Code Quality
- **Nomes claros**: Variáveis, funções e componentes com nomes em português, descritivos
- **Sem duplicação**: Funções reutilizáveis, componentes genéricos onde possível
- **Separação de conceitos**: Lógica de negócio isolada, interface independente
- **Revisão obrigatória**: Toda mudança passa por revisão antes da branch principal

### Testing Strategy
- **Testes unitários**: Toda regra de negócio implementada deve ter teste automatizado
- **Testes manuais**: Fluxos críticos documentados e testados pelo grupo
- **Testes de regressão**: Toda correção de bug inclui teste que previne volta do problema
- **Documentação de testes**: Cenários testados registrados em checklist ou documento

## UX & Interface Standards

### Usability
- Interfaces simples e objetivas para usuários com pouca experiência técnica
- Mensagens de erro claras indicando ação específica
- Funcionalidade compreensível sem documentação externa
- Responsivo em notebook e celular

### Language & Accessibility
- Todas as mensagens, labels e menus em português brasileiro
- Linguagem clara, sem jargão técnico
- Validações em tempo real com feedback imediato

## Performance Standards

### Response Times
- Ações principais: máximo 2 segundos em conexão normal
- Carregamentos desnecessários: evitar
- Consultas: trazer apenas dados que serão usados

### Optimization Techniques
- Paginação para listas grandes
- Lazy loading onde apropriado
- Cache inteligente de dados
- Índices no banco de dados para queries frequentes

## Project Constraints & Context

### Technical Constraints
- **Custo baixo**: Stack técnico deve minimizar custos de infraestrutura
- **Prazo**: Soluções devem ser viáveis para disciplina (não complexas demais)
- **Conexão instável**: Sistema deve lidar gracefully com conexões lentas/instáveis
- **Documentação mínima**: MkDocs apenas para conceitos críticos, não redundante

### Language & Localization
- Interface em português brasileiro
- Mensagens, validações e feedback em português
- Documentação técnica em português

### Deployment & Maintenance
- Sistema deve ter baixo custo de manutenção
- Escolha de tecnologias deve ser baseada em manutenibilidade
- Evitar technical debt desnecessário

## Development Workflow

### Code Review Process
1. Toda mudança submetida via Pull Request (ou merge request equivalente)
2. Revisor diferente do autor DEVE revisar código
3. Revisor valida: adequação da solução, qualidade do código, testes, aderência à constitution
4. Apenas após aprovação: merge para branch principal

### Testing & Quality Gates
1. Testes unitários DEVEM passar antes de merge
2. Fluxos críticos DEVEM ser testados manualmente antes de release
3. Correções de bugs DEVEM incluir teste de regressão
4. Documento ou checklist DEVE registrar cenários testados por release

### Release & Deployment
1. Releases planejadas com antecedência
2. Fluxos críticos testados manualmente pelo grupo
3. Tag de versão no Git
4. Documentação de mudanças (changelog)

## Governance

### Constitution Authority
Esta constituição estabelece os padrões governantes do projeto IFES Extensão. Todas as decisões de arquitetura, design e implementação devem estar em conformidade com estes princípios. Quando conflito entre eficiência e estes princípios surge, a constituição prevalece, a menos que explicitamente ammendada pelo grupo.

### Amendment Process
1. Proposta de mudança discutida e documentada
2. Impacto avaliado (templates afetados, workflow impactado)
3. Consenso do grupo obtido
4. Versão bumped conforme semântica: MAJOR (mudança incompatível), MINOR (adição), PATCH (esclarecimento)
5. Documentação de mudança adicionada como comentário HTML no topo do arquivo

### Compliance Review
- Code reviews verificam aderência aos princípios
- Antes de release, fluxo de testes valida qualidade
- Periodicamente (a cada release ou fim de Sprint) group revisa se constitution está sendo seguida

**Version**: 1.0.0 | **Ratified**: 2026-05-10 | **Last Amended**: 2026-05-10
