FROM node:latest AS build-fe
WORKDIR /app
COPY keepdaily_fe/package*.json ./
RUN npm install
COPY keepdaily_fe/ .
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["keepdaily_be/keepdaily_be/keepdaily_be.csproj", "keepdaily_be/"]
COPY ["keepdaily_be/DataLayer/DomainLayer.csproj", "DataLayer/"]
COPY ["keepdaily_be/RepoLayer/RepoLayer.csproj", "RepoLayer/"]
COPY ["keepdaily_be/ServiceLayer/ServiceLayer.csproj", "ServiceLayer/"]
RUN dotnet restore "keepdaily_be/keepdaily_be.csproj"
RUN mkdir /DayImgs
RUN mkdir /UserImgs
RUN mkdir /Videos
COPY . .
WORKDIR "/src/keepdaily_be"
RUN dotnet build "keepdaily_be/keepdaily_be.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "keepdaily_be/keepdaily_be.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN apt-get -y update
RUN apt-get -y upgrade
RUN apt-get install -y --no-install-recommends ffmpeg
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-fe /app/dist/keepdaily_fe ./ClientApp/dist
RUN mkdir -p /app/ffmpeg
RUN cp /usr/bin/ffmpeg /app/ffmpeg/
RUN cp /usr/bin/ffplay /app/ffmpeg/
RUN cp /usr/bin/ffprobe /app/ffmpeg/
ENTRYPOINT ["dotnet", "keepdaily_be.dll"]