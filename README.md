# Description (What)

Service zum registrieren und entdecken von microservices.

## Discovery

### HttpClient 
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

### Rest-Sharp
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

### Asp.Net Core 2.1

~~~csharp

	public void Configuration(this IServiceCollection services) {
		services.DiscoverHttpClient("myServiceClient", "myService", client => {
			// No need to set Base address - as it will be set by the DiscoveryProcess
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			client.DefaultRequestHeaders.Add("User-Agent", "myServiceClientExample");
		});
	}
~~~



## Registration

~~~c-sharp
    _registrationService.Register(new )
~~~

**Name**: The name of the *service-definition* this *service-instance* implements. (`required`)

**Id**: The id of the current agent. This must be unique for the system boundary (there must not be any other *service-instance* using the same id that registers with this *RegistryAgent*) (`required`)

**ApiVersion**: The Api Version this *service-instance* implements. (Default: 1)

**InstanceVersion**: Version Information for the instance. (Default: 1.0.0.0)



## Health Checks

When registering a Service the 

## Capabilities

* Simple
* Supports Health Checks


# Possible Future Features

* 

# Limitations

* No Security
* No Load - Balancing


# Glossary

**service-instance**: 
**Registry-Agent**: The Application that provides access to the *service-registry*
**service-registry**: A set of *service-instances* that implement *service-definitions*.
