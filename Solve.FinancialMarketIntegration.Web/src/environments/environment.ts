// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

import { IEnvironment } from './environment.base';

export const environment: IEnvironment = {
  name: 'local',
  production: false,
  apiUrls: {
    tickets: 'http://localhost:5000/api/v1/tickets',
    security: 'http://localhost:5000/api/v1/security',
    users: 'http://localhost:5000/api/v1/user',
    files: 'http://localhost:5000/api/v1/files',
    workflow: 'http://localhost:5000/api/v1/wk'
  }
};
