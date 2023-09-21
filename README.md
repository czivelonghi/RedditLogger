# Reddit Logger

## Technology Stack & Tools
- .NET 7.0 SDK (v7.0.401) - Windows x64
- xUnit Testing Framework

## Requirements For Initial Setup
- Windows 10
- Install [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/preview/). We recommend using Visual Studio 2022 Preview.
- Install [.NET 7.0 SDK (v7.0.401) - Windows x64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.401-windows-x64-installer). Install .NET 7.0 SDK (v7.0.401) - Windows x64.

## Setting Up
### 1. Clone/Download the Repository

### 2. Create a GitHub Repository
Create a GitHub repository at [https://github.com/new](https://github.com/new).

### 3. Connect to GitHub Repository
In your terminal, execute:
`git remote set-url origin <GIT_URL>`

For the **GIT_URL**, this will be the .git link to your personal repository on GitHub.

### 4. Push to GitHub Repository
In your terminal, execute:
`git push origin master`

### 5. Create a New Reddit Script App
- [Register](https://www.reddit.com/wiki/api/) for access.
- [Create](https://www.reddit.com/prefs/apps) a new script app by enter a name, choosing script option, and enter https://localhost:500 in redirect uri field.
- Retain App Id (below app name) and Secret.

### 6. Configure Application Variables
Edit the following variables in appsettings.json:
- **Id="YOUR_APP_ID"** (App Id)
- **Secret="YOUR_APP_SECRET"** (Secret)
- **Subreddits=""** (Add subreddits in this section)

### 7. Run application
Login with username and password used when creating new script app.

## Helpfull Links
- [Reddit API](https://www.reddit.com/dev/api/)
- [Reddit Wiki](https://support.reddithelp.com/hc/en-us/articles/16160319875092-Reddit-Data-API-Wiki)

