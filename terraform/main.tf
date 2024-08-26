terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "3.115.0"
    }
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg-clean-tt" {
  name     = "rg-clean-tt"
  location = "westeurope" # Germany West Central region
}

resource "azurerm_service_plan" "sp-clean-tt" {
  name                = "main-service-plan"
  location            = azurerm_resource_group.rg-clean-tt.location
  resource_group_name = azurerm_resource_group.rg-clean-tt.name
  os_type             = "Windows"
  sku_name            = "F1"
}

resource "azurerm_windows_web_app" "clean-tt-api" {
  name                = "clean-table-tennis-api"
  location            = azurerm_resource_group.rg-clean-tt.location
  resource_group_name = azurerm_resource_group.rg-clean-tt.name
  service_plan_id     = azurerm_service_plan.sp-clean-tt.id


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

  app_settings = {
    "WEBSITE_LOAD_CERTIFICATES" = "*"
    "UseInMemoryDatabase" = false
  }

}
resource "azurerm_static_web_app" "clean-tt-frontend" {
  name                = "clean-tt-frontend"
  resource_group_name = azurerm_resource_group.rg-clean-tt.name
  location            = azurerm_resource_group.rg-clean-tt.location
}