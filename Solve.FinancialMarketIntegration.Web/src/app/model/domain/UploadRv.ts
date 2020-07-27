import { Entity } from './Entity';

export class UploadRv extends Entity {
  id = 0;

  constructor() {
    super();
  }

  fileBase64: string;
  accountManagerId: number;
}
