import * as React from "react";
import { LinkDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { Icon, ProgressBar, RedirectTo, Skeleton, TextFiedWithPassword } from "../../../../Shared/Components";
import { UserSigninProps } from "../userSignin";

interface UserSigninViewProps extends ViewProperties, UserSigninProps {
    caption: string;
    button: string;
    link1: LinkDto;
    link2: LinkDto;
    buttonHandler: () => void;
    progress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    password: string;
    labelEmail: string;
    labelPassword: string;
}

const ActiveButton = (props: UserSigninViewProps): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            className="bulma-button bulma-is-light bulma-is-fullwidth"
            disabled={props.progress}
        >
            {!props.progress ? props.button : <ProgressBar size={20} />}
        </button>
    );
};

export const UserSigninView = (props: UserSigninViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "pt-32 pb-80" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <Skeleton isLoading={props.isLoading}>
                                <div className="has-text-centered">
                                    <Icon name="Account" size={3} className="account" />
                                    <p className="is-size-3 has-text-grey">
                                        {props.caption}
                                    </p>
                                </div>
                                <div className="my-5">
                                    <input
                                        required
                                        id="email"
                                        name="email"
                                        autoComplete="email"
                                        onKeyUp={props.keyHandler}
                                        onChange={props.formHandler}
                                        value={props.email}
                                        placeholder={props.labelEmail}
                                        disabled={props.progress}
                                        className="bulma-input bulma-is-link mb-4"
                                    />
                                    <TextFiedWithPassword
                                        className="mb-4"
                                        uuid="password"
                                        fullWidth={true}
                                        value={props.password}
                                        label={props.labelPassword}
                                        onKeyUp={props.keyHandler}
                                        onChange={props.formHandler}
                                        isDisabled={props.progress}
                                    />
                                </div>
                                <div className="my-5">
                                    <ActiveButton {...props} />
                                </div>
                                <div className="is-flex is-flex-direction-row my-5">
                                    <div className="is-justify-content-flex-start">
                                        <RedirectTo path={props.link1?.href} name={props.link1?.text} />
                                    </div>
                                    <div className="is-justify-content-flex-end">
                                        <RedirectTo path={props.link2?.href} name={props.link2?.text} />
                                    </div>
                                </div>
                            </Skeleton>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
