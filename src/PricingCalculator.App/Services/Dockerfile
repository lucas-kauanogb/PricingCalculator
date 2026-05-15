
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY *.sln .
COPY src/PricingCalculator.App/*.csproj ./src/PricingCalculator.App/
COPY tests/PricingCalculator.Tests/*.csproj ./tests/PricingCalculator.Tests/
RUN dotnet restore


COPY . .
WORKDIR /app/src/PricingCalculator.App
RUN dotnet publish -c Release -o /out


FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /out .


ENTRYPOINT ["dotnet", "PricingCalculator.App.dll"]