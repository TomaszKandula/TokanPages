import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { SetCookie, GetCookie } from "../../Shared/Services/CookieServices";
import { HasSnapshotMode } from "../../Shared/Services/SpaCaching";
import { CookiesView } from "./View/cookiesView";
import Validate from "validate.js";

export const Cookies = (): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const cookies = data?.components?.cookiesPrompt;
    const hasSnapshotMode = HasSnapshotMode();

    const [isOpen, setIsOpen] = React.useState(hasSnapshotMode);
    const currentCookie = GetCookie({ cookieName: "cookieConsent" });
    const onClickEvent = React.useCallback(() => {
        setIsOpen(true);
        SetCookie({
            cookieName: "cookieConsent",
            value: "granted",
            days: cookies.days,
            sameSite: "Strict",
            secure: false,
        });
    }, [cookies?.days]);

    return (
        <CookiesView
            isLoading={data?.isLoading}
            modalClose={isOpen}
            shouldShow={Validate.isEmpty(currentCookie)}
            caption={cookies?.caption}
            text={cookies?.text}
            detail={cookies?.detail}
            options={cookies?.options}
            buttons={cookies?.buttons}
            onClickEvent={onClickEvent}
        />
    );
};
