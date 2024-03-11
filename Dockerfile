FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY . .
RUN dotnet restore
WORKDIR /source
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 5045
ENV ASPNETCORE_URLS=http://+:5045
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "pix.dll"]

CMD [ "dotnet", "watch" ]