# How To Lookup Services BaseUrl?

## ApplicationInsights

[DomainNameRoleInstanceTelemetryInitializer](https://github.com/Microsoft/ApplicationInsights-aspnetcore/blob/04b5485d4a8aa498b2d99c60bdf8ca59bc9103fc/src/Microsoft.ApplicationInsights.AspNetCore/TelemetryInitializers/DomainNameRoleInstanceTelemetryInitializer.cs)

~~~csharp
private string GetMachineName()
        {
            string hostName = Dns.GetHostName();

            // Issue #61: For dnxcore machine name does not have domain name like in full framework 
#if !NETSTANDARD1_6
            string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            if (!hostName.EndsWith(domainName, StringComparison.OrdinalIgnoreCase))
            {
                hostName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", hostName, domainName);
            }
#endif
            return hostName;
        }
~~~

[IISExtension](https://github.com/aspnet/IISIntegration/blob/df88e322cc5e52db3dbce4060d5bc7db88edb8e4/src/Microsoft.AspNetCore.Server.IISIntegration/WebHostBuilderIISExtensions.cs#L19)