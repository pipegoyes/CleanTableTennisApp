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
- Shows error when match configuration is not valid
- Validate doubles, player does not play in both doubles (wizard)
- Go back to home in quick view
- COntinue and lets play button must be displayed just for admins
- Finish a match 
	. validate all scores 
	. force finished (auth user)
- Validations does not display detail message

Tech debt
- Single View to display score and double scores 
- Overview and quick-view are too much similar to be different components


Questions
- Domain Events ? Create score ? to notify all users ?
- AuditableEntity for all entities ?