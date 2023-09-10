provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "app_rg" {
  name     = "app_rg"
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
    ftps_state = "FtpsOnly"
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

  # connection_string {
  #   name  = "DefaultConnection"
  #   type  = "SQLServer"
  #   value = "Server=tt-db.database.windows.net;Database=CleanTableTennisAppDb;User=XXX;Password=XXX;MultipleActiveResultSets=true"
  # }

  app_settings = {
    "WEBSITE_LOAD_CERTIFICATES" = "*"
    "UseInMemoryDatabase" = false
  }

}

#  Deploy code from a public GitHub repo
# resource "azurerm_app_service_source_control" "sourcecontrol" {
#   app_id                 = azurerm_windows_web_app.table_tennis_webapp.id
#   repo_url               = "https://github.com/pipegoyes/CleanTableTennisApp.git"
#   branch                 = "master"
#   use_manual_integration = true
#   use_mercurial          = false
# }

# resource "azurerm_static_site" "front-end" {
#   name = "front-end"
#   resource_group_name = azurerm_resource_group.app_rg.name
#   location = azurerm_resource_group.app_rg.location
# }


# resource "azurerm_storage_account" "storageacount" {
#   name                     = "storagedb"
#   resource_group_name      = azurerm_resource_group.app_rg.name
#   location                 = azurerm_resource_group.app_rg.location
#   account_tier             = "Standard"
#   account_replication_type = "LRS"
# }
resource "azurerm_mssql_server" "server" {
  name                         = "random-pet"
  resource_group_name          = azurerm_resource_group.app_rg.name
  location                     = azurerm_resource_group.app_rg.location
  administrator_login          = "super-admin"
  administrator_login_password = "password"
  version                      = "12.0"
}

resource "azurerm_mssql_database" "db" {
  name      = "CleanTableTennisAppDb"
  server_id = azurerm_mssql_server.server.id
}

resource "azurerm_static_site" "front-end" {
  name                = "front-end"
  resource_group_name = azurerm_resource_group.app_rg.name
  location            = azurerm_resource_group.app_rg.location
}