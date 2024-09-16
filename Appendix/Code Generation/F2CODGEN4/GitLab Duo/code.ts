private _isConfigurationIsValid(configuration: ConfiguratorModel | null | undefined): boolean {
    // Check if configuration and its errors property exist
    if (!configuration?.errors) {
        return false; // If configuration or errors are undefined/null, consider it invalid
    }

    // Check if all errors are of type 'Information' or 'Warning'
    return configuration.errors.every((error) => error.type === 'Information' || error.type === 'Warning');
}