interface Properties {
    cookieName: string;
}

export const EraseCookie = (props: Properties) => {
    document.cookie = `${props.cookieName}=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
};
