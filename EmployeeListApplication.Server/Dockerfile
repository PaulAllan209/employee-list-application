FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Library needed for SQL Server authentication
# Make sure to install libraries like this in the final runtime image (base/final stage)
RUN apt-get update && apt-get install -y libgssapi-krb5-2

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy proj files
COPY ["EmployeeListApplication.Server/EmployeeListApplication.Server.csproj", "EmployeeListApplication.Server/"]
COPY ["EmployeeListApplication.Core/EmployeeListApplication.Core.csproj", "EmployeeListApplication.Core/"]

RUN dotnet restore "EmployeeListApplication.Server/EmployeeListApplication.Server.csproj"

#Copy all source code
COPY ["EmployeeListApplication.Server", "EmployeeListApplication.Server"]
COPY ["EmployeeListApplication.Core", "EmployeeListApplication.Core"]

WORKDIR "/src/EmployeeListApplication.Server"
# Build and publish a release
RUN dotnet publish EmployeeListApplication.Server.csproj -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "EmployeeListApplication.Server.dll" ]