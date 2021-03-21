This function app lets you access an ARM linked template located in a private GitHub repo. 

Query parms:
privateRepoURI
accessToken

url format:

<function-app uri>?code=<function App key>&privateRepoURI=https://raw.githubusercontent.com/<ARMLinkedTeplate uri>/azuredeploy.json&accessToken=<GitHub PAT>


Note:
- This function app is written in .NET Core 3.1 and depends on the v3 Function runtime.

<TargetFramework>netcoreapp3.1</TargetFramework>
<AzureFunctionsVersion>v3</AzureFunctionsVersion>





