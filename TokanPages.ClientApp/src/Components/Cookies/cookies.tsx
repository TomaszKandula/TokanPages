import * as React from "react";
import { IGetCookiesPromptContent } from "../../Redux/States/Content/getCookiesPromptContentState";
import { SetCookie, GetCookie } from "../../Shared/cookies";
import Validate from "validate.js";
import CookiesView from "./cookiesView";

const Cookies = (props: IGetCookiesPromptContent): JSX.Element => 
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
            secure: ""
        });
    };

    return (<CookiesView bind=
    {{
        modalClose: modalClose,
        shouldShow: Validate.isEmpty(currentCookie),
        caption: props.content?.caption,
        text: props.content?.text,
        onClickEvent: onClickEvent,
        buttonText: props.content?.button
    }}/>);
}

export default Cookies;
