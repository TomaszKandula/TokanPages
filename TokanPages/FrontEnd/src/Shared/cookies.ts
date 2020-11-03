interface ISetCookie 
{
    ACookieName: string, 
    AValue: string, 
    ADays: number, 
    ASameSite: string, 
    ASecure: string    
}

function SetCookie(props: ISetCookie) 
{

    const IsEmpty = (AValue: string) => 
    {
        return typeof !AValue.trim() || typeof AValue === undefined || AValue === null;        
    }

    let LDate = new Date();
    if (props.ADays)
    {

        // We set time in miliseconds
        LDate.setTime(LDate.getTime() + (props.ADays * 24 * 60 * 60 * 1000));

        let LSecure = !IsEmpty(props.ASecure) ? `; ${props.ASecure}` : "";
        let LNewCookie = `${props.ACookieName}=${props.AValue}; expires=${LDate.toUTCString()}; path=/; SameSite=${props.ASameSite} ${LSecure}`;

        document.cookie = LNewCookie;

    }

}

interface IGetCookie
{
    ACookieName: string
}

function GetCookie(props: IGetCookie)
{

    let LCookieName = `${props.ACookieName}=`;
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

    return null;    

}

interface IEraseCookie
{
    ACookieName: string
}

function EraseCookie(props: IEraseCookie)
{
    document.cookie = `${props.ACookieName}=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
}

export 
{
    SetCookie,
    GetCookie,
    EraseCookie
}
