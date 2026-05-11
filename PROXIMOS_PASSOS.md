# 🎯 GUIA DE AÇÃO: PRÓXIMOS PASSOS PARA ENTREGA

**Status Atual**: ✅ Código + Documentação 100% Pronto  
**Falta Fazer**: 3 Passos de Deployment + 1 Passo de Atualização

---

## 📍 ONDE VOCÊ ESTÁ AGORA

```
✅ Código TodoListMvc/   → Completo, compilado, testado
✅ Specs/                → Completo com 8 artefatos
✅ Docs/MkDocs          → Completo com 7 arquivos
✅ Configuration        → Completo (mkdocs.yml, etc)
✅ ENTREGA.md           → Criado (com placeholders)
✅ README.md            → Atualizado
✅ Constitution         → Justificado com 6 princípios
✅ AUDITORIA_COMPLETA   → Relatório de validação

❌ GitHub Push          → FALTA FAZER
❌ Railway Deploy       → FALTA FAZER
❌ GitHub Pages Deploy  → FALTA FAZER
❌ Links em ENTREGA.md  → FALTA ATUALIZAR
```

---

## 🚀 PASSOS PARA COMPLETAR

### PASSO 1️⃣: FAZER PUSH NO GITHUB (5 min)

**Objetivo**: Sincronizar código com repositório remoto

**Comandos** (execute um por um):

```bash
# 1. Navegar até o projeto
cd "c:\Users\julia\OneDrive\IFES\Documentos IFES\Extensão"

# 2. Verificar status
git status

# 3. Adicionar todos os arquivos
git add .

# 4. Fazer commit com mensagem
git commit -m "Projeto TODO List - Spec-Driven Development Completo

- Arquitetura MVC monolítica
- Armazenamento em memória (sem BD)
- 3 user stories implementadas: Cadastrar, Remover, Lembrete
- 42 tarefas concluídas em 6 fases
- Documentação MkDocs com 7 páginas
- Constitution com 6 princípios justificados
- Pronto para deploy em servidor gratuito"

# 5. Fazer push
git push origin main
```

**Resultado Esperado**:
```
Enumerating objects: XX, done.
Counting objects: 100% (XX/XX), done.
Writing objects: 100% (XX/XX), done.
Total XX (delta XX), reused 0 (delta 0)
To github.com:seu-usuario/seu-repositorio.git
   abc1234..def5678  main -> main
```

---

### PASSO 2️⃣: DEPLOY EM RAILWAY (10 min)

**Objetivo**: Publicar aplicação em servidor gratuito e obter URL pública

**Instruções Passo-a-Passo**:

1. **Abrir Railway**
   - Ir em https://railway.app
   - Fazer login com GitHub (clicar "Continue with GitHub")
   - Autorizar Railway

2. **Criar Novo Projeto**
   - Clicar "+ New Project"
   - Selecionar "Deploy from GitHub"
   - Autorizar se pedido

3. **Selecionar Repositório**
   - Procurar "seu-repositorio"
   - Clicar em "Import"

4. **Configurar Build**
   - Railway deve detectar automaticamente que é ASP.NET Core
   - Se não, definir:
     - Tipo: .NET
     - Version: 8.0
   - Se project em subpasta:
     - Settings → Build → Root Directory: `TodoListMvc/`

5. **Esperar Deploy**
   - Status: "Building" → "Deploying" → "Running"
   - Pode levar 2-5 minutos

6. **Obter URL**
   - Ir para "Settings"
   - Procurar "Domains"
   - Copiar URL tipo: `https://seu-app-production.railway.app`

**✅ Testar**:
```
Abrir no navegador: https://seu-app-production.railway.app
Deve mostrar: Página de TODO List (lista de tarefas)
```

---

### PASSO 3️⃣: DEPLOY MKDOCS EM GITHUB PAGES (5 min)

**Objetivo**: Publicar documentação online em GitHub Pages

**Comandos** (executo um por um):

```bash
# 1. Navegar até o projeto
cd "c:\Users\julia\OneDrive\IFES\Documentos IFES\Extensão"

# 2. Fazer deploy MkDocs para GitHub Pages
.\.venv\Scripts\python.exe -m mkdocs gh-deploy
```

**Resultado Esperado**:
```
INFO    -  Cleaning site directory
INFO    -  Building documentation to directory: site
INFO    -  Cloning repository...
INFO    -  Deploying documentation to gh-pages...
✅ Success
```

**Configurar GitHub (se necessário)**:
1. Ir para GitHub → Seu Repositório
2. Settings → Pages
3. Source: `gh-pages` branch
4. Clicar "Save"

**✅ Testar**:
```
Abrir no navegador: https://seu-usuario.github.io/seu-repositorio
Deve mostrar: Página inicial da documentação (Material theme azul)
```

---

### PASSO 4️⃣: ATUALIZAR ENTREGA.md (2 min)

**Objetivo**: Preencher os 3 links reais na entrega

**Localização**: `c:\Users\julia\OneDrive\IFES\Documentos IFES\Extensão\ENTREGA.md`

**Substituir**:

