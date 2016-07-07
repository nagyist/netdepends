# NetDepends

## Introduction

The purpose of this project is to query .NET assemblies for input to [itdepends](https://github.com/zebmason/itdepends). The .NET assemblies are processed into sets of interfaces and classes organised according to the hierarchy of the namespaces. Each interface or class has a list of resolvable dependencies and a list of missing dependencies associated with it. This information can then be analysed to produce various statistics and documentation.

## Usage

NetDepends is a command line tool that takes as its arguments the name of a file for output followed by a list of .NET assemblies.

To see an example look at the start options on the Debug tab of the project properties page. This sends the output to a JSON file in the Example directory where a batch file is located that will then process that file through [itdepends](https://github.com/zebmason/itdepends) and then run Doxygen to create HTML pages. Note that some of the paths in the batch file may need altering to work on specific machines.
