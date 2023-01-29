import { useDispatch, useSelector } from "react-redux";
import { ApplicationLanguageAction } from "../../Store/Actions";
import { ApplicationState } from "../../Store/Configuration";
import { GetContentManifestDto, LanguageItemDto } from "../../Api/Models";
import { SELECTED_LANGUAGE } from "../../Shared/constants";
import { GetDataFromStorage } from "./StorageServices";
import Validate from "validate.js";

const GetDefaultId = (items: LanguageItemDto[]): string | undefined => 
{
    for(let index in items) 
    {
        if (items[index].isDefault)
        {
            return items[index].id
        }
    };

    return undefined;
}

const IsLanguageIdValid = (id: string, items: LanguageItemDto[]): boolean => 
{
    if (Validate.isEmpty(id)) 
    {
        return false;
    }

    for(let index in items)
    {
        if (items[index].id === id)
        {
            return true;
        }
    }

    return false;
}

export const UpdateUserLanguage = (manifest: GetContentManifestDto): void => 
{
    const languages = manifest.languages;
    const defaultId = GetDefaultId(languages);

    if (defaultId === undefined)
    {
        throw new Error("Cannot find the default language ID.");
    }

    const preservedId = GetDataFromStorage({ key: SELECTED_LANGUAGE }) as string;
    const languageId = IsLanguageIdValid(preservedId, languages) ? preservedId : defaultId;

    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    if (language === undefined) 
    {
        return;
    }

    if (language.id !== languageId)
    {
        dispatch(ApplicationLanguageAction.set({ id: languageId, languages: languages }));
    }
}
