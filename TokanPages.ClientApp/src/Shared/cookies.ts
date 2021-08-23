import Validate from "validate.js";

export interface ISetCookie 
{
    cookieName: string, 
    value: string, 
    days: number, 
    sameSite: string, 
    secure: string,
    exact?: string    
}

export const SetCookie = (props: ISetCookie): string =>
{
    let newCookie = "";
    let date = new Date();

    if (props.days)
    {

        let dateString = props.exact;

        if (Validate.isEmpty(props.exact))
        {
            date.setTime(date.getTime() + (props.days * 24 * 60 * 60 * 1000));
            dateString = date.toUTCString();
        }

        let LSecure = !Validate.isEmpty(props.secure) ? `; ${props.secure}` : "";
        newCookie = `${props.cookieName}=${props.value}; expires=${dateString}; path=/; SameSite=${props.sameSite} ${LSecure}`;

        document.cookie = newCookie;

    }

    return newCookie;
}

export interface IGetCookie
{
    cookieName: string
}

export const GetCookie = (props: IGetCookie): string =>
{
    let cookieName = `${props.cookieName}=`;
    let cookieArray = document.cookie.split(";");

    for (let item of cookieArray)
    {
        let cookie = item;
        while (cookie.charAt(0) === " ")
        {
            cookie = cookie.substring(1, cookie.length);
        }

        if (cookie.indexOf(cookieName) === 0)
        {
            return cookie.substring(cookieName.length, cookie.length);
        }
    }

    return "";
}

export interface IEraseCookie
{
    cookieName: string
}

export const EraseCookie = (props: IEraseCookie) =>
{
    document.cookie = `${props.cookieName}=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
}
