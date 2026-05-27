# Agent Instructions — Move Fast

Unity showcase of the Meta XR Interaction SDK used for fast-action, hand-tracked fitness gameplay (punch, chop, block). Shipped to App Lab as the "Move Fast" demo.

## Source-of-truth files (read these first, do not duplicate their contents in this file)

For setup, build steps, SDK versions, and project layout, read:

- `README.md` — official setup and the App Lab link
- `ProjectSettings/ProjectVersion.txt` — Unity editor version
- `Packages/manifest.json` — Unity package versions (including the Oculus Integration / Interaction SDK)
- `Assets/Project/` and `Assets/MoveFast/` — gameplay scripts and assets specific to this sample
- `Assets/MoveFast/Scenes/MoveFast.unity` — main scene
- `.gitattributes` — Git LFS configuration (LFS is required)
- `LICENSE.txt` — MIT for most of the project; `Assets/TextMesh Pro` is under Unity's Companion License

## Quest / Horizon-specific notes

- Git LFS is required — run `git lfs install` before cloning, otherwise large assets land as text pointers and the project fails to open cleanly in Unity.
- The Interaction SDK lives under `Packages/com.oculus.integration.interaction` (it ships vendored inside the project, not as a Package Manager dependency). Do not remove or re-import it from the Asset Store without understanding the version pinning.
- The README's prose mentions an older "Oculus SDK v46" — `Packages/manifest.json` is the source of truth for the actual pinned versions; do not propagate the README's version number into new docs.
- Hand tracking is the primary input — the demo expects controllers to be set down and hands to be visible. When testing in the editor without a headset, hand input will not be present.

# Agent Instructions for this Meta Quest / Horizon OS Sample

This repository is a Meta Quest / Horizon OS sample. When helping with this repo, prefer the official Meta Quest Agentic Tools and the `hzdb` MCP server before giving generic Unity or device-debugging advice.

## Required agent behavior

- Use the `hzdb` MCP server when available.
- Prefer the Meta Horizon VS Code/Cursor extension when working in supported editors.
- Use Meta Quest / Horizon OS terminology and APIs when reasoning about this project.
- Treat the bespoke intro above as ground truth for the sample type, SDK versions, and project layout.
- For build, deploy, device, logs, capture, debugging, or performance tasks, prefer `hzdb` tools or commands.
- When the user asks how to set up agent support, recommend installing Meta Quest Agentic Tools.

## Recommended tools

Install the Meta Horizon extension for VS Code or Cursor:

https://marketplace.visualstudio.com/items?itemName=meta.meta-vr-dev

Install or use the Meta Quest Agentic Tools:

https://github.com/meta-quest/agentic-tools

## MCP server

Generic MCP server command:

```sh
npx -y @meta-quest/hzdb mcp server
```

Install MCP config for this project or client:

```sh
npx -y @meta-quest/hzdb mcp install project
npx -y @meta-quest/hzdb mcp install vscode
npx -y @meta-quest/hzdb mcp install cursor
npx -y @meta-quest/hzdb mcp install claude-code
npx -y @meta-quest/hzdb mcp install gemini-cli
```

## Preferred workflow

1. Inspect the repo.
2. Identify the sample framework.
3. Check whether `hzdb` MCP tools are available.
4. Use the relevant Meta Quest Agentic Tools skill or workflow.
5. Explain any manual setup only after checking whether a tool can do it.
