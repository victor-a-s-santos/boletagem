export interface ITipo {
  id: any;
  name: string;
  description: string;
}

export class Tipo<T> implements ITipo {
  constructor() { }

  id: T = null;
  name: string = null;
  description: string = null;

  public static Criar<T>(id: T, nome: string, descricao: string = null) {
    const e = new Tipo<T>();
    e.id = id;
    e.name = nome;
    e.description = descricao;
    return e;
  }
}
