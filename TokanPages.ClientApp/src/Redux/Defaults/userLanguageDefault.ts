import { IUserLanguage } from "../../Redux/States/userLanguageState";
import { GetUserLanguageFromStore } from "../../Shared/Services/languageService";

const preservedId = GetUserLanguageFromStore();
const languageId = preservedId === "" ? "eng" : preservedId;

export const UserLanguageDefault: IUserLanguage = 
{
    id: languageId
}
