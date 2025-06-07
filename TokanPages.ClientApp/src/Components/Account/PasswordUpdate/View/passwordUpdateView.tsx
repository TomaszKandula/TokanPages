import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { Icon, ProgressBar, TextFiedWithPassword } from "../../../../Shared/Components";
import { PasswordUpdateProps } from "../passwordUpdate";

interface Properties extends ViewProperties, PasswordUpdateProps {
    progress: boolean;
    caption: string;
    button: string;
    newPassword: string;
    verifyPassword: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    disableForm: boolean;
    labelNewPassword: string;
    labelVerifyPassword: string;
}

const ActiveButton = (props: Properties): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            className={`bulma-button ${props.isLoading ? "bulma-is-skeleton" : ""} bulma-is-light bulma-is-fullwidth`}
            disabled={props.progress || props.disableForm}
        >
            {!props.progress ? props.button : <ProgressBar size={20} />}
        </button>
    );
};

export const PasswordUpdateView = (props: Properties): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "pt-96 pb-80" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <div className="has-text-centered">
                                <Icon name="Account" size={3} className="account" />
                                <p className={`is-size-3 ${props.isLoading ? "bulma-is-skeleton" : "has-text-grey"}`}>
                                    {props.caption}
                                </p>
                            </div>
                            <div className="my-5">
                                <TextFiedWithPassword
                                    className="pb-4"
                                    uuid="newPassword"
                                    fullWidth={true}
                                    value={props.newPassword}
                                    label={props.labelNewPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    isDisabled={props.disableForm || props.progress}
                                />
                                <TextFiedWithPassword
                                    className="pb-4"
                                    uuid="verifyPassword"
                                    fullWidth={true}
                                    value={props.verifyPassword}
                                    label={props.labelVerifyPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    isDisabled={props.disableForm || props.progress}
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
