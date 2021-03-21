
1. **Provision a function app targeting .NET runtime.**

https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp


2. **Replace contents of the default .cs file with the code in the *PrivateRepoARMLinkedTemplateProxy.cs* file.**

This function app lets you access an ARM linked template located in a private GitHub repo. 

```
Query parms:

privateRepoURI
accessToken
```


Note:
- This function app is written in .NET Core 3.1 and depends on the v3 Function runtime.

```
<TargetFramework>netcoreapp3.1</TargetFramework>
<AzureFunctionsVersion>v3</AzureFunctionsVersion>
```

3. **IMPORTANT**

Set CORS policy (Function app blade in portal -> CORS under the API section)
Allowed Origins: *

This will allow the function API to be called from the browser in cross-origin request scenarios (e.g. Deploy to Azure button invoking the function URI from the porta.azure.com domain)


4. **Test with your ARM linked template located in a private GitHub repo**


url format:
```
<function-app uri>?code=<function App key>&privateRepoURI=https://raw.githubusercontent.com/<ARMLinkedTeplate uri>/azuredeploy.json&accessToken=<GitHub PAT>
```
*replace placeholder (<>) values*

---
**Congratulations!** 

Your function app should return the json for the ARM template in your private GitHub repo.

