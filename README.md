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

To see how to do it in XAML look at the "TranslateMe.Examples" project in the solution.

*Remark : By default the translation made in the XAML are automatically updated when Current language changed*

## OK But... ...How I define my translations ?
Translations are defined in JSON files with the extension "*.tm.json".

Here an example :

```json
{
  "LanguageName": {
    "en": "English",
    "es": "Español",
    "fr": "Français"
  },
  "[TranslateMe.Examples.MainWindow].lblCurrentLanguage[Label].Content": {
    "en": "Current language",
    "es": "Lenguaje actual",
    "fr": "Langue courrante"
  },
  "[TranslateMe.Examples.MainWindow].lblHelloInCurrentLanguage[Label].Content": {
    "en": "Hello",
    "es": "Hola",
    "fr": "Bonjour"
  },
  "HelloInCurrentLanguage": {
    "en": "Hello in the current language",
    "es": "Hola en la lengua actual",
    "fr": "Bonjour dans la langue actuelle"
  },
  "[TranslateMe.Examples.MainWindow].lblHelloInCurrentLanguage[Label].ToolTip": {
    "en": "In english",
    "es": "En español",
    "fr": "En français"
  }
}
```

And to load it :

```csharp
TMLanguagesLoader.AddFile(@"PathToTheFile\Example1.tm.json");
// or load directly a directory with multiple "*.tm.json" files.
TMLanguagesLoader.AddDirectory(@"PathToTheDirectory");
```

So you can change the text of your app or translate it in a new language without recompile all your application.

```csharp
// or you can also load a translation by code (textId, languageId, value)
TMLanguagesLoader.AddTranslation(""SayHello"", "en", "Hello" );
TMLanguagesLoader.AddTranslation(""SayHello"", "es", "Hola" );
TMLanguagesLoader.AddTranslation("SayHello", "fr", "Bonjour" );
```
