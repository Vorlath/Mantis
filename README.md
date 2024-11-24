# Mantis
_A joint MonoGame engine effort_
<br />
<br />
<br />

## Rules and Goals
1. Target unit test code coverage: 75%
2. Custom attributes are allowed under the following conditions:
   - The same effect can be achieved at runtime with a dedicated fluent API
   - When applicable, custom code analyzers will keep attribute usage in check
3. Embrace DI (preferably Autofac) and avoid global values
4. Embrace mock git/open source traditions
   - Work on feature branches, require PRs to merge into main
   - Require issues for all PRs and commits into main (squash?)
   - Learn how to setup automated workflows for unit tests
5. Work together to decide if a feature belongs in the engine or is too specific and belongs in a game
6. No copy/pasting code. All code must be hand written even if sourced from elsewhere
7. Keep code well documented, easy to read, and simple
   - Avoid unnecessary complexity even if its 'cool'.
   - If, for whatever reason, complexity is unavoidable (reflection, IL, ect) create a local readme to document in detail what is going on and why
8. Embrace existing libraries where possible. No need to re-invent the wheel
   - Prioritize complex libraries (physics engines, networking, monogame, ect)
   - Avoid just adding libraries out of lazyness
   - Use submodules when possible - having local source code makes debugging easier
