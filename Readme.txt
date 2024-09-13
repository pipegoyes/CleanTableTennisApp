Node version:
16.15.0
Installers link -> https://nodejs.org/dist/v16.15.0/

NPM version:
8.5.5

Angular CLI
npm install -g @angular/cli@12.0.1
12.0.1

.Net 5
choco install dotnet-5.0-sdk

PrimeNG
https://www.primefaces.org/primeng-v12-lts

Package manager CLI
Add-Migration "Name" -Project Infrastructure 

SWA - deploying cli
npm install -g @azure/static-web-apps-cli

-------------------------------
Whats need to be done for beta version?

Bugs
- (resolved) ComboBox does not display initial game points correctly
- (resolved) There is no error when you select 11 and nothing, it saves score in single match
- Consider set order in scores (single and doubles)

Features
- Display names in double and single matches
- Validations does not display detail message
- Update score in quick-view without refreshing the page
- Shows error when match configuration is not valid
- Validate doubles, player does not play in both doubles (wizard)
- Finish a match 
	. validate all scores 
	. popup to confirm action

TODOS
- Refactor API to make it real REST (plural at endpoints and so on)

Tech debt
- Single View to display score and double scores 
- Update to latest angular version
- Update npm packages 
- Update to latest .net version
- Reactivate feature EF and audit create by and modify by 
- Migrate requests/response to contracts Project
	. DoubleScore
	. Overview
	. Score

Questions
- Domain Events ? Create score ? to notify all users ?
- AuditableEntity for all entities ?

Azure
- Install static-web-apps-cli
	npm install -g @azure/static-web-apps-cli
- Build and deploy front end 	
	swa deploy -n clean-tt-frontend ./dist --env production
- cors setting with defined origin

