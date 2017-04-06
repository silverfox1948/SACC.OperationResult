# SACC.OperationResult
A little .NET library to standardize the results of invoking a method.
It targets .NET Standard 1.0, so should be working on just about everything .NET...

### Rationale:
Over the years, I've gotten tired of seeing code like this when code needs to invoke a method, return a true/false indication, and possibly some data (structured or otherwise):
``` C#
// if errorMessage is empty, then it worked
string errorMessage = DoSomething(string someParam);
if {string.IsNullOrEmpty(errorMessage) 
{
	// do some more stuff here
}
```
or
``` C#
// DoSomething returns a boolean value 
if (DoSomething(someParam))
{
	// do some more stuff here
}
```
or
``` C#
// we want a boolean and a data value
var object = outVal;
if (DoSomething(someParam, out outVal))
{
	// do some more stuff here
}
```
and finally
```C#
if (DoSomething(someParam))
{
	if (DoSomethingElse(otherParam)
	{
		if (DoOneMoreThing(yetanotherParam)
		{
			// yousa!
		}
		else
		{
			// what error to return here?		
		}
	else
	{
		// what error to return here?		
	}
}
else
{
	// what error to return here?		
}
```
somehow, all of this seems pretty sloppy and disorganized. Wouldn't it be nice if there was just a common way to invoke a method and tell if it worked or not? And a common way to return errors?

### What's a better way?
I've gone through a couple of iteratiions of this, trying to strip out as much un-necessary stuff, and have come up with basicall three classes that solve the problem in a way I find useful, and far more standardized.
Let's start with the first two basic classes.
Error.cs
``` C#
public class Error
{
	public string ErrorCode { get; private set; }
	public string ErrorMessage { get; private set; }

	public Error(string errorMessage)
	{
		this.ErrorCode = string.Empty;
		this.ErrorMessage = errorMessage;
	}

	public Error(string errorCode, string errorMessage)
	{
		this.ErrorCode = errorCode;
		this.ErrorMessage = errorMessage;
	}
}
```
Pretty basic... A string error code, and a string message with a couple of constructors.

OperationResult.cs
``` C#
public class OperationResult<T>
{
	public bool IsSuccessful => Errors.Count == 0;
	public bool IsUnsuccessful => !IsSuccessful;
	public List<Error> Errors { get; set; }
	public T ResultData { get; set; }

	public OperationResult()
	{
		Errors = new List<Error>();
	}

	public OperationResult(T resultData)
	{
		Errors = new List<Error>();
		ResultData = resultData;
	}

	public OperationResult(T resultData, Error error)
	{
		Errors = new List<Error>();
		Errors.Add(error);
		ResultData = resultData;
	}

	public OperationResult(T resultData, List<Error> errors)
	{
		Errors = new List<Error>();
		Errors.AddRange(errors);
		ResultData = resultData;
	}

	public OperationResult<T> AddResultError(Error error)
	{
		Errors.Add(error);
		return this;
	}

	public OperationResult<T> AddResultErrors(List<Error> errors)
	{
		Errors.AddRange(errors);
		return this;
	}
```
This is a bit more interesting. Note two properties that indicate whether the operation is successful or not. 
* An Errors list, which allows you to provide multiple error messages if you want
* A few constructors
* A few methods to add one or more Error objects (see the first class) to the Errors list
* Returns itself so we can get some fluency

### Show me the code!
Let's see an example of how this can be used (taken from the test project). First we need a class and method that we can call.
``` C#
public class SomeUsefulClass
{
	public static OperationResult<TestResultData> SomeUsefulMethodWithErrors(string someParam)
	{
		var result = new OperationResult<TestResultData>();
		result.Errors.Add(MakeError("oofta! We got's problems!"));
		return result;
	}

	public static OperationResult<TestResultData> SomeUsefulMethodNoErrors(string someParam)
	{
		var result = new OperationResult<TestResultData>();
		result.ResultData = new TestResultData(); //assumes TestResultData is a class. 
													 // a value type of int, string etc can be assigned if <T> is a value type
		return result;
	}
}
```
here ```TestResultData``` just represents some object we want to get back. Wait! What if I don't want to return anything? I deal with that later on in this document, so hold yer horses ..

Let's now invoke the method:
```C#
public class InvokeStuff
{
	// just looking for boolean return - pretty much the same structure
	if (SomeUsefulClass.SomeUsefulMethodNoErrors("Hah! Use this!").IsSuccessful))
	{

	}
	// optional ...
	else
	{
		foreach(var error in result.Error)
			processError(error);
	}

	// looking for data return 
	var result = SomeUsefulClass.SomeUsefulMethod("Hah! Use this!");
	if (result.IsSuccessful)
	{
		var myData = result.ResultData;
	}
	// optional ...
	else
	{
		foreach(var error in result.Error)
			processError(error);
	}
}
```
finally, let's look at the "stacked" calls:
```C#
if (result.AddResultErrors((SomeUsefulClass.SomeUsefulMethod("hah").Errors)).IsUnsuccessful) { return result; }
if (result.AddResultErrors((SomeUsefulClass.AnotherUsefulMethod("hah").Errors)).IsUnsuccessful) { return result; }
if (result.AddResultErrors((SomeUsefulClass.LastUsefulMethod("hah").Errors)).IsUnsuccessful) { return result; }
```
All of a sudden, stacking this stuff up looks very regular. And that's really the point. When your code has a common, reliable, repeatable structure, it's much easier for someone else to understand when your code looks like their code. This makes code more maintainable - and believe it or not, makes your unit tests better as well!

In a way, one of the best confirmations I had of this, was when I started using in an application at one of my clients. Without comment, and without any sort of "what's this?" questions, others started using it.

### So why the NullResultData class?
I promised I'd get to this ...
It's simple. It has to do with the ResultData property in the OperationResult class. 
You can't have 
```C#
Null ResultData;
```
as a property declaration (as far as I know). So I have an "empty" class named NullResultData that is a class with no members at all.
I could have called it "EmptyResultData" but that didn't seem as "techspeak" as using "Null" in the front of the name. So I took the lazy way out ...