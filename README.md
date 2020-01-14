# agent-orange-zest
Assumptions: 
Visual Studio community or better is installed on users machine, as well as the frameworks needed to run .NET and .NET core applications



## Installation and configuration
- In your working directory: 
```
git clone git@github.com:ChrismeanR/agent-orange-zest.git
```
- `cd C:\<working_directory>\agent-orange-zest`

## Running the solution

## URLs (Insomnia Rest Client)
Download (used instead of postman) https://insomnia.rest/ 
- import file: (note: you may be able to import this file into Postman)

Or

### Agent: 
- GET: http://localhost:5000/agent
- GET: http://localhost:5000/agent/{id}
- POST (create agent): http://localhost:5000/agent/
- PUT: http://localhost:5000/agent/

### Customer:
- GET: http://localhost:5000/customer
- GET: http://localhost:5000/customer/{id}
- POST: http://localhost:5000/customer
- PUT:http://localhost:5000/customer
- DELETE: http://localhost:5000/customer/{id}


### Gotchas
1. serializing and deserializing tripped me up more than I'd like
2. Storing data to a file was not as straight forward as I thought and I likely overcomplicated things.
3. 
