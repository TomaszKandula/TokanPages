import * as React from "react";
import { LinkDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import {
    Icon,
    ProgressBar,
    RedirectTo,
    Skeleton,
    TextField,
    TextFieldWithPassword,
} from "../../../../Shared/Components";
import { UserSigninProps } from "../userSignin";

interface UserSigninViewProps extends ViewProperties, UserSigninProps {
    isMobile: boolean;
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

const ActiveButton = (props: UserSigninViewProps): React.ReactElement => (
        <button
            type="submit"
            onClick={props.buttonHandler}
            className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
            disabled={props.progress}
        >
            {!props.progress ? props.button : <ProgressBar size={20} />}
        </button>
    );


export const UserSigninView = (props: UserSigninViewProps): React.ReactElement => (
        <section className={props.background}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "py-6" : props.className}>
                    <div className={`bulma-card ${props.isMobile ? "m-4" : ""}`}>
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
                                        isDisabled={props.progress}
                                        className="mb-4"
                                    />
                                    <TextFieldWithPassword
                                        uuid="password"
                                        value={props.password}
                                        placeholder={props.labelPassword}
                                        onKeyUp={props.keyHandler}
                                        onChange={props.formHandler}
                                        isDisabled={props.progress}
                                    />
                                </div>
                                <div className="mb-5">
                                    <ActiveButton {...props} />
                                </div>
                                <div className="is-flex is-flex-direction-row is-justify-content-space-between">
                                    <div className="my-2">
                                        <RedirectTo path={props.link1?.href} name={props.link1?.text} />
                                    </div>
                                    <div className="my-2">
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
