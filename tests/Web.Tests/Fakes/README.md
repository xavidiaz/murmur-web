# Fakes

Hand-written test doubles for the web app's seams will go here, for example:

- `FakeApiClient`: implements `IApiClient`, records every call and returns canned results set by the test.
- `FakeChatHubClient`: implements `IChatHubClient`, records calls and lets a test raise hub events.

These arrive with the first web stories, which define the `IApiClient`/`IChatHubClient` interfaces.
Until then, nothing here. Don't invent the interfaces ahead of those stories.

Rules: no mocking library, no real HTTP or SignalR. Register fakes in a test through
`Services` on `WebTestContext` (see `../Infrastructure/WebTestContext.cs`).
