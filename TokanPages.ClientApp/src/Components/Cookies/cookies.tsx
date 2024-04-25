import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { SetCookie, GetCookie } from "../../Shared/Services/CookieServices";
import { CookiesView } from "./View/cookiesView";
import Validate from "validate.js";

export const Cookies = (): JSX.Element => {
    const cookies = useSelector((state: ApplicationState) => state.contentCookiesPrompt);

    const [isOpen, setIsOpen] = React.useState(false);
    const currentCookie = GetCookie({ cookieName: "cookieConsent" });
    const onClickEvent = React.useCallback(() => {
        setIsOpen(true);
        SetCookie({
            cookieName: "cookieConsent",
            value: "granted",
            days: cookies.content?.days,
            sameSite: "Strict",
            secure: false,
        });
    }, [cookies.content?.days]);

    return (
        <CookiesView
            isLoading={cookies.isLoading}
            modalClose={isOpen}
            shouldShow={Validate.isEmpty(currentCookie)}
            caption={cookies.content?.caption}
            text={cookies.content?.text}
            onClickEvent={onClickEvent}
            buttonText={cookies.content?.button}
        />
    );
};
