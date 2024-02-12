import * as React from "react";
import { ContentCookiesPromptState } from "../../Store/States";
import { SetCookie, GetCookie } from "../../Shared/Services/CookieServices";
import { CookiesView } from "./View/cookiesView";
import Validate from "validate.js";

export const Cookies = (props: ContentCookiesPromptState): JSX.Element => {
    const [isOpen, setIsOpen] = React.useState(false);
    const currentCookie = GetCookie({ cookieName: "cookieConsent" });
    const onClickEvent = React.useCallback(() => {
        setIsOpen(true);
        SetCookie({
            cookieName: "cookieConsent",
            value: "granted",
            days: props.content?.days,
            sameSite: "Strict",
            secure: false,
        });
    }, [props.content?.days]);

    return (
        <CookiesView
            isLoading={props.isLoading}
            modalClose={isOpen}
            shouldShow={Validate.isEmpty(currentCookie)}
            caption={props.content?.caption}
            text={props.content?.text}
            onClickEvent={onClickEvent}
            buttonText={props.content?.button}
        />
    );
};
