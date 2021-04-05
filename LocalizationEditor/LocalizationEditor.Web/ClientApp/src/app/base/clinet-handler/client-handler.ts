export interface IClientHandler<TSource> {
  handle(entity: TSource);
  delete(id: number);
}
