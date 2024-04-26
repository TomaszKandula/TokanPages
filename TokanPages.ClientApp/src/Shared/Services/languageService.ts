import { useDispatch } from "react-redux";
import { ApplicationLanguageAction } from "../../Store/Actions";
import { GetContentManifestDto, LanguageItemDto } from "../../Api/Models";
import { SELECTED_LANGUAGE } from "../constants";
import { GetDataFromStorage } from "./StorageServices";
import Validate from "validate.js";

const GetDefaultId = (items: LanguageItemDto[]): string | undefined => {
    for (let index in items) {
        if (items[index].isDefault) {
            return items[index].id;
        }
    }

    return undefined;
};

const IsLanguageIdValid = (id: string, items: LanguageItemDto[]): boolean => {
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

export const MapToPayULanguageId = (iso: string): string => {
    const currencyIso = iso.toLowerCase();
    switch (currencyIso) {
        case "pol":
            return "pl";
        case "ukr":
            return "uk";
        case "eng":
            return "en";
        case "fra":
            return "fr";
        case "spa":
            return "es";
        case "ger":
            return "de";

        default:
            return "pl";
    }
};

export const UpdateUserLanguage = (manifest: GetContentManifestDto | undefined): void => {
    if (manifest === undefined) return;

    const languages = manifest.languages;
    const defaultId = GetDefaultId(languages);

    if (defaultId === undefined) {
        throw new Error("Cannot find the default language ID.");
    }

    const preservedId = GetDataFromStorage({ key: SELECTED_LANGUAGE }) as string;
    const languageId = IsLanguageIdValid(preservedId, languages) ? preservedId : defaultId;

    const dispatch = useDispatch();
    dispatch(ApplicationLanguageAction.set({ id: languageId, languages: languages }));
};
