import { Dispatch } from "redux";
import { ApplicationLanguageAction } from "../../Store/Actions";
import { ErrorContentDto, GetContentManifestDto, LanguageItemDto } from "../../Api/Models";
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
};

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

export const GetErrorBoundaryContent = (languageId: string, errorBoundary: ErrorContentDto[]): ErrorContentDto => {
    const filtering = (value: ErrorContentDto): boolean => {
        return value.language === languageId;
    };

    const result = errorBoundary.filter(filtering)[0];
    return result;
};

export const UpdateReduxStore = (manifest: GetContentManifestDto, dispatch: Dispatch<any>): void => {
    const languages = manifest.languages;
    const boundary = manifest.errorBoundary;
    const defaultId = GetDefaultId(languages);
    const pathname = window.location.pathname;
    const paths = pathname.split("/").filter(e => String(e).trim());

    if (paths.length > 0) {
        if (paths[0] === "snapshot") {
            dispatch(
                ApplicationLanguageAction.set({
                    id: paths[1],
                    languages: languages,
                    errorBoundary: boundary,
                })
            );
        } else if (IsLanguageIdValid(paths[0], languages)) {
            dispatch(
                ApplicationLanguageAction.set({
                    id: paths[0],
                    languages: languages,
                    errorBoundary: boundary,
                })
            );
        } else if (defaultId) {
            dispatch(
                ApplicationLanguageAction.set({
                    id: defaultId,
                    languages: languages,
                    errorBoundary: boundary,
                })
            );
            const urlWithDefaultLanguageId = `${window.location.origin}/${defaultId}`;
            window.history.pushState({}, "", urlWithDefaultLanguageId);
        }
    } else if (defaultId) {
        const urlWithDefaultLanguageId = `${window.location.href}${defaultId}`;
        window.history.pushState({}, "", urlWithDefaultLanguageId);
        dispatch(
            ApplicationLanguageAction.set({
                id: defaultId,
                languages: languages,
                errorBoundary: boundary,
            })
        );
    }
};
