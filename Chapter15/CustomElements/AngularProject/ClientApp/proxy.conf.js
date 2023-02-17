const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:20486';

const PROXY_CONFIG = [
  {
    // <8. Add proxy for Blazor app>
    context: [
      "/weatherforecast",
      "/_framework",
      "/_content",
    ],
    // </8. Add proxy for Blazor app>
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
