import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { Icon, ProgressBar, Skeleton, TextField } from "../../../../Shared/Components";
import { PasswordResetProps } from "../passwordReset";

interface Properties extends ViewProperties, PasswordResetProps {
    isMobile: boolean;
    progress: boolean;
    caption: string;
    button: string;
    email: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    labelEmail: string;
}

const ActiveButton = (props: Properties): React.ReactElement => (
    <button
        type="submit"
        onClick={props.buttonHandler}
        className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
        disabled={props.progress || props.email.length === 0}
    >
        {!props.progress ? props.button : <ProgressBar size={20} />}
    </button>
);

export const PasswordResetView = (props: Properties): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet">
            <div className="py-6">
                <div className={`bulma-card ${props.isMobile ? "mx-4" : ""}`}>
                    <div className="bulma-card-content">
                        <div className="is-flex is-flex-direction-column is-align-items-center">
                            <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                                <Icon name="AccountCircle" size={3.75} className="card-icon-colour" />
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                <h2 className="is-size-3 has-text-black">{props.caption}</h2>
                            </Skeleton>
                        </div>
                        <div className="my-5">
                            <Skeleton isLoading={props.isLoading} mode="Rect">
                                <TextField
                                    required
                                    uuid="email"
                                    autoComplete="email"
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    value={props.email}
                                    placeholder={props.labelEmail}
                                />
                            </Skeleton>
                        </div>
                        <div className="my-5">
                            <Skeleton isLoading={props.isLoading} mode="Rect">
                                <ActiveButton {...props} />
                            </Skeleton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
);
