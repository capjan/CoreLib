# ITextFormatter

## Features

* The ITextFormatter Interface provides a generic interface to format objects.
* Write the formatted output to TextWriter instances (Streamwriter, Console, etc) or to a String.
* Hides the implementation of the formatter. You can change the implementation at any time.
* Makes Dependency injection a breeze.
* Makes it easy to write additional formatters, because there is only one method to implement.

## Interface

```c#
public interface ITextFormatter<in T>
{
    void Write(T value, TextWriter writer);
}
```

## Supports many outputs

Due to directly formatting to Textwriter every ITextFormatter supports

* **Console.Out** and Console.Error - Writing to Console
* **StreamWriter** - Writes encoded text to any Stream (File, Memory, Network, etc.)
* StringWriter - Implements writing to a String (StringBuilder) via TextWriter
* IndentedTextWriter - Supports writing intented Text
* HttpWriter - Allows direct writing to a HttpResponse object.
* HtmlTextWriter - Supports writing of formatted html

## Extensions

Feel free to extend the ITextFormatter.

Current Extensions:

* **WriteToString()**

  ```C#
  var formatter = new SiFormatter {FormatProvider = CultureInfo.InvariantCulture};
  var std = formatter.WriteToString(144e4m);
  // std = "1.44 M"
  ```

* **ToFormattedString()**

  * Every Type provides a ToFormattedString() String method.
  * If the formatting comes from the object itself, the possibility of formatting is found easier by a developer.
  * hopefully all common objects are providing default extension without the need for providing a formatter.
  * **Do not use this extension method for core framework code and prefer injecting formatters.**

  ```C#
  TimeSpan.FromDays(1.234).ToFormattedString();
  // "1 day, 5 hours, 36 minutes"
  ```

  



