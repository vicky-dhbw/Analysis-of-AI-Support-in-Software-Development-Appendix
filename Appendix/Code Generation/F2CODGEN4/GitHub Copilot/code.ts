private _isConfigurationIsValid(configuration: ConfiguratorModel): boolean {
    // Check if configuration or configuration.errors is null or undefined
    if (!configuration?.errors) {
        return true; // If there are no errors, consider the configuration valid
    }

    // Use the every method to check if all errors are of type 'Information' or 'Warning'
    return configuration.errors.every((error) => error.type === 'Information' || error.type === 'Warning');
}