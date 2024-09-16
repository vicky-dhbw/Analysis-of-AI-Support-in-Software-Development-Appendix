/**
 * Retrieves links with pagination.
 * @param skip The number of links to skip.
 * @param take The number of links to take.
 * @returns An observable that emits an array of links.
 */
public getLinksWithPagination(skip: number, take: number): Observable<Link[]> {
    return this._http.get<Link[]>(`${this._apiRoot}/Links/getAllLinks?skip=${skip}&take=${take}`);
  }