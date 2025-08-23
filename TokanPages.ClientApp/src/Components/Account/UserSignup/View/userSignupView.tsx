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
    Notification,
} from "../../../../Shared/Components";
import { UserSignupProps } from "../userSignup";
import "./userSignupView.css";

interface UserSignupViewProps extends ViewProperties, UserSignupProps {
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

interface RenderSignupCardProps extends UserSignupViewProps {
    className: string;
}

interface RenderNotificationProps extends UserSignupViewProps {
    className: string;
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

const RenderSignupCard = (props: RenderSignupCardProps) => (
    <div className="bulma-card user-signup-view-card">
        <div className="bulma-card-content">
            <div className="is-flex is-flex-direction-column is-align-items-center">
                <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                    <Icon name="AccountCircle" size={3.75} className="card-icon-colour" />
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text">
                    <p className="is-size-3 has-text-black">{props.caption}</p>
                </Skeleton>
            </div>
            <div className="my-5">
                <div className="bulma-columns">
                    <div className="bulma-column pb-0">
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
                    </div>
                    <div className="bulma-column pb-0">
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
                    </div>
                </div>
                <div className="bulma-columns">
                    <div className="bulma-column pb-0">
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
                    </div>
                </div>
                <div className="bulma-columns">
                    <div className="bulma-column">
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
                </div>
            </div>
            <Skeleton isLoading={props.isLoading} mode="Text" height={30}>
                <Notification text={props.consent} />
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
);

const RenderNotification = (props: RenderNotificationProps) => (
    <article className="bulma-message bulma-is-info">
        <div className="bulma-message-header">
            <Skeleton isLoading={props.isLoading} mode="Text" height={24} width={150}>
                <p>{props.warning?.textPre}</p>
            </Skeleton>
        </div>
        <div className="bulma-message-body bulma-content">
            <Skeleton isLoading={props.isLoading} mode="Text" height={24} width={250} className="my-3">
                <RenderList list={props.warning?.textList} />
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" height={24} width={300} className="my-3">
                <RenderParagraphs text={props.warning?.textPost} />
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" height={24} width={350} className="my-3">
                <a href={props.warning?.textNist?.href} target="_blank" rel="noopener nofollow">
                    {props.warning?.textNist?.text}
                </a>
            </Skeleton>
        </div>
    </article>
);

export const UserSignupView = (props: UserSignupViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container">
            <div className="bulma-columns mx-4 my-6 is-gap-4.5">
                <div className="bulma-column is-flex is-justify-content-center user-signup-view-column py-0 px-2">
                    <RenderSignupCard {...props} className="bulma-card is-flex is-flex-direction-column" />
                </div>
                <div className="bulma-column is-flex is-justify-content-center is-align-items-center py-0 px-2">
                    <RenderNotification {...props} className="bulma-card is-flex is-flex-direction-column" />
                </div>
            </div>
        </div>
    </section>
);
