# Declaração de Dependências (Dependencies)

Este documento detalha as dependências e pacotes NuGet utilizados nesta solução C# (.NET 8.0), para fins de fácil visualização na raiz do repositório.

---

## 🚀 Projeto Principal (src/PricingCalculator.App/PricingCalculator.App.csproj)
O projeto de linha de comando foi construído utilizando os recursos nativos do framework, visando alta performance e zero dependências externas de terceiros para a lógica de cálculo.

- **Framework:** .NET 10.0
- **Pacotes NuGet:** Nenhum pacote externo de terceiros exigido para a execução da CLI.

---

## 🧪 Projeto de Testes (tests/PricingCalculator.Tests/PricingCalculator.Tests.csproj)
O projeto de testes automatizados utiliza o ecossistema padrão do xUnit para garantir a qualidade do código principal:

- **xunit (v2.9.3):** Framework central para a criação e execução dos testes de unidade.
- **xunit.runner.visualstudio (v3.1.4):** Permite a execução dos testes dentro do Visual Studio e via linha de comando.
- **Microsoft.NET.Test.Sdk (v17.14.1):** Infraestrutura base para execução de testes em .NET.
- **coverlet.collector (v6.0.4):** Coletor de dados para análise de cobertura de código.

---

## ⚙️ Nota Técnica
Conforme o padrão oficial da arquitetura .NET, as referências reais e versões exatas dos pacotes estão injetadas diretamente nos arquivos `.csproj` localizados em suas respectivas pastas. Este arquivo serve como um manifesto de alto nível para consulta rápida.