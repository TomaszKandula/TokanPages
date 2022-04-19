import { IGetCookie } from "./interface";

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
