import { useDispatch } from "react-redux";
import { ActionCreators } from "../../Redux/Actions/userLanguageAction";
import { IGetContentManifestDto, ILanguageItem } from "../../Api/Models";
import { SELECTED_LANGUAGE } from "../../Shared/constants";
import { GetDataFromStorage } from "./StorageServices";
import Validate from "validate.js";

const GetDefaultId = (items: ILanguageItem[]): string => 
{
    let result = "eng";
    items.forEach(items => 
    {
        if (items.isDefault)
        {
            result = items.id
        }
    });
    
    return result;
}

export const UpdateUserLanguage = (manifest: IGetContentManifestDto): void => 
{
    const defaultId = GetDefaultId(manifest.languages);
    const preservedId = GetDataFromStorage({ key: SELECTED_LANGUAGE }) as string;
    const languageId = Validate.isEmpty(preservedId) ? defaultId : preservedId;

    const dispatch = useDispatch();    
    dispatch(ActionCreators.setLanguage({ id: languageId, languages: manifest.languages }));
}
