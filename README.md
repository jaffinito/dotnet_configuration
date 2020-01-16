dotnet_configuration
====================

This is a configuration tool for the .Net Agent built on **.Net Framework 4.0, Client Profile**.  You must have .NET Framework 4.0 installed.

Supported Operating Systems:

* Windows XP or greater
* Windows Server 2003 or greater

The .Net Agent does not need to be installed for this tool to work, but it makes the tool far more useful.
The tool has the newrelic.config and newrelic.xsd embedded in it.

Current included version of the config/xsd: [8.23.107.0](https://docs.newrelic.com/docs/release-notes/agent-release-notes/net-release-notes/net-agent-8231070).

**Process that occurs when openning a file:**

1. The default location for the XSD is checked and the registry is checked for a location key.
2. If both of those fail to find a file the user will be prompted for one.
3. Hitting cancel to that dialog will ask he user if they want to open the embedded resource.
4. If they do, it is copied to the app root and opened. Hitting no cancels the open process.
5. Once the XSD has been loaded the same process occurs for the config, except that the user is always prompted for it.
6. If they cancel that dialog they are again asked if they want the embedded resource which will be copied to the app root as before.


Enable tool error logging:
Create a file called enabledebug in a the app root. [Requires restart to take effect.]

Goals:

* Support all versions of the agent that use a newrelic.xsd/newrelic.config file combination.
* Require little to no development effort to maintain the tool when the agent is updated or new features  are added.
* Provide an easy way for customers to update their newrelic.config files.

Possible Stretch Goals:

* Read IIS and build file from there.
* Allow users to select non-IIS applications via OpenFileDialog.
* Quick-select for debugging(done) and other common changes.
