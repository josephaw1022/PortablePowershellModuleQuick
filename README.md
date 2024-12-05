
# Portable Powershell Module - Quick POC

## Introduction

This project serves as a quick proof of concept (POC) and spike into creating **portable PowerShell modules**. By using **.NET Standard 2.1**, we’ve created a PowerShell module that can run on **macOS**, **Linux**, or **Windows**. This module showcases how you can develop cross-platform PowerShell cmdlets using C# and .NET, which can be a useful approach for building modules that work seamlessly across different operating systems.

The purpose of this POC is not only to generate a fun **Pet Fortune** cmdlet based on a pet type and favorite number, but also to give you hands-on experience in understanding how **cmdlets** work, how PowerShell integrates with .NET libraries, and how you can develop modules that are platform-agnostic. This is a great way to get a deeper understanding of PowerShell cmdlet development, module creation, and cross-platform compatibility using **.NET Standard**.


## Additional Resources

To make this project work, we rely on the [PowerShellStandard.Library](https://www.nuget.org/packages/PowerShellStandard.Library#readme-body-tab) NuGet package. This package provides the necessary libraries to develop PowerShell modules that are portable across different platforms using .NET Standard. It enables seamless integration of PowerShell with .NET, allowing this project to run on macOS, Linux, and Windows without modification.

For more details on how to write portable modules for PowerShell, check out the [official documentation on portable PowerShell modules](https://learn.microsoft.com/en-us/powershell/scripting/dev-cross-plat/writing-portable-modules?view=powershell-7.4#supporting-technologies).


## Prerequisites

Before running the tasks, make sure you have the following installed:

- **Taskfile CLI**: This is the task automation tool used to run the steps.
  - On **Windows**: You can install it using [winget](https://winget.run/) with the following command:
    ```bash
    winget install Task.Task
    ```
  - If you are not on Windows or don't use winget, follow the installation instructions on [Taskfile's official installation page](https://taskfile.dev/installation/).

- **Docker or Podman**: Ensure Docker or Podman is installed and running. This is required for running the tasks in containers.


- You don't need to install .net 8 or powershell on your machine. The tasks will run in a container.

---

## Steps to Run the Project

This project provides a simple PowerShell module (`myModule`) that generates fun fortunes for your pets based on their type (Cat, Dog, Horse) and your favorite number. You can build and run it using a series of predefined tasks.

### 1. **Run `task init` to Create the PowerShell Script**
   The first step is to create a PowerShell script (`test-module.ps1`) that will import the compiled module (`myModule.dll`) and execute the cmdlets. To do this, run:

   ```bash
   task init
   ```

   After running this command, a PowerShell script (`test-module.ps1`) will be generated that looks like this:

   ```powershell
   # Import the module
   Import-Module .\bin\Debug\netstandard2.0\myModule.dll
   ```

   This file is created automatically, but **you will need to modify this file** to run the new cmdlet. You need to add the cmdlet with the parameters as shown below:

   ```powershell
   # Import the module
   Import-Module .\bin\Debug\netstandard2.0\myModule.dll


   # Running the new cmdlet with parameters
   Get-PetFortune -FavoritePet "Cat" -FavoriteNumber 7
   ```

   **Note**: You can change the `FavoritePet` and `FavoriteNumber` parameters to suit your needs. This is just an example to test the module.

---

### 2. **Run `task e2e` to Build the C# Project and Test the Module**
   After modifying the PowerShell script, you can build the C# project and run the PowerShell test script using the following command:

   ```bash
   task e2e
   ```

   This command will:
   - Build the C# project to generate the `myModule.dll` file.
   - Run the PowerShell script (`test-module.ps1`), which loads the `myModule.dll` and tests the `Get-PetFortune` cmdlet, providing a fun "fortune" based on your favorite pet and number.

---

## How the Tasks Work

The tasks are automated using `Taskfile` to simplify the process. Here's what each task does:

- **`task init` (alias: `ps1`)**:
  - This task runs the `docker run` command to create the `test-module.ps1` file, which includes the necessary code to load the C# module and call the cmdlet.
  - After running this task, you must manually edit the `test-module.ps1` file to include the cmdlet, as shown above.

- **`task dll` (alias: `build_module`)**:
  - This task builds the C# PowerShell module (`myModule.dll`) using the .NET SDK Docker image.
  
- **`task run` (alias: `run_build_with_powershell`)**:
  - This task runs the PowerShell script created in the `task init` step, which loads the module and runs the `Get-PetFortune` cmdlet with the provided parameters.

- **`task e2e` (alias: `full_build_and_test`)**:
  - This is the main task that combines the build and test steps into one, running both `task dll` and `task run` to build the module and test it in one go.

---

## Example Output

When you run `task e2e`, you'll get an output similar to the following, based on the pet and favorite number:

```txt
Cat's Lucky Fortune:
A good day for cats to seek out new adventures! Your lucky number 7 will bring you a nap in a sunny spot.
Expect a mysterious event to occur, possibly involving a string or laser pointer.
Lucky Number Bonus: 15. This will bring you a special surprise today—possibly involving snacks!
```


