import { useDispatch } from "react-redux";
import { ApplicationLanguageAction } from "../../Store/Actions";
import { GetContentManifestDto, LanguageItemDto } from "../../Api/Models";
import Validate from "validate.js";

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

export const IsLanguageIdValid = (id: string, items: LanguageItemDto[]): boolean => {
    if (Validate.isEmpty(id)) {
        return false;
    }

    for (let index in items) {
        if (items[index].id === id) {
            return true;
        }
    }

    return false;
};

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

    const dispatch = useDispatch();
    const languages = manifest.languages;
    const defaultId = GetDefaultId(languages);
    const pathname = window.location.pathname;
    const paths = pathname.split("/").filter(e => String(e).trim());

    if (paths.length > 0 && IsLanguageIdValid(paths[0], languages)) {
        dispatch(ApplicationLanguageAction.set({ id: paths[0], languages: languages }));
    } else if (defaultId) {
        const urlWithDefaultLanguageId = `${window.location.href}${defaultId}`;
        window.history.pushState({}, "", urlWithDefaultLanguageId);
        dispatch(ApplicationLanguageAction.set({ id: defaultId, languages: languages }));
    }
};
