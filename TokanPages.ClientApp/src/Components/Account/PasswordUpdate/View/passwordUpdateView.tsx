import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { Icon, ProgressBar, Skeleton, TextFieldWithPassword } from "../../../../Shared/Components";
import { PasswordUpdateProps } from "../passwordUpdate";

interface Properties extends ViewProperties, PasswordUpdateProps {
    isMobile: boolean;
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

const ActiveButton = (props: Properties): React.ReactElement => (
    <button
        type="submit"
        onClick={props.buttonHandler}
        className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
        disabled={props.progress || props.disableForm}
    >
        {!props.progress ? props.button : <ProgressBar size={20} />}
    </button>
);

export const PasswordUpdateView = (props: Properties): React.ReactElement => (
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
                                <p className="is-size-3 has-text-black">{props.caption}</p>
                            </Skeleton>
                        </div>
                        <div className="my-5">
                            <Skeleton isLoading={props.isLoading} mode="Rect">
                                <TextFieldWithPassword
                                    className="pb-4"
                                    uuid="newPassword"
                                    value={props.newPassword}
                                    placeholder={props.labelNewPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    isDisabled={props.disableForm || props.progress}
                                />
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Rect">
                                <TextFieldWithPassword
                                    className="pb-4"
                                    uuid="verifyPassword"
                                    value={props.verifyPassword}
                                    placeholder={props.labelVerifyPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    isDisabled={props.disableForm || props.progress}
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
