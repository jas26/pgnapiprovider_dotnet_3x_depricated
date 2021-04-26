FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

RUN useradd -d /home/pgnuser -m -s /bin/bash pgnuser


WORKDIR /app
ADD ./app .

RUN chmod a+rwx $(find . -type d)

USER pgnuser

ENTRYPOINT ["dotnet", "PGN_Db_Operations.dll"]