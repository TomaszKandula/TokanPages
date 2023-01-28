import * as React from "react";
import { IContentCookiesPrompt } from "../../Store/States";
import { SetCookie, GetCookie } from "../../Shared/Services/CookieServices";
import { CookiesView } from "./View/cookiesView";
import Validate from "validate.js";

export const Cookies = (props: IContentCookiesPrompt): JSX.Element => 
{
    const [modalClose, setModalClose] = React.useState(false);
    const currentCookie = GetCookie({cookieName: "cookieConsent"});
    const onClickEvent = () => 
    { 
        setModalClose(true); 
        SetCookie(
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: props.content?.days,
            sameSite: "Strict",
            secure: false
        });
    };

    return (<CookiesView
        isLoading={props.isLoading}
        modalClose={modalClose}
        shouldShow={Validate.isEmpty(currentCookie)}
        caption={props.content?.caption}
        text={props.content?.text}
        onClickEvent={onClickEvent}
        buttonText={props.content?.button}
    />);
}
