import { EraseCookieProps } from "../Types";

export const EraseCookie = (props: EraseCookieProps): void => {
    document.cookie = `${props.cookieName}=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
};
