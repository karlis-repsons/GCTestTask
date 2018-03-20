# Usage:
* Open solution in VisualStudio (created with version 2017).
* Start GCTestTask.ConsoleApp and see further info in its output.

# Projects and their purposes:
* GCTestTask.ConsoleApp - test task application.
* GCTestTask.Lib - reusable test task functions, which could be called from WebAPI or desktop application.
* GCTestTask.Lib.Tests - this project can test GCTestTask.Lib functionality by simulating interactions with drivers and checking their simulated state.
* WindowsOS.Lib - reusable elements related to WindowsOS; along with the code, which makes OS calls, there is testing code, which simulates the same calls and is useful for integration tests.

# Further work:
* Add integration tests, which make use of TestDriversProxy and fix defects.
