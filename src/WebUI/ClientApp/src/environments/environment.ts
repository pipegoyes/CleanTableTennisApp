// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
export const environment = {
  production: false,
  apiUrl: "https://localhost:44312",
  auth0Audience: "https://cleantt-identity-dev/api",
  auth0Domain: "dev-dydx45fd0tuoy8tt.us.auth0.com",
  auth0ClientId: "cUkx2OcPufVLok3s6MVZZH2ghWkHsW4c"
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
