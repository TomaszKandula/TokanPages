import { useDispatch } from "react-redux";
import { ApplicationLanguageAction } from "../../Store/Actions";
import { GetContentManifestDto, LanguageItemDto } from "../../Api/Models";

export const MapLanguageId = (input: string): string => {
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
}

export const GetDefaultId = (items: LanguageItemDto[]): string | undefined => {
    for (let index in items) {
        if (items[index].isDefault) {
            return items[index].id;
        }
    }

    return undefined;
};

export const UpdateUserLanguage = (manifest: GetContentManifestDto | undefined): void => {
    if (manifest === undefined) return;

    const languages = manifest.languages;
    const defaultId = GetDefaultId(languages);

    if (defaultId !== undefined) {
        const dispatch = useDispatch();
        dispatch(ApplicationLanguageAction.set({ id: defaultId, languages: languages }));
    }
};
