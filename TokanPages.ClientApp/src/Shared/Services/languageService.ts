import * as CookieHelper from "../Services/CookieServices";

const LanguageSelection = "userLanguage";

export interface ILanguage
{
    id: string;
    name: string;
    isDefault: boolean
}

export const GetDefaultLanguageId = (): string => 
{
    let result = GetUserLanguage();
    if (result === "")
    {
        const languages = GetLanguages();

        for (let item of languages)
        {
            if (item.isDefault)
            {
                result = item.id;
                break;
            }
        }
    }

    return result;
}

export const GetLanguages = (): ILanguage[] => 
{
    return [
        { 
            id: "eng", 
            name: "English", 
            isDefault: true 
        },
        { 
            id: "pol", 
            name: "Polski", 
            isDefault: false 
        },
        { 
            id: "esp", 
            name: "Español", 
            isDefault: false 
        },
        { 
            id: "ukr", 
            name: "україна", 
            isDefault: false 
        }
    ];
}

export const GetUserLanguage = (): string => 
{
    return CookieHelper.GetCookie({ cookieName: LanguageSelection });
}

export const SetUserLanguage = (value: string): void => 
{
    CookieHelper.SetCookie({ 
        cookieName: LanguageSelection, 
        value: value, 
        days: 14, 
        sameSite: "None", 
        secure: false 
    });
}
