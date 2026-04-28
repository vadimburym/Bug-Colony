# Bug Colony

Unity 6 / C# / OOP gameplay architecture test project.

## О проекте

Данный проект разрабатывался исключительно в качестве реализации тестового задания **Bug Colony**. Цель проекта — продемонстрировать навыки разработки на Unity, владение принципами ООП, умение проектировать расширяемую архитектуру и применять архитектурные паттерны в игровом коде.

В рамках реализации основной акцент делается не на визуальную составляющую, а на структуру проекта, читаемость кода, разделение ответственности между системами, возможность расширения поведения сущностей и поддержку новых типов игровых объектов без необходимости переписывать существующую логику.

Проект может использоваться как демонстрационный пример подхода к построению gameplay-архитектуры: с конфигурируемыми сущностями, фабриками, сервисами, правилами поведения, обработкой жизненного цикла объектов и возможностью дальнейшего масштабирования.

## Важное примечание

Данное тестовое задание было найдено в открытом доступе на просторах интернета. Оно не является реальным тестовым заданием, с которым автор проекта пытался устроиться в конкретную компанию, и не связано с каким-либо фактическим процессом найма.

Реализация проекта создана исключительно в учебных и демонстрационных целях — чтобы показать подход к решению архитектурной задачи в Unity и продемонстрировать навыки работы с C#, ООП и паттернами проектирования.

## Статус проекта

Проект является законченным gameplay-прототипом для демонстрации архитектурного подхода.

Что реализовано:

- автоматический spawn еды на игровой области;
- worker bugs, которые ищут и потребляют food;
- predator bugs, которые могут потреблять food, workers и predators;
- размножение worker после накопления 2 единиц consumption;
- размножение predator после накопления 3 единиц consumption;
- lifetime predator — 10 секунд;
- автоматический spawn нового worker, если колония полностью вымерла;
- UI-счетчики погибших workers и predators через uGUI;
- конфигурируемое поведение bugs через ScriptableObject;
- gameplay loop через отдельные systems/services, без ECS/DOTS.

Графика намеренно минимальная: задача проекта — не визуальное качество, а архитектура, расширяемость и читаемость игрового кода.

## Быстрый запуск

### Требования

- Unity **6000.3.10f1** или совместимая версия Unity 6.
- Git.
- Установленные зависимости Unity Packages из `Packages/manifest.json`.
- Доступ к vendored plugins внутри `Assets/Plugins`.

### Как запустить

1. Склонировать репозиторий:

   ```bash
   git clone https://github.com/vadimburym/Bug-Colony.git
   ```

2. Открыть проект через Unity Hub.

3. Использовать Unity Editor версии **6000.3.10f1**.

4. Дождаться импорта packages и assets.

5. Открыть стартовую сцену:

   ```text
   Assets/_Project/Scenes/0.Bootstrap.unity
   ```

6. Нажать **Play**.

В `EditorBuildSettings` подключены две runtime-сцены:

```text
Assets/_Project/Scenes/0.Bootstrap.unity
Assets/_Project/Scenes/1.Gameplay.unity
```

`Editor.UI.unity` используется как вспомогательная/editor сцена и не является основной сценой запуска gameplay.

## Управление

В проекте нет активного player input. Это автономная симуляция.

После запуска нужно просто наблюдать за поведением колонии:

- еда появляется на сцене с заданным интервалом;
- bugs двигаются по сцене и выбирают цели;
- workers собирают food;
- predators атакуют bugs и food;
- UI показывает количество погибших worker bugs и predator bugs.

## Ожидаемое поведение

### Food

Food появляется на игровой области случайно во времени.

Текущие значения задаются в:

```text
Assets/_Project/Configs/Gameplay/BugColonyStaticData.asset
```

Текущая конфигурация:

```text
FoodSpawnInterval: 5..10 seconds
FoodSpawnCount:    5..10 items
```

### Worker Bug

Worker:

- перемещается по сцене;
- ищет consumable target;
- потребляет food;
- после 2 потребленных ресурсов уничтожается с причиной `Split`;
- вместо него создаются 2 новых bug entity.

При количестве bugs в колонии больше или равно заданному threshold включается логика мутации.

Текущая конфигурация находится в:

