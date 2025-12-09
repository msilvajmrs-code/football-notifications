# FootballNotifications

## Solution overview

This solution implements a small sample system that processes football events and publishes notifications. It demonstrates a producer HTTP API, message publishing (RabbitMQ), and two simple consumer/display projects.

### Projects

- `Visma.Technical.FootballProducer` — HTTP API that receives football events, processes them and publishes messages. Configuration is in `Visma.Technical.FootballProducer/appsettings.json`.  
- `Visma.Technical.DisplayNotifications` — consumer that subscribes to notification messages and displays them.  
- `Visma.Technical.DisplayScores` — consumer that subscribes to score update messages and displays them.  
- `Visma.Technical.Core` — shared contracts, models, features and infrastructure.  
- `Visma.Technical.UnitTests` — unit tests for core features and messaging.

### Visual Studio

Developed and tested with Visual Studio 2026. If opened in a different VS version, the IDE may prompt to retarget projects or install missing workloads.


### Run three projects at the same time (Visual Studio)

1. Right‑click the solution and choose __Properties__.  
2. Open __Startup Project__.  
3. Select __Multiple startup projects__.  
4. For `Visma.Technical.FootballProducer`, `Visma.Technical.DisplayNotifications`, and `Visma.Technical.DisplayScores` set the action to `Start`.  
5. Save and press F5 (or click Start). Visual Studio will launch all selected projects.

To submit tests go to the API url /scalar to execute some events.

### Run three projects at the same time (command line)

Open three terminal windows (or tabs) and run each project from the repository root:
```
dotnet run --project .\Visma.Technical.FootballProducer\Visma.Technical.FootballProducer.csproj 
dotnet run --project .\Visma.Technical.DisplayNotifications\Visma.Technical.DisplayNotifications.csproj 
dotnet run --project .\Visma.Technical.DisplayScores\Visma.Technical.DisplayScores.csproj
```
### Additional notes

- The producer reads RabbitMQ settings from `Visma.Technical.FootballProducer/appsettings.json`. Ensure a RabbitMQ broker is reachable or update settings to point to a running instance for message delivery to work.  
- Run unit tests with `dotnet test` at the solution root.  
- Verify ports and firewall rules if services cannot communicate.