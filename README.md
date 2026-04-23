# Projeto De Criação API Gerenciamento De Reserva de Espaços

#### Alice Virgília Andrade
#### Orientador: Fred Aguiar

Atividade feita no curso técnico de Desenvolvimento de Sistemas sobre criação de uma API com funcionalidade para gerenciamento de reservas de espaços e com funcionalidade de realizar uma tratativa de erros, visando a qualidade e robustez do tratamento de erros e corrigir as falhas que geram no erro 500. Utilizando blocos de tratamentos de exceções (try/catch).

---

A implementação solicitada pelo orientador foca no método **Put**, onde o sistema deverá realizar uma verificação preventiva no banco de dados antes de confirmar qualquer alteração. Em resumo, essa API tem como funcionalidade comparar a sala e o horário enviado com os registros já existentes, garantindo que nenhum outro agendamento (com ID diferente) possua o mesmo horário no mesmo local; caso a duplicidade seja confirmada, a execução é interrompida e retorna um **Erro 500**, servindo como uma barreira de segurança que impede a entrada de dados redundantes e garante a integridade da tabela.

---

## Arquitetura do Projeto

O projeto utiliza uma arquitetura em camadas para garantir que cada parte do código tenha uma responsabilidade única, sendo separados em Pastas e classes:

| Pastas 📂 | Classes ⚙️ |
| :--- | :--- |
| **Controllers:** Porta de entrada da API, responsável por gerenciar as rotas e receber as requisições HTTP do usuário. | **ReservaController.cs:** Onde ocorre a tratativa de erro sendo aplicada para validar sala e horário. |
| **Services:** Centraliza as funções que não pertencem diretamente ao acesso a dados ou à interface. | **ReservaService.cs:** Responsável por organizar o fluxo de informações e regras de negócio. |
| **Repositories:** Abstrai a complexidade do Entity Framework e pelas consultas no SQL. | **ReservaRepository.cs:** Método que verifica se uma sala já está ocupada antes da persistência. |
| **Interface:** Define quais métodos uma classe deve obrigatoriamente implementar. | **IReservaRepository.cs:** Garante que os serviços de persistência sigam o mesmo padrão. |
| **Models:** Define a estrutura do objeto manipulado pelo sistema. | **Reserva.cs:** Propriedades da entidade (Sala, Usuário, Data e ID). |
| **Data:** Espelha as tabelas de dados em formato de código C#. | **AppDbContext.cs:** Configuração da conexão com o banco de dados SQLite. |

---


## Pacotes usados e instalados para esse projeto

- **Microsoft.EntityFrameworkCore.Sqlite (9.0.12):** Comunicação com o banco de dados SQLite.
- **Microsoft.EntityFrameworkCore.Tools (9.0.12):** Ferramentas para execução de comandos de Migrations.

---

## Conexão e comandos para o Banco De Dados

- **dotnet ef migrations add InitialCreate:** Gera o histórico de criação das tabelas baseado nas Models.
- **dotnet ef database update:** Cria o arquivo físico `.db` e estrutura o banco de dados.

---

## Métodos presentes na interface - (GET, POST, PUT, DELETE)

| Metódos 📝 | Funcionalidade 💻 |
| :--- | :--- |
| **GET** | Listagem assíncrona de todas as reservas cadastradas. |
| **GET by ID** | Busca os detalhes de uma reserva específica através do ID. |
| **POST** | Cadastro de novos agendamentos com validação de disponibilidade. |
| **PUT** | Atualização de reservas com tratativa de erro 500 para conflitos. |
| **DELETE** | Remoção definitiva de registros através do ID. |

---

## Testes de erros no método - PUT

* **Erro 400:** IDs da URL e do Body não coincidem.
* **Erro 500:** Tentativa de reservar sala/horário já existente no banco de dados.
* **Status 200:** Atualização realizada com sucesso após validação de integridade.

---

## Entrega Final e Considerações Finais

O projeto cumpre os requisitos de arquitetura em camadas e tratamento de exceções. A principal conquista foi garantir que a lógica de verificação interrompa a execução no momento exato, protegendo o banco de dados de informações inconsistentes.

#### Escola de Robótica e Programação - SENAI NOVA LIMA
