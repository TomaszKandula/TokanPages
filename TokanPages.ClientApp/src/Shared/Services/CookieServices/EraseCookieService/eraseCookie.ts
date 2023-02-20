import { EraseCookieInput } from "./interface";

export const EraseCookie = (props: EraseCookieInput) =>
{
    document.cookie = `${props.cookieName}=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
}