```text
Assets/_Project/Configs/Gameplay/WorkerBug.asset
```

Ключевые настройки:

```text
ConsumptionToSplit: 2
SplitCount:         2
MutationChance:     0.1
BugsCountCondition: 10
```

### Predator Bug

Predator:

- перемещается по сцене;
- может потреблять food;
- может атаковать worker bugs;
- может атаковать predator bugs;
- живет 10 секунд;
- после 3 потребленных объектов делится на 2 predator bugs;
- новые predator bugs получают свежий lifetime timer.

Текущая конфигурация находится в:

```text
Assets/_Project/Configs/Gameplay/PredatorBug.asset
```

Ключевые настройки:

```text
ConsumptionToSplit: 3
SplitCount:         2
LifeTime:           10 seconds
```

### Colony Respawn

Если на сцене не осталось живых bugs, система автоматически создает нового worker bug в случайной точке игровой области.

Это позволяет симуляции не останавливаться полностью после вымирания колонии.

### UI

UI отображает:

- total dead worker bugs;
- total dead predator bugs.

Важно: уничтожение bug из-за split не считается смертью и не увеличивает death counter. Смерти учитываются только для соответствующих destroy-cases.

## Архитектурная идея

Главная цель архитектуры — отделить данные, правила поведения, runtime-сущности, Unity view и инфраструктуру создания объектов.

Вместо одного большого `BugController` поведение bug собирается из набора правил:

- target selection rule;
- movement rule;
- contact rule;
- consumable rule;
- split rule;
- lifetime rule;
- destroy rule.

Это позволяет добавлять новые типы bugs через новые конфигурации и правила, не переписывая существующую gameplay-систему целиком.

## Основной data flow

Упрощенно runtime-flow выглядит так:

```text
GameplayEntryPoint
    -> WarmUp systems
    -> Init systems
    -> Tick / PausableTick / LateTick systems
        -> Spawn systems
        -> Target selection
        -> Movement
        -> Circle physics
        -> Contact processing
        -> Consumption update
        -> Split / lifetime / destroy rules
        -> Statistics update
        -> UI update
```

Создание объекта:

```text
System / Rule
    -> BugFactory / FoodFactory
        -> create domain entity
        -> resolve config
        -> spawn view from pool
        -> register entity in services
```

Уничтожение объекта:

```text
Destroy rule
    -> unregister from services
    -> return view to pool
    -> update statistics if destroy cause is death
```

## Основные архитектурные решения

### OOP вместо ECS/DOTS

Техническое требование запрещает ECS/DOTS, поэтому проект построен на классическом OOP-подходе:

- domain entities;
- services;
- systems;
- factories;
- strategy-like rules;
- ScriptableObject configs;
- Unity views.

### ScriptableObject как gameplay config

`BugConfig` и `FoodConfig` хранят gameplay-настройки и набор правил поведения. Это позволяет менять поведение объектов через assets, а не через правку MonoBehaviour в сцене.

Примеры:

```text
Assets/_Project/Configs/Gameplay/WorkerBug.asset
Assets/_Project/Configs/Gameplay/PredatorBug.asset
Assets/_Project/Configs/Gameplay/Food.asset
Assets/_Project/Configs/Gameplay/BugColonyStaticData.asset
```

### Rules / Strategy

Разные аспекты поведения вынесены в отдельные rules. Это сделано для расширяемости: новый тип bug может переиспользовать существующие rules или подключить свои.

Примеры:

```text
DefaultSplitRule
MutationSplitRule
DefaultLifeTimeRule
InfinityLifeTimeRule
ConsumerMovementRule
BugDestroyRule
FoodDestroyRule
```

### Factory

Создание bugs и food централизовано в factory layer.

Factory отвечает за:

- создание domain entity;
- применение config;
- создание view;
- регистрацию объекта в runtime services.

Это убирает создание объектов из правил поведения и gameplay systems.

### Object Pool

Unity views создаются через pool. Это снижает количество runtime instantiate/destroy операций и отделяет жизненный цикл domain entity от жизненного цикла Unity GameObject.

### DI

В проекте используется Zenject. DI применяется для связывания сервисов, систем, фабрик и entry points.

Основная цель DI здесь — сделать зависимости явными и убрать ручной поиск объектов через `FindObjectOfType`, singletons и static access.

