import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { SetCookie, GetCookie } from "../../../Shared/Services/CookieServices";
import { HasSnapshotMode } from "../../../Shared/Services/SpaCaching";
import { OperationStatus } from "../../../Shared/Enums";
import { ApplicationCookieView } from "./View/applicationCookieView";
import Validate from "validate.js";

export const ApplicationCookie = (): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const hasContentLoadingFinished = data?.status === OperationStatus.hasFinished;

    const cookies = data?.components?.sectionCookiesPrompt;
    const currentCookie = GetCookie({ cookieName: "cookieConsent" });
    const hasSnapshotMode = HasSnapshotMode();
    const hasCookieConsent = !Validate.isEmpty(currentCookie);

    const [isClose, setClose] = React.useState(false);
    const [isLoading, setLoading] = React.useState(true);
    const [canShowOptions, setShowOptions] = React.useState(false);
    const [hasNecessaryOnly, setNecessaryOnly] = React.useState(false);
    const [hasStatistics, setStatistics] = React.useState(true);
    const [hasMarketing, setMarketing] = React.useState(true);
    const [hasPersonalization, setPersonalization] = React.useState(true);

    const onStatisticsCheckboxEvent = React.useCallback(() => {
        setStatistics(!hasStatistics);
    }, [hasStatistics]);

    const onMarketingCheckboxEvent = React.useCallback(() => {
        setMarketing(!hasMarketing);
    }, [hasMarketing]);

    const onPersonalizationCheckboxEvent = React.useCallback(() => {
        setPersonalization(!hasPersonalization);
    }, [hasPersonalization]);

    /* ACCEPT COOKIES AND ALLOW TO COLLECT DATA */
    const onAcceptButtonEvent = React.useCallback(() => {
        setClose(true);

        const grantSelected = JSON.stringify({
            granted: {
                necessary: true,
                statistics: hasStatistics,
                marketing: hasMarketing,
                personalization: hasPersonalization,
            },
        });

        const grantBasic = JSON.stringify({
            granted: {
                necessary: true,
                statistics: false,
                marketing: false,
                personalization: false,
            },
        });

        SetCookie({
            cookieName: "cookieConsent",
            value: hasNecessaryOnly ? grantBasic : grantSelected,
            days: cookies.days,
            sameSite: "Strict",
            secure: false,
        });
    }, [cookies?.days, hasNecessaryOnly, hasStatistics, hasMarketing, hasPersonalization]);

    /* DISPLAY COOKIE SETTINGS */
    const onManageButtonEvent = React.useCallback(() => {
        setShowOptions(!canShowOptions);
    }, [canShowOptions]);

    /* CLOSE WINDOW AND DO NOT COLLECT DATA */
    const onCloseButtonEvent = React.useCallback(() => {
        if (!hasNecessaryOnly) {
            setNecessaryOnly(true);
        }
        onAcceptButtonEvent();
    }, [hasNecessaryOnly]);

    /* SWITCH FROM LOADING SCREEN TO MAIN SCREEN */
    React.useEffect(() => {
        if (hasCookieConsent) {
            return;
        }

        if (isLoading && hasContentLoadingFinished) {
            setTimeout(() => setLoading(false), 3000);
        }
    }, [hasCookieConsent, isLoading, hasContentLoadingFinished]);

    /* CLOSE WINDOW AND ACCEPT NECESSARY CONSENT */
    React.useEffect(() => {
        if (hasNecessaryOnly) {
            onAcceptButtonEvent();
        }
    }, [hasNecessaryOnly]);

    return (
        <ApplicationCookieView
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
            canShowOptions={canShowOptions}
            onAcceptButtonEvent={onAcceptButtonEvent}
            onManageButtonEvent={onManageButtonEvent}
            onCloseButtonEvent={onCloseButtonEvent}
            onStatisticsCheckboxEvent={onStatisticsCheckboxEvent}
            onMarketingCheckboxEvent={onMarketingCheckboxEvent}
            onPersonalizationCheckboxEvent={onPersonalizationCheckboxEvent}
            hasStatistics={hasStatistics}
            hasMarketing={hasMarketing}
            hasPersonalization={hasPersonalization}
        />
    );
};
