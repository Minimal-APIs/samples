# React Todo APP with Minimal APIs 


**Goal**: In this exercise, you will be building the backend of a TodoReact App using minimal APIs in C#. 


# Prerequisites

1. Install [.NET Core 6.0](https://dotnet.microsoft.com/download)
1. Install [Node.js](https://nodejs.org/en/)



Build the backend with minimal APIs
-------------------------------------------------------

**Please Note: The completed exercise is available in the [samples folder](/Complete-Sample). Feel free to reference it at any point during the tutorial.**
###  Run the frontend application

1. Once you clone the Todo repo, navigate to the `TodoReact` folder inside of the `React\Tutorial` folder and run the following commands 
```sh
TodoReact> npm i 
TodoReact> npm start
```
- The commands above
    - Restores packages `npm i `
    - Starts the react app `npm start`
1. The app will load but have no functionality
![todo-frontend-react](https://user-images.githubusercontent.com/2546640/130237627-855bd837-12e6-4f23-a471-8e73c0f31e56.png)

    > Keep this React app running as we'll need it once we build the back-end in the upcoming steps

### Build the minimal AP backend 
**Create a new project**

1. Create a new minimal API  and add the necessary packages in a new project called `TodoApi` 

``` sh
Tutorial> dotnet new web -o TodoApi
Tutorial> cd TodoApi
TodoApi> dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 5.0.7
```
   - The commands above
     - create a new  named  `TodoApi`
     - Change directory `TodoApi`
     - Adds the NuGet packages required in the next section `dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 5.0.7`

2.  Open the `TodoApi` Folder in editor of your choice.

Your `Program.cs` looks like this

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

## Create the database model

1. Create a class that models the data we want to collect. The code for your TodoItem will go after `app.Run()`;

   ```csharp
   class TodoItem
   {
       public int Id { get; set; }
       public string? Item { get; set; }
       public bool IsComplete { get; set; }
   }
   ```
   The above model will be used for reading in JSON and storing todo items into the database.

1. Add `using Microsoft.EntityFrameworkCore;` to the top of your `Program.cs file`.

1. Below the TodoItem create a TodoDb class

    ```csharp
   class TodoDb : DbContext
   {
       public TodoDb(DbContextOptions options) : base(options) { }
       public DbSet<TodoItem> Todos { get; set; }
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
            optionsBuilder.UseInMemoryDatabase("Todos");
        }
    }
    