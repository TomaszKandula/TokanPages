import * as React from "react";
import { LinkDto, WarningPasswordDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import {
    RedirectTo,
    RenderParagraphs,
    RenderList,
    TextField,
    TextFieldWithPassword,
    Icon,
    ProgressBar,
    Skeleton,
} from "../../../../Shared/Components";
import { UserSignupProps } from "../userSignup";

interface UserSignupViewProps extends ViewProperties, UserSignupProps {
    isMobile: boolean;
    caption: string;
    warning: WarningPasswordDto;
    consent: string;
    button: string;
    link: LinkDto;
    buttonHandler: () => void;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    progress: boolean;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    terms?: boolean;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelPassword: string;
}

const ActiveButton = (props: UserSignupViewProps): React.ReactElement => (
    <button
        type="submit"
        onClick={props.buttonHandler}
        className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
        disabled={props.progress}
    >
        {!props.progress ? props.button : <ProgressBar size={20} />}
    </button>
);

export const UserSignupView = (props: UserSignupViewProps): React.ReactElement => (
    <section className={props.background}>
        <div className="bulma-container bulma-is-max-tablet">
            <div className={!props.className ? "py-6" : props.className}>
                <div className={`bulma-card ${props.isMobile ? "m-4" : ""}`}>
                    <div className="bulma-card-content">
                            <div className="is-flex is-flex-direction-column is-align-items-center">
                                <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                                    <Icon name="AccountCircle" size={3} className="has-text-link" />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Text">
                                    <p className="is-size-3 has-text-grey">{props.caption}</p>
                                </Skeleton>
                            </div>
                            <div className="my-5">
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                <TextField
                                    required
                                    uuid="firstName"
                                    autoComplete="one-time-code"
                                    autoFocus={true}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    value={props.firstName}
                                    placeholder={props.labelFirstName}
                                    isDisabled={props.progress}
                                    className="mb-3"
                                />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                <TextField
                                    required
                                    uuid="lastName"
                                    autoComplete="one-time-code"
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    value={props.lastName}
                                    placeholder={props.labelLastName}
                                    isDisabled={props.progress}
                                    className="mb-3"
                                />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                <TextField
                                    required
                                    uuid="email"
                                    autoComplete="one-time-code"
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    value={props.email}
                                    placeholder={props.labelEmail}
                                    isDisabled={props.progress}
                                    className="mb-3"
                                />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                <TextFieldWithPassword
                                    uuid="password"
                                    value={props.password}
                                    placeholder={props.labelPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    isDisabled={props.progress}
                                />
                                </Skeleton>
                            </div>
                            <Skeleton isLoading={props.isLoading} mode="Rect" height={300}>
                            <article className="bulma-message bulma-is-info">
                                <div className="bulma-message-header">
                                    <p>{props.warning?.textPre}</p>
                                </div>
                                <div className="bulma-message-body bulma-content">
                                    <RenderList list={props.warning?.textList} className="" />
                                    <RenderParagraphs text={props.warning?.textPost} className="" />
                                    <a href={props.warning?.textNist?.href} target="_blank" rel="noopener nofollow">
                                        {props.warning?.textNist?.text}
                                    </a>
                                </div>
                            </article>
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" height={30}>
                                <div className="bulma-notification">
                                    <p className="is-size-6">{props.consent}</p>
                                </div>
                            </Skeleton>
                            <div className="mb-5">
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                    <ActiveButton {...props} />
                                </Skeleton>
                            </div>
                            <div className="has-text-right">
                                <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={30}>
                                    <RedirectTo path={props.link?.href} name={props.link?.text} />
                                </Skeleton>
                            </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
);
