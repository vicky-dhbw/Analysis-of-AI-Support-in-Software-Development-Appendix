public downloadBookmarks(links: Link[]): void {
    // Create HTTP headers
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/octet-stream'
    });

    // Make POST request to download bookmarks
    this._http.post(`${this._apiRoot}/Links/DownloadBookmarks`, links, { headers, responseType: 'blob' })
      .subscribe((response: Blob) => {
        // Create a blob file from the response
        const blob = new Blob([response], { type: 'application/octet-stream' });
        // Save the file as HTML
        FileSaver.saveAs(blob, 'bookmarks.html');
      });
  }