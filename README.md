# Simple Service Discovery

## Table Of Content

<!-- TOC -->

- [Simple Service Discovery](#simple-service-discovery)
    - [Table Of Content](#table-of-content)
    - [Status](#status)
    - [Description (What)](#description-what)
        - [Server](#server)
            - [Development Environment with InMemory Storage](#development-environment-with-inmemory-storage)
            - [Development Environment with Entity Framework InMemoryDatabase](#development-environment-with-entity-framework-inmemorydatabase)
            - [Environment with SQL Server (`Add Example`)](#environment-with-sql-server-add-example)
        - [Discovery Client](#discovery-client)
            - [HttpClient (`untested`)](#httpclient-untested)
            - [Rest-Sharp (`Not Supported`)](#rest-sharp-not-supported)
            - [Asp.Net Core 2.1 (`Not Supported`)](#aspnet-core-21-not-supported)
        - [Registration Client (`untested`)](#registration-client-untested)
            - [Asp.Net Core 2.1 Without IIS Proxy](#aspnet-core-21-without-iis-proxy)
                - [`Program.cs`](#programcs)
            - [Register manually](#register-manually)
        - [Health Checks (`Not Supported`)](#health-checks-not-supported)
        - [Capabilities](#capabilities)
    - [Future Features](#future-features)
    - [Limitations](#limitations)
    - [Glossary](#glossary)

<!-- /TOC -->

## Status

`Experimental` - The first version is currently in development no part of this can be considered to be anywhere near testable. Expect major changes. 

## Description (What)

Service zum registrieren und entdecken von microservices.

### Server

The server depends on an MVC WebApplication.

#### Development Environment with InMemory Storage

~~~csharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddServiceDiscoveryServer()
            .AddInMemoryServiceRegistryStore();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMvc();
    }
~~~

#### Development Environment with Entity Framework InMemoryDatabase

~~~csharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddEntityFrameworkInMemoryDatabase();
        services.AddServiceDiscoveryServer()
            .AddServiceRegistryStore(opt => { opt.UseInMemoryDatabase("testDb"); });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseMvc();
    }
~~~

#### Environment with SQL Server (`Add Example`)

### Discovery Client

#### HttpClient (`untested`)

~~~csharp
    var discoveryResult = await _discoveryClient.DiscoverAsync("myService");
    if(discoveryResult.IsError) {
        // handle discovery failure
    }
    var httpClient = new HttpClient();
    discoverResult.ApplyTo(httpClient);
    await httpClient.GetStringAsync("/resource/123");
    var resource = JsonConvert.DeserializeObject<Resource>(responseString);
        return resource;
~~~

#### Rest-Sharp (`Not Supported`)

~~~csharp
    public void Configuration(this IServiceCollection services) 
    {
        services.AddServiceDiscovery(o => {
            o.DiscoveryAgent = Configuration.GetSection("DiscoveryAgent");
        });

        services.Add<RestClient>( (svc) => {
            var discoveryClient = svc.Get<IDiscoveryClient>();
            var discoveryResult = _discoveryClient.Discover("myService");
            if(discoveryResult.IsError) {
                // handle discovery failure
            }
            return new RestClient(discoveryClient.BaseUrl);
        });
    }
~~~

#### Asp.Net Core 2.1 (`Not Supported`)

~~~csharp
    public void Configuration(this IServiceCollection services) {
        services.DiscoverHttpClient("myServiceClient", "myService", client => {
            // No need to set Base address - as it will be set by the DiscoveryProcess
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "myServiceClientExample");
        });
    }
~~~

### Registration Client (`untested`)

#### Asp.Net Core 2 (without C# 7.1 )

replace all calls 

~~~csharp
await CreateWebHostBuilder(args)
            .Build()
            .RegisterAndRun(...);
~~~

with:

~~~csharp
CreateWebHostBuilder(args)
            .Build()
            .RegisterAndRun(...).GetAwaiter().GetResult();
~~~

#### Asp.Net Core 2 (wit  C# 7.1)

This relies on the application to be accessible through the first of the serverUrls configured.
(By default this is `http://localhost:5000` )

- A unique id is created for the *service-instance* on startup.
- The *service-instance* is registered.
- If the registration fails the startup fails.
- The *service-instance* is unregistered on shutdown.

 `Program.cs`

~~~csharp
    private const string DiscoveryServerBaseUrl = "http://localhost:53357";
    private const string ServiceDefinitionName = "Example.Api"; // the serviceDefinition this api is implementing.

    public static async Task Main(string[] args) {
        await CreateWebHostBuilder(args)
            .Build()
            .RegisterAndRunAsync(DiscoveryServerBaseUrl,  ServiceDefintionName);
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
~~~

#### Asp.Net Core 2.1 With IIS Proxy

 `Program.cs`

~~~csharp
    private const string DiscoveryServerBaseUrl = "http://localhost:53357";
    private const string ServiceDefinitionName = "Example.Api"; // the serviceDefinition this api is implementing.
    private const string BaseUrl = "https://localhost/anApplicationPath/"; // the proxy baseUrl

    public static async Task Main(string[] args) {
        await CreateWebHostBuilder(args)
            .Build()
            .RegisterAndRunAsync(DiscoveryServerBaseUrl,  ServiceDefintionName, baseUrl: BaseUrl);
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
~~~

#### Register manually

~~~csharp
    var registrationClient = new RegistartionClient(DiscoveryServerBaseUrl);
    await registrationClient.RegisterAsync(new ServiceInstance {
            Id = "SimpleServiceInstance",
            BaseUrl = "http://simpleService.instance.com/",
            ServiceDefinition = "SimpleService"
    });
~~~

**ServiceDefinition**: The name of the *service-definition* this *service-instance* implements. (`required`)

**Id**: The id of the current agent. This must be unique for the system boundary (there must not be any other *service-instance* using the same id that registers with this *RegistryAgent*) (`required`)
**BaseUrl**: The apis *BaseUrl* (`required`)

~~**ApiVersion**: The Api Version this *service-instance* implements. (Default: 1)~~

~~**InstanceVersion**: Version Information for the instance. (Default: 1.0.0.0)~~

### Health Checks (`Not Supported`)

### Capabilities

- Register Service (`untested`)
- Unregister Service (`untested`)
- Discover Service (`untested`)

## Future Features

- Simple Healthchecks
- Rediscover after failure
- Support Api Versioning

## Limitations

- No Security
- No Load - Balancing
- Untested

## Glossary

- **service-instance**: An instance of a service that implements a *service-definition*
- **Registry-Agent**: The Application that provides access to the *service-registry*
- **service-registry**: A set of *service-instances* that implement *service-definitions*.
