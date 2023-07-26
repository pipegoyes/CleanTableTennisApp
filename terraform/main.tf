provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "app_rg" {
  name     = "main-group"
  location = "westeurope" # Germany West Central region
}

resource "azurerm_service_plan" "app_service_plan" {
  name                = "main-service-plan"
  location            = azurerm_resource_group.app_rg.location
  resource_group_name = azurerm_resource_group.app_rg.name
  os_type             = "Windows"
  sku_name            = "B1"
}

resource "azurerm_windows_web_app" "table_tennis_webapp" {
  name                = "clean-table-tennis-app"
  location            = azurerm_resource_group.app_rg.location
  resource_group_name = azurerm_resource_group.app_rg.name
  service_plan_id     = azurerm_service_plan.app_service_plan.id


  site_config {
    always_on = false
    application_stack {
      current_stack  = "dotnet"
      dotnet_version = "v6.0"
    }
    app_command_line = "dotnet ./CleanTableTennisApp.WebUI.dll"
    virtual_application {
      physical_path = "site\\wwwroot"
      preload       = false
      virtual_path  = "/"
    }
  }

  # node version ?

  # virtual_directory {
  #   virtual_path  = "/"
  #   physical_path = "ClientApp/dist"
  # }

  connection_string {
    name  = "DefaultConnection"
    type  = "SQLServer"
    value = "Data Source=myserver.database.windows.net;Database=CleanTableTennisAppDb;User=sa;Password=Your_password123;MultipleActiveResultSets=true;"
  }

  app_settings = {
    "UseInMemoryDatabase" = false
  }

}

#  Deploy code from a public GitHub repo
resource "azurerm_app_service_source_control" "sourcecontrol" {
  app_id                 = azurerm_windows_web_app.table_tennis_webapp.id
  repo_url               = "https://github.com/pipegoyes/CleanTableTennisApp.git"
  branch                 = "master"
  use_manual_integration = true
  use_mercurial          = false
}

# output "app_service_default_hostname" {
#   value = azurerm_app_service.app_service.default_site_hostname
# }
