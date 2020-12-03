Esemény vezérelt alkalmazások 3. feladat

**K ![](RackMultipart20201118-4-11029pc_html_685a88dde4041b51.png) észítette:**

Fikó Róbert

Neptun kód G55OFZ

Csoport: 15-ös csoport

E-mail: [g55ofz@inf.elte.hu](mailto:g55ofz@inf.elte.hu)

Személyes e-mail: [fiko.robert+elte@gmail.com](mailto:fiko.robert+elte@gmail.com)

**Feladat leírása**

Készítsünk programot, amellyel a következő játékot játszhatjuk. Adott egy 𝑛 × 𝑛 mezőből álló játékpálya, amelyben egy elszabadult robot bolyong, és a feladatunk az, hogy betereljük a pálya közepén található mágnes alá, és így elkapjuk. A robot véletlenszerű pozícióban kezd, és adott időközönként lép egy mezőt (vízszintesen, vagy függőlegesen) úgy, hogy általában folyamatosan előre halad egészen addig, amíg falba nem ütközik. Ekkor véletlenszerűen választ egy új irányt, és arra halad tovább. Időnként még jobban megkergül, és akkor is irányt vált, amikor nem ütközik falba. A játékos a robot terelését úgy hajthatja végre, hogy egy mezőt kiválasztva falat emelhet rá. A felhúzott falak azonban nem túl strapabíróak. Ha a robot ütközik a fallal, akkor az utána eldől. A ledőlt falakat már nem lehet újra felhúzni, ott a robot később akadály nélkül áthaladhat. A program biztosítson lehetőséget új játék kezdésére a pályaméret megadásával (7 × 7, 11 × 11, 15 × 15), valamint játék szüneteltetésére (ekkor nem telik az idő, nem lép a robot, és nem lehet mezőt se kiválasztani). Ismerje fel, ha vége a játéknak, és jelenítse meg, hogy milyen idővel győzött a játékos. A program játék közben folyamatosan jelezze ki a játékidőt. Ezen felül szüneteltetés alatt legyen lehetőség a játék elmentésére, valamint betöltésére.

**Elemzés**

- A játékot három pályamérettel játszhatjuk: 7x7, 11x11, 15x15, a célunk minden esetben ugyanaz: a robotot becsalni a mágnes alá.
- A feladatot egyablakos asztali alkalmazásként Windows Forms grafikus felülettel valósítom meg.
- Az ablak felső sorában elhelyezésre kerül egy menü a következő menüvel:
  - New game
    - Size: 7 x 7 (Méret)
    - Size: 11 x 11 (Méret)
    - Size: 15 x 15 (Méret)
  - Game menu
    - Pause (Megállít)
    - Play (Lejátszás)
    - Save (Mentés)
    - Load (Betöltés)
- Az ablak alján egy státusz sort jelenítünk meg:
  - Kezdő képernyő esetén közli, hogy nincsen éppen futó játék
  - Játék alatt kijelzi az eltelt időt
  - Megállított játék alatt közli a megállítás tényét
- A táblát tulajdonképpen egy nyomógomb ráccsal valósítjuk meg (7,11 vagy 15-ös mérettel). A nyomógombra kattintáskor, az általa reprezentált mezőre fal kerül. A mágnesre és a robotra való kattintás esetén semmi nem történik. (és ez rendben is van)
- A játék képes érzékelni mikor vége a játéknak, ekkor feldob egy dialógus ablakot, hogy nyertünk, s mennyi idővel.
- Mikor betöltünk vagy mentünk egy játékot a program ennek sikerességét, vagy sikertelenség esetén a hiba okát dialógusban jelzi.
- A felhasználói eseteket az ábra bemutatja:

![](RackMultipart20201118-4-11029pc_html_baa01bcc9f60566c.png)

**Tervezés**

**Programszerkezet**

A programot háromrétegű architektúrában valósítjuk meg. A megjelenés a CrazyBot.View, a modell a CrazyBot.Model, míg a perzisztencia a CrazyBot.Persistence névtérben helyezkedik el. Az alábbi ábra illusztrálja ![](RackMultipart20201118-4-11029pc_html_ce3a06306fff5f66.png)

**Perzisztencia**

