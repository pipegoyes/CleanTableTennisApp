# Clean Table Tennis App - Development Guide

## Project Setup

### Environment Requirements
- **Node.js**: v16.15.0 
  - Installers: [Node.js v16.15.0](https://nodejs.org/dist/v16.15.0/)
- **NPM**: v8.5.5
- **Angular CLI**: v12.0.1 
  - Install: `npm install -g @angular/cli@12.0.1`
- **.NET**: v8.0
  - Install: `choco install dotnet-5.0-sdk`

### Additional Tools
- **PrimeNG**: [v12 LTS](https://www.primefaces.org/primeng-v12-lts)
- **Static Web Apps CLI**: 
  - Install: `npm install -g @azure/static-web-apps-cli`

## Development Roadmap for Beta Version

### Resolved Bugs
- [x] ComboBox initial game points display
- [x] Prevent saving single match scores with incomplete selection

### Pending Bugs
- [ ] Review set order in single and double match scores

### Feature Backlog
- [ ] Display player names in matches
- [ ] Enhance validation error messaging
- [ ] Implement real-time score updates without page refresh
- [ ] Add match configuration validation
- [ ] Create wizard to prevent player duplicates in doubles
- [ ] Match Completion Process
  - Comprehensive score validation
  - Confirmation popup for match end

### Technical Todos
- [ ] Refactor API to follow RESTful conventions (plural endpoints)

### Technical Debt
- [ ] Consolidate single and double score views
- [ ] Upgrade to latest Angular version

## Package Manager Commands
- Create Migration: `Add-Migration "Name" -Project Infrastructure`