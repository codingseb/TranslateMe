# TranslateMe

__Obsolete : replaced by [CodingSeb.Localization](https://github.com/codingseb/Localization)__

A C# and WPF apps translation library

## Status

|Branch|Status|
|---|---|
|master|[![Build Status](https://coding-seb.visualstudio.com/_apis/public/build/definitions/6e2bf9e9-7c56-4266-b4d4-748a1cfa263c/3/badge)](https://coding-seb.visualstudio.com/TranslateMe/_build/index?definitionId=3)|
|dev|[![Dev Status](https://coding-seb.visualstudio.com/_apis/public/build/definitions/6e2bf9e9-7c56-4266-b4d4-748a1cfa263c/4/badge)](https://coding-seb.visualstudio.com/TranslateMe/_build/index?definitionId=4)|
|nuget|[![NuGet Status](http://img.shields.io/nuget/v/TranslateMe.svg?style=flat&max-age=86400)](https://www.nuget.org/packages/TranslateMe/)|
## Quick installation

### With Nuget

```
PM> Install-Package TranslateMe
```

or in Visual Studio Right-Click on your *.csproj --> "Manage nuget package..." --> Search for "TranslateMe" Install the "TranslateMe" package by Coding Seb.

## To see how to use it
Simply clone this repository

## Simple Use

### In C# :

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

### In XAML (WPF) :
(no xmlns needed Tr Markup is available as soon as TranslateMe and TranslateMe.WPF are in project's references)

```xml
<!-- textId can be automatically calculate (with x:Name and the context of the element) -->
<Label x:Name="lbMyLabel" Content="{Tr }" />
<!-- or -> specify a custom textId -->
<Label x:Name="lblMyLabel" Content="{Tr 'textId'}" />
<!-- or -> specify a default text to show in designer and/or when the text is not translated in the current language-->
<!-- To escape special character you can use standard xml escapes like &gt; &quot; ... for single quote escape use [apos] -->
<Label x:Name="lblMyLabel" Content="{Tr 'textId', DefaultText='my default Text here'}" />
<!-- or -> force the translationLanguage -->
<Label x:Name="lblMyLabel" Content="{Tr 'textId', LanguageId='en'}" />
```

In general use XML escape to escape special characters. For single quote use ```[apos]``` to escape. XML escape does'nt work in this case for inline Tr markup. Or use the following format : 

```xml
<!-- textId can be automatically calculate (with x:Name and the context of the element) -->
<Label x:Name="lbMyLabel" >
  <Label.Content>
    <Tr DefaultText="Text with a ' here" />
  </Label.Content>
</Label>
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

*Remark : By default the translation made in the XAML are automatically updated when current language changed*

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
TMLanguagesLoader.Instance.AddFile(@"PathToTheFile\Example1.tm.json");
// or load directly a directory with multiple "*.tm.json" files.
TMLanguagesLoader.Instance.AddDirectory(@"PathToTheDirectory");
```

So you can change the text of your app or translate it in a new language without recompile all your application.

```csharp
// or you can also load a translation by code (textId, languageId, value)
TMLanguagesLoader.Instance.AddTranslation("SayHello", "en", "Hello" );
TMLanguagesLoader.Instance.AddTranslation("SayHello", "es", "Hola" );
TMLanguagesLoader.Instance.AddTranslation("SayHello", "fr", "Bonjour" );
```

### Implement your own file format
If you want to support an other format than json, you can create your custom FileLanguageLoader.
Simply create a class that implement the ITMFileLanguageLoader interface and add an instance of your class in the FileLanguageLoaders :

```csharp
TMLanguagesLoader.Instance.FileLanguageLoaders.Add(new YouCustomClassImplementingITMFileLanguageLoader());
```

Look at the TranslateMe/TMJsonFileLanguageLoader.cs file to see how it works.

## Find Missing Translations
You can activate an option to generate a file with all missing translations. 

```csharp
// This will create a file named "TMMissingTranslations.json" in the directory of your assembly
// with all TextId and LanguageId that are missing when you trying to translate them.
TM.Instance.LogOutMissingTranslations = true;
```
