public downloadBookmarks(links: Link[]): void {
    // Create HttpHeaders object
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/octet-stream'
    });

    // Formulate the API endpoint URL
    const url = `${this._apiRoot}/Links/DownloadBookmarks`;

    // Make the POST request
    this._http.post(url, links, {
      headers: headers,
      responseType: 'blob'
    }).subscribe({
      next: (response: Blob) => {
        // Create a Blob from the response
        const blob = new Blob([response], { type: 'application/octet-stream' });

        // Save the file using FileSaver
        FileSaver.saveAs(blob, 'bookmarks.html');
      },
      error: (error) => {
        console.error('Error downloading bookmarks:', error);
        // Handle error (e.g., show user-friendly message)
      }
    });
  }