### Reactive UI

Для UI-статистики используется реактивный подход. Statistics service хранит значения счетчиков, а UI подписывается на изменения и обновляет view.

### Custom lightweight circle physics

Для симуляции используется собственная простая circle-physics логика, ориентированная на gameplay-задачу проекта:

- 2D-позиционирование в top-down сцене;
- радиусы collider-like bodies;
- contact processing;
- spatial partitioning через grid.

Это не замена полноценной Unity physics, а небольшой gameplay-layer для контролируемой симуляции.

## Структура проекта

```text
Assets/
  _Project/
    Code/
      Core/               # Общие abstractions, keys, events
      GameApp/            # Entry points, installers, app-level composition
      Gameplay/
        _Core/            # Общие gameplay services: movement, physics, lifetime, etc.
        BugColony/        # Основной gameplay feature
          Domain/         # Runtime entities
          Factory/        # Bug/Food factories
          Providers/      # Scene/runtime providers
          Rules/          # Behavior rules
          Services/       # Colony-specific services
          Systems/        # Gameplay systems
        Statistics/       # Runtime statistics + UI presenters/views
      Infrastructure/     # Utility/infrastructure services
      Local/              # Local DI/context infrastructure
      StaticData/         # ScriptableObject configs and static data access
    Configs/
      Gameplay/           # Worker, Predator, Food, Colony static data
    Content/              # Prefabs/views/content assets
    Scenes/
      0.Bootstrap.unity   # Entry scene
      1.Gameplay.unity    # Gameplay scene
      Editor.UI.unity     # Supporting/editor UI scene
    Settings/
  Plugins/
    Sirenix/
    UniRx/
    UniTask/
    Zenject/
  Packages/
```

## Ключевые классы и зоны ответственности

### Entry / Composition

- `GameplayEntryPoint` — управляет warm-up, init, tick, pausable tick, late tick и dispose фазами gameplay.
- `GameplayInstaller` — регистрирует gameplay services, systems and factories в DI container.

### Bug Colony

- `BugEntity` — domain entity bug.
- `FoodEntity` — domain entity food.
- `BugFactory` — создание и регистрация bugs.
- `FoodFactory` — создание и регистрация food.
- `BugColonyService` — учет bugs в колонии.
- `BugsSpawnSystem` — auto-respawn worker при полном вымирании.
- `FoodSpawnSystem` — периодический spawn food.

### Core gameplay services

- `ConsumerService` — учет consumption и consumer targets.
- `ConsumableService` — регистрация consumable entities.
- `DestructibleService` — destroy lifecycle.
- `LifeTimeService` — lifetime update.
- `MovableService` — movement state.
- `CirclePhysicsService` — lightweight collision/contact simulation.
- `EntityViewService` — связь domain entity с Unity view.

### Statistics / UI

- `StatisticService` — хранение runtime counters.
- `StatisticWidget` — создание UI counters.
- `DeathStatPresenter` — связывает statistics и UI view.
- `DeathStatView` — uGUI/TMP view одного счетчика.

## Как добавить новый тип bug

Минимальный сценарий добавления нового bug type:

1. Добавить новый id в enum/key bug type.
2. Создать новый `BugConfig` asset.
3. Настроить:
   - `MoveSpeed`;
   - `ColliderRadius`;
   - `ConsumeReward`;
   - `ViewPoolId`;
   - selector rule;
   - split rule;
   - consumable rule;
   - lifetime rule;
   - destroy rule;
   - movement rule;
   - contact rules.
4. Добавить config в `BugColonyStaticData`.
5. Добавить/настроить view prefab и pool id.
6. При необходимости реализовать новые rules.
7. Запустить сцену и проверить behavior через manual checklist.

В идеальном случае новый тип bug не должен требовать изменений в существующих services/factories, кроме регистрации нового id/config/view.

## Как добавить новое правило поведения

1. Создать класс rule, реализующий нужный gameplay interface.
2. Сделать rule serializable, если он должен храниться в ScriptableObject.
3. Добавить необходимые serialized fields.
4. Добавить зависимости через DI, если rule использует services/factories.
5. Подключить rule в нужный `BugConfig` или `FoodConfig`.
6. Проверить, что rule проходит injection на старте gameplay.

