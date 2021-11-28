# TablasDeVerdad
Un juego para aprender a resolver tablas de verdad, el juego presenta distintos niveles en los que hay que conseguir acertar en los valores que cada celda de una tabla
de verdad deberia tener.

## Informacion tecnica
Desarollado en Unity utilizando C#, el juego puede leer los niveles desde un archivo de configuracion, es decir que se pueden agregar nuevos niveles con solo editar
el archivo de configuracion encontrado en StreamingAssets, con la siguiente estructura:
```
--
<variables>
<expresiones (si hay, si no, dejar como un salto de line vacio)>
<valores de verdad, columnas separadas por ';' y valores en fila separadas por ','>
```
Ejemplo:
```
--
p, q

v,v,f,f;v,f,v,f
```
como se observa, cada tabla debe empezar con '--' para facilitar la lectura humana.

Las tablas en el juego se ubican en el centro de la pantalla manera automatica.
