# Demo application with React + TypeScript + .NET Core + ASP.NET Core Mvc

`Work in still progress`

Next steps:
1) Show messages of Won / Lost when game ends;
2) Port to .Net Core; (DONE...)
3) Serve static files from Backend
3) Host it on Heroku;
4) Multiple strategies for Game instances storage (Redis, Mongo, InMemory)

![](example.png)

## How to build & run this project

```
$ cd path/to/project
$ npm install
$ typings install
$ npm start #this command will run webpack & dotnet run the API project
```

Yes, this is a .Net project and not a Node one.

## But why use npm to run the project and tests?

.Net CLI is still very poor and you can't really do much with it.
NPM Scripts on the other hand is extremely stable & powerful by now.
Use the best tool for job, right?