import Validate from "validate.js";

interface Properties {
    cookieName: string;
    value: string;
    days: number;
    sameSite: string;
    secure: boolean;
    exact?: string;
}

export const SetCookie = (props: Properties): string => {
    let newCookie = "";
    let date = new Date();

    if (props.days === 0) return newCookie;

    let dateString = props.exact;

    if (Validate.isEmpty(props.exact)) {
        date.setTime(date.getTime() + props.days * 24 * 60 * 60 * 1000);
        dateString = date.toUTCString();
    }

    let secure = props.secure ? "Secure;" : "";
    if (props.sameSite === "None") secure = "Secure;";

    const sameSite = !Validate.isEmpty(props.sameSite) ? `${props.sameSite};` : "";

    newCookie =
        `${props.cookieName}=${props.value}; expires=${dateString}; path=/; SameSite=${sameSite} ${secure}`.trim();
    document.cookie = newCookie;

    return newCookie;
};
