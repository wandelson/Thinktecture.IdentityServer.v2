<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Azure" generation="1" functional="0" release="0" Id="5466d155-46f2-4147-a922-5a0b93c7f759" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WebSite:SSLEndpoint" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/Azure/AzureGroup/LB:WebSite:SSLEndpoint" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|WebSite:SSLCertificate" defaultValue="">
          <maps>
            <mapMoniker name="/Azure/AzureGroup/MapCertificate|WebSite:SSLCertificate" />
          </maps>
        </aCS>
        <aCS name="WebSite:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Azure/AzureGroup/MapWebSite:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WebSiteInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Azure/AzureGroup/MapWebSiteInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WebSite:SSLEndpoint">
          <toPorts>
            <inPortMoniker name="/Azure/AzureGroup/WebSite/SSLEndpoint" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapCertificate|WebSite:SSLCertificate" kind="Identity">
          <certificate>
            <certificateMoniker name="/Azure/AzureGroup/WebSite/SSLCertificate" />
          </certificate>
        </map>
        <map name="MapWebSite:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Azure/AzureGroup/WebSite/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWebSiteInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Azure/AzureGroup/WebSiteInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WebSite" generation="1" functional="0" release="0" software="C:\git\nobre\fork\Thinktecture.IdentityServer.v2\src\OnPremise\Azure\csx\Debug\roles\WebSite" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="3584" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="SSLEndpoint" protocol="https" portRanges="443">
                <certificate>
                  <certificateMoniker name="/Azure/AzureGroup/WebSite/SSLCertificate" />
                </certificate>
              </inPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WebSite&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WebSite&quot;&gt;&lt;e name=&quot;SSLEndpoint&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0SSLCertificate" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/Azure/AzureGroup/WebSite/SSLCertificate" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="SSLCertificate" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Azure/AzureGroup/WebSiteInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Azure/AzureGroup/WebSiteUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Azure/AzureGroup/WebSiteFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WebSiteUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WebSiteFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WebSiteInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="c3e86c28-66a9-435f-bd9b-0dc7282e8400" ref="Microsoft.RedDog.Contract\ServiceContract\AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="e1846a12-efa0-466e-b62c-b09fe6fd8abe" ref="Microsoft.RedDog.Contract\Interface\WebSite:SSLEndpoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Azure/AzureGroup/WebSite:SSLEndpoint" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>