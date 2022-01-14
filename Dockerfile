# How to stand up N-Tier architecture with Docker

# Get Base Image (Full .NET Core SDK)
FROM mcr.microsoft.com/dotnet/sdk:3.1 as sdkimage

WORKDIR /app

# Copy all layers .csproj files and .sln file in /app
COPY ./Business/*.csproj ./Business/
COPY ./ConsoleUI/*.csproj ./ConsoleUI/
COPY ./Core/*.csproj ./Core/
COPY ./DataAccess/*.csproj ./DataAccess/
COPY ./Entities/*.csproj ./Entities/
COPY ./WebAPI/*.csproj ./WebAPI/
COPY ./*.sln .

# Restore dependencies
RUN dotnet restore

# After restoring, copy all files to /app
COPY . .

# Just publish WebAPI layer (like set as startup project in Visual Studio)
RUN dotnet publish ./WebAPI/*.csproj -c Release -o publish

###########################################################################

# Generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=sdkimage /app/publish .
EXPOSE 80
ENTRYPOINT [ "dotnet","WebAPI.dll" ]