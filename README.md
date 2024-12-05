🏛️ VOTA-E API
API REST para gestão de sugestões e projetos comunitários municipais, facilitando a participação cidadã nas decisões da cidade.

📋 Sobre o Projeto
O VOTA-E é uma plataforma que permite aos cidadãos submeterem sugestões para melhorias em sua cidade. As sugestões passam por uma bancada avaliadora do município e podem ser convertidas em projetos.
Os cidadãos podem votar nos projetos que mais gostarem e o projeto mais votado será implementado. Esse é um projeto desenvolvido para trabalhar lado a lado com a prefeitura e dar voz à população.

Funcionalidades Principais
•	Autenticação segura via JWT
•	Gestão de usuários
•	Criação de sugestões
•	Integração com a câmara de vereadores
•	(Futuro) Geolocalização para vincular usuários às suas cidades

🚀 Tecnologias Utilizadas
•	.NET 8
•	Oracle Database
•	Entity Framework (Migrations)
•	JWT (Autenticação)

📝 Endpoints Principais
Autenticação 
•	POST - /api/autenticacao/login – Gerar Token

Alguns endpoints estão com restrição de acesso, para poder testar todos os endpoints sem problema, gere um token como ‘adm’. 
Usuários
•	POST /api/usuario - Criar usuário
•	GET /api/usuario - Listar usuários
•	GET /api/usuario/{id} - Buscar usuário específico 
•	PUT /api/usuario/{id} - Atualizar usuário
•	DELETE /api/usuario/{id} - Deletar usuário
•	POST /api/usuario/total – Traz o total de usuários cadastrados

Sugestões
•	POST /api/sugestao - Criar sugestão
•	GET /api/sugestao - Listar sugestões
•	GET /api/sugestao/{id} - Buscar sugestão específica
•	PUT /api/sugestao/{id} - Atualizar sugestão
•	DELETE /api/sugestao/{id} – Deletar sugestão
•	POST /api/sugestao/total – Traz o total de sugestões cadastrados
 
Projetos
•	GET /api/projeto - Listar projetos
•	POST /api/projeto - Criar projeto
•	GET /api/projeto/{id} - Buscar projeto específico
•	PUT /api/projeto/{id} - Atualizar status do projeto
•	GET /api/projeto/porcentagem/sugestoes/projetos – Traz a porcentagem de sugestões que viraram projetos
•	GET /api/projeto/mais/votado – Traz o projeto mais votado
