import * as React from "react";
import { Backdrop } from "@material-ui/core";
import { ButtonsDto, OptionsDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";

interface Properties extends ViewProperties {
    modalClose: boolean;
    shouldShow: boolean;
    caption: string;
    text: string;
    detail: string;
    options: OptionsDto;
    buttons: ButtonsDto;
    onClickEvent: () => void;
}

export const CookiesView = (props: Properties): React.ReactElement => {
    const style = props.modalClose ? "cookies-close" : "cookies-open";

    const renderBox = () => {
        return (
            <div className={style}>
                <Backdrop className="backdrop" open={props.shouldShow}>
                </Backdrop>
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
                            {props.options?.enabled 
                            ? <div className="cookie-window-options-list">
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
                            : null}
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
		                </div>
	                </div>
                </div>
            </div>
        );
    }

    return <>{props.shouldShow ? renderBox() : null}</>;
};