- Az adatkezelés feladata a játéktáblával kapcsolatos információk tárolása, illetve a betöltés és a mentés megvalósítása, biztosítása
- A CrazyBotInfo osztály minden információt tartalmaz, amivel meglehet konstruálni egy játékmodellt, majd egy nézetet
- A játék információs osztályon a módosítások elvégzésnem a játékmodell osztály áll rendelkezésünkre.
- A játék kimentését (így a hosszabb távon való eltárolását az ICrazyBotDataModel interfész valósítja meg.
- Az interfészt szöveges (\*.crazy) fájlok olvasását és mentését a CrazyBotDataAccess osztály valósítja meg, az ezen folyamatok közben fellépő hibákat IllegalOperationException-nel jelezzük
  - Ezen fájlokban tárolt adatokat itt a mintának megfelelően kell formáznunk (ábrát lásd a következő oldalon)

![](RackMultipart20201118-4-11029pc_html_3ae1e320a3640565.gif) Az első sor megadja a tábla méretét, majd következik a robot pozíciója, amit a robot iránya követ.

Majd a mező típusa, amina robot áll. Ezek után megadjuk az eltelt időt, majd a véletlenszerű irányváltozástatásig hátralévő időt.

Végül a táblát leíró mátrix következik transzponálva.

![](RackMultipart20201118-4-11029pc_html_cbbe04436ab0389d.png)

A fejlesztés során, úgynevezett enum-okat, felsorolókat vezettem be, hogy a kód olvashatóbb legyen, de a kimentés során ezen felsorolási értékeket az álltaluk reprezentált egész szám voltában iratjuk ki.

**Modell**

- A modell egy igen jelentős részét a CrazyBotModel osztály valósítja meg, amely reagál a játéktáblán történt eseményekre. Ezen kívül szabályozza a játék egyéb paramétereit, mint az eltelt időt (time). Az osztály lehetőséget ad új játék kezdésére a newGame metódusa segítségével, valamint fal letételére (invertWall). A időléptetése az AdvanceTime metódussal történik, aminek függvényébena robot is lépni fog.
- A modell a nézet felé tudja jelezni, hogy szükséges a _teljes_ tábla frissítése (refreshBoard), ezt megtudja tenni csupán egy mezővel, ami amennyiben csak néhány mező változott, egy hatékonabb kivitel, ezt a refreshField eseményen keresztül tudja megtenni. Ehhet az eseményhez tartozik egy saját event argumentum is, a fieldRefreshEventArgs, ami tartalmaz egy Position-t ami pedig információval látja el a nézetet, hogy melyik mezőt is kellene újrarajzolni
- Amikor a modell newGame metódusát meghívjuk paraméterül átadhatunk neki, egy gameInfo osztályt (ami persze opcionális), ebben az esetben a játékot a paraméterül megadott játék álással fogja inicializálni.
- A játék időbeli kezelését egy időzítő végzi, amelyet mindig aktiválunk játék során, illetve inaktiválunk.

**Nézet**

- A nézetet a CrazyBotView osztály biztosítja, ami tárolja a model osztályát.
- Az adatelérést a modelen keresztül tudjuk elérni
- A játéktáblát egy dinamikusan megvalósított nyomógomb rács reprezentálja. A felület tetején menüsor helyezkedik el, ami megegyezik a tervezésben leírtakkal, az alján pedig egy státuszsor, ami szintén hasonló paraméterezéssel rendelkezik.

A program szerkezetének jobb megértéséhez tekintsük át az alábbi ábrát:
# 1

![](RackMultipart20201118-4-11029pc_html_eb199a32eac3fb37.png)

Tekintsük az osztályok részletesebb leírását

![](RackMultipart20201118-4-11029pc_html_fe5eb30049d39ddf.png) ![](RackMultipart20201118-4-11029pc_html_f82aa4436bc9784a.png)

A nézet UML ábrája:

![](RackMultipart20201118-4-11029pc_html_dfc410fab148c4d6.png)

A nézet a megfelelő enumerációs értékekhez az alábbi textúrákat rendeli hozzá:

| FieldType | NO\_WALL
 0 | WALL
 1 | CANNOT\_WALL
 2 | ROBOT
 3 | MAGNET
 4 |
| --- | --- | --- | --- | --- | --- |
| Texture name | noWallTexture | WallTexture | cannotWallTexture | robotTexture | magnetTexture |
| Texture | ![](RackMultipart20201118-4-11029pc_html_3e7ae9216091c539.png)
 | ![](RackMultipart20201118-4-11029pc_html_c79de80ae864aa7c.png)
 | ![](RackMultipart20201118-4-11029pc_html_eb8b499a54ff42a8.png)
 | ![](RackMultipart20201118-4-11029pc_html_5d1020fb6aff1840.png)
 | ![](RackMultipart20201118-4-11029pc_html_1241e406d4e55900.png)
 |

_Megjegyzés: a robotot a szoftver maszkolja rá a CANNOT\_WALL és a NO\_WALL textúrákra. A mágnes esetében ugyan ez a helyzet, bár a mágnes nem lehet már lerombolt (CANNOT\_WALL) helyen, mivel a mágnesre nem tehetünk falat, ahogy a robotra sem, viszont a robot áthaladhat lerombolt falon._

**Tesztelés**

A tesztelés a CrazyBotTest osztály segítségével, MS Test rendszer Unit, azaz egység tesztei segítségével lett megvalósítva, melyet az alábbi táblázat összegez:

ConstructorCheck

Ellnőrzi, hogy a játék megfelelően áll e fel: megfelelő mérettel, megfelelő információs (tábla) osztállyal jön e létre a model, s az idő valóban telik.

MoveRobot

A robot mozgását teszteli mind a négy lehetséges irányba, az időzítő tick-elését szimulálva.

PlaceWalls

Teszteli a falak letételét helyes (NO\_WALL) helyre, és helytelen (CANNOT\_WALL, MAGNET, ROBOT), illetve már letett falak felvételének megpróbálása.

HitAndPlaceWall

Falak letétele a pályára és robot nekivezetése annak, mind a négy irányból, a fal ledőlésének ellenőrzése, és a robot irányváltoztatásának ellenőrzése

HitEdge

A robot a pálya szélének vezetése, és visszapattanás ellenőrzése.

WalkOnCannotWall

Annak ellenőrzése, hogy a robot valóban áttud-e menni korábban már ledöntött falakon.

RobotGotMagnet

A robot mágnes pozíciójába vezetése, ezálltal a játék végének kiváltása.

RobotRandomChange

A robot véletlen időközönként irányt vált. Ennek ellenőrzésére szolgál ez a teszt eset.

[1](#sdfootnote1anc) Az ábrán \*-al jelölt osztályt (CrazyBotModel) még használ egy saját EventArgs osztályt. Ezt a könnyebb olvashatóság kedvéért erről az ábráról kihagytam. Egy másik, az osztályt részletesebben leíró ábrán ott van.

Fikó Róbert (G55OFZ) 6. oldal
