export interface IEnvironment {
  name: string;
  production: boolean;
  apiUrls: {
    tickets: string;
    security: string;
    users: string;
    files: string;
    workflow: string;
  };
}
