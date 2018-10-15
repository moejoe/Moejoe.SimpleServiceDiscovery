# Status

`Experimental` - The first version is currently in development no part of this can be considered to be anywhere near testable. Expect major changes. 

# Description (What)

Service zum registrieren und entdecken von microservices.

## Discovery

### HttpClient (`untested`)
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

### Rest-Sharp (`Not Supported`)
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

### Asp.Net Core 2.1 (`Not Supported`)

~~~csharp

	public void Configuration(this IServiceCollection services) {
		services.DiscoverHttpClient("myServiceClient", "myService", client => {
			// No need to set Base address - as it will be set by the DiscoveryProcess
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			client.DefaultRequestHeaders.Add("User-Agent", "myServiceClientExample");
		});
	}
~~~



## Registration (`untested`)

~~~c-sharp 
    _registrationService.Register(new ServiceInstance {
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



## Health Checks (`Not Supported`)


## Capabilities

* Register Service (`untested`)
* Unregister Service (`untested`)
* Discover Service (`untested`)

# Future Features

* Simple Healthchecks
* Rediscover after failure
* Support Api Versioning


# Limitations

* No Security
* No Load - Balancing
* Untested


# Glossary

+   **service-instance**: An instance of a service that implements a *service-definition*
+   **Registry-Agent**: The Application that provides access to the *service-registry*
+   **service-registry**: A set of *service-instances* that implement *service-definitions*.
