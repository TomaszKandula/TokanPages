import { IUserLanguage } from "../../Redux/States/userLanguageState";
import { GetUserLanguage } from "../../Shared/Services/languageService";

const preservedId = GetUserLanguage();
const languageId = preservedId === "" ? "eng" : preservedId;

export const UserLanguageDefault: IUserLanguage = 
{
    id: languageId
}
