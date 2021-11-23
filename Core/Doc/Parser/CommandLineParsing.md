# Argument Parsing for Command Line Tools

[Home](../README.md) / [Parser](./README.md)

## Introduction

- Parsing arguments is essential for a command line tools 
- This library do not want to reinvent the wheel, so please use an existing solutions for that task

## CLI Parsing Solutions

There are many well solutions out there. Just to name a few:

- [dotnet/CommandLine API](https://github.com/dotnet/command-line-api) - recommended solution from the [.NET Platform](https://github.com/dotnet) organization 
- [commandlineparser/commandline Parser](https://github.com/commandlineparser/commandline)  - Command Line Parser Library for CLR and NetStandard
- [natemcmaster/CommandLineUtils](https://github.com/natemcmaster/CommandLineUtils) - Command line parsing and utilities for .NET
- [fclp/fluent-command-line-parser](https://github.com/fclp/fluent-command-line-parser) - A simple, strongly typed .NET C# command line parser library using a fluent easy to use interface.
- [j-maly/CommandLineParser](https://github.com/j-maly/CommandLineParser) - CommandLine Parser Library lets you easily define strongly typed command line arguments, allows automatic parsing of command line arguments and mapping the values to properites of your objects.
- [nDesk Options](http://www.ndesk.org/Options) - NDesk.Options is a callback-based program option parser for C#.
- Or take a look at the [Search Results](https://github.com/search?l=C%23&q=command+line+parser&type=Repositories) for "command line parser" at GitHub

## Historical Note

Early versions of this library added a copy of nDesk/Options to provide an easy and working default solution for parsing arguments, but later we removed it.

**Here are the reasons why:**

- Forcing the use of a particular solution unnecessarily restricts the freedom of a developer to find and make their own choices
- Especially because the usage of existing solutions is simple and well documented
- Due to the integration of the nDesk/Options parser we were de facto obliged to maintain the provided external solution
- But maintaining an external copy (apart from the fact that it is a bad idea in general) is hard and includes a lot of unnecessary work
- Version 7 removes that burden and follows the [agil principle](https://agilemanifesto.org/iso/en/principles.html) to **maximize the amount of work not done**
