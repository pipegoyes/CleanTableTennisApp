Node version:
16.15.0
Installers link -> https://nodejs.org/dist/v16.15.0/

NPM version:
8.5.5

Angular CLI
12.0.1

PrimeNG
https://www.primefaces.org/primeng-v12-lts

Package manager CLI
Add-Migration "Name" -Project Infrastructure 

-------------------------------
Whats need to be done for beta version?

Bugs
- ComboBox does not display initial game points correctly
- There is no error when you select 11 and nothing, it saves score in single match
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
- Deploy or Open local ports at home.

Tech debt
- Single View to display score and double scores 


Questions
- Domain Events ? Create score ? to notify all users ?
- AuditableEntity for all entities ?

Azure 
- in DB Server add networking exception for azure services 
- Create/export a certificate with powershell (createCertificate.ps1)
- Add app setting in web app api
- Install static-web-apps-cli
	npm install -g @azure/static-web-apps-cli
- Build and deploy front end
	swa deploy -n front-end ./dist --env production
- cors setting with defined origin
- do not forget to change enviroment.prod to point to the right api url with port enviroment.prod.ts