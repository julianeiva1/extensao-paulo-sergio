# Boas Práticas e Justificativas

Esta página reúne as boas práticas adotadas no projeto TODO List, suas justificativas e sua relação com a constitution. Cada prática está vinculada a um princípio governante e explica como é aplicada no contexto deste projeto acadêmico.

---

## 1. Simplicidade e Pragmatismo

**Relação com Constitution**: Princípio I — Pragmatism & Simplicity

### Prática Adotada

Priorizar soluções simples que atendem aos requisitos, evitando complexidade arquitetural desnecessária, over-engineering e abstrações que não agregam valor.

### Justificativa

A simplicidade foi adotada porque o objetivo da atividade é aplicar Spec-Driven Development e demonstrar boas práticas, não criar uma arquitetura excessivamente complexa. Soluções simples reduzem risco de erro, facilitam manutenção e melhor atendem ao prazo acadêmico. Em um projeto pequeno como TODO List, um monolito MVC é mais simples que uma API separada. Armazenamento em memória é mais simples que um banco de dados. Bootstrap CDN é mais simples que frameworks com build tools.

### Como Aplicar no Projeto

- Use KISS (Keep It Simple, Stupid): escolha a solução mais direta que funciona
- Evite abstrações desnecessárias: uma interface/classe por conceito, não crie abstrações "para o futuro"
- Use as capacidades built-in do ASP.NET Core (validação, autenticação, MVC)
- Questione cada camada de complexidade: "é realmente necessário?"
- Refatore para simplicidade regularmente se o código ficar complexo

---

## 2. Clareza e Manutenibilidade do Código

**Relação com Constitution**: Princípio II — Code Clarity

### Prática Adotada

Código deve ser claro, bem nomeado e fácil de entender para qualquer desenvolvedor. Use nomes descritivos, funções com responsabilidade única, comments que explicam o "por quê", e estrutura visual coerente.

### Justificativa

A clareza de código foi adotada porque o projeto pode ser revisado por professores, colegas e futuros membros do time. Nomes descritivos, funções pequenas com responsabilidade única e separação clara de responsabilidades reduzem significativamente o esforço de entendimento do codebase. Em um projeto acadêmico, código claro demonstra que você entendeu o que implementou e pode defender suas decisões.

### Como Aplicar no Projeto

- Use nomes em português descritivo: `RepositorioTarefas`, `ValidarTitulo()`, `obterTarefasAtivas()`
- Funções com uma responsabilidade: se valida, só valida; se persiste, só persiste
- Comments explicam o "por quê", não o óbvio
- Documente métodos públicos com XML comments se propósito ou efeitos não são óbvios
- Use a mesma estrutura de pastas/classes em todo projeto (Controllers, Models, Services, Views)
- Evite duplicação de código (DRY)
- Mantenha separação de responsabilidades (lógica de negócio em Services, não em Controllers)

---

## 3. Experiência do Usuário e Acessibilidade

**Relação com Constitution**: Princípio III — User Experience & Accessibility

### Prática Adotada

Interface deve ser simples, responsiva, acessível e em português claro. Use labels associados aos campos, mensagens de erro acionáveis, validações com feedback imediato, layout responsivo e contraste visual adequado.

### Justificativa

A prática de UX e acessibilidade foi adotada porque o sistema deve ser compreensível para usuários com pouca experiência técnica. Mensagens claras em português, labels associados aos campos de formulário e layout responsivo reduzem significativamente erros de interação. Para um projeto acadêmico, boa UX também demonstra profissionalismo e atenção aos detalhes da entrega.

### Como Aplicar no Projeto

- Use linguagem clara em português: evite jargão técnico ("Erro 400" → "Por favor, insira um título")
- Cada input tem `<label for="...">` correspondente
- Mensagens de erro aparecem logo abaixo do campo, explicam o problema e sugerem solução
- Ações destrutivas (delete) exigem diálogo de confirmação
- Layout testado em mobile (<768px), tablet (768-1023px) e desktop (≥1024px)
- Texto sempre legível, botões claramente identificáveis
- Use `aria-required`, `aria-describedby` para acessibilidade básica

---

## 4. Testes e Validação

**Relação com Constitution**: Princípio IV — Testing Discipline

### Prática Adotada

Todas as funcionalidades devem ser validadas antes de release, combinando testes manuais documentados (100% cobertura esperada), testes automatizados para regras de negócio (quando viável) e testes de regressão ao corrigir bugs.

### Justificativa

