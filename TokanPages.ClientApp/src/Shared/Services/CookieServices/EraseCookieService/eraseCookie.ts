import { IEraseCookie } from "./interface";

export const EraseCookie = (props: IEraseCookie) =>
{
    document.cookie = `${props.cookieName}=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
}
