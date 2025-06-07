import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { Icon, ProgressBar } from "../../../../Shared/Components";
import { PasswordResetProps } from "../passwordReset";

interface Properties extends ViewProperties, PasswordResetProps {
    progress: boolean;
    caption: string;
    button: string;
    email: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    labelEmail: string;
}

const ActiveButton = (props: Properties): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            className={`bulma-button ${props.isLoading ? "bulma-is-skeleton" : ""} bulma-is-light bulma-is-fullwidth`}
            disabled={props.progress || props.email.length === 0}
        >
            {!props.progress ? props.button : <ProgressBar size={20} />}
        </button>
    );
};

export const PasswordResetView = (props: Properties): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "pt-96 pb-80" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <div className="has-text-centered my-3">
                                <Icon name="Account" size={3} className="account" />
                                <p className={`is-size-3 ${props.isLoading ? "bulma-is-skeleton" : "has-text-grey"}`}>
                                    {props.caption}
                                </p>
                            </div>
                            <div className={`my-5 ${props.isLoading ? "bulma-is-skeleton" : ""}`}>
                                <input
                                    required
                                    id="email"
                                    name="email"
                                    autoComplete="email"
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    value={props.email}
                                    placeholder={props.labelEmail}
                                    className="bulma-input bulma-is-link"
                                />
                            </div>
                            <div className="my-5">
                                <ActiveButton {...props} />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
