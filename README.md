# TFG Juego 1 - Mars Miner

Mars Miner es un juego desarrollado en Unity como Trabajo de Fin de Grado por Mario Torbado de la Rosa para el grupo GREIDI de la Universidad de Valladolid.
El juego trata de ser una herramienta para la enseñanza de los alumnos de primer curso de Grado en Ingeniería Informática para la comprensión de estructuras
de código iterativas y condicionales. Para un análisis en detalle del juego, consulte el apartado correspondiente del TFG. //completar


## Pre-requisitos

Para poder abrir y editar el proyecto, necesitaremos tener instalada una versión compatible del editor de [Unity](https://unity3d.com/es/get-unity/download). Aunque versiones posteriores pueden ser compatibles, el proyecto ha sido desarrollado utilizando la versión **2018.4.16f1**, por lo que se recomienta encarecidamente utilizar dicha versión en caso de querer realizar cualquier edición del mismo, con tal de evitar problemas.

Para leer o modificar los archivos de código (.cs) se recomienda usar un entorno compatible con el lenguaje C# y extensiones para la detección del lenguaje y
funciones de Unity. Durante el desarrollo se ha usado el editor Visual Studio Code junto a las siguientes extensiones:

- [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [C# XML Documentation Comments](https://marketplace.visualstudio.com/items?itemName=k--kato.docomment)
- [Unity Code Snippets](https://marketplace.visualstudio.com/items?itemName=kleber-swf.unity-code-snippets)


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



## Construcción y ejecución del proyecto

Mars Miner está destinado a ser construido como un proyecto WebGL, para su exportación y ejecución desde la plataforma web Greidi de la UVa (https://greidi.infor.uva.es/programajugando/). Para construir una nueva versión del proyecto, debes seguir los siguientes pasos:

1. Abrir el proyecto en Unity.
2. Ir a _File_ -> _Build Setings..._
3. Seleccionar las dos escenas existentes (Scenes/Start Menu y Scenes/Main Scene).
4. Seleccionar WebGL como _Platform_.
5. Pulsar el botón _Build_.


## Versionado

Para el versionado del proyecto se ha utilizado Git y Github como plataforma online para albergar el repositorio remoto.


## Autores 

* **Mario Torbado de la Rosa** - *Desarrollo del proyecto Unity, modelado 3D y documentación* - [mtorbado](https://github.com/mtorbado)


## Licencia

Este proyecto está bajo la Licencia "" - mira el archivo [LICENSE.md](LICENSE.md) para detalles

-----------------------------------------------------------------------------------------------------------------------------------------------------

