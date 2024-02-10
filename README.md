# GungeonAlly

GungeonAlly is Blazor WASM Web App for aiding your adventures in Enter the Gungeon. Use it to quickly look up key information without having to search through different wiki pages.

## Installation

### Dependencies:

* Docker - This project is deployed as a set of docker containers, and uses ASP.NET Core 7.0 and SQL Server 2022 images.

Clone the repository in order to download the necessary projects for building and running the app.

```
git clone --recursive https://github.com/amayesingnathan/GungeonAlly.git
```

From the solution directory, execute the command `docker-compose up`. This will download an ASP.NET and SQL Server image,
before running initialisation scripts to create the database and tables used by the app.

Next, it will execute a simple web scraper .NET application that will query the Enter the Gungeon wiki in order to populate the newly initialised database.

Finally, it will launch the Blazor WASM web app locally and is exposed on port 5000 on your machine. The port can be edited in the docker-compose file.

I created this app purely for personal use and runs on a small server in my home network, so it is not hosted anywhere publicly. 
By providing docker support, users can easily host their own instance of the web app to run and use.

## License

[MIT](https://github.com/amayesingnathan/GungeonAlly/blob/main/LICENSE)
