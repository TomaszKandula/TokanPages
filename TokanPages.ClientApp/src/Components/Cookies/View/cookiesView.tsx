import * as React from "react";
import { Backdrop } from "@material-ui/core";
import { ButtonsDto, OptionsDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { GetDateTime } from "../../../Shared/Services/Formatters";
import { ReactChangeEvent } from "../../../Shared/types";

interface Properties extends ViewProperties {
    isClose: boolean;
    hasSnapshotMode: boolean;
    hasCookieConsent: boolean;
    caption: string;
    text: string;
    detail: string;
    loading: string[];
    options: OptionsDto;
    buttons: ButtonsDto;
    canShowOptions: boolean;
    onAcceptButtonEvent: () => void;
    onManageButtonEvent: () => void;
    onCloseButtonEvent: () => void;
    onStatisticsCheckboxEvent: (event: ReactChangeEvent) => void;
    onMarketingCheckboxEvent: (event: ReactChangeEvent) => void;
    onPersonalizationCheckboxEvent: (event: ReactChangeEvent) => void;
    hasStatistics: boolean;
    hasMarketing: boolean;
    hasPersonalization: boolean;
}

const CookieWindowOptions = (props: Properties): React.ReactElement => {
    return (
        <div className="cookie-window-options-list">
            <label className="cookie-window-checkbox pointer-not-allowed">
                <input 
                    type="checkbox" 
                    className="cookie-window-checkbox-input" 
                    disabled={true} 
                    checked={true}
                />
                <span className="cookie-window-checkbox-visual-input"></span>
                <span className="cookie-window-checkbox-label">
                    {props.options?.necessaryLabel}
                </span>
            </label>
            <label className="cookie-window-checkbox">
                <input 
                    type="checkbox" 
                    className="cookie-window-checkbox-input"
                    onChange={props.onStatisticsCheckboxEvent}
                    checked={props.hasStatistics}
                />
                <span className="cookie-window-checkbox-visual-input"></span>
                <span className="cookie-window-checkbox-label">
                    {props.options?.statisticsLabel}
                </span>
            </label>
            <label className="cookie-window-checkbox">
                <input 
                    type="checkbox" 
                    className="cookie-window-checkbox-input"
                    onChange={props.onMarketingCheckboxEvent}
                    checked={props.hasMarketing}
                />
                <span className="cookie-window-checkbox-visual-input"></span>
                <span className="cookie-window-checkbox-label">
                    {props.options?.marketingLabel}
                </span>
            </label>
            <label className="cookie-window-checkbox">
                <input 
                    type="checkbox" 
                    className="cookie-window-checkbox-input"
                    onChange={props.onPersonalizationCheckboxEvent}
                    checked={props.hasPersonalization}
                />
                <span className="cookie-window-checkbox-visual-input"></span>
                <span className="cookie-window-checkbox-label">
                    {props.options?.personalizationLabel}
                </span>
            </label>
        </div>
    );
}

const CookieWindowActions = (props: Properties): React.ReactElement => {
    return (
        <div className="cookie-window-actions">
            {props.buttons?.acceptButton.enabled 
            ? <button 
                className="cookie-window-button cookie-window-button-accent cookie-window-action" 
                onClick={props.onAcceptButtonEvent}
            >
                {props.buttons?.acceptButton.label}
            </button>
            : <></>}
            {props.buttons?.manageButton.enabled 
            ? <button 
                className="cookie-window-button cookie-window-action"
                onClick={props.onManageButtonEvent}
            >
                {props.buttons?.manageButton.label}
            </button>
            : <></>}
            {props.buttons?.closeButton.enabled 
            ? <button 
                className="cookie-window-button"
                onClick={props.onCloseButtonEvent}
            >
                {props.buttons?.closeButton.label}
            </button>
            : null}
        </div>
    );
}

const CookieWindowPrompt = (props: Properties): React.ReactElement => {
    return (
        <div className="cookie-window">
            <div className="cookie-window-caption">
                {props.caption}
            </div>
            <div className="cookie-window-box">
                <div className="cookie-window-section">
                    <p className="cookie-window-section-text">
                        {props.text}
                    </p>
                </div>
                <div className="cookie-window-section">
                    <p className="cookie-window-section-detail">
                        {props.detail}
                    </p>
                    {props.options?.enabled && props.canShowOptions ? <CookieWindowOptions {...props} /> : null}
                    <CookieWindowActions {...props} />
                </div>
            </div>
        </div>
    );
}

const CookieWindowLoading = (props: Properties): React.ReactElement => {
    const maxLength = props.loading?.length-1;
    const dateTime = new Date().toString();
    const formattedDateTime = GetDateTime({ value: dateTime, hasTimeVisible: true });
    return (
        <div className="cookie-window">
            <div className="cookie-window-caption">
                {props.caption}
            </div>
            <div className="cookie-window-box">
                <div className="cookie-window-section cookie-window-section-fixed-height cookie-window-section-left">
                    {props.loading?.map((value: string, index: number) => (
                    <p className="cookie-window-section-text cookie-window-section-left" key={index} >
                        {value.replace("{DT}", formattedDateTime)}
                        {maxLength === index ? <span className="cookie-window-caret"></span> : null}
                    </p>
                    ))}
                </div>
            </div>
        </div>
    );
}

const CookieWindowContainer = (props: Properties): React.ReactElement => { 
    const style = props.isClose ? "cookie-window-close" : "cookie-window-open";
    const transition = props.isLoading ? undefined : 0;
    return (
        <div className={style}>
            <Backdrop className="backdrop" open={true} transitionDuration={transition}>
                {props.isLoading ? <CookieWindowLoading {...props} /> : <CookieWindowPrompt {...props} />}
            </Backdrop>
        </div>
    )
};

export const CookiesView = (props: Properties): React.ReactElement => {
    if (props.hasSnapshotMode) {
        return <div className="cookie-window-open"></div>;
    }

    if (props.hasCookieConsent) {
        return <></>;
    }

    if (props.loading?.length === 0) {
        return <></>;
    }

    return <CookieWindowContainer {...props} />;
}
