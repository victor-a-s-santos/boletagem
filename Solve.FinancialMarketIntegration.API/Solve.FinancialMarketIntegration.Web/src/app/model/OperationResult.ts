export interface IResult {
  success: boolean;
  message: string;
}

export interface IDataResult<TData> {
  success: boolean;
  message: string;
  data: TData;
}
