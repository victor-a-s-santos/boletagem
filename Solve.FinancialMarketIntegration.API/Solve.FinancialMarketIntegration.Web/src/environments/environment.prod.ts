import { IEnvironment } from './environment.base';

export const environment: IEnvironment = {
  name: 'production',
  production: true,
  apiUrls: {
    tickets: 'http://localhost:5000/api/v1/tickets',
    security: 'http://localhost:5000/api/v1/security',
    users: 'http://localhost:5000/api/v1/user',
    files: 'http://localhost:5000/api/v1/files',
    workflow: 'http://localhost:5000/api/v1/wk'
  }
};
