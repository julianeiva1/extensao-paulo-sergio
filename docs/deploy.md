# Deploy: Publicação em Servidor Gratuito

**Status**: Guia de deployment ⏳

## Opções de Servidor Gratuito para ASP.NET Core

### 1. Railway ⭐ Recomendado

**Vantagens:**
- ✅ ASP.NET Core suportado
- ✅ GitHub integration direto
- ✅ Deploy automático em push
- ✅ Tier gratuito generoso
- ✅ Interface amigável
- ✅ Plano free: $5/mês de crédito

**Passos:**

1. **Criar conta**
   - Ir em https://railway.app
   - Login com GitHub

2. **Conectar repositório**
   - "New Project" → "Deploy from GitHub"
   - Autorizar GitHub
   - Selecionar repositório

3. **Configurar aplicação**
   - Plataforma: ASP.NET Core
   - Service: Criar novo
   - Railway detecta automaticamente

4. **Variáveis de ambiente**
   - `ASPNETCORE_ENVIRONMENT`: Production
   - `ASPNETCORE_URLS`: http://*:8080 (Railway redireciona porta)

5. **Deploy automático**
   - Cada push para `main` gera novo deploy
   - Verifica status em Railway Dashboard
   - URL pública gerada automaticamente

6. **Acessar aplicação**
   - URL tipo: `https://seu-projeto-production.railway.app`
   - Aparece no Dashboard do Railway

---

### 2. Heroku (Alternativa)

**Vantagens:**
- ✅ ASP.NET Core suportado
- ✅ Buildpack automático
- ❌ Tier free descontinuado (nov/2022)
- ❌ Requer crédito no cartão agora

**Não recomendado** para esta atividade (usar Railway em vez disso)

---

### 3. Azure App Service (Alternativa)

**Vantagens:**
- ✅ ASP.NET Core nativo
- ✅ Tier gratuito: 1 app grátis
- ✅ Integração GitHub Actions
- ❌ Pode exigir verificação de cartão

**Passos (resumido):**
1. Criar conta Azure free
2. Criar "App Service"
3. Conectar GitHub repository
4. Configurar runtime: .NET 8
5. Deploy automático

**URL**: tipo `seu-app.azurewebsites.net`

---

### 4. Render

**Vantagens:**
- ✅ ASP.NET Core suportado
- ✅ Tier free com crédito
- ✅ GitHub integration
- ✅ PostgreSQL gratuito (em caso de usar DB depois)

**Passos (resumido):**
1. https://render.com
2. "New +" → "Web Service"
3. Conectar GitHub
4. Runtime: .NET 8
5. Deploy automático

---

## Configurações Necessárias para Deploy

### 1. Program.cs - Configuração de Produção

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<RepositorioTarefas>();

var app = builder.Build();

