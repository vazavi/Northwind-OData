# Azure Reverse Proxy 

See http://ruslany.net/2014/05/using-azure-web-site-as-a-reverse-proxy/ for full details.

Any site hosted in Azure Web Sites has URL Rewrite and ARR enabled. However the proxy functionality is disabled by default in ARR. To enable that we will use the Azure Site Extension XDT transform which will modify the applicationHost.config file for our site and will enable proxy features.

This is the xdt transform file content to enable proxy:

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer xdt:Transform="InsertIfMissing">
    <proxy xdt:Transform="Remove" />
    <proxy xdt:Transform="InsertIfMissing" enabled="true" preserveHostHeader="false" reverseRewriteHostInResponseHeaders="false" />
    <rewrite xdt:Transform="InsertIfMissing">
      <allowedServerVariables xdt:Transform="Remove" />
      <allowedServerVariables xdt:Transform="InsertIfMissing">
        <add name="HTTP_X_ORIGINAL_HOST" xdt:Transform="InsertIfMissing" />
        <add name="HTTP_X_UNPROXIED_URL" xdt:Transform="InsertIfMissing" />
        <add name="HTTP_X_ORIGINAL_ACCEPT_ENCODING" xdt:Transform="InsertIfMissing" />
        <add name="HTTP_ACCEPT_ENCODING" xdt:Transform="InsertIfMissing" />
      </allowedServerVariables>
    </rewrite>
  </system.webServer>
</configuration>
```

Create a file named applicationHost.xdt and copy paste this code in there. Then upload this file into the “site” directory of your web site:

After that restart your site for the change to take effect.

Next add the following rewrite rule:

```xml
<?xml version="1.0"?>  
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">  
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="ForceSSL" stopProcessing="true">
          <match url="(.*)" />
          <conditions>
            <add input="{HTTPS}" pattern="^OFF$" ignoreCase="true" />
          </conditions>
          <action type="Redirect" url="https://{HTTP_HOST}/{R:1}" redirectType="Found" />
        </rule>
        <rule name="Proxy" stopProcessing="true">
          <match url="(.*)" />
          <serverVariables>
            <set name="HTTP_X_UNPROXIED_URL" value="http://gsawtor1303/{R:1}" />
            <set name="HTTP_X_ORIGINAL_ACCEPT_ENCODING" value="{HTTP_ACCEPT_ENCODING}" />
            <set name="HTTP_X_ORIGINAL_HOST" value="{HTTP_HOST}" />
            <set name="HTTP_ACCEPT_ENCODING" value="" />
          </serverVariables>
          <action type="Rewrite" url="http://gsawtor1303/{R:1}" />
        </rule>
      </rules>
      <outboundRules>
        <rule name="ChangeReferencesToOriginalUrl" patternSyntax="ExactMatch" preCondition="CheckContentType">
          <match filterByTags="None" pattern="http://gsawtor1303/" />
          <action type="Rewrite" value="https://{HTTP_X_ORIGINAL_HOST}/" />
        </rule>
        <preConditions>
          <preCondition name="CheckContentType">
            <add input="{RESPONSE_CONTENT_TYPE}" pattern="^(text/xml|application/json)" />
          </preCondition>
        </preConditions>
      </outboundRules>
      <rewriteMaps>
        <rewriteMap name="MapProtocol" defaultValue="http">
          <add key="ON" value="https" />
          <add key="OFF" value="http" />
        </rewriteMap>
      </rewriteMaps>
    </rewrite>
  </system.webServer>
</configuration>`
```