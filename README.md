# 🏛️ VOTA-E API

API REST para gestão de sugestões e projetos comunitários municipais, facilitando a participação cidadã nas decisões da cidade.

---

## 📋 Sobre o Projeto

O **VOTA-E** é uma plataforma cívica que permite aos cidadãos submeterem sugestões para melhorias na cidade. As propostas são avaliadas pela prefeitura e, se aprovadas, transformam-se em projetos públicos que podem ser votados pela população.

---

## 🚀 Funcionalidades Principais

- 🔐 Autenticação segura via JWT  
- 👥 Gestão de usuários  
- ✍️ Criação de sugestões  
- 🏛️ Integração com a câmara de vereadores  
- 🌍 *(Futuro)* Geolocalização para vincular usuários às suas cidades  

---

## 📦 Tecnologias

- ✅ **C# / .NET 8**
- 🧩 **Oracle Database**
- 🛠️ **Entity Framework** (com suporte a migrations)
- 🔐 **JWT** para autenticação
- 🔁 **GitHub Actions** (CI/CD)
- 🐳 **Docker** (opcional)
- 🌱 **GitFlow** para gerenciamento de branches
- ☁️ **Azure Web App** para deploy

---

## 🚀 Workflows CI/CD

| Workflow                           | Gatilho                                   | Ambiente   |
|------------------------------------|-------------------------------------------|------------|
| Continuous Integration (CI)        | Pull Request fechada na branch `develop`  | `develop`  |
| Continuous Delivery - Staging (CD) | Push em branch `release/**`               | `staging`  |
| Continuous Delivery - Produção (CD)| Push na branch `master`                   | `produção` |



---

## 📁 Estrutura do Projeto

```text
VotaE_API/
├── .github/                                         # Workflows de CI/CD
│   └── workflows/
│       ├── continuous-integration.yml               # CI - develop
│       ├── continuous-delivery.yml                  # CD - staging
│       └── continuous-delivery-producao.yml         # CD - produção
├── Controllers/                                     # Controllers da aplicação
├── Data/                                            # Contextos de banco de dados
├── Interface/                                       # Interfaces dos repositórios/serviços
├── Migrations/                                      # Migrations do Entity Framework
├── Models/                                          # Entidades do domínio
├── Repository/                                      # Implementações de acesso a dados
├── Services/                                        # Regras de negócio
├── ViewModel/                                       # ViewModels para entrada/saída de dados
├── Properties/
│   └── launchSettings.json                          # Configurações de execução local
├── appsettings.Development.json                     # Configurações específicas para dev
├── appsettings.json                                 # Configuração padrão
├── Dockerfile                                       # Definição do container da aplicação
├── Program.cs                                       # Ponto de entrada da aplicação
├── VotaE_API.http                                   # Arquivo de testes de endpoints HTTP
├── README.md                                        # Documentação do projeto
└── .gitignore                                       # Ignora arquivos desnecessários no Git
```

## 📥 Collection Postman

Abaixo está disponível a collection do Postman com os endpoints da API.

👉 [Baixar Collection](vota-e-cidade-inteligente/VotaE_API/vota-e-postman)


### Como importar no Postman:

1. Baixe o arquivo acima.
2. Abra o **Postman**.
3. Clique em **"Import"** no canto superior esquerdo.
4. Selecione o arquivo `VotaE.postman_collection.json`.
5. A collection será adicionada automaticamente.
