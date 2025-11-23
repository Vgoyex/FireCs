# FireCs

API — File Upload Service

API backend desenvolvida em .NET para upload de arquivos para um bucket (Cloudflare R2, Amazon S3 ou outro storage compatível).
Este projeto serve como base sólida para sistemas que lidam com imagens, vídeos e documentos, mantendo alta organização, segurança e escalabilidade.

⚙️ Funcionalidades

🔐 Gerenciamento de Usuários

Criação de usuários
Validações automáticas
Armazenamento seguro de dados
Preparado para adicionar login em breve (JWT ou OAuth2)

☁ Upload de Arquivos

Uploads dos arquivos, imagens e vídeos
Envio direto para bucket configurado
Suporte a múltiplos arquivos
Tratamento de tipos permitidos
Armazenamento com chave única (UUID)

🖥️ Visualização Front-End
Visualização em formato de posts
Home de posts com algoritmo baseado em afinidade de usuários
Visualização dos arquivos em posts no front-end (web)
Visualização dos usuários

🔧Tecnologias Utilizadas

.NET 8+
ASP.NET Core Web API
Banco SQL(PostgreSQL)
Javascript/Node.js/React
HTML/CSS
Cloudflare R2 / Amazon S3 (via SDK)
Swagger / OpenAPI
RabbitMQ
Docker
NGinx
