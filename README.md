PROYECTO 1 BASES DE DATOS 1 SECCION B 2024

OBJETIVO:
Este proyecto, tiene por finalidad que el estudiante ponga en práctica los conocimientos adquiridos durante el desarrollo
del curso, las investigaciones realizadas y demás fuente relacionada; y conforme a lo anterior analice, desarrolle y
entregue lo que a continuación se le pide:

DESCRIPCIÓN DEL PROYECTO:
La empresa “TRANSQL” tiene planificado crear el Sistema de Reservación de Vehículos Propios (los vehículos de la propia
organización). Después de aplicar todos los métodos para analizar la situación dada, ha concluido con la definición de la
siguiente oportunidad de mejora:

Reservación de Vehículos:
Se necesita crear un sistema de reservación de vehículos propios, en donde los colaboradores puedan visualizar el tracking
de los vehículos, es decir, llevar el seguimiento de estos para poder ver el estado actual de los mismos, por ejemplo, si
están en estado Disponible, Solicitado, En Ruta, Inactivo; que les servirá para ver cuando un vehículo está disponible y
poder realizar la reservación en caso de ser necesario. Tomar en cuenta que los vehículos son de únicamente de estos
tipos: pickups, automóviles y buses.

Para tal caso todos los colaboradores que tengan acceso al sistema podrán realizar la solicitud de reservación del vehículo
(cuando deseen realizar un viaje por capacitación, compra de insumos, repuestos, etc.), sin especificar vehículo alguno,
siempre y cuando existan vehículos cuyo estado sea DISPONIBLE. Al momento de procesar la solicitud, también deberá
enviar una notificación al proceso de Logística y Transporte sobre la solicitud de reservación para su entera satisfacción.
La asignación de los vehículos únicamente la hará el proceso de Logística y Transporte, encargados de la administración
de estos.

Hay que recordar que, en el departamento o proceso de Logística y Transporte, los encargados podrán revisar la solicitud
y tomar la decisión adecuada para Aprobar o rechazar la solicitud de reservación.

Si la solicitud de reservación es aprobada se deberán tomar las siguientes acciones:
1. Seleccionar el departamento o proceso solicitante
2. Asignar el vehículo o vehículos y demás datos
3. Notificar a la Garita de Control para que estén informados
4. Notificar al colaborador solicitante
   
Si la solicitud de reservación es rechazada también se deberán tomar ciertas acciones, tales como:

5. NO habrá asignación de vehículos
6. Justificar el rechazo de la solicitud
7. Notificar el rechazo al colaborador solicitante

Inspección de entrega y recepción de vehículos:

Además, se necesita automatizar el proceso de inspección de entrega y recepción de vehículos propios, en donde cada
inspección depende y debe estar relacionado con las solicitudes de vehículos aprobadas.

En el proceso de entrega e inspección de vehículos es necesario registrar el nombre del responsable de recepción del
vehículo, el valor del odómetro (inicial), responsable de entrega y algunas observaciones. Además, el estado de los
accesorios del vehículo, como por ejemplo retrovisores, radio, aire acondicionado, etc. (mal, medio o buen estado).

En el proceso de recepción de vehículos es necesario registrar el nombre del responsable de entrega del vehículo, el valor
del odómetro (final), responsable de recepción y algunas observaciones.

*Sera un plus si en la entrega y recepción de vehículos se logra incluir imágenes del estado del vehículo y firmas digitales.

El trabajo que deberán desarrollar como DBA’s, será lo siguiente:

● Crear el modelo E-R
● Implementar el Esquema E-R mediante:
  o Sentencias representadas por el DDL y DCL
  o Toda la parte que involucra DML, deberá ser implementado con Procedimientos almacenados, triggers
y/o cursores (Transact-SQL).
● Aplicar los conceptos de integridad de entidades y referenciales, dominios
● Aplicar los conceptos de normalización, transacción, propiedades ACID, etc.
● Validar que sus consultas sean optimizadas, creando índices adecuados

En síntesis, como Programadores y DBA’s, deberán preparar el escenario para atender los requerimientos de la capa
BACKEND y FRONTEND, aplicando la arquitectura Cliente – Servidor.

Consultas y Reportes
• Generar informes de los movimientos o viajes realizados por cada vehículo o todos los vehículos por rango de fechas
donde se visualice: numero de la solicitud, destino del vehículo, fecha y hora de entrega, responsable de recepción,
recorrido en la entrega, fecha y hora de recepción, responsable de entrega, recorrido en la recepción, total del viaje.
• Consulta de movimientos o viajes realizados por cada vehículo y por rango de fecha

FORMA DE PRESENTACIÓN:
La presentación del Proyecto es vital, por lo tanto, deberá de apegarse a lo siguiente:
● Entrega del Diagrama E-R
● Entrega de todo el código de TRANSACT-SQL (Esquema conceptual), según los requerimientos planteados
● Entrega del código fuente de la solución (Entity Framework, ADO.NET, Windows Forms, Blazor, React o Angular)
● Alojar todo el código de TRANSACT-SQL y código fuente de la solución, en algún repositorio en la nube (Drive,
GitHub, etc.)
● Presentar la solución en clase, tiempo de duración 30 minutos, además deberá crear un video de la presentación
con buena resolución y sonido, alojado en drive.
● Informe ejecutivo del proyecto
PENALIZACIONES:
● La detección de similitud de código TRANSACT-SQL entre proyectos, anulará los proyectos involucrados de forma
parcial o total
● De no detectar la aplicación de los temas mencionados, anulará la nota del proyecto

FECHA DE ENTREGA:
El proyecto deberá ser entregado el día 26 de octubre de 2024

VALORACIÓN:
El proyecto tiene un valor de 20 puntos netos de la nota del curso, sin embargo, esta nota se verá afectada si el proyecto
adolece de los aspectos requeridos.

OBSERVACIONES:
● Incluir el porcentaje de participación de los integrantes del grupo dentro de la documentación

INTEGRANTES POR GRUPO: 6

IMPORTANTE:
Para evitar contratiempo, todos los grupos deberán estar preparados al inicio de la clase, grupo que no esté preparado
tendrá una penalización, tiempo máximo de presentación 30 minutos