## Known limitations / self-review

Этот проект сделан как тестовое задание, поэтому часть решений оставлена в состоянии prototype-quality. Ниже — честный список ограничений и мест, которые я бы улучшал в первую очередь.

### Defensive coding

В некоторых services есть прямой доступ к dictionary по entity id. Для текущего controlled flow этого достаточно, но для production-кода я бы добавил более безопасные `TryGet...` / `TryRemove...` методы и явные guards на случай, если entity была уничтожена в середине update/contact pipeline.

### Target invalidation

Если target был уничтожен до movement update, movement rule должен явно сбрасывать velocity или быстрее выбирать новую цель. Сейчас это поведение можно улучшить, чтобы entity не продолжала двигаться по старой velocity.

### Tests

Что стоило бы покрыть тестами:

- worker split after 2 consumption;
- predator split after 3 consumption;
- predator lifetime death after 10 seconds;
- colony respawn when bugs count is zero;
- death statistics should not count split as death;
- mutation probability rule should match exact task interpretation;
- unregister lifecycle should not leave stale entities in services.

### Visual polish

Графика намеренно не полировалась, так как в задании прямо указано, что visual quality не влияет на оценку. При необходимости можно добавить:

- разные визуальные маркеры для worker/predator/food;
- spawn/death effects;
- небольшие UI hints;
- debug overlay для количества alive bugs, food, predators.

## Почему архитектура сделана именно так

Задание отдельно акцентирует внимание на архитектуре и просит представить, что в будущем появятся новые типы bugs: flying, burrowing, healer и другие.

Поэтому проект сознательно не сделан как набор hardcoded MonoBehaviour-компонентов под два текущих типа bugs. Вместо этого используется data-driven и rule-based подход:

- worker и predator — это не отдельные большие классы поведения;
- поведение описывается конфигом;
- отдельные правила можно переиспользовать;
- новый bug type должен добавляться через новый config и небольшой набор новых rules;
- factories/services остаются общими.

Это решение сложнее, чем минимальная реализация тестового задания, но оно лучше демонстрирует подход к расширяемой gameplay architecture в Unity.

При этом я понимаю, что для маленького prototype такая архитектура может выглядеть избыточной. Это осознанный trade-off: я выбрал показать масштабируемую структуру и separation of concerns, а не самый короткий путь к рабочей симуляции.

---

# Test Assignment
## Bug Colony

## Description

Bug Colony is a 3D simulation of a bug colony. Bugs move around the scene, collect resources, reproduce and mutate.

## Camera

Top down view

## Graphics

No graphics required. Boxes, spheres, capsules and any other primitives are fine. You are free to use any assets from asset stores or any other resources, but note that graphic this will not affect final results at all.

## Gameplay

Resources (food) randomly appear on the scene over time.
## Worker Bug

Worker bug moves around the scene and picks up resources. When a worker has eaten 2 resources, it splits into 2 worker bugs.

If there are more than 10 alive bugs in the colony, each time a worker splits there is a 10% chance that one of the offspring mutates into a predator bug.
## Predator Bug

Predator bug attacks and eats everything: other bugs (both workers and other predators) and food resources. Predator bug lives for 10 seconds, then dies.

When a predator has eaten 3 resources or bugs, it splits into 2 predator bugs (each with a fresh 10 second timer).

## Colony Rules

If there are no alive bugs left on the scene, a new worker bug spawns automatically.
## UI

Display the following counters:

  * Total dead worker bugs
  * Total dead predator bugs

UI should be created using uGUI.
## What we are looking for

The most important thing we evaluate is code architecture. We expect to see clean, easy to extend and easy to understand code. Imagine that in the near future we will add 10 new bug types with different behaviors (flying bugs, burrowing bugs, healer bugs, etc.)

We also pay attention to the tools and frameworks you use. If you are familiar with DI containers, UniTask, R3/UniRx or other professional Unity development tools, please use them.
If you have experience with architecture patterns (Strategy, Factory, Object Pool, etc.) please demonstrate them.
## Technical Requirements

  * Unity 6
  * C#
  * Do NOT use ECS/DOTS Yes. We use DOTS in our project but we are interested in OOP knowledge

## Submission

Please publish the project to a git repository and send us the link.
