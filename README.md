# TranslateMe

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
<!-- To escape special character you can use standard xml escapes like &gt; &quot; ... for single quote escape use [apos]
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
TMLanguagesLoader.AddFile(@"PathToTheFile\Example1.tm.json");
// or load directly a directory with multiple "*.tm.json" files.
TMLanguagesLoader.AddDirectory(@"PathToTheDirectory");
```

So you can change the text of your app or translate it in a new language without recompile all your application.

```csharp
// or you can also load a translation by code (textId, languageId, value)
TMLanguagesLoader.AddTranslation("SayHello", "en", "Hello" );
TMLanguagesLoader.AddTranslation("SayHello", "es", "Hola" );
TMLanguagesLoader.AddTranslation("SayHello", "fr", "Bonjour" );
```

## Find Missing Translations
You can activate an option to generate a file with all missing translations. 

```csharp
// This will create a file named "TMMissingTranslations.json" in the directory of your assembly
// with all TextId and LanguageId that are missing when you trying to translate them.
TM.Instance.LogOutMissingTranslations = true;
```
