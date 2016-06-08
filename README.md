NameParser
====

[![Join the chat at https://gitter.im/binaryfog/NameParser](https://badges.gitter.im/binaryfog/NameParser.svg)](https://gitter.im/binaryfog/NameParser?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build status](https://ci.appveyor.com/api/projects/status/8d3nkp5crr6mdftx/branch/master?svg=true)](https://ci.appveyor.com/project/binaryfog/nameparser/branch/master)   Human name parsing.

Parses names using English conventions for persons names. 
Intended to be extendable, the library can be extended just by implement `IPattern` interface and assign a score to the returned result.

For the sake of performance, the assembly and types implementing `IPattern` must be loaded before the first attempt to use the `NameParser`.

If you have a person name that is not parsed correctly, let me know; harapu@gmail.com. I'll see what I can do.

Usage:
```csharp
string fullName = "Mr. Jack Johnson"; 
FullNameParser target = new FullNameParser(fullName); 
target.Parse();
string firstName = target.FirstName;
string middleName = target.MiddleName;
string lastName = target.LastName;
string title = target.Title;
string nickName = target.NickName;
string suffix = target.Suffix;
string displayName = target.DisplayName;
```

Alternative usage:
```csharp
string fullName = "Mr. Jack Johnson"; 
FullNameParser target = FullNameParser.Parse(fullName);
string firstName = target.FirstName;
string middleName = target.MiddleName;
string lastName = target.LastName;
string title = target.Title;
string nickName = target.NickName;
string suffix = target.Suffix;
string displayName = target.DisplayName;
```


Nov. 13 2015: More cases are now handled. These are the cases:
* SR. John Henry William dela Vega, Jr Esq.
* MANUEL ESQUIPULAS SOHOM
* Maria Delores Esquivel-Moreno
* PHILIP DEHART ESQ
* DEHART, PHILIP
* john 'jack' kennedy
* john(jack) f kennedy
* kennedy, john(jack) f
* Mr.Jack Johnson, ESQ"
* Jose Miguel De La Vega
    
Jan. 8 2016: 100% code coverage. More test cases. These are the cases:
* Empty string and white space cases.
* ~~SR. John Henry William dela Vega, Jr Esq.~~ SR. is not a valid title.
* Mr. Jack Francis Van Der Waal Sr.
* Mr. Jack Francis Marion Van Der Waal Sr.
