# How to deploy linked ARM templates stored in a private GitHub repo

## Context:
Linked ARM templates allow you to modularize your ARM templates leading to better readability, reusability and maintainability.

https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/linked-templates?tabs=azure-powershell

Azure Resource Manager can access linked ARM templates via provided **tempateLink uri** property during the deployment of the master template.

e.g.

```
{
  "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
  "contentVersion": "1.0.0.0",
  "parameters": {},
  "variables": {},
  "resources": [
    {
      "type": "Microsoft.Resources/deployments",
      "apiVersion": "2019-10-01",
      "name": "linkedTemplate",
      "properties": {
        "mode": "Incremental",
        "templateLink": {
          "uri":"https://mystorageaccount.blob.core.windows.net/AzureTemplates/newStorageAccount.json",
          "contentVersion":"1.0.0.0"
        }
      }
    }
  ],
  "outputs": {
  }
}
```


## The Problem:
 You cannot use local files or URI locations that require some form of password-based authentication or the usage of HTTP authentication headers i.e. you cannot add HTTP headers in ARM templates.

As a result, ARM templates cannot reference artifacts located in a private GitHub repository out of the box.


- For private GitHub repos, PAT token must be provided as Authorization header.
- GitHub also requires this additonal header for specifying the media type:

“Accept”: “application/vnd.github.VERSION.raw”


## Solution:
**Azure Functions to the rescue!**

A Function App can act as a proxy that accepts (as query parms) the uri for a linked tempate located in a private repo and the PAT token, and authenticates to GitHub retrieivng the template json (i.e. function uri becomes the artifact location for ARM)

```
High-level flow:
ARM template
    -> references the Az Function URI passing in the raw github private repo uri & PAT
	    -> Function app makes REST API call to GitHub raw url & passes in the PAT as an Authorization Header, and the addt'l header
		    -> returns the json for a private ARM template
```

## Alternate solution:
A "low-code" approach in lieu of a function app would be to use a Logic App that accomplishes the same thing.



# To get started:
1. **Deploy a function app proxy using the sample code in the *function-app* folder. Alternatively, go "low-code" with a logic app using the sample in the *logic-app* folder.**
2. **Setup a private GitHub repo with the sample linked templates located in the *ARMLinkedTemplate-sample* folder, and follow the instructions in that folder's README to provide your function app URI in the ARM template.**



### **NOTE:**

**Each folder has its own README.md with instructions to deploy and test the solution.**

