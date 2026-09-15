# Employee Master &mdash; .NET 6 MVC Demo (for AWS Transform)

A deliberately small ASP.NET Core MVC app targeting **.NET 6** &mdash; one of
AWS Transform's documented legacy source versions (.NET Framework 3.5+,
.NET Core 3.1, .NET 5, .NET 6, .NET 7 &rarr; all modernizable to .NET 8.0+).
No database, no external NuGet packages, no auth &mdash; just a controller,
two Razor views, and an in-memory list, so there's nothing in the way of
seeing what AWS Transform actually does to it.

Verified locally: builds clean (0 warnings, 0 errors) and serves both pages
correctly.

```
EmployeeApp.sln
EmployeeApp/
├── EmployeeApp.csproj      # TargetFramework net6.0, zero package references
├── Program.cs               # minimal hosting model
├── Models/Employee.cs
├── Controllers/EmployeeController.cs   # Index / Create / Delete
├── Views/
│   ├── Shared/_Layout.cshtml
│   └── Employee/{Index,Create}.cshtml
└── wwwroot/css/site.css
```

## Running it locally

```bash
cd EmployeeApp
dotnet run
```
Then open the URL it prints (usually `http://localhost:5000` or similar).

## Using this with AWS Transform (.NET)

Unlike the "custom" CLI flow, .NET modernization runs through AWS Transform's
**web application** as an actual guided, click-through workflow. Rough
outline (see AWS's docs for the current, authoritative version &mdash; this
UI evolves):

1. **Push this project to a repo.** AWS Transform's .NET path only reads
   from GitHub, Bitbucket, or GitLab &mdash; you can't upload a zip directly.
   Create a repo, push this folder to it, and make sure there's a writable
   branch for AWS Transform to commit the transformed code back to.

2. **Sign in to AWS Transform** at the web application URL (Transform must
   be enabled in your AWS account first, via IAM Identity Center).

3. **Create a .NET job** from your workspace landing page, and confirm the
   details AWS Transform asks about in the chat panel.

4. **Set up a source code connector.** Chat with the agent in the left
   pane &mdash; it walks you through creating an AWS CodeConnections
   connection to your repo. An AWS administrator will need to approve the
   connection in the CodeConnections console.

5. **Let it analyze the repo.** AWS Transform scans for supported .NET
   project types, shows you what it found (this repo, one project,
   default branch), and proposes a modernization plan targeting .NET 8.0+.
   By default it will also offer to convert the MVC Razor views &mdash;
   though since this project is already ASP.NET Core (not classic
   ASP.NET Framework MVC), that specific conversion won't have much to do;
   the version upgrade itself is the main thing to watch here.

6. **Review and kick off the bulk transformation.** Confirm which
   repo/projects to include, then AWS Transform pulls the code into a
   managed development environment and starts transforming.

7. **Track progress** via the Worklog (detailed action log) or Dashboard
   (high-level metrics) in the web app.

8. **Review the result in Visual Studio.** Once transformed (fully or
   partially), switch to the AWS Transform extension in Visual Studio to
   inspect the diff, fix any remaining build errors, and approve
   completion.

## Note on scope

This is intentionally the simplest possible legacy-flavored .NET app, not a
showcase of AWS Transform's flagship "classic ASP.NET Framework MVC \u2192
ASP.NET Core Razor" conversion feature (that specifically applies to
System.Web-based MVC 5 apps on .NET Framework, which don't build outside
Windows/Visual Studio and can't be verified in this environment). If you
want to specifically exercise that conversion path, the next step up is a
.NET Framework 4.x MVC 5 project &mdash; ask and I can build that version,
just note it can only really be verified by building it in Visual Studio
on Windows.
