LogUtility
==========

a light log tools

how to use?
====

show me log in console:
```C#
var logger = new ConsoleLogger()
logger.Info( "Hello World!" );
```



show me log in console and write to file:
```C#
var logger = new ConsoleLogger() + new TextFileLogger( filepath )
logger.Info( "Hello World!" );
```


show me log in console and write to files and auto rolling new log file:
```C#
var logger = new ConsoleLogger() + new TextFileLogger( new DirectoryInfo( directory ) )
logger.Info( "Hello World!" );
```

