import * as React from "react";
import { Backdrop } from "@material-ui/core";
import { ButtonsDto, OptionsDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { GetDateTime } from "../../../Shared/Services/Formatters";

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
    onClickEvent: () => void;
}

const Options = (props: Properties): React.ReactElement => {
    return (
        <div className="cookie-window-options-list">
            <label className="cookie-window-checkbox">
                <input type="checkbox" className="cookie-window-checkbox-input" disabled={true} checked={true} />
                <span className="cookie-window-checkbox-visual-input"></span>
                <span className="cookie-window-checkbox-label">
                    {props.options?.necessaryLabel}
                </span>
            </label>
            <label className="cookie-window-checkbox">
                <input type="checkbox" className="cookie-window-checkbox-input" />
                <span className="cookie-window-checkbox-visual-input"></span>
                <span className="cookie-window-checkbox-label">
                    {props.options?.statisticsLabel}
                </span>
            </label>
            <label className="cookie-window-checkbox">
                <input type="checkbox" className="cookie-window-checkbox-input" />
                <span className="cookie-window-checkbox-visual-input"></span>
                <span className="cookie-window-checkbox-label">
                    {props.options?.marketingLabel}
                </span>
            </label>
            <label className="cookie-window-checkbox">
                <input type="checkbox" className="cookie-window-checkbox-input" />
                <span className="cookie-window-checkbox-visual-input"></span>
                <span className="cookie-window-checkbox-label">
                    {props.options?.personalizationLabel}
                </span>
            </label>
        </div>
    );
}

const Actions = (props: Properties): React.ReactElement => {
    return (
        <div className="cookie-window-actions">
            {props.buttons?.acceptButton.enabled 
            ? <button className="cookie-window-button cookie-window-button-accent cookie-window-action" onClick={props.onClickEvent}>
                {props.buttons?.acceptButton.label}
            </button>
            : <></>}
            {props.buttons?.manageButton.enabled 
            ? <button className="cookie-window-button cookie-window-action">
                {props.buttons?.manageButton.label}
            </button>
            : <></>}
            {props.buttons?.closeButton.enabled 
            ? <button className="cookie-window-button cookie-window__close">
                {props.buttons?.closeButton.label}
            </button>
            : null}
        </div>
    );
}

const CookieWindow = (props: Properties): React.ReactElement => {
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
                    {props.options?.enabled ? <Options {...props} /> : null}
                    <Actions {...props} />
                </div>
            </div>
        </div>
    );
}

const RenderCookieLoading = (props: Properties): React.ReactElement => {
    const maxLength = props.loading?.length-1;
    const dateTime = new Date().toString();
    const formattedDateTime = GetDateTime({ value: dateTime, hasTimeVisible: true });
    return (
        <div className="cookie-window">
            <div className="cookie-window-caption">
                {props.caption}
            </div>
            <div className="cookie-window-box">
                <div className="cookie-window-section" style={{ height: "300px", textAlign: "left" }}>
                    {props.loading?.map((value: string, index: number) => (
                    <p className="cookie-window-section-text" style={{ textAlign: "left" }} key={index} >
                        {value.replace("{DT}", formattedDateTime)}
                        {maxLength === index ? <span className="cookie-window-caret"></span> : null}
                    </p>
                    ))}
                </div>
            </div>
        </div>
    );
}

export const CookiesView = (props: Properties): React.ReactElement => {
    const ModalCookieWindow = (): React.ReactElement => { 
        return (
            <Backdrop 
                className="backdrop" 
                open={!props.isClose}
                transitionDuration={props.isLoading ? undefined : 0}
            >
                {props.isLoading ? <RenderCookieLoading {...props} /> : <CookieWindow {...props} />}
            </Backdrop>
        )
    };

    if (props.hasSnapshotMode) {
        return <></>;
    }

    if (props.hasCookieConsent) {
        return <></>;
    }

    if (props.loading?.length === 0) {
        return <></>;
    }

    return <ModalCookieWindow />;
}
