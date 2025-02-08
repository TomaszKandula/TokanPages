import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { SetCookie, GetCookie } from "../../Shared/Services/CookieServices";
import { HasSnapshotMode } from "../../Shared/Services/SpaCaching";
import { OperationStatus } from "../../Shared/enums";
import { CookiesView } from "./View/cookiesView";
import Validate from "validate.js";

export const Cookies = (): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const hasContentLoadingFinished = data?.status === OperationStatus.hasFinished;

    const cookies = data?.components?.cookiesPrompt;
    const currentCookie = GetCookie({ cookieName: "cookieConsent" });
    const hasSnapshotMode = HasSnapshotMode();
    const hasCookieConsent = !Validate.isEmpty(currentCookie);

    const [isClose, setClose] = React.useState(false);
    const [isLoading, setLoading] = React.useState(true);

    /* ACCEPT COOKIES AND ALLOW TO COLLECT DATA */
    const onAcceptButtonEvent = React.useCallback(() => {
        setClose(true);
        SetCookie({
            cookieName: "cookieConsent",
            value: "granted",
            days: cookies.days,
            sameSite: "Strict",
            secure: false,
        });
    }, [cookies?.days]);

    /* DISPLAY COOKIE SETTINGS */
    const onManageButtonEvent = React.useCallback(() => {
        //TODO: add implementation.
    }, []);

    /* CLOSE WINDOW AND DO NOT COLLECT DATA */
    const onCloseButtonEvent = React.useCallback(() => {
        //TODO: add implementation.
    }, []);

    /* SWITCH FROM LOADING SCREEN TO MAIN SCREEN */
    React.useEffect(() => {
        if (isLoading && hasContentLoadingFinished) {
            setTimeout(() => setLoading(false), 3000);
        }
    }, [isLoading, hasContentLoadingFinished]);

    return (
        <CookiesView
            isLoading={isLoading}
            isClose={isClose}
            hasSnapshotMode={hasSnapshotMode}
            hasCookieConsent={hasCookieConsent}
            caption={cookies?.caption}
            text={cookies?.text}
            detail={cookies?.detail}
            options={cookies?.options}
            loading={cookies?.loading}
            buttons={cookies?.buttons}
            onAcceptButtonEvent={onAcceptButtonEvent}
            onManageButtonEvent={onManageButtonEvent}
            onCloseButtonEvent={onCloseButtonEvent}
        />
    );
};
