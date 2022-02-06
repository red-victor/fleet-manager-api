
<div id="top"></div>

# Fleet Manager - Internal WEB Application API

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#developer-features">Developer Features</a></li>
        <li><a href="#integrated-services">Integrated Services</a></li>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#development-team">Development Team</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Fleet Manager is an Internal website built for managing a company's vehicle assets, their usage, maintenance and costs.

<p align="right"><a href="#top">Top ↑</a></p>


### Developer Features

- Entity CRUD
- Registration/Login with JWT Token
- Account Management
- User Roles
- File Import/Export (Excel, Images)
- Search
- Pagination
- Email notifications
- Error logging
<p align="right"><a href="#top">Top ↑</a></p>

### Built With

 Web API design pattern
React JS

* Back End:  [ASP .NET Core][asp-net-core],  [C#][c#],  [Entity Framework Core][ef-core],   [AutoMapper][auto-mapper],  [Office Open XML][office-open-xml]
* Security:  [Identity][identity-core]
* Front End:  [HTML][html],  [CSS][css],   [JavaScript][js],   [React.js][react],   [Bootstrap][bootstrap]
* Database Management:  [Microsoft SQL Server][msql-server],   [Microsoft SQL Server Management Studio][ssms]
* IDE:  [Microsoft Visual Studio][visual-studio]
* Deployment: [Docker][docker]
<p align="right"><a href="#top">Top ↑</a></p>

### Integrated Services

* Email:  [MailKit][mail-kit]
<p align="right"><a href="#top">Top ↑</a></p>


<!-- GETTING STARTED -->
## Getting Started

### Installation

**This Project Requires _both_ API and [Client][project-client] app in order to run.**

- Clone the Repo on your machine and open the solution with **Visual Studio**.
- Create a MSSQL database
- Add User Secret to Project:
	```json
	"ConnectionStrings:Default": 
		"Data Source={YOUR SERVER};Initial Catalog=FleetManager;Integrated Security=True;"
	```
- *(optional)* Create a Mail Kit account [here][mail-kit]
- *(optional)* Add User Secret to Project -> Fill in the ...... [how to locate them in your MailKit account][stripeKey]
    ```json
	"MailSettings": {
	    "Mail": "",
	    "DisplayName": "",
	    "Password": "",
	    "Host": "",
	    "Port": ""
	  },
    ```
- Follow the [Client][project-client] Steps if you haven't already.
<p align="right"><a href="#top">Top ↑</a></p>



<!-- USAGE EXAMPLES -->
## Usage

Run the project with IIS Express.

<p align="right"><a href="#top">Top ↑</a></p>


## Development Team

* [Victor Nicolae's GitHub][victor-nicolae]
* [Adrian Deaconu's GitHub][adrian-deaconu]
* [Alex Buza's GitHub][alex-buza]

<p align="right"><a href="#top">Top ↑</a></p>

<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

Thanks for all the support to the [Codecool][codecool] mentors that have guided us!


<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->

[project-client]: https://github.com/red-victor/fleet-manager-client
[project-api]: https://github.com/red-victor/fleet-manager-api

[asp-net-core]: https://dotnet.microsoft.com/en-us/learn/aspnet/what-is-aspnet-core
[ef-core]: https://docs.microsoft.com/en-us/ef/core/
[auto-mapper]: https://automapper.org/
[c#]: https://docs.microsoft.com/en-us/dotnet/csharp/
[html]: https://html.com/
[css]: https://www.w3.org/Style/CSS/Overview.en.html
[js]: https://www.javascript.com/
[react]: https://reactjs.org/
[react-net]: https://reactjs.net/
[bootstrap]: https://getbootstrap.com
[jquery]: https://jquery.com
[msql-server]: https://www.microsoft.com/en-us/sql-server/sql-server-2019
[ssms]: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15
[visual-studio]: https://visualstudio.microsoft.com/
[identity-core]: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio
[docker]: https://www.docker.com/

[victor-nicolae]: https://github.com/red-victor
[adrian-deaconu]: https://github.com/AdiDD
[alex-buza]: https://github.com/alexmarian99

[codecool]: https://codecool.com/en/

[mail-kit]: https://www.mailkit.com/
[office-open-xml]: https://epplussoftware.com/docs/5.0/api/OfficeOpenXml.html
