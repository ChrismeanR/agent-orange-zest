# agent-orange-zest
Assumptions: 
Visual Studio community or better is installed on users machine, as well as the frameworks needed to run .NET and .NET core applications


## Installation and configuration
1. In your working directory: 
```
git clone git@github.com:ChrismeanR/agent-orange-zest.git
```
2. In command window:
- `cd C:\<working_directory>\agent-orange-zest`

## Running the solution
In Visual Studio (2019), open the project, set API to the starting project. Run in debug, or without. Use the browser links below in Agent and Customer section, respectively, or leverage the Insomnia Rest Client and import `Insomnia_2020-01-15.json` which will have the objects I was using.

Via command in `dotnet core`

`cd C:\<working_directory>\agent-orange-zest\API`

`dotnet build`

`dotnet run`

Open a browser and use the links below, or use Insomnia Rest Client to trigger endpoints (after file import found in solution)

## URLs (Insomnia Rest Client)
Download (used instead of postman) https://insomnia.rest/ 
- import file: (note: you may be able to import this file into Postman)
`Insomnia_2020-01-15.json` after installing Insomnia client, import this file. Else, give Postman a try (it's just json, and the clients are quite similar)

Or, use the following:

### Agent: 
- GET: http://localhost:5000/agent
- GET: http://localhost:5000/agent/{id}
- POST (create agent): http://localhost:5000/agent/
- PUT: http://localhost:5000/agent/

### Customer:
- GET: http://localhost:5000/customer
- GET: http://localhost:5000/customer/{id} (note, this is the agentId)
- POST: http://localhost:5000/customer 
- PUT:http://localhost:5000/customer
- DELETE: http://localhost:5000/customer/{id}


### Gotchas
1. Serializing and deserializing tripped me up more than I'd like
2. Storing data to a file was not as straight forward as I thought and I likely overcomplicated things.

