# Nome do Projeto

Aplicação C# com integração de GitHub Actions para automação de build, testes e deploy.

## 📦 Tecnologias

- C# (.NET 8)
- GitHub Actions
- Docker (opcional)
- GitFlow

## 🚀 Estrutura dos Workflows

| Workflow                         | Quando é executado                           |
|----------------------------------|----------------------------------------------|
| 01 - Pull Request para Develop   | Ao abrir/fechar um PR para a branch `develop` |
| 02 - Merge para Master           | Ao fazer merge de um PR para a branch `master` |
| 03 - Feature Branch              | Ao fazer push em branches com prefixo `feature/` |

## 📁 Estrutura de Pastas

