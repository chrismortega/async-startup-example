# Question:
>Since we talked a bit about async with dependency injection, I have a question similar to one that was asked earlier when talking about .Configure. A common approach we have to setting up items that are being registered with DI is to make an HTTP request to get configuration values (say an API token for an HTTP Client), but obviously making an HTTP request is something that will likely be done asynchronously. What kind of approach would you take in a similar scenario?

# Example Description

This project is setup to illustrate an example of the question posed above.

Looking at Startup, you'll see that we use the extension `AddHttpClientWithBasicAuth` to reister a proxy service that will need to attach basic auth credentials to each request.

Let's say we don't want to store the passwords locally, so we query them from a third party using the `ISecretKeyValueStore`, which exposes a method to query them async. 

This is the issue. If we look up the call stack we are still in `ConfigureServices` in `Startup`, which is not async. It is also not in our control to change it and make it async. But we need to make an async call?

So is there a better way we might approach this?