import * as React from "react";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { Animated, Icon, ProgressBar, Skeleton, TextArea, TextField } from "../../../Shared/Components";
import { ContactFormProps } from "../contactForm";
import "./contactFormView.css";

interface ContactFormViewProps extends ViewProperties, ContactFormProps {
    isMobile: boolean;
    caption: string;
    text: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    messageHandler: (event: ReactChangeTextEvent) => void;
    firstName: string;
    lastName: string;
    email: string;
    subject: string;
    message: string;
    buttonHandler: () => void;
    progress: boolean;
    buttonText: string;
    consent: string;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelSubject: string;
    labelMessage: string;
    minRows?: number;
}

const ActiveButton = (props: ContactFormViewProps): React.ReactElement => (
        <button
            type="submit"
            onClick={props.buttonHandler}
            disabled={props.progress}
            className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
        >
            {!props.progress ? props.buttonText : <ProgressBar size={20} />}
        </button>
    );

export const ContactFormView = (props: ContactFormViewProps): React.ReactElement => {
    const boxPadding = props.isMobile ? "py-6" : "p-6";
    const cardPadding = props.isMobile ? "" : "px-6";
    const colPadding = props.hasIcon ? "pt-5" : "";

    return (
        <section className={props.background}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className={!props.className ? boxPadding : props.className}>
                    {props.hasCaption ? (
                        <Animated dataAos="fade-down">
                            <Skeleton isLoading={props.isLoading} mode="Text" height={40}>
                            <p className="is-size-3	has-text-centered has-text-link">{props.caption?.toUpperCase()}</p>
                            </Skeleton>
                        </Animated>
                    ) : null}
                    <div className={cardPadding}>
                        <div className={`bulma-card ${!props.hasShadow ? "contact-card-no-shadow" : ""}`}>
                            <div className="bulma-card-content background-colour-inherited">
                                {props.hasIcon ? (
                                    <div className="is-flex is-flex-direction-column is-align-items-center">
                                        <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                                            <Icon name="CardAccountMail" size={3} className="has-text-link" />
                                        </Skeleton>
                                        <Skeleton isLoading={props.isLoading} mode="Text">
                                            <p className="is-size-3 has-text-grey">{props.caption}</p>
                                        </Skeleton>
                                    </div>
                                ) : null}
                                <div className={colPadding}>
                                    <div className="bulma-columns">
                                        <div className="bulma-column">
                                            <Animated dataAos="zoom-in">
                                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                                <TextField
                                                    required
                                                    uuid="firstName"
                                                    autoComplete="fname"
                                                    onKeyUp={props.keyHandler}
                                                    onChange={props.formHandler}
                                                    value={props.firstName}
                                                    placeholder={props.labelFirstName}
                                                />
                                                </Skeleton>
                                            </Animated>
                                        </div>
                                        <div className="bulma-column">
                                            <Animated dataAos="zoom-in">
                                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                                <TextField
                                                    required
                                                    uuid="lastName"
                                                    autoComplete="lname"
                                                    onKeyUp={props.keyHandler}
                                                    onChange={props.formHandler}
                                                    value={props.lastName}
                                                    placeholder={props.labelLastName}
                                                />
                                                </Skeleton>
                                            </Animated>
                                        </div>
                                    </div>
                                    <div className="bulma-columns">
                                        <div className="bulma-column">
                                            <Animated dataAos="zoom-in">
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
                                            </Animated>
                                        </div>
                                    </div>
                                    <div className="bulma-columns">
                                        <div className="bulma-column">
                                            <Animated dataAos="zoom-in">
                                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                                <TextField
                                                    required
                                                    uuid="subject"
                                                    autoComplete="subject"
                                                    onKeyUp={props.keyHandler}
                                                    onChange={props.formHandler}
                                                    value={props.subject}
                                                    placeholder={props.labelSubject}
                                                />
                                                </Skeleton>
                                            </Animated>
                                        </div>
                                    </div>
                                    <div className="bulma-columns">
                                        <div className="bulma-column">
                                            <Animated dataAos="zoom-in">
                                                <Skeleton isLoading={props.isLoading} mode="Rect" height={200}>
                                                <TextArea
                                                    isFixedSize
                                                    required
                                                    rows={props.minRows}
                                                    uuid="message"
                                                    autoComplete="message"
                                                    onChange={props.messageHandler}
                                                    value={props.message}
                                                    placeholder={props.labelMessage}
                                                />
                                                </Skeleton>
                                            </Animated>
                                        </div>
                                    </div>
                                    <div className="bulma-content">
                                        <Skeleton isLoading={props.isLoading} mode="Text" height={30}>
                                        <div className="bulma-notification">
                                            <p className="is-size-6">{props.consent}</p>
                                        </div>
                                        </Skeleton>
                                        <Animated dataAos="fade-up">
                                            <Skeleton isLoading={props.isLoading} mode="Rect">
                                                <ActiveButton {...props} />
                                            </Skeleton>
                                        </Animated>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
