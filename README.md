🏛️ VOTA-E API
API REST para gestão de sugestões e projetos comunitários municipais, facilitando a participação cidadã nas decisões da cidade.

📋 Sobre o Projeto
O VOTA-E é uma plataforma que permite aos cidadãos submeterem sugestões para melhorias em sua cidade. As sugestões passam por uma bancada avaliadora do município e podem ser convertidas em projetos.
Os cidadãos podem votar nos projetos que mais gostarem e o projeto mais votado será implementado. Esse é um projeto desenvolvido para trabalhar lado a lado com a prefeitura e dar voz à população.

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

