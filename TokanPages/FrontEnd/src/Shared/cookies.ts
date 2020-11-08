import { IsEmpty } from "./helpers"; 

interface ISetCookie 
{
    cookieName: string, 
    value: string, 
    days: number, 
    sameSite: string, 
    secure: string,
    exact?: string    
}

function SetCookie(props: ISetCookie): string
{
   
    let LNewCookie = "";
    let LDate = new Date();

    if (props.days)
    {

        let dateString = props.exact;

        if (IsEmpty(props.exact))
        {
            LDate.setTime(LDate.getTime() + (props.days * 24 * 60 * 60 * 1000));
            dateString = LDate.toUTCString();
        }

        let LSecure = !IsEmpty(props.secure) ? `; ${props.secure}` : "";
        LNewCookie = `${props.cookieName}=${props.value}; expires=${dateString}; path=/; SameSite=${props.sameSite} ${LSecure}`;

        document.cookie = LNewCookie;

    }

    return LNewCookie;

}

interface IGetCookie
{
    cookieName: string
}

function GetCookie(props: IGetCookie): string
{

    let LCookieName = `${props.cookieName}=`;
    let LCookieArray = document.cookie.split(";");

    for (let Index = 0; Index < LCookieArray.length; Index++)
    {

        let LCookie = LCookieArray[Index];

        while (LCookie.charAt(0) === " ")
        {
            LCookie = LCookie.substring(1, LCookie.length);
        }

        if (LCookie.indexOf(LCookieName) === 0)
        {
            return LCookie.substring(LCookieName.length, LCookie.length);
        }

    }

    return "";

}

interface IEraseCookie
{
    cookieName: string
}

function EraseCookie(props: IEraseCookie)
{
    document.cookie = `${props.cookieName}=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
}

export 
{
    SetCookie,
    GetCookie,
    EraseCookie
}
