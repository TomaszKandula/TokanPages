export const MapLanguage = (input: string, type: string): string => {
    switch (input.toLowerCase()) {
        case "eng":
            return `en.${type}`;
        case "fra":
            return `fr.${type}`;
        case "ger":
            return `de.${type}`;
        case "pol":
            return `pl.${type}`;
        case "esp":
            return `es.${type}`;
        case "ukr":
            return `uk.${type}`;
        default:
            return `en.${type}`;
    }
};
