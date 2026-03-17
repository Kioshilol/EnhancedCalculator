# EnhancedCalculator

Addition-only calculator built with Unity (URP, Unity 6 LTS).

## Architecture

MVP (Model-View-Presenter) with Clean Architecture layering.

**Modules (separate assemblies):**

- `Calculator` - core calculator module: domain logic, data persistence, presentation layer
- `MessageBox` - reusable dialog module, decoupled from calculator
- `App` - composition root, DI setup, UI factory

**Layers:**

- `Domain` - pure C# business logic (`AdditionCalculator`, `CalculationResult`, `CalculatorState`). No Unity dependencies.
- `Data` - persistence via `PlayerPrefs`. Abstract `BaseRepository<T>` with generic serialization, concrete `CalculatorRepository` on top.
- `Presentation` - `CalculatorPresenter` orchestrates view and model through interfaces (`ICalculatorView`, `ICalculator`, `ICalculatorRepository`).
- `View` - MonoBehaviour implementations binding Unity UI to presenter contracts.

## DI

VContainer as IoC container. `AppBootstrapper` (extends `LifetimeScope`) wires all dependencies in `Configure()`.

## UI

Built programmatically via `UiFactory` at runtime. No prefabs or scene-based UI setup required - single `AppBootstrapper` component on an empty GameObject.

## State Persistence

`CalculatorState` (current input + history) serialized to JSON via `PlayerPrefs`. State is saved on every input change and after each calculation. Restored on app start.

## Validation

Regex `^\d+\+\d+$` - only non-negative integers with a single `+` operator. Everything else produces `ERROR` and triggers the error dialog.

## Setup

1. Open project in Unity 6+
2. Import TMP Essentials (Window > TextMeshPro > Import TMP Essential Resources)
3. Open `Assets/EnhancedCalculator` scene
4. Press Play
