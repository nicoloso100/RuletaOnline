# RuletaOnline
Prueba técnica Masivian
Nicolás Angarita Ortiz

## Para ejecutar la API

Al clonar el repositorio la API podrá ser ejecutada mediante docker-compose.
Si desea ejecutarla en debug recuerde correr el contenedor de MongoDB.

También podrá ejecutar la API sin clonar el repositorio mediante el siguiente docker-compose: 

	version: "3.4"

	services:
	  mongo:
	    image: mongo
	    restart: always
	    environment:
	      MONGO_INITDB_ROOT_USERNAME: root
	      MONGO_INITDB_ROOT_PASSWORD: ruletaonlinepass
	    ports:
	      - 27017:27017

	  mongo-express:
	    image: mongo-express
	    restart: always
	    ports:
	      - 8081:8081
	    environment:
	      ME_CONFIG_MONGODB_ADMINUSERNAME: root
	      ME_CONFIG_MONGODB_ADMINPASSWORD: ruletaonlinepass
	    depends_on:
	      - mongo

	  ruletaonline:
	    image: nicoloso100/ruletaonline:tagname
	    restart: always
	    ports:
	      - 8080:80
	    environment:
	      MongoDB__Host: mongo
	    depends_on:
	      - mongo

Una vez iniciados los contenedores podrán acceder a los siguientes EndPoints:

	[POST] http://localhost:8080/Roulette/CreateRoulette
	[POST] http://localhost:8080/Roulette/EnableRoulette
	[POST] http://localhost:8080/Roulette/BetOnRoulette
	[POST] http://localhost:8080/Roulette/DisableRulette
	[GET] http://localhost:8080/Roulette/GetAllRoulettes

Los servicios que no requieren parámetros son:

 - CreateRoulette
 - GetAllRoulettes
 
Los servicios que requieren como parámetro únicamente el id de la ruleta (long rouletteId) son:
 - EnableRoulette
 - DisableRulette

Para el caso del servicio BetOnRoulette requiere la siguente estructura:

	long  RouletteId
	int  BetAmount
	int? BetNumber
	string  BetColor

## Flujo de trabajo en Git
![enter image description here](https://i.imgur.com/pR86a71.png)

Para el flujo de Git se utilizaron las ramas master, develop y features, manteniendo un flujo de trabajo basado en GitFlow.

La rama master está conectada al repositorio de Docker para poder actualizar el contenedor cuando se suba una nueva versión a master de la aplicación
![enter image description here](https://i.imgur.com/ycJGubc.png)

Cada feature tiene una unica responsabilidad y al culminar su desarrollo se incorpora con la rama develop; al terminar los requerimientos de la prueba técnica se incroporaron los cambios a máster desplegando la primera versión de la API.

## Diagrama de estructura del API

La estructura se toma el concepto del diseño de cebolla o Domain Driven Design; la estructura se divide en:

 - Capa de controlador
 - Capa de servicios
 - Capa de repositorios
 - Objetos y Enums
 - DTOs
 - Modelos

Se implementaron principios del Clean Code e inyección de dependencias.

![enter image description here](https://i.imgur.com/higdCDp.png)

La comuniación entre capas se reliza mediante DTOs u Objetos, para este caso los objetos hacen el papel del Dominio ya que tienen sus validaciones y reglas internas, además son objetos inmutables, mientras que los DTOs solo llevan información a las otras capas.

Cada capa se comunica mediante contratos con las interfaces, las cuales son inyectadas para poder comunicarse desde capas internas.

Finalmente los repositorios se comunican con la base de datos mediante los modelos.
