# Instruções de Uso

1. **Clone o repositório** em sua máquina.

2. **Configuração do JWT**  
   Acesse o arquivo `appsettings.json` no projeto .NET 8 e crie uma Key de criptografia para o JWT.

3. **Subindo os containers**  
   Abra o terminal na pasta raiz do projeto (onde está o arquivo `docker-compose.yml`) e execute:
   docker-compose up
   
4. **Acesso à aplicação**
	Aguarde a criação dos containers e acesse a página http://localhost:3000 em seu navegador.
	
5.  **Usuário Seed para login**

O projeto já está preparado com um Seed para popular a base com o usuário abaixo. Você pode logar com as seguintes credenciais:
Email: admin@admin.com.br
Senha: Admin123