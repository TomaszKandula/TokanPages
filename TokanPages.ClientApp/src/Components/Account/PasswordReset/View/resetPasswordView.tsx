import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { Icon, ProgressBar, Skeleton, TextField } from "../../../../Shared/Components";
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
            className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
            disabled={props.progress || props.email.length === 0}
        >
            {!props.progress ? props.button : <ProgressBar size={20} />}
        </button>
    );
};

export const PasswordResetView = (props: Properties): React.ReactElement => {
    return (
        <section className={props.background}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "py-6" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <Skeleton isLoading={props.isLoading}>
                                <div className="is-flex is-flex-direction-column is-align-items-center">
                                    <Icon name="AccountCircle" size={3} className="has-text-link" />
                                    <p className="is-size-3 has-text-grey">{props.caption}</p>
                                </div>
                                <div className="my-5">
                                    <TextField
                                        required
                                        uuid="email"
                                        autoComplete="email"
                                        onKeyUp={props.keyHandler}
                                        onChange={props.formHandler}
                                        value={props.email}
                                        placeholder={props.labelEmail}
                                    />
                                </div>
                                <div className="my-5">
                                    <ActiveButton {...props} />
                                </div>
                            </Skeleton>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