// Em produção: Tratamento de erros
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTPS strict transport security
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();
```

### 2. appsettings.Production.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 3. Dockerfile (Opcional, mas recomendado)

```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TodoListMvc/TodoListMvc.csproj", "TodoListMvc/"]
RUN dotnet restore "TodoListMvc/TodoListMvc.csproj"
COPY . .
RUN dotnet build "TodoListMvc/TodoListMvc.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "TodoListMvc/TodoListMvc.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "TodoListMvc.dll"]
```

### 4. .dockerignore

```
**/.classpath
**/.dockerignore
**/.env
**/.git
**/.gitignore
**/.project
**/.settings
**/.toolstarget
**/.vs
**/.vscode
**/*.*proj.user
**/*.dbmdl
**/*.jfm
**/azds.yaml
**/bin
**/charts
**/docker-compose*
**/Dockerfile*
**/node_modules
**/npm-debug.log
**/obj
**/secrets.dev.yaml
**/values.dev.yaml
LICENSE
README.md
```

---

## Deployment via Railway - Passo a Passo

### Pré-requisitos
- Conta GitHub com repositório (público ou privado)
- Código preparado em `main` branch
- Conta Railway (login com GitHub)

### 1. Preparar Repositório

```bash
# Garantir que .gitignore tem as pastas certas
git add .
git commit -m "Pronto para deploy"
git push origin main
```

### 2. Deploy no Railway

1. Acessar https://railway.app
2. Fazer login com GitHub
3. Clicar "+ New Project"
4. Selecionar "Deploy from GitHub repo"
5. Autorizar Railway no GitHub
6. Selecionar seu repositório
7. Railway detecta ASP.NET Core automaticamente

### 3. Configurar Build

Railway executa na pasta do projeto `.csproj`. Se em subpasta:

1. Ir para "Settings"
2. "Build" → "Root Directory": `TodoListMvc/`
3. Salvar

### 4. Variáveis de Ambiente

Adicionar se necessário:
- `ASPNETCORE_ENVIRONMENT`: Production
- `ASPNETCORE_URLS`: http://*:8080

### 5. Validar Deploy

1. Ir a "Deployments"
2. Clicar no deploy mais recente
3. Esperar "Build" → "Deploy" → "Running"
4. Clicar em "View logs" para acompanhar

### 6. Obter URL Pública

1. Ir a "Settings"
2. Copiar "Public Domain"
3. Acessar em navegador

---

## Verificação Pós-Deploy

### ✅ Checklist de Validação

- [ ] Aplicação carrega (sem erro 500)
- [ ] Página de lista funciona
- [ ] Criar tarefa funciona
- [ ] Editar tarefa funciona
- [ ] Remover tarefa funciona
- [ ] Adicionar lembrete funciona
- [ ] Remover lembrete funciona
- [ ] Responsivo em mobile (F12 device emulation)
- [ ] HTTPS funciona (🔒 na URL)
- [ ] Sem erros de console (F12)

### Endereço URL

```
https://[seu-app]-production.railway.app
```

Exemplo:
```
https://todo-list-ifes-production.railway.app
```

---

## Documentação MkDocs Online

### Publicar em GitHub Pages

1. **Instalar MkDocs**
   ```bash
   pip install mkdocs mkdocs-material
   ```

2. **Build local (teste)**
   ```bash
   mkdocs serve
   # Acessar http://localhost:8000
   ```

3. **Deploy para GitHub Pages**
   ```bash
   mkdocs gh-deploy
   ```

4. **Configurar GitHub Pages**
   - Ir para repository Settings
   - Em "Pages", selecionar source: `gh-pages` branch
   - Salvar

5. **Acessar documentação**
   ```
   https://seu-usuario.github.io/seu-repositorio
   ```

---

## Monitoramento

### Railway Dashboard

- Monitorar CPU e memória
- Ver logs em tempo real
- Reverter deploy anterior se necessário
- Métricas de requisições

### Logs

Acessar em Railway:
- Ir a aplicação
- "Deployment" → View logs
- Buscar por erros

---

## Troubleshooting

### Erro: Application startup exception

**Solução:**
- Verificar `appsettings.Production.json`
- Olhar logs completos em Railway
- Testar localmente em modo Production: `ASPNETCORE_ENVIRONMENT=Production dotnet run`

### Erro: Build failed

**Solução:**
- Validar que `.csproj` compila: `dotnet build`
- Verificar raiz do projeto em Railway Settings
- Checar logs do build

### Aplicação muito lenta

**Solução:**
- Tier free Railway tem limite de recursos
- Considerar upgrade se necessário
- Otimizar queries (não aplicável, in-memory)

### Dados desaparecem após restart

**Solução:**
- Esperado! Armazenamento in-memory por requisito
- Se quiser persistência depois, adicionar banco de dados

---

## Links Finais

Após deployment bem-sucedido, atualizar [ENTREGA.md](../ENTREGA.md) com:
- Link GitHub: `https://github.com/seu-usuario/seu-repositorio`
- Link Aplicação: `https://[seu-app]-production.railway.app`
- Link Documentação: `https://seu-usuario.github.io/seu-repositorio`

---

**Deployment Guide Completo** ✅
