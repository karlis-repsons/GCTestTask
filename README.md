# Usage:
* Open solution in VisualStudio (created with version 2017).
* Start GCTestTask.ConsoleApp and see further info in its output.
  Content example for GCTask_schedule.txt file:
      ACPI 2018-03-21_10:10:00, 2018-03-21_10:20:00
* Note that repetitive disable calls will not be made.

# Projects and their purposes:
* GCTestTask.ConsoleApp - test task application.
* GCTestTask.Lib - reusable test task functions, which could be called from WebAPI or desktop application.
* GCTestTask.Lib.Tests - this project can test GCTestTask.Lib functionality by simulating interactions with drivers and checking their simulated state.
* WindowsOS.Lib - reusable elements related to WindowsOS; along with the code, which makes OS calls, there is testing code, which simulates the same calls and is useful for integration tests.
