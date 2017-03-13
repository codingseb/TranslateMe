# TranslateMe
[![translateme MyGet Build Status](https://www.myget.org/BuildSource/Badge/translateme?identifier=6060db76-e2c8-43be-b02e-e37bf48855b2)](https://www.myget.org/)

A C# and WPF apps translation library

## Quick installation

### With Nuget

```
PM> Install-Package TranslateMe
```

or in Visual Studio Right-Click on your *.csproj --> "Manage nuget package..." --> Search for "TranslateMe" Install the "TranslateMe" package by Coding Seb.

## To see how to use it
Simply clone this repository

## Simple Use

###In C# :

```csharp
using TranslateMe;

// ...

// Text Id is the identifier of the text to translate
string translatedText = TM.Tr(textId);
// or -> defaultText is the text to show if no translations are defined for this textId in the current language.
string translatedText = TM.Tr(textId,defaultText);
// or -> languageId to force the language in which is show the text
// "en", "fr", "es" ...
string translatedText = TM.Tr(textId, defaultText, languageId);
```

###In XAML (WPF) :
(no xmlns needed Tr Markup is available as soon as TranslateMe and TranslateMe.WPF are in project's references)

```xml
<!-- textId can be automatically calculate (with x:Name and the context of the element) -->
<Label x:Name="lbMyLabel" Content="{Tr 'defaultText'}" />
<!-- or -> specify a custom textId -->
<Label x:Name="lblMyLabel" Content="{Tr 'defaultText', TextId='myCustomLabel'}" />
<!-- or -> force the translationLanguage -->
<Label x:Name="lblMyLabel" Content="{Tr 'defaultText', LanguageId='en'}" />
```

## Language Selection
To select the language of the application.

```csharp
TM.Instance.CurrentLanguage = "en";
TM.Instance.CurrentLanguage = "fr";
TM.Instance.CurrentLanguage = "es";
// ...
// To get availables languages
Collection<string> languages = TM.Instance.AvailableLanguages;
```

To see How to do it in XAML look at the "TranslateMe.Examples" project in the solution.

*Remark : By default the translation made in the XAML are automatically updated when Current language changed*
