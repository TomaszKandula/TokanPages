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
    const hasSnapshotMode = HasSnapshotMode();

    const [isOpen, setIsOpen] = React.useState(hasSnapshotMode);
    const [isLoading, setLoading] = React.useState(true);

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

    React.useEffect(() => {
        if (isLoading && hasContentLoadingFinished) {
            setTimeout(() => setLoading(false), 3000);
        }
    }, [isLoading, hasContentLoadingFinished]);

    return (
        <CookiesView
            isLoading={isLoading}
            modalClose={isOpen}
            shouldShow={Validate.isEmpty(currentCookie)}
            caption={cookies?.caption}
            text={cookies?.text}
            detail={cookies?.detail}
            options={cookies?.options}
            loading={cookies?.loading}
            buttons={cookies?.buttons}
            onClickEvent={onClickEvent}
        />
    );
};
