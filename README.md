# README

## Projektname
*Behind The Bar*

## Projektziel
*Behind The Bar ist ein VR-Lernspiel, in dem der Spieler die Rolle des Barkeepers einnimmt und Erfahrungen mit dem Mixen von Getränken sammelt.*

## Credits               
*Philipp Trompetter & Luca-Leon Krause*

## Installation & Voraussetzungen
*Release-Ordner:*
https://gitlab.iue.fh-kiel.de/avr_ws2425/krause-trompetter/behind-the-bar/-/tree/main/Release?ref_type=heads

*Um das Spiel zu instalieren kann man einfach das Repo entweder mit "git clone "https://gitlab.iue.fh-kiel.de/avr_ws2425/krause-trompetter/behind-the-bar.git"" klonen, oder eine .zip direkt hier bei git herunterladen und anschließend die .exe im "Release" Ordner ausführen*

- *Hardware(Brille und PC der Uni):*

  - *Oculus Rift S + beide Touch Controller*

  - *Processor: Intel(R) Xeon(R) W-2235 3.8Ghz*

  - *Grafikkarte: NVIDIA Quadro RTX 4000*

  - *Arbeitsspeicher: 32GB*

  - *SDD: 2TB*

  - *HDD: 2TB*

- *Software:*

  - *Meta Quest Link*
  - *Gegebenenfalls Unity, wenn man in die Projektdatei möchte (mehr dazu unter "Aufsetzen der Entwicklungsumgebung"*

## Benutzung
*Vor dem Start der .exe musst du sicherstellen, dass die Oculus angeschlossen und die Oculus-App gestartet ist, damit die Brille und die Controller erkannt werden.*

- *Steuerung:* 
  - *Linkter Stick: Bewegung*
  - *Rechter Stick: Drehen*
  - *Griff Tasten: Greifen der Komponenten*
  - *Trigger Tasten: Auswahl bzw. Buttons betätigen*
  - *Menü Button: Rückehr ins Hauptmenü*
  
*Insgesamt gibt es drei Modi: Tutorial, Anfänger und Experte.*

*Das Ziel des Spiels ist es, die Rezepte im Anfängermodus zu verinnerlichen und sie im Expertenmodus ohne Hilfe umsetzen zu können.*

## Contribution / Entwicklung

### Aufsetzen der Entwicklungsumgebung
*Die verwendete Unity-Version lautet "Unity (2022.3.45f1)".*

*Verwendete Unity-Packages:*
*Burst*  
*Core RP Library*  
*Custom NUnit*  
*Input System* 
*JetBrains Rider Editor*  
*Mathematics*  
*Oculus XR Plugin*  
*OpenXR Plugin*  
*Post Processing*  
*ProBuilder*  
*Searcher*  
*Settings Manager*  
*Shader Graph*  
*Test Framework* 
*TextMeshPro*  
*Timeline*  
*Unity UI*  
*Universal RP*  
*Universal RP Config*  
*Version Control*  
*Visual Scripting*  
*Visual Studio Code Editor*  
*Visual Studio Editor*  
*XR Core Utilities*  
*XR Interaction Toolkit*  
*XR Legacy Input Helpers*  
*XR Plugin Management*

### Dokumentation der Software

*Da wir eine Vielzahl an Skripten haben, habe wir nur die wichtigsten als UML-Klassendiagramm umgesetzt*

![Klassendiagram](PNG%20für%20README.md/Klassendiagramm.png)




### Ordnerstruktur
- **Assets/** → Enthält alle projektbezogenen Ressourcen und Assets.
  - **Notwendige Pakete/** → Bwinhaltet Unity-Pakete wie z.B. XR.
  - **Projekt/** → Hauptordner für projektspezifische Einstellungen und Daten.
    - **Affordance Theme/** → Visuelle Feedback-Elemente für interaktive Objekte.
    - **Farben/** → Farbpaletten und Farbschemata.
    - **Images/** → Texturen und Bilder (z. B. für Tablet-UI).
    - **Menu Assets/** → Skripte, Fonts und Audio für Menü Szene.
    - **NPC/** → Animationen des Getränke-testenden NPCs.
    - **Prefabs/** → Wiederverwendbare Objekte und Komponenten.
      - **Materials für Prefabs/** → Materialien für Prefab-Objekte (z. B. Glas, Metall).
      - **Prefab für Tablet/** → Spezielles Prefab für Tablet-Objekte.
      - **3D Modelle Asset/** → 3D-Modelle, die im Projekt verwendet werden.
      - **Checkliste/** → UI-Prefabs für die interaktive Aufgabenliste.
      - **Deko/** → Dekorative Elemente.
      - **Deko für Gläser/** → Dekor für Glasobjekte.
      - **Flaschen/** → Vordefinierte Flaschen-Prefabs mit Flüssigkeitslogik.
      - **Flüssigkeit/** → Effekte für Tropfen und Flüssigkeitsbewegung.
      - **Gläser/** → Modelle oder Assets von Gläsern.
    - **Shader Flüssigkeit/** → Custom Shader für realistische Flüssigkeitssimulation in Gläsern.
    - **Skripte/** → Alle Skripte und Code-Dateien.
    - **Sounds/** → Audio-Dateien, Musik und Soundeffekte.
    - **Tablet Asset/** → Externes 3D-Tablet-Modell (aus dem Asset Store).
  - **Scenes/** → Beinhaltet alle Spiel-Level und Menü-Szenen.
    - **Menu** → Hauptmenü.
    - **Tutorial** → Lernmodus.
    - **Anfänger** → Modus mit Anleitung.
    - **Experte** → Modus ohne Hilfe.
    - **Basic/Tutorial** → Entwicklertests.
  - **Umgebung/** → 3D-Modelle und Texturen für die Barumgebung (z.B. Tische, Theke, Wände etc.).
  - **Unity, VisualScripting, Generated/** → Automatisch generierte Dateien (Unity, Visual Scripting etc.).
  - **XR/** → Assets für erweiterte Realität (AR/VR) und XR-Anwendungen.
- **Packages/** → Alle Pakete, die für die Ausführung des Projekts relevant sind.

## Eigenleistung

Eigene Werke (von Phillip Trompetter und Luca-Leon Krause)
  | Kategorie         |     Name           |     
  |-------------------|--------------------|
  | **Eigene 3D-Modelle** |                |                     
  |                  |      3D Modelle Asset (Ordner)|                      
  |                  |      Deko (Ordner)            |    
  |                  |  Deko für Gläser (Ordner)     | 
  |                  |    Fluessigkeit (Ordner)      |  
  |                  |    Gläser (Ordner)            | 
  |                  |  SalzPfeffer (Ordner)         |
  |                  |                               | 
  | **Skripte**      |                               | 
  |                  |  BarlöffelAudio.cs            |
  |                  | Behälter.cs                   |
  |                  |  DeckelSocketInteraction.cs   |
  |                  |  DrinkDisplayUI.cs            |
  |                  |  DrinkRecepies.cs             |
  |                  |  DrinkRecipeModel.cs          |
  |                  |  FillAnimation.cs             |
  |                  |  Fillspices.cs                |
  |                  |  FillstandHUD.cs              |
  |                  |  FillstandHUDJigger.cs        |
  |                  |  FillistundHUDShaker.cs       |
  |                  |  HoverTextController.cs       |
  |                  |  HUDFollowCamara.cs           |
  |                  |  Jigger.cs                    |
  |                  | JiggerAttachPoints.cs         |
  |                  |LeaveScene.cs                  |
  |                  |  Mülleimer.cs                 |
  |                  |  ObjektAnvisieren.cs          |
  |                  |  RespawnObject.cs             |
  |                  |  RespawnObjectForMülleimer.cs |
  |                  |  SelectDrink.cs               |
  |                  |  Shaker.cs                    |
  |                  |  Spülbecken.cs                |
  |                  |  StößelAudio.cs               |
  |                  |  TabletNavigation.cs          |
  |                  |  TestDetecter.cs              |
  |                  |  TestDetecterExpert.cs        |
  |                  |  Tutorial Sprechblasen.cs     |  
  |                  | Zuckerl.öffel.cs              | 
  |                  | MenuManager.cs                |
  |                  |                               |
  | **Bilder**       |                               |                
  |                  |Barlöffel                      |                
  |                  | Shaker                        |               
  |                  | Jigger                        |
  |                  |  Stößel                       |
  |                  |  Teelöffel                    |
  |                  | Tumbler Glas                  |
  |                  |Longdrink Glas                 |
  |                  | Hurricane Glas                |
  |                  | Coupette Glas                 |
  |                  | Cocktail Glas                 | 
  
  
Fremde Werke
  | Kategorie                       |      Name          | Autor          | Link |
  |---------------------------------|--------------------|----------------|------|
  | **Animationen**                 |                    | Keine Autoren Bekannt | Alle Animationen sind von https://www.mixamo.com/#/. Einzelne Links nicht möglich|  
  |                                 | Drinking           |                |      |
  |                                 | standing Idel      |                |      |
  |                                 | daumen hoch, sitzen|                |      |
  |                                 | letzte talk        |                |      |
  |                                 | nod                |                |      |
  |                                 | Normal Sitzen      |                |      |
  |                                 | Pointing           |                |      |
  |                                 | SCHACH             |                |      |
  |                                 | sit                |                |      |
  |                                 | Sitzen hand bewegung    |           |      |
  |                                 | Sitzen und Arm Kratzen  |           |      |
  |                                 | Sitzen und reden        |           |      |
  |                                 | stand talk              |           |      |
  |                                 | talking            |                |      |
  |                                 |                    |                |      |
  | **Bilder**                      |                    |                |      |
  |                                 | Space UI pack      | 	Deketele Creations   | https://dante-deketele.itch.io/simple-space-ui-pack|
  |                                 | Freie notebook Mockup Vektor| freepick  |https://de.freepik.com/vektoren-kostenlos/freie-notebook-mockup-vektor_726387.htm|
  |                                 | Koko Caramel       |      ---       |https://uigradients.com/#NeonLife|
  |                                 |chat bubbles, to chat with, chat bubbles png |    ---    |https://www.pngegg.com/en/png-ddmoy |
  |                                 |Vodka-E             |spirituosenworld|https://spirituosenworld.de/cocktails/vodka-e?srsltid=AfmBOoraustO-ehZ3amfooxdJafK3KScgGFk14qS_jG6URC7A68_yxqR|
  |                                 |Long Island Iced Tea|      ---       |https://www.bargpt.app/ai-cocktail-recipe/long-island-iced-tea7506 |
  |                                 |Manhattan Cocktail  |Slawomir Fajer  |https://www.essen-und-trinken.de/rezepte/59761-rzpt-manhattan-cocktail|
  |                                 |Negroni             |Evgeny Karandae |https://www.essen-und-trinken.de/rezepte/59764-rzpt-negroni|
  |                                 |Cola Korn           |     ---        |https://www.echter-nordhaeuser.de/cocktails-co/longdrinks/korn-doppelkorn/korn-cola/|
  |                                 |Cola Rum            |     ---        |https://www.schweppes.de/mixen/cocktails/rum-cola|
  |                                 |Whiskey Cola        |Lukas           |https://www.maltwhisky.de/whiskey-cola/|
  |                                 |Wodka Sou           |     ---        |https://www.schweppes.de/mixen/cocktails/wodka-sour|
  |                                 |Daiquiri            |                |      |
  |                                 |                    |                |      |
  | **Sounds**                      |                    |                |      |
  |                                 | glug glug glug     | IwanPlays      |https://pixabay.com/de/sound-effects/glug-glug-glug-39140/ |
  |                                 | martini_shake_pour | philberts      |https://pixabay.com/de/sound-effects/martini-shake-pour-34765/|
  |                                 | Stirring a cup of coffee | greatnessdon |https://pixabay.com/de/sound-effects/stirring-a-cup-of-coffee-193831/|
  |                                 | Squeezing Lemon    | PhilllChabbb   |https://pixabay.com/de/sound-effects/squeezing-lemon-72629/|
  |                                 | Drink pour         | mfearnley      |https://pixabay.com/de/sound-effects/drink-pour-7169/|
  |                                 | Water Pour and Drink     | joeboarder14    |https://pixabay.com/de/sound-effects/water-pour-and-drink-73447/|
  |                                 | Podcast Jazz Waltz Cozy Relaxing Vibes  | Denis-Pavlov-Music |https://pixabay.com/music/traditional-jazz-podcast-jazz-waltz-cozy-relaxing-vibes-233733/|
  |                                 |Daiquiri            | ---            |https://www.bacardi.com/de/de/rum-cocktails/daiquiri/|
  |                                 |Mojito              | Gutekueche     |https://www.gutekueche.at/mojito-cocktail-rezept-17926|
  |                                 |Caipirinha          | Gutekueche     |https://www.gutekueche.at/caipirinha-cocktail-rezept-17919|
  |                                 |Sex on the Beach    | Gutekueche     |https://www.gutekueche.at/sex-on-the-beach-cocktail-rezept-18119|
  |                                 |Bloody Mary         | Gutekueche     |https://www.gutekueche.at/sex-on-the-beach-cocktail-rezept-18119|
  |                                 |                    |                |      |
  | **3D-Modelle und Materialien**  |                    |                |      |
  |                                 |VR-Game-Jam-Template|Valem Tutorials |https://github.com/ValemVR/VR-Game-Jam-Template|
  |                                 |Stylized NPC - Peasant Nolant (DEMO) |WINGED BOOTS STUDIO|https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/stylized-npc-peasant-nolant-demo-252440|
  |                                 |Real Stars Skybox Lite|GEOFF DALLIMORE|https://assetstore.unity.com/packages/3d/environments/sci-fi/real-stars-skybox-lite-116333|
  |                                 |Low Poly Chess Pack |BROKEN VECTOR   |https://assetstore.unity.com/packages/3d/props/low-poly-chess-pack-50405|
  |                                 |Low-Poly Simple Nature Pack|JUSTCREATE|https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153|
  |                                 |3D Low Poly Tools, Weapons & Containers|NEXUSARCADESTUDIO|https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153|
  |                                 |WasteOvergrowth - Comprehensive Trash and Waste Pack|SENTE-ASSEMBLY|https://assetstore.unity.com/packages/3d/environments/wasteovergrowth-comprehensive-trash-and-waste-pack-290387|
  |                                 |Gold Coins          |DEVTOID         |https://assetstore.unity.com/packages/3d/props/gold-coins-1810|
  |                                 |Cabin Environment   |GREGORY SEGURU  |https://assetstore.unity.com/packages/3d/environments/cabin-environment-98014|
  |                                 |Free Wood Door Pack |BIOSTART        |https://assetstore.unity.com/packages/3d/props/interior/free-wood-door-pack-280509|
  |                                 |PBR Materials Sampler Pack|INTEGRITY SPFTWARE & GAMES|https://assetstore.unity.com/packages/2d/textures-materials/pbr-materials-sampler-pack-40112|
  |                                 |World Materials Free|AVIONX          |https://assetstore.unity.com/packages/2d/textures-materials/world-materials-free-150182|
  |                                 |Kitchen Furniture Starterpack|MAVI3D |https://assetstore.unity.com/packages/3d/props/furniture/kitchen-furniture-starterpack-209331|
  |                                 |Bar / Interior - Functional Low Poly assets|FRIES and SEAGULL|https://assetstore.unity.com/packages/3d/props/interior/bar-interior-functional-low-poly-assets-306976|
  |                                 |Bar Props           |SIMPLEMODELSFORME|https://assetstore.unity.com/packages/3d/props/barprops-137130|
  |                                 |Stylized Wood Textures|CAMISADO STUDIOS|https://assetstore.unity.com/packages/2d/textures-materials/wood/stylized-wood-textures-213607|
  |                                 |                    |                |      |
  | **Skripte**                     |                    |                |      |
  |                                 |AudioManager.cs     |Valem Tutorials |https://www.youtube.com/watch?v=apnfGuMI0Dc&list=PLuIdYvsj2nVOyXBFzNaDLoxbA6ecDBr38&index=24|
  |                                 |PlayAudioFromAudioManager.cs | Valem Tutorials |https://www.youtube.com/watch?v=apnfGuMI0Dc&list=PLuIdYvsj2nVOyXBFzNaDLoxbA6ecDBr38&index=24|
  |                                 |UIAudio.cs          | Valem Tutorials|https://www.youtube.com/watch?v=apnfGuMI0Dc&list=PLuIdYvsj2nVOyXBFzNaDLoxbA6ecDBr38&index=24|
  |                                 |Wobble.cs           |Binary Lunar    |https://www.youtube.com/watch?v=eIZgPAZx56s|
  |                                 |                    |                |      |
  |  **Shader-Graph**               |                    |                |      |
  |                                 |LiquidWobbleShader  |Binary Lunar    |https://www.youtube.com/watch?v=eIZgPAZx56s|
  |                                 |                    |                |      |

