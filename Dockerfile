
#build configuration
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
COPY *.csproj .
WORKDIR /src
RUN dotnet restore
COPY src .
RUN dotnet publish -c Release -o /publish
#runtime configuration
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "EdenAI CSharp Code Test.dll"]