# Constitution: Princípios do Projeto

**Versão**: 2.0.0 | **Aprovado**: 2026-05-17

## Propósito

Este documento estabelece os princípios governantes do projeto TODO List, garantindo consistência, qualidade e manutenibilidade em todas as decisões técnicas e de design.

A constitution é **geral e evolutiva**, separada de decisões técnicas específicas que estão documentadas em Architecture Decision Records (ADRs).

---

## Princípios Centrais

### I. Pragmatism & Simplicity

Priorizar soluções simples que atendem requisitos, sobre complexidade arquitetural desnecessária.

**Justificativa:**
- Projetos simples são mais fáceis de entender, manter e evoluir
- Reduz tempo de onboarding de novos contribuidores
- Facilita identificação e correção de bugs
- Diminui débito técnico

**Aplicação no Projeto:**
- Escolher bibliotecas e frameworks bem conhecidos
- Evitar abstrações que não agregam valor
- Questionar cada camada de complexidade adicionada
- Documentar trade-offs de simplicidade vs. escalabilidade

---

### II. Code Clarity

Código deve ser claro, bem nomeado e fácil de entender para qualquer desenvolvedor.

**Justificativa:**
- Código é lido mais vezes do que é escrito
- Clareza reduz bugs e facilita manutenção
- Time cresce; novos membros precisam entender rápido
- Documentação dentro do código evita desatualização

**Aplicação no Projeto:**
- Nomes de variáveis, métodos e classes em português descritivo
- Funções com responsabilidade única e clara
- Comments explicam *por quê*, não *o quê*
- Documentar métodos públicos quando seu propósito, parâmetros ou efeitos não forem óbvios
- Estrutura visual coerente no codebase



---

### III. User Experience & Accessibility

Interface deve ser simples, responsiva, acessível e em português claro.

**Justificativa:**
- Usuário final não é desenvolvedor; precisa de clareza
- Acessibilidade beneficia todos os usuários, não apenas pessoas com deficiência
- Interface clara reduz erros e aumenta satisfação
- Sistema responsivo funciona em qualquer dispositivo

**Aplicação no Projeto:**
- Interface em português brasileiro claro
- Labels associados aos campos de formulário
- Mensagens de erro claras e acionáveis
- Validações server-side com feedback imediato
- Layout responsivo testado em mobile, tablet e desktop
- Contraste visual adequado para legibilidade
- Evitar jargão técnico para usuário final
- Confirmação obrigatória para ações destrutivas



---

### IV. Testing Discipline

Todas as funcionalidades devem ser validadas antes de release, com foco em testes automatizados quando viável e testes manuais documentados.

**Justificativa:**
- Testes previnem regressões
- Validação documentada permite repetição
- Qualidade consistente em iterações
- Confiança para refatoração

**Aplicação no Projeto:**
- Testes automatizados para regras de negócio centrais
- Checklist manual documentado para fluxos críticos
- Testes de regressão ao corrigir bugs
- Cenários testados registrados no MkDocs
- Testes de acessibilidade e responsividade



---

### V. Code Review

Todo código passa por revisão antes de integração, garantindo qualidade, clareza e aderência aos princípios.

**Justificativa:**
- Múltiplos olhos identificam problemas
- Conhecimento é compartilhado entre time
- Padrões são mantidos consistentes
- Aprende-se com soluções dos colegas

**Aplicação no Projeto:**
- Pull requests obrigatórios
- Checklist mínimo de PR antes de merge
- Validação de compilação e testes
- Aderência aos princípios verificada
- Feedback construtivo e documentado



---

### VI. Performance

Operações devem ser rápidas e eficientes, respeitando os limites de armazenamento e processamento do projeto.

**Justificativa:**
- Usuários esperam respostas ágeis
- Performance impacta experiência
- Boas práticas de performance escalam caso o projeto evolua

**Aplicação no Projeto:**
- Evitar processamento desnecessário
- Evitar duplicação de dados em memória
- Renderizar apenas informações úteis
- Manter operações simples com resposta perceptivelmente rápida, preferencialmente abaixo de 2 segundos em condições normais
- Código que deixa claro quando paginação, filtros e índices aplicam-se
- **Nota:** Otimizações de banco de dados (índices, cache) aplicam-se quando projeto evoluir para persistência



---

### VII. Living Documentation

A documentação deve acompanhar decisões, práticas e mudanças relevantes, servindo como fonte de verdade do projeto.

**Justificativa:**
- Documentação desatualizada é pior que não ter documentação
- Decisões documentadas evitam retrabalho
- Novo time entende contexto e justificativas
- MkDocs versionado no repo garante histórico

**Aplicação no Projeto:**
- Toda decisão técnica relevante possui justificativa documentada em ADR
- Práticas da constitution possuem página correspondente no MkDocs
- MkDocs explica decisões, fluxos e critérios de qualidade
- MkDocs não repete código desnecessariamente; referencia o repo
- Mudanças em arquitetura, testes, UX ou critérios de entrega atualizam MkDocs
- Checklists de qualidade são mantidos e atualizados

---



## Quality Standards

| Aspecto | Padrão |
|---------|--------|
| **Code Review** | Obrigatório para todo merge (veja checklist) |
| **Tests** | Fluxos críticos testados e documentados; regras centrais com testes automatizados quando viável |
| **Performance** | Operações simples com resposta perceptivelmente rápida, preferencialmente abaixo de 2 segundos |
| **Responsividade** | Testado em mobile, tablet e desktop |
| **Linguagem** | Português Brasileiro |
| **Validações** | Server-side obrigatório |
| **Erros** | Mensagens claras, acionáveis em PT |

---

## Development Workflow

1. **Especificar**: User stories em português com critérios de aceitação
2. **Planejar**: Arquitetura, decisões técnicas, tarefas
3. **Implementar**: Fase a fase, commits incrementais
4. **Testar**: Manual e automatizado contra acceptance criteria
5. **Revisar**: Code review com checklist (veja qualidade/checklist-pr.md)
6. **Validar**: Checklist de qualidade completo
7. **Documentar**: MkDocs + comments no código
8. **Deploy**: Servidor publicado com links verificados

---

## Compliance Checklist

Antes de cada release, validar:

- [ ] Todas as user stories implementadas
- [ ] Testes manuais passaram (veja qualidade/checklist-testes.md)
- [ ] Testes automatizados passaram (se aplicável)
- [ ] Interface responsiva testada
- [ ] Mensagens em português claro
- [ ] Sem erros de compilação
- [ ] Code review realizada com checklist (veja qualidade/checklist-pr.md)
- [ ] Constitution verificada
- [ ] MkDocs atualizada
- [ ] Critérios de entrega atendidos (veja qualidade/criterios-de-entrega.md)
- [ ] Links funcionando (GitHub, Deploy, Docs)

---

## Referências

- [Boas Práticas e Justificativas](boas-praticas.md)
- [Decisões Arquiteturais](decisoes-arquiteturais/mvc-monorepo-in-memory.md)
- [Checklists de Qualidade](qualidade/checklist-pr.md)

---

Constitution aprovada e ratificada.
