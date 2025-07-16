FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/EmployeeLeave.Web.Host/EmployeeLeave.Web.Host.csproj", "src/EmployeeLeave.Web.Host/"]
COPY ["src/EmployeeLeave.Web.Core/EmployeeLeave.Web.Core.csproj", "src/EmployeeLeave.Web.Core/"]
COPY ["src/EmployeeLeave.Application/EmployeeLeave.Application.csproj", "src/EmployeeLeave.Application/"]
COPY ["src/EmployeeLeave.Core/EmployeeLeave.Core.csproj", "src/EmployeeLeave.Core/"]
COPY ["src/EmployeeLeave.EntityFrameworkCore/EmployeeLeave.EntityFrameworkCore.csproj", "src/EmployeeLeave.EntityFrameworkCore/"]
WORKDIR "/src/src/EmployeeLeave.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/EmployeeLeave.Web.Host", "src/EmployeeLeave.Web.Host"]
COPY ["src/EmployeeLeave.Web.Core", "src/EmployeeLeave.Web.Core"]
COPY ["src/EmployeeLeave.Application", "src/EmployeeLeave.Application"]
COPY ["src/EmployeeLeave.Core", "src/EmployeeLeave.Core"]
COPY ["src/EmployeeLeave.EntityFrameworkCore", "src/EmployeeLeave.EntityFrameworkCore"]
WORKDIR "/src/src/EmployeeLeave.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "EmployeeLeave.Web.Host.dll"]
