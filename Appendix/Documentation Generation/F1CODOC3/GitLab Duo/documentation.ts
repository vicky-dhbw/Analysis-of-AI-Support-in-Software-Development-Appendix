/**
 * Retrieves a paginated list of links.
 * 
 * @param skip - The number of items to skip before starting to collect the result set.
 * @param take - The number of items to return in the result set.
 * @returns An Observable that emits an array of Link objects.
 * 
 * @example
 * // Get the first 10 links
 * this.getLinksWithPagination(0, 10).subscribe(
 *   links => console.log(links),
 *   error => console.error(error)
 * );
 */
public getLinksWithPagination(skip: number, take: number): Observable<Link[]> {
    return this._http.get<Link[]>(`${this._apiRoot}/Links/getAllLinks?skip=${skip}&take=${take}`);
  }