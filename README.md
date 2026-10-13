# Análisis y Corrección de Vulnerabilidades con OWASP ZAP

En este proyecto se realizó un análisis de seguridad utilizando **OWASP ZAP** sobre la API en .NET (`ApiEmpresa`).

## Archivos de Evidencia
- `Informe_ZAP_Antes.html`: Resultado del primer escaneo antes de aplicar correcciones.
- `Informe_ZAP_Despues.html`: Resultado del segundo escaneo tras implementar los middlewares de seguridad.

---

## Tabla Comparativa de Resultados

| Alerta encontrada | Riesgo | Corrección realizada | Resultado final |
| :--- | :--- | :--- | :--- |
| **Falta de cabecera Anti-Clickjacking** | Medio | Se agregó la cabecera `X-Frame-Options: DENY` en el middleware de .NET (`Program.cs`). | Corregida |
| **Cabecera Content Security Policy (CSP) no configurada** | Medio | Se implementó la cabecera `Content-Security-Policy` en el middleware de .NET. | Corregida |
| **Falta encabezado X-Content-Type-Options** | Bajo | Se agregó la cabecera `X-Content-Type-Options: nosniff` en `Program.cs`. | Corregida |
| **Advertencias de CSP (script-src / style-src unsafe-inline)** | Bajo / Informativo | Requeridas por la interfaz gráfica de Swagger UI para renderizar la documentación interactiva en entorno de desarrollo. | Mitigada / Documentada |
| **Librería JS Vulnerable** | Medio / Bajo | Corresponde a los archivos estáticos internos incluidos en el paquete de Swagger UI. No representa riesgo en producción ya que Swagger solo está activo en desarrollo. | Falso Positivo / Documentada |
| **Divulgación de Marcas de Tiempo - Unix** | Bajo | Metadatos de compilación de archivos estáticos de la interfaz Swagger. | Documentada |
| **Aplicación Web Moderna** | Informativo | Notificación informativa de ZAP al detectar la Single Page Application de Swagger. | Informativa |
