# TFG Juego 1 - Mars Miner

Mars Miner es un juego desarrollado en Unity como Trabajo de Fin de Grado por Mario Torbado de la Rosa para el grupo GREIDI de la Universidad de Valladolid.
El juego trata de ser una herramienta para la enseñanza de los alumnos de primer curso de Grado en Ingeniería Informática para la comprensión de estructuras
de código iterativas y condicionales. Para un análisis en detalle del juego, consulte el apartado correspondiente del TFG. //completar

--------------------------------------------------------------------------------------------------------------------------------------------------------------

## Pre-requisitos

Para poder abrir y editar el proyecto, necesitaremos tener instalada una versión compatible del editor de [Unity](https://unity3d.com/es/get-unity/download). Aunque versiones posteriores pueden ser compatibles, el proyecto ha sido desarrollado utilizando la versión **2018.4.16f1**, por lo que se recomienta encarecidamente utilizar dicha versión en caso de querer realizar cualquier edición del mismo, con tal de evitar problemas.

Para leer o modificar los archivos de código (.cs) se recomienda usar un entorno compatible con el lenguaje C# y extensiones para la detección del lenguaje y
funciones de Unity. Durante el desarrollo se ha usado el editor Visual Studio Code junto a las siguientes extensiones:

- [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [Unity Code Snippets](https://marketplace.visualstudio.com/items?itemName=kleber-swf.unity-code-snippets)

--------------------------------------------------------------------------------------------------------------------------------------------------------------

## Edición de Niveles

Mars Miner ha sido diseñado de tal modo que los niveles o pantallas de juego que lo componen puedan modificarse o añadir otros nuevos, sin necesidad de
tener que trabajar dentro del editor de Unity ni conocer todo el código del proyecto. Para ello debemos generar 3 archivos por cada nivel y colocarlos en
la ruta adecuada dentro del proyecto:

- Archivo de texto que muestra el código del robot en la pantalla de juego, en _Assets/Level Files/Resouces/Level Text_.
- Script C# que controla el movimiento del robot, en _Assets/Level Files/Resouces/Level Scripts/\<Dificultad\>_.
- Tabla CSV con la información del tablero de juego, en _Assets/Level Files/Resouces/Level Tables_.

Cada archivo de nivel debe tener como nombre _\<Dificultad\>X.\<Extensión\>_ Donde _Dificultad_ expresa la dificultad a la que pertenece el nivel
(_Easy_, _Medium_, _Hard_ o _Challenge_), _X_ es el número del nivel, y _Extensión_ es la extensión correspondiente a cada tipo de archivo de los 3
mencionados previamente (_.html_ para el archivo de texto, _.cs_ para el script y _.csv_ para la tabla).

Es importante que la enumeración de los niveles sea contínua, es decir, no podemos saltarnos ningún número natural, ya que de no ser así causaría un error durante la ejecución del juego.


A continuación se detalla cómo generar los diferentes archivos de nivel correctamente:

### Texto del código:
El texto de código debe reflejar en sintaxis Java, las acciones que realizará el robot al comenzar el nivel. Es el texto que se muestra a la derecha en la
pantalla de juego.
Para añadir color o formato a dicho texto, pueden utilizarse una serie de etiquetas (ver [TextMesh Pro Rich Text](http://digitalnativestudios.com/textmeshpro/docs/rich-text/)). Es recomendable que consultes los archivos de los niveles existentes para saber cómo se les ha dado formato.


### Script C#:
El Script contiene la secuencia de movimientos del robot que se ejecutará en el nivel, Para generar nuevos scripts de nivel debemos seguir las siguientes pautas:

- Se debe importar (_using_) la clase _System.Collections_.
- Debe haber una definición _public override void Initialize() {...}_, donde se debe indicar el número de minerales que el robot debe recoger para superar el nivel (_oreGoal_) y se debe obtener una referencia al componente _RobotActions_.
- Debe haber una definición _public override IEnumerator Play(string[] args) {..}_, que contiene la secuencia de acciones del robot.
- Entre cada secuencia de movimiento del robot que mueva a este de casilla (_robotActions.MoveFoward()_ o _robotAcions.MoveBackward()_) se debe hacer una comprobación de si el robot colisiona con una roca (_CheckLevelFailed()_), y retornar la animación _robotActions.BreakAnimation()_ en tal caso.
- Al final de la secuencia de código, deberá llamarse al método _CheckLevelPassed()_, excluyendo la condición _CheckLevelFailed()_ de tal forma que no se puedan dar de forma simultánea ambas condiciones.

Es recomendable que consultes los scripts de los niveles existentes para comprender mejor la estructura de código a seguir. Para ver todas las acciones disponibles para el robot, consulta los métodos debajo de "_=== COROUTINES ===_" del script _RobotActions.cs_ en _/Assets/Scripts/Game System/Robot_.


### Tablero CSV:
Un archivo .csv (_comma-separated-values_) con forma de matriz 20x20, donde cada posición representa una casilla del tablero de juego.
La esquina superior izquierda de la matriz corresponde con la esquina inferior de la pantalla de juego. Los valores posibles por cada casilla son los siguientes:

- 0 - casilla vacía.
- 1 - roca movible (plateada).
- 2 - roca inmóvil (marrón).
- u - robot orientado hacia arriba (up).
- d - robot orientado hacia abajo (down).
- r - robot orientado hacia la derecha (right).
- l - robot orientado hacia la izquierda (left).

--------------------------------------------------------------------------------------------------------------------------------------------------------------

## Modificación del cálculo de puntuaciones

El cálculo para la puntuación de cada nivel está preestablecido en el código del proyecto, mediante una ecuación y una serie de parámetros fijos. Sin embargo, debido a la imposibilidad de que el proyecto sea probado por su usuario final (alumnos) antes de su entrega, aquí se incluye una guía sobre cómo modificar dicha ecuación y parámetros, en caso de querer modificarlo en el futuro.

El script encargado de calcular las puntuaciones se puede encontrar en esta ruta: _/Assets/Scripts/Progress System/ScoreManager.cs_. Dentro del script, en la función _LevelPoints()_ encontraremos la ecuación que calcula la puntuación por cada nivel. Esta función utiliza los siguientes parámetros que podemos modificar:

- _SCORE\_FACTOR_: Es un factor de división con respecto a la puntuación máxima del nivel actual. La puntuación máxima para el nivel será la puntuación máxima de la dificultad dividida entre este valor (4 originalmente).
- _ATTEMP\_PENALTY_: Es la puntuación que se descontará por cada vez que se reintente el nivel (50 originalmente).
- _MIN\_LEVEL\_SCORE_ : Es la puntuación mínima que se obtendrá por nivel que se supere independientemente de los intentos y el tiempo que se tarde (100 originalmente).

Para ajustar la penalización de la puntuación por tiempo, debemos ir a _/Assets/Scripts/UI/TimerScript.cs_. En este script encontraremos los siguientes parámetros que podemos modificar:

- _MAX\_PENALTY_: Es la puntuación máxima que se puede llegar a descontar por nivel (200 originalmente).
- _PENALTY\_LAPSE_: Es el intervalo de tiempo, en segundos, entre cada resta de puntos (30 segundos originalmente).
- _PENALTY_: Son los puntos que se restarán a la puntuación del nivel cada vez que pase el tiempo marcado por _PENALTY\_LAPSE_.

Para ajustar la puntuación necesaria para desbloquear la siguiente dificultad, podemos hacerlo de nuevo en _ScoreManager.cs_, en el parámetro _PASS\_DIFICULTY\_SCORE_.

La puntuación necesaria para marcar el juego como superado en la plataforma se indica también en  _ScoreManager.cs_, en el parámetro _PASS\_GAME\_SCORE_.

Por último, en caso que queramos modificar la puntuación máxima por dificultad, estas están definidas en _/Assets/Scripts/Game System/LevelDificulty.cs_. 

--------------------------------------------------------------------------------------------------------------------------------------------------------------

## Construcción y ejecución del proyecto

Mars Miner está destinado a ser construido como un proyecto WebGL, para su exportación y ejecución desde la plataforma web Greidi de la UVa (https://greidi.infor.uva.es/programajugando/). Para construir una nueva versión del proyecto, debes seguir los siguientes pasos:

1. Abrir el proyecto en Unity.
2. Ir a _File_ -> _Build Setings..._
3. Seleccionar la escena _Main Scene_.
4. Seleccionar WebGL como _Platform_.
5. Pulsar el botón _Build_.

--------------------------------------------------------------------------------------------------------------------------------------------------------------

## Versionado

Para el versionado del proyecto se ha utilizado Git y Github como plataforma online para albergar el repositorio remoto.
El repositorio del proyecto puede encontrarse en https://github.com/mtorbado/tfg.

--------------------------------------------------------------------------------------------------------------------------------------------------------------

## Autores 

* **Mario Torbado de la Rosa** - *Desarrollo del proyecto Unity, modelado 3D y documentación* - [mtorbado](https://github.com/mtorbado)

--------------------------------------------------------------------------------------------------------------------------------------------------------------

## Colaboradores

* **Oliver L. Sanz San José** - *Música y sonido*

--------------------------------------------------------------------------------------------------------------------------------------------------------------

