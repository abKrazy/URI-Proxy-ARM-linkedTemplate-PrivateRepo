This logic app lets you access an ARM linked template located in a private GitHub repo. 
It uses Managed Identity to retrieve a GitHub PAT (Personal Access Token) from a Key Vault and passes it in the Authorization header.

Steps:
1. **Create a GitHub PAT if you do not have one already**
https://docs.github.com/en/github/authenticating-to-github/creating-a-personal-access-token

2. **Provision a Key Vault in your Azure subscription and create a secret for your GitHub PAT token.**

https://docs.microsoft.com/en-us/azure/key-vault/general/quick-create-portal

e.g.
```
KEY							VALUE
PAT-GithubPrivateRepo		<xxxxxxxxx>
```

3. **Provision a logic app in your Azure subscription**
https://docs.microsoft.com/en-us/azure/logic-apps/quickstart-create-first-logic-app-workflow

	a. The Logic Apps Designer opens and shows a page with an introduction video and commonly used triggers. Close this intro page (Click 'X' on the top right).
	
	b. Turn on Managed Identity (Identity blade)
		https://docs.microsoft.com/en-us/azure/logic-apps/create-managed-service-identity

	c. In your key vault resource, add 'Get' and 'List' permissions to the Managed Identity you created in the previous step
		https://docs.microsoft.com/en-us/azure/key-vault/general/assign-access-policy-portal

	d. In 'Logic app code view', replace everything with the contents of LogicApp-PrivateRepoARMLinkedTemplateProxy.txt		
	
	e. Replace the following placeholders in the code with the correct values (without the <>) from your subscription:
```
<subscriptionId>
<resourceGroup>
<region> - e.g. southcentralus
```
Note:
Key Vault secret name must match the name referenced in this section:
```
"path": "/secrets/@{encodeURIComponent('PAT-GithubPrivateRepo')}/value" 
```

This example assumes the secret name is PAT-GithubPrivateRepo. Modify this if you created the secret with a different name.

	 f. Save
		Note: saving will fail if you have not enabled Managed Identity as outlined in Step 3b.


4. **Your logic app is ready to run!**

	a. Get the http url for your logic app from the 'Logic app designer' view.
	b. append as query string - the uri to an ARM template located in a private GitHub repo (make sure to use the raw uri).

```
e.g. uri format:
<logicAppUri>&url=https://raw.githubusercontent.com/<path>/azuredeploy.json
```
---
**Congratulations!** 

Your logic should return the json for the ARM template in your private GitHub repo.


