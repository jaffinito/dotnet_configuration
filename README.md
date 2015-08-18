dotnet_configuration
====================

This is a configuration tool for the .Net Agent built on **.Net Framework 4.0, Client Profile**.

Supported Operating Systems:

* 2003 and XP
* 2008 R1/R2 and Vista/Win7
* 2012 R1/R2 and Win8/Win8.1

The .Net Agent does not need to be installed for this tool to work.
The tool has the newrelic.config and newrelic.xsd embedded in it.
The version of the tool matches the version of the files embedded in it.
So, Tool version 3.10.43 has the config/xsd for Agent verison 3.10.43.

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
