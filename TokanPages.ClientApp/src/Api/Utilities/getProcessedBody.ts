import { ConfigurationProps } from "./Types";

export const GetProcessedBody = (configuration: ConfigurationProps): string | FormData | null => {
    const optionalBody = configuration.body ? JSON.stringify(configuration.body) : null;
    const optionalFormData = configuration.form ? configuration.form : null;
    return optionalBody !== null ? optionalBody : optionalFormData !== null ? optionalFormData : null;
};
