[![License](https://img.shields.io/badge/license-BSD-blue.svg?style=flat)](http://opensource.org/licenses/BSD-3-Clause)
[![GitHub release](https://img.shields.io/github/release/dmitry-merzlyakov/nledger.svg?style=flat)](https://github.com/dmitry-merzlyakov/nledger/releases)
[![Gitter chat room](https://badges.gitter.im/nledger/Lobby.svg)](https://gitter.im/nledger/lobby)

# .Net Ledger: Double-Entry Accounting System

.Net Ledger (NLedger) is a complete .Net port of the 
[Ledger](http://ledger-cli.org), excellent powerful command line accounting system. 
The main purpose of this project is to enable all features of Ledger to .Net world. 

NLedger command line utility (NLedger.cli) is highly compatible with the original
Ledger. It supports all original features and command line options; 
the produced output is 100% equal. The reliable level of compatibility is verified by Ledger tests that
are 99% passed (with exception of few ones that require unavailable on Windows features).
It is expected that people may feel like they use the genuine Ledger but in Windows
command line.

NLedger is written on pure C# with no dependencies on any external libraries
or any platform-specific features. It lets NLedger properly work on any 
.Net compatible system and can be embedded into any .Net application.

## Quick Start

If you have just installed NLedger MSI or ZIP package on Windows and want to know what to do next, you can try:

- Run NLedger in a command line:
  - Click *.Net Ledger Folder* icon in the main menu;
  - Type *cmd* in the address bar of Windows Explorer window;
  - In the command line window, type *ledger --version* - NLedger should show its version;
  - Type *ledger bal -f test/input/drewr3.dat* - you will get a balance for one of example files that are in *test\input* folder;
- Run NLedger Live Demo:
  - Click *.Net Ledger Live Demo* icon in the main menu;
  - Observe the documentation and try all examples in action;
- Read this file to the end, *Ledger 3 Documentation*, *.Net Ledger Guide* and try to find the use of this tool.

If you have installed NLedger from the sources, you can open a new command line window, navigate to NLedger folder and try:

```
ledger --version
ledger bal -f test/input/drewr3.dat
pwsh -file ./get-nledger-tools.ps1 -demo
```

## Features

NLedger thoroughly derives all valuable Ledger capabilities. Basically,
all what Ledger can do, NLedger can do as well. Here is a short list of the most
significant Ledger features:

- As an excellent accounting system:
  - **Double-entry accounting**. Nuff said; it is a must-have feature;
  - **Multi-posting transactions** allow to incorporate several 
    monetary movements into one logical operation. For example, one transaction may include
    cash withdrawal, writing off money from the card account and corresponded bank fees;
  - **Commodities and Currencies** can represent whatever you work with as a dimension 
    of measurement. It may be currencies, shares, time and whatever else
    that can be measured in numbers and need to be included into accounting;
  - **Amounts** are "smart" numbers that incorporate a number and a corresponded commodity.
    It gives exact information about the matter of an amount and let the application 
    properly manipulate with it. For example, it never adds up 10.00 US dollars with
    5 AAPL but can convert one amount to another; 
  - **Prices and Price History** gives information how to convert one amount to another
    at any moment. You can give this information explicitly (as a price table) but 
    the application can also collect it also in implicit way, by analyzing transactions and
    data conversions in them;
  - **Balancing and auto-balancing** make accounting transactions smarter: the application
    can validate that a transaction balances even it has posts in different currencies
    or commodities. Moreover, in some conditions it can auto-complete it (calculate the last amount)
    that simplify writing the journal records;
  - **Journal, Account** and other basic accounting attributes are here;
- As an effective command line utility:
  - **Text files** are the only source for the accounting utility. It does not require
    any other components, applications, special data files - only text. It is completely
    under your control;
  - **Never change any file** - yes, Ledger only reads input files and generates reports;
  - **Easy syntax of the journal** lets you keep your accounting files in 
    the natural language;
  - **Huge amount of reports** lets you organize your work in the most efficient way;
  - **Command line interface** is very flexible thing that let you configure
    your typical commands in batch files. Once you have them, your efficiency will be 
    much better rather you get the same result in any UI tool;
- As a technically advanced tool:
  - **Inline expressions** allow you to configure your own functions in the journal;
  - **Functions and evaluations** let you do the same in the command line;
  - **Tags** in the journal file let you add meta data to your transactions;

*And many other features, really*. Ledger is very efficient tool with wide capabilities,
so it is highly recommended to familiar with the [original documentation](https://www.ledger-cli.org/docs.html),
read other resources (http://plaintextaccounting.org)
and keep yourself in loop with [Ledger Community](https://www.ledger-cli.org/contribute.html).

### The Use of NLedger

*So, if Ledger is so nice, why NLedger?* Here is the answer or, in other words, the project mission :)

The first use of NLedger is enabling Ledger features in Windows world.
Even as a developer, I faced difficulties running Ledger 3.1.1 on Windows 10 and,
I believe, it would be unsolvable problem for regular people.

I would like to let everyone install NLedger by one click and use it exactly
in the same way as it is described in Ledger documentation. Ledger but on any 
Windows - it was the first goal.

The second use of NLedger is that it is a native .Net application. 
It gives unlimited abilities to extend it with extra functions and integrate with
other products on .Net platform. It was the second thing why it was decided 
to port Ledger on .Net.

## Project Vision and Development Progress

The ultimate overall goal of this project is to have a fully functional Ledger
on .Net platform in the form of a command line utility plus provide a way to give 
seamless access to the same functions for external .Net applications.

- Current **Project Status** is:
  - Ported from [Ledger 3.2.1](https://github.com/ledger/ledger), branch Master, commit 56c42e11; 2020/5/18
  - Core functionality is ported; command line utility is available;
  - Ledger testing framework is ported; 
  - Ledger tests are passed to some extend:
    - 98% (694 out of 707) test cases passed;
    - 13 test cases are ignored because of known limitations;
    - 0 failed.
- **Current limitations** (technical restrictions that will be addressed by next releases) are:
  - No Python integration. Ledger tests that require Python are disabled;
  - DateTime parser on .Net has less specific error messages and does not allow
      to detect the same mistakes as Ledger does. Corresponded Ledger test is disabled till further decision;
  - It was found that in some conditions the original Ledger produced incorrect 
    rounding at the last rendering step (*stream_out_mpq*). It was caused by specifics of its
    arbitrary-precision arithmetic library; some combinations of
    divisible and divisor produce rounded result that does not match expected
    banking rounding. Some Ledger tests were corrected (opt-lot-prices, opt-lots, opt-lots_basis).
      
- **Development Roadmap** is available by this [link](https://github.com/dmitry-merzlyakov/nledger/blob/master/roadmap.md).
  It describes the plan to complete all intended features by the version 1.0.

## Installation

Because of .Net nature, NLedger can run on any system that supports .Net (either .Net Framework or .Net Core).
However, its testing framework and helper tools use PowerShell, so
you can run Ledger tests only if your system has installed PowerShell as well.

### System Requirements

- .Net Framework 4.5 or higher and/or .Net Core SDK 3.1 or higher. It is required component to run the command line application;
- PowerShell 5.0 or higher. It is needed to run testing framework and other tools.

Therefore, PowerShell is not required component, you can still use NLedger, but ability to run PowerShell scripts makes your life easier.

### Build and Install from source code

(Windows, Linux, OSX) Execute the following commands:

```
git clone https://github.com/dmitry-merzlyakov/nledger
cd nledger
pwsh -file ./get-nledger-up.ps1 -install
```

On Windows, depending on your Powershell version, the last command might look like:
```
powershell -ExecutionPolicy RemoteSigned -File ./get-nledger-up.ps1 -install
```

### Install from NLedger Installation Package

(Windows only)

- Download the latest NLedger installation package (MSI file) from [Releases](https://github.com/dmitry-merzlyakov/nledger/releases);
- Run the installer and follow instructions on the screen;
- Review *nledger.html* when installation finishes.

*Note: the installer will request elevated permissions to call NGen.*

### Install from Binary Package

(Windows only)

- Download the latest NLedger installation package from [Releases](https://github.com/dmitry-merzlyakov/nledger/releases);
- Unpack the package to a temp folder;
- Open the file *nledger.html* and follow the installation instruction.

OR (for impatient people):

- move unpacked NLedger to a place of permanent location (e.g. *Program Files*);
- Open *NLedger/Install* folder and execute NLedger.Install.cmd (confirm administrative privileges to let it call NGen); close the console;
- Run Windows Command Line (e.g. type *cmd* in the search bar) and type *ledger* in it.

## NLedger NuGet package

NLedger is available as NuGet package. Read more how to embed NLedger into your software [here](https://github.com/dmitry-merzlyakov/nledger/blob/master/build.md) - section Developing with NLedger.

## Documentation

As it was mentioned, the main information source about how to use this application 
is Ledger resources and community. Therefore:
- Refer to [Ledger documentation](https://www.ledger-cli.org/docs.html) or 
  read [other resources](http://plaintextaccounting.org/) to get conceptual 
  information about command line accounting and Ledger capabilities;
- Refer to [NLedger documentation](https://github.com/dmitry-merzlyakov/nledger/blob/master/nledger.md) if you have questions about running .Net 
  application in your system.

## Contribution

NLedger is currently under an active development and some big enhancements 
are coming. You can check the planes in [Roadmap](https://github.com/dmitry-merzlyakov/nledger/blob/master/roadmap.md).
However, code quality is a primary focus, 
so any bug fixing requests and/or fixing changes will be processed in the 
first order. Therefore, if you want to help this project:

- Any help in testing NLedger are very appreciated. You can leave information about found 
  defect on [Issues](https://github.com/dmitry-merzlyakov/nledger/issues) tab; they will be processed in the first order;
- Anyone who would like to provide a fix for any found defect are welcome.
  Please, create pull requests for fixing changes; they will be processed in the first order as well;
- Coming enhancements are developed under my control.
  Of course, anyone of you can make a fork from this code and extend it on your own, enjoy!

### How to inform about found defects

1. First of all, please check project [Issues](https://github.com/dmitry-merzlyakov/nledger/issues) 
   on GitHub and Known Issues in [CHANGELOG](https://github.com/dmitry-merzlyakov/nledger/blob/master/CHANGELOG.md).
   The issue you found might be already recorded.
2. Check that the issue is reproducible and describe it.
3. Ideally, locate the defect and create Ledger test file that exposes the problem.
   This file should properly pass test with the original Ledger and fail with NLedger.

## Credits

Special thanks to *John Wiegley* for the nicest accounting tool I've ever seen.
I really like it very much and it was a great pleasure for me to analyze its code
in the smallest detail. Thought it was quite big challenge for me 
(GDB left the corns on my hands :)) I've got an invaluable experience. Thank you! 

## Contact

- Join us in the chat room here: [![Gitter chat room](https://badges.gitter.im/nledger/Lobby.svg)](https://gitter.im/nledger/lobby);
- Twitter: [#nledger](https://twitter.com/search?q=%23nledger) .Net Ledger news;
- Send an email to [Dmitry Merzlyakov](mailto:dmitry.merzlyakov@gmail.com)

## Licensing

The code is licensed under 3-clause [FreeBSD license](https://github.com/dmitry-merzlyakov/nledger/blob/master/LICENSE).

(c) 2017-2020 [Dmitry Merzlyakov](mailto:dmitry.merzlyakov@gmail.com)