A disciplina de testes foi adotada porque o projeto precisa demonstrar que os fluxos principais funcionam corretamente antes da entrega acadêmica. Como é um projeto acadêmico simples, a estratégia combina testes manuais documentados para fluxos críticos (mais flexíveis e rápidos de executar) com testes automatizados para regras de negócio centrais (reduzem chance de regressão).

### Como Aplicar no Projeto

- Identifique fluxos críticos: cadastrar tarefa, remover tarefa, adicionar lembrete, editar, marcar concluída
- Documente cenários: para cada fluxo, liste casos de sucesso e erro
- Execute testes manuais: abra a app, percorra cada cenário, valide o resultado
- Registre resultado no MkDocs com "✅ PASSOU" ou "❌ FALHOU"
- Crie testes automatizados com xUnit para validações (ex: comprimento máximo de título)
- Antes de release: todos os cenários devem ter sido executados e documentados
- Ao corrigir bugs, crie teste que reproduz o bug, corrija, valide e re-execute testes anteriores

---

## 5. Revisão de Código

**Relação com Constitution**: Princípio V — Code Review

### Prática Adotada

Revisão obrigatória de código antes de integrar mudanças à branch principal. Pull Requests devem incluir descrição clara, testes realizados e checklist de validação.

### Justificativa

A revisão de código foi adotada porque reduz significativamente a chance de erros chegarem à branch principal, permite que os membros do grupo compartilhem conhecimento sobre o codebase e garante que a implementação esteja alinhada aos princípios da constitution antes da entrega.

### Como Aplicar no Projeto

- Toda mudança deve ser em Pull Request (nunca commit direto em main/develop)
- PR deve ter título descritivo: `feat: adicionar validação de lembretes`
- Descrição deve incluir: mudanças realizadas, testes executados, checklist
- Revisor valida: requisito atendido, sem duplicação, testes realizados, Constitution respeitada
- SLA de 2 horas para review
- Feedback deve ser construtivo e referenciar princípios (ex: "Isso seria melhor em Services, alinhado ao Princípio II")
- Merge apenas após aprovação

---

## 6. Performance e Eficiência

**Relação com Constitution**: Princípio VI — Performance

### Prática Adotada

Operações devem ser rápidas e eficientes, evitando processamento desnecessário, duplicação de dados em memória, renderização de informações inúteis e mantendo respostas perceptivelmente rápidas.

### Justificativa

A prática de performance foi adotada para garantir que operações simples sejam rápidas. Como o projeto usa armazenamento em memória (sem banco de dados persistente), a prioridade é evitar duplicação de dados, processamento redundante e renderização desnecessária. Para um projeto acadêmico pequeno, performance é menos crítica que em aplicações de grande escala, mas as boas práticas são educacionais.

### Como Aplicar no Projeto

- Evite processamento desnecessário: carregar apenas dados necessários, não "carregar tudo e filtrar depois"
- Evite duplicação: dados têm um único lugar de verdade, não copie para múltiplas variáveis
- Renderize apenas o útil: na view, não renderize dados ocultos via CSS
- Mantenha operações simples abaixo de 2 segundos
- Deixe claro no código/documentação onde otimizações aplicam-se (comentário, documentação)
- Evite premature optimization: só otimize quando profiler mostrar bottleneck real

---

## 7. Documentação Viva

**Relação com Constitution**: Princípio VII — Living Documentation

### Prática Adotada

A documentação deve acompanhar decisões de arquitetura, práticas de desenvolvimento, testes e mudanças relevantes no projeto. Deve ser mantida atualizada e servir como referência para avaliação acadêmica e continuidade do projeto.

### Justificativa

Documentação viva foi adotada para evitar perda de contexto sobre decisões arquiteturais, facilitar avaliação acadêmica por professores e colegas, e ajudar futuros membros a entenderem as razões por trás de cada decisão técnica.

### Como Aplicar no Projeto

- Atualize o MkDocs quando houver mudanças em arquitetura, testes, UX ou critérios de entrega
- Mantenha ADR (Architecture Decision Record) para decisões significativas
- Registre testes executados no checklist de testes
- Documente padrões de código e decisões de design
- Este MkDocs serve como forma de clareza e justificativa do projeto

---

## Relação com Constitution

As 7 boas práticas acima estão diretamente vinculadas aos 7 princípios da [Constitution](constitution.md). Cada prática concretiza um princípio em ações específicas do TODO List.

A consistência entre constitution e boas práticas garante que decisões técnicas respeitem os valores governantes do projeto, facilitam avaliação acadêmica e demonstram aplicação coerente de Spec-Driven Development.
