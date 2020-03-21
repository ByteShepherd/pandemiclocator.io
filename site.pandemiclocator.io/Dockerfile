#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["site.pandemiclocator.io/site.pandemiclocator.io.csproj", "site.pandemiclocator.io/"]
RUN dotnet restore "site.pandemiclocator.io/site.pandemiclocator.io.csproj"
COPY . .
WORKDIR "/src/site.pandemiclocator.io"
RUN dotnet build "site.pandemiclocator.io.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "site.pandemiclocator.io.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "site.pandemiclocator.io.dll"]