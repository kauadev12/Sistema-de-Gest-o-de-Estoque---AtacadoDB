# Sistema de Gestão de Estoque - AtacadoDB

Sistema de gerenciamento de estoque desenvolvido em **ASP.NET Core MVC** com **MySQL**. O projeto permite cadastrar produtos, fornecedores e usuários, e já vem com um **seed.sql** populando o banco com produtos de tecnologia fictícios.

---

## 💻 Tecnologias Utilizadas

* **Backend:** ASP.NET Core MVC (.NET 9)
* **Banco de Dados:** MySQL
* **ORM:** Entity Framework Core
* **Frontend:** HTML, CSS, Bootstrap
* **Ferramenta de DB:** HeidiSQL (para criar e popular o banco)

---

## 📂 Estrutura do Projeto

```
MeuProjeto/
│
├─ Controllers/       -> Controladores MVC
├─ Models/            -> Models (Produto, Fornecedor, Usuario)
├─ Views/             -> Views Razor
├─ Database/          -> seed.sql (dados fictícios)
├─ wwwroot/           -> Arquivos estáticos (CSS, JS, imagens)
└─ Program.cs         -> Configuração do app
```

---

## ⚙️ Configuração do Banco de Dados

1. Crie o banco `atacadodb` no MySQL:

```sql
CREATE DATABASE atacadodb;
```

2. Importe a estrutura do banco (`estrutura.sql`) usando HeidiSQL, Workbench ou linha de comando.

3. Importe os dados de teste com o **seed**:

```bash
mysql -u root -p atacadodb < Database/seed.sql
```

4. Confirme que as tabelas foram populadas:

```sql
SELECT * FROM fornecedores;
SELECT * FROM produtos;
SELECT * FROM usuarios;
```

---

## 🚀 Como Executar

1. Abra o projeto no **Visual Studio** ou **VS Code**.
2. Verifique a string de conexão com o MySQL no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=127.0.0.1;Database=atacadodb;Uid=root;Pwd=sua_senha;"
}
```

3. Execute o projeto (F5 ou `dotnet run`).
4. Acesse no navegador: `https://localhost:5001` ou `http://localhost:5000`.

---

## 📌 Funcionalidades

* Cadastro, listagem, edição e exclusão de:

  * Produtos
  * Fornecedores
* Controle de estoque e estoque mínimo
* Pesquisa e filtros básicos
* Dados iniciais de teste (seed.sql) para simulação de estoque real de tecnologia

---

## ⚠️ Observações

* Este projeto é **para fins de estudo**.
* Os dados do `seed.sql` são **fictícios**, mas representam produtos e fornecedores reais da área de tecnologia.

---

## 📄 Licença

Este projeto está licenciado sob a licença MIT.
