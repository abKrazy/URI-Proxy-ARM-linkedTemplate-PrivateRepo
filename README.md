The Problem:
Azure Resource Manager can access linked ARM templates via provided URI property during the deployment of the master template. But you cannot use local files or URI locations that require some form of password-based authentication or the usage of HTTP authentication headers i.e. you cannot add HTTP headers in ARM templates.

As a result, ARM templates cannot reference artifacts located in a private GitHub repository out of the box.


- for private GitHub repos, PAT token must be provided as Authorization header.
- GitHub also requires this additonal header “Accept”: “application/vnd.github.VERSION.raw”


Solution:
A Function acting as a GitHub proxy (i.e. function uri becomes the artifact location for ARM)

ARM template
    -> references the Az Function URI passing in the raw github private repo uri & PAT
	    -> Function app makes REST API call to GitHub raw url & passes in the PAT as an Authorization Header, and the addt'l header
		    -> returns the json for a private ARM template


Alternate solution:
A "low-code" approach in lieu of a function app would be to use a Logic App that accomplishes the same thing.

How to leverage the contents in this repo:
This repo contains the following folders:
1. A sample ARM linked template (that can be stored in a private GitHub repo)
2. A function app proxy
3. A logic app proxy

Each folder has its own README.md with instructions to deploy and test the solution.