```markdown
# ❌ ANTES (placeholders)

### Repositório GitHub
```
https://github.com/seu-usuario/seu-repositorio
```
Status: ⏳ Substitua `seu-usuario` e `seu-repositorio`

### Aplicação Publicada (Railway)
```
https://[seu-app]-production.railway.app
```
Status: ⏳ Deploy via Railway

### Documentação Online (GitHub Pages)
```
https://seu-usuario.github.io/seu-repositorio
```
Status: ⏳ Deploy via MkDocs
```

**POR**:

```markdown
# ✅ DEPOIS (valores reais)

### Repositório GitHub
```
https://github.com/julia-rodrigues-ifes/todo-list-mvc
```
Status: ✅ Repositório público

### Aplicação Publicada (Railway)
```
https://todo-list-mvc-production.railway.app
```
Status: ✅ Deployada em Railway

### Documentação Online (GitHub Pages)
```
https://julia-rodrigues-ifes.github.io/todo-list-mvc
```
Status: ✅ Publicada em GitHub Pages
```

**Depois de atualizar**:
```bash
git add ENTREGA.md
git commit -m "Atualizar links de entrega com URLs reais"
git push origin main
```

---

## ⏱️ TIMELINE

```
┌─────────────────────────────────────────────────────────────┐
│                                                             │
│  PASSO 1 (5 min)   → GitHub Push                            │
│  ↓                                                           │
│  PASSO 2 (10 min)  → Railway Deploy                         │
│  ↓                                                           │
│  PASSO 3 (5 min)   → GitHub Pages Deploy                    │
│  ↓                                                           │
│  PASSO 4 (2 min)   → Atualizar ENTREGA.md                   │
│                                                             │
│  TOTAL: ~22 MINUTOS                                        │
│                                                             │
│  RESULTADO: ENTREGA 100% COMPLETA ✅                       │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

---

## 🎓 VERIFICAÇÃO FINAL

Depois de completar todos os 4 passos, você terá:

### ✅ Código
```
Repository URL: https://github.com/seu-usuario/seu-repositorio
- Acesso público
- Todo o código e documentação
- Git history completo
```

### ✅ Aplicação Publicada
```
URL: https://seu-app-production.railway.app
- Acessível publicamente
- Funcionalidades testadas:
  □ Lista tarefas
  □ Criar tarefa
  □ Editar tarefa
  □ Marcar concluída
  □ Remover tarefa
  □ Adicionar lembrete
```

### ✅ Documentação Online
```
URL: https://seu-usuario.github.io/seu-repositorio
- Visão geral (index.md)
- Atividade SDD (aula-09-sdd.md)
- Especificação (spec.md)
- Plano (plan.md)
- Tasks (tasks.md)
- Constitution (constitution.md)
- Deploy (deploy.md)
```

### ✅ Arquivo de Entrega
```
Arquivo: ENTREGA.md
Contém:
- ✅ Link GitHub
- ✅ Link Aplicação
- ✅ Link Documentação
- ✅ Checklist completo
```

---

## 🚨 TROUBLESHOOTING

### Problema: "Git command not found"
**Solução**: 
- Instalar Git: https://git-scm.com
- Ou usar Git Bash que vem com instalação

### Problema: "Railway não detecta ASP.NET Core"
**Solução**:
- Ir em Settings → Environment
- Adicionar: `ASPNETCORE_ENVIRONMENT=Production`
- Guardar e fazer redeploy

### Problema: "MkDocs command not found"
**Solução**:
```bash
pip install mkdocs mkdocs-material
.\.venv\Scripts\python.exe -m mkdocs gh-deploy
```

### Problema: "Aplicação Railway lenta ao carregar"
**Solução**:
- Primeiro acesso pode demorar (cold start)
- Aguardar ~30s
- Recarregar página

### Problema: "GitHub Pages mostra 404"
**Solução**:
- Esperar 2-3 minutos após deploy
- Verificar se Settings → Pages → gh-pages branch
- Limpar cache do navegador (Ctrl+Shift+Del)

---

## ✅ CHECKLIST FINAL

Antes de enviar, verifique:

- [ ] Projeto compila: `dotnet build` → SUCCESS
- [ ] Git push realizado: `git log --oneline` mostra commits
- [ ] Railway deploy completo: URL acessível (não 502 ou erro)
- [ ] Aplicação funciona: Consegue criar/remover/lembrete
- [ ] MkDocs publicado: URL acessível (material theme azul)
- [ ] ENTREGA.md atualizado: 3 links reais preenchidos
- [ ] Links testados: Todos os 3 URLs funcionam no navegador

---

## 🎉 PRONTO!

Após completar estes 4 passos, você terá:

✅ **Código no GitHub**  
✅ **Aplicação Publicada**  
✅ **Documentação Online**  
✅ **Arquivo ENTREGA.md Preenchido**  

**Sua entrega estará 100% COMPLETA** 🚀

---

**Tempo Total Estimado**: ~22 minutos  
**Dificuldade**: ⭐ Fácil (passos simples)  
**Suporte**: Consulte AUDITORIA_COMPLETA.md ou RESUMO_AUDITORIA.md

Boa entrega! 🎓
