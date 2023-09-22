# Grupos de Corrida

Projeto desenvolvido durante as vídeo aulas "Teddy Smith - ASP.NET Core MVC 2022 .NET 6". 

<img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" /> <img src="https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white" /> <img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" /> <img src="https://img.shields.io/badge/Rider-000000?style=for-the-badge&logo=Rider&logoColor=white" /> <img src="https://img.shields.io/badge/Pop!_OS-48B9C7?style=for-the-badge&logo=Pop!_OS&logoColor=white" />

## Descrição do Projeto

Uma plataforma online para corredores. 
Esta plataforma irá ajudá-lo a encontrar clubes, agendar eventos e conhecer outros corredores em sua área.

## Imagens

<img src="/imagem-projeto.png">

## Como usar 

1. Clonar esse repositório:
   
```bash
git clone https://github.com/csleal/DesafioWebApp
cd DesafioWebApp
```
2. Criar um Banco de dados local SQL Server [Guia de Instalação](https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-setup?view=sql-server-ver16)

3. Adicione a string de conexão no arquivo app settings.json. https://www.connectionstrings.com/sql-server/
   
 ```bash
Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword; Trusted_Connection=True;
```

4. Registre-se para obter uma [conta Cloudinary](https://cloudinary.com/users/register/free) (%100 grátis) e adicione Cloudname, ApiKey e segredo de API no appsettings.json.

5. Registre-se para obter um token de uma API de Geo Localização https://ipgeolocation.io/
e adicione sua url com token em DesafioWebApp/Controllers/HomeController.cs na variavel local url.

 ```bash
string url = "https://ipinfo.io?token=99999999999999";
```

### Referências 

* [Playlist ASP.NET Core MVC 2022 .NET 6 - MVC Explained](https://www.youtube.com/watch?v=q2AcJmB03Io&list=PL82C6-O4XrHde_urqhKJHH-HTUfTK6siO&pp=iAQB)
* [Thank you Teddy](https://github.com/teddysmithdev/RunGroop)
