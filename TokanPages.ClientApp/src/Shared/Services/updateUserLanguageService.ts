import { useDispatch } from "react-redux";
import { ApplicationLanguage } from "../../Store/Actions";
import { IGetContentManifestDto, ILanguageItem } from "../../Api/Models";
import { SELECTED_LANGUAGE } from "../../Shared/constants";
import { GetDataFromStorage } from "./StorageServices";
import Validate from "validate.js";

const GetDefaultId = (items: ILanguageItem[]): string | undefined => 
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

const IsLanguageIdValid = (id: string, items: ILanguageItem[]): boolean => 
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

export const UpdateUserLanguage = (manifest: IGetContentManifestDto): void => 
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
    dispatch(ApplicationLanguage.set({ id: languageId, languages: languages }));
}
