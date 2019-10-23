Import-Module .\bin\Debug\CmdletsProject.dll -Force
# Get-Command -module CmdletsProject
# Get-Help Get-NetworkAdapter -full
# Format-Table => resultado como tabela e não linha a linha
Get-NetworkAdapter -Manufacturer Microsoft -entries 3 | Format-Table 

<#
	Lendo informações sobre os atributos do objeto NetworkAdapter

	Import-Module .\bin\Debug\CmdletsProject.dll -Force
    Get-NetworkAdapter | Select-Object Description
    Get-NetworkAdapter | Select-Object DeviceId
    Get-NetworkAdapter | Select-Object Manufacturer
    Get-NetworkAdapter | Select-Object Name     
    Get-NetworkAdapter | Select-Object NetConnectionId
    Get-NetworkAdapter | Select-Object PhysicalAdapter
    Get-NetworkAdapter | Select-Object Description

	Usando input parameters
	Import-Module .\bin\Debug\CmdletsProject.dll -Force
	Get-NetworkAdapter -Manufacturer Microsoft | Format-Table
#>