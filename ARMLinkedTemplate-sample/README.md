This is an example of an ARM linked template that provisions an App Service plan, App Service, and a storage account.

The storage account definition is stored in its own template (*linkedStorageAccount.json*), and is linked from the main template (*azuredeploy.json*)


# Instructions:

1. Store the 2 ARM templates in a private GitHub repository...
2. Create a GitHub PAT (Personal Access Token)
https://docs.github.com/en/github/authenticating-to-github/creating-a-personal-access-token
3. In *azuredeploy.json*, replace the following placeholders (<>):

```
"privateRepoProxyUri" :{
      "type": "string",
      "defaultValue": "<function-app uri>",
      "metadata": {
        "description": "Function app base url"
      }

"_artifactsLocation": {
      "type": "string",
      "defaultValue": "https://raw.githubusercontent.com/<privateRepoaseURI>",
      "metadata": {
        "description": "The private repo base URI where artifacts required by this template are located"
      }
```


4. Deploy the associated function app and/or logic app (follow their respective README.md docs)

5. Deploy your ARM template passing in the required parameters!

Bonus:
Add a ' Deploy to Azure' button to your README.md making sure to replace the placeholder (<>) values:

https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-to-azure-button

![Deploy to Azure](https://aka.ms/deploytoazurebutton)

```
(https://portal.azure.com/#create/Microsoft.Template/uri/<function app uri>?code=<funtion app key>&privateRepoURI=https://raw.githubusercontent.com/<privateRepo>/azuredeploy.json&accessToken=<GitHub PAT>)
```