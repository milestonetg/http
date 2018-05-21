# MilestoneTG.Http
Extensions for System.Net.Http.

## Common Types
* GZipCompressionDelegatingHandler
* DeflateCompressionDelegatingHandler

## Usage
```cs
var httpClient = new HttpClient(new GZipCompressionDelegatingHandler());
```
