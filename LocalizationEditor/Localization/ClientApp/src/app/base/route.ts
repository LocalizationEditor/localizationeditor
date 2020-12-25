export class CustomRoute {
  id: number;
  name: string;
  route: string;
  childRoutes?: CustomRoute[]

  constructor(id: number, name: string, route: string, childRoutes?: CustomRoute[]) {
    this.id = id;
    this.name = name;
    this.route = route;
    this.childRoutes = childRoutes;
  }
}
