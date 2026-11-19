# murmur-web

Murmur is a self-hosted chat app in the style of Discord. This repository holds its web client, a
standalone **Blazor WebAssembly** app. The server lives in
[murmur-api](https://github.com/xavidiaz/murmur-api).

The client uses the familiar four-column layout: a server rail, a channel or DM list, the message
pane and a member list with presence dots. Dark theme is the default.

**Milestone 1** covers accounts, servers, text channels, real-time messages, direct messages,
presence and typing indicators.

## Project status

**Milestone 1: web stories not written yet — scaffold done**

| Area | Status |
| --- | --- |
| Scaffold: Blazor WASM + ShadcnBlazor verified on .NET 10, Tailwind build, bUnit test helper | ✅ |
| Sign-in pages & token refresh | ⬜ |
| App shell & navigation | ⬜ |
| Messages | ⬜ |
| Dialogs (server/channel/invite) | ⬜ |
| Direct messages | ⬜ |
| Presence & typing | ⬜ |

✅ done (links to the merged PR) · 🚧 in progress (links to the issue) · ⬜ not started

Each story PR updates its own row here and the progress line above.

## Stack

- **.NET 10**, Blazor WebAssembly (standalone).
- **[ShadcnBlazor](https://github.com/ChiaraBm/ShadcnBlazor)**, a Blazor port of shadcn/ui, for
  the UI components.
- **Tailwind CSS v4**, compiled by the standalone Tailwind CLI from an MSBuild target. There is no
  Node.js or npm step, and the generated CSS is not committed.
- **SignalR .NET client** for real-time events (arrives with the realtime stories).
- **Contracts**: the request, response and event types are shared with the API through its
  `Contracts` project, so client and server use the same types.
- Tests: **bUnit** component tests on **xUnit v3**.

## Repository layout

```
web.slnx                   solution: src/Web, tests/Web.Tests and ../api/src/Contracts
Directory.Build.props      net10.0, nullable, warnings as errors
Directory.Packages.props   central package versions (app and tests)
src/Web/                   the Blazor WebAssembly app
  Styles/app.css           Tailwind entry point (compiled to wwwroot/css/app.css at build time)
  wwwroot/                 static files (index.html, icons)
tests/Web.Tests/           bUnit component tests
```

### Sibling checkout of murmur-api

The client compiles against the API's `Contracts` project through a relative project reference
to `../api/src/Contracts/Contracts.csproj`. Clone both repositories side by side, as `api/` and
`web/`:

```
murmur/
  api/   ← git clone https://github.com/xavidiaz/murmur-api.git api
  web/   ← git clone https://github.com/xavidiaz/murmur-web.git web
```

```bash
mkdir murmur && cd murmur
git clone https://github.com/xavidiaz/murmur-api.git api
git clone https://github.com/xavidiaz/murmur-web.git web
```

The web build fails if `../api` is missing or named differently.

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- The sibling `api/` checkout described above.
- Network access for the first build (see below).

### Build

```bash
cd web
dotnet build web.slnx
```

The first build downloads the standalone Tailwind CLI (about 110 MB) and `tw-animate-css` into
`.tools/`, a git-ignored folder, and checks both downloads against pinned SHA-256 checksums.
Later builds reuse them, and Tailwind only runs again when styles, markup or code change. The build
is expected to finish with zero warnings, because warnings are treated as errors.

### Run

```bash
dotnet run --project src/Web
```

Then open http://localhost:5059.

### Test

```bash
dotnet test web.slnx
dotnet test web.slnx --filter "Story=<story tag>"   # one story's tests
```

## How work is done

Work happens one story at a time, tests first:

1. **Story.** Each story describes a user-visible behaviour as a set of scenarios.
2. **Issue.** Each story gets a GitHub issue labelled `story`, with its scenarios as a checklist,
   and a branch `story/<issue>-<slug>`.
3. **Tests first.** Failing bUnit tests for every scenario are committed on the branch before any
   implementation.
4. **PR.** The implementation makes those tests pass, along with every earlier test, with zero
   warnings. It is then opened as a pull request that closes the issue and updates the
   project status table above. A pull request is merged only after review.
