# FiveInARow0x88
An AI to play a game similar to Tic-Tac-Toe except you need 5 in a row to win. This game will run on an 8x8 board represented by the [0x88 representation](http://mediocrechess.blogspot.com/2006/12/0x88-representation.html "0x88 board explanation"). The 0x88 representation is most often used to represent chess boards. Since chess boards and this board has the same size, we can also use 0x88 to represent the Five in a Row board.

# Install C# Compiler
Download the C# compiler (dotnet.exe and csc.exe) [here](https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.300-windows-x64-installer "C# Compiler").

# How to Compile and Run
Type into a command line
```
dotnet run
```

# How to Compile Only
Type into a command line:
```
dotnet build
```

# How to Build an executable
Type into a command line:
```
dotnet publish -c Release -r os_version
```
where `os_version` is the desired OS. For example, if you want to make an executable for Windows 10 64-bit then the os_version will be win10-x64. The full command line would be `dotnet publish -c Release -r win10-x64`
