export const MapLanguage = (input: string): string => {
    switch (input.toLowerCase()) {
        case "eng":
            return "en.png";
        case "fra":
            return "fr.png";
        case "ger":
            return "de.png";
        case "pol":
            return "pl.png";
        case "esp":
            return "es.png";
        case "ukr":
            return "uk.png";
        default:
            return "en.png";
    }
};
