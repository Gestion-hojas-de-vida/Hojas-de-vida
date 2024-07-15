# Hojas-de-vida

Sistema de Gestión y Creación de Hojas de Vida

Especificaciòn del proyecto:
Desarrollar un sistema monolítico de gestión y creación de hojas de vida. Los usuarios
podrán registrarse, llenar su hoja de vida, generar un enlace para compartir su hoja de
vida, descargarla en formato PDF y generar informes en Excel, el sitio debe ser responsive.

Tecnologias:
ASP.Net, MVC, Bootstrap, MySql, C#.

Estrategia de Branch:
2 ramas principales (main y develop)
Se manejaran task-# dentro de la rama develop, cada task corresponde a una incidencia asignada en jira.

Tablero de Jira:
https://mini-drive.atlassian.net/jira/software/projects/GHDV/boards/2?atlOrigin=eyJpIjoiYmUzNGE3YzE3ODExNDg5YzhlNWJjNzY0YjI5MmQ5Y2IiLCJwIjoiaiJ9

Para cargar los iconos usados en el proyecto deberas instalar la libreria correspondiente.
Instalar Material Design Icons:
npm install @mdi/font

terminadores de línea automáticamente:
LF (Line Feed): Es un terminador de línea usado en sistemas Unix y Linux.
CRLF (Carriage Return + Line Feed): Es un terminador de línea usado en sistemas Windows.
Configuración Global: Puedes configurar Git para manejar los terminadores de línea automáticamente al hacer commit o al chequear los archivos. Usa el siguiente comando para configurar Git para convertir CRLF a LF en todos los archivos cuando hagas commit, y para convertir LF a CRLF cuando hagas checkout en Windows:
git config --global core.autocrlf true
