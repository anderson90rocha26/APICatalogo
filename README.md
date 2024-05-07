# APICatalogo (Usando Controladores)

## Escopo

**Criar uma Web API para um catálogo de produtos/categorias que pode atender uma rede de lojas ou supermercados!**
- Criar um serviço RESTful que permita que aplicativos clientes gerenciem o catálogo de produtos e categorias
- Expor endpoints para criar, ler, editar e excluir produtos e também para consultar produtos e um produto específico
- Expor endpoints para criar, ler, editar e excluir categorias e também para consultar categorias, uma categoria específica e os produtos de uma categoria
- Para categoria, precisamos armazenar: o nome e o caminho da imagem
- Para produtos, precisamos armazenar: o nome, descrição, valor unitário, caminho da imagem, estoque, data do cadastro e categoria

**Definição dos recursos, dos endpoints e do mapeamento**

**Endpoint da API: /v1/api/produtos**
- Para consulta: GET (/v1/api/produtos)
- Para consulta: GET (/v1/api/produtos/1)
- Para inclui produto: Post (/v1/api/produtos)
- Para alterar: PUT (/v1/api/produtos/1)
- Para excluir: DELETE (/v1/api/produtos/1)

A mesma coisa para categoria

**Implementação de segurança**

- Permitir o acesso às APIs somente a usuários autenticados
- Definir uma política de autorização de acesso aos usuários
- Criar um serviço RESTfull que permita gerenciar os usuários
- Expor endpoints para criar, ler, editar e excluir usuários e também para consultar usuários e um usuário específico
- Para usuário precisar armazenar: nome, e-mail e senha. 
