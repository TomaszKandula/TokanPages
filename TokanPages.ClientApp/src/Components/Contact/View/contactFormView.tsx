import * as React from "react";
import { PresentationDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../../Shared/types";
import {
    Animated,
    Icon,
    ProgressBar,
    Skeleton,
    TextArea,
    TextField,
    Notification,
    PresentationView,
} from "../../../Shared/Components";
import { ContactFormProps } from "../contactForm";
import "./contactFormView.css";

interface ContactFormViewProps extends ViewProperties, ContactFormProps {
    caption: string;
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
    presentation: PresentationDto;
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

const RenderCaption = (props: ContactFormViewProps): React.ReactElement | null =>
    props.hasCaption ? (
        <Animated dataAos="fade-down">
            <Skeleton isLoading={props.isLoading} mode="Text" height={40}>
                <h2 className="is-size-3 has-text-centered has-text-link pb-5">{props.caption?.toUpperCase()}</h2>
            </Skeleton>
        </Animated>
    ) : null;

const RenderHeader = (props: ContactFormViewProps): React.ReactElement | null =>
    props.hasIcon ? (
        <div className="is-flex is-flex-direction-column is-align-items-center">
            <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                <Icon name="CardAccountMail" size={2.5} className="card-icon-colour" />
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                <h2 className="is-size-3 has-text-black">{props.caption}</h2>
            </Skeleton>
        </div>
    ) : null;

export const PageContactFormView = (props: ContactFormViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container pb-6">
            <div className="bulma-columns mx-4 my-6">
                <div className="bulma-column bulma-is-half p-0">
                    <RenderCaption {...props} />
                    <div className={`bulma-card ${!props.hasShadow ? "contact-form-card-no-shadow" : ""}`}>
                        <div className="bulma-card-content background-colour-inherited">
                            <RenderHeader {...props} />
                            <div className={props.hasIcon ? "pt-5" : ""}>
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
                                                    isDisabled={props.progress}
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
                                                    isDisabled={props.progress}
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
                                                    isDisabled={props.progress}
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
                                                    isDisabled={props.progress}
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
                                                    isDisabled={props.progress}
                                                />
                                            </Skeleton>
                                        </Animated>
                                    </div>
                                </div>
                                <div className="bulma-content">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={30}>
                                        <Notification text={props.consent} hasIcon />
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
                <div className="bulma-column contact-form-view-column-margins pt-0">
                    <PresentationView
                        isLoading={props.isLoading}
                        title={props.presentation.title}
                        subtitle={props.presentation.subtitle}
                        description={props.presentation.description}
                        image={{
                            link: props.presentation.image.link,
                            title: props.presentation.image.title,
                            alt: props.presentation.image.alt,
                            width: props.presentation.image.width,
                            heigh: props.presentation.image.heigh,
                        }}
                        icon={{
                            name: props.presentation.icon.name,
                            href: props.presentation.icon.href,
                        }}
                        logos={{
                            title: props.presentation.logos.title,
                            images: props.presentation.logos.images,
                        }}
                    />
                </div>
            </div>
        </div>
    </section>
);

export const SectionContactFormView = (props: ContactFormViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet">
            <div className="py-6">
                <RenderCaption {...props} />
                <div
                    className={`bulma-card ${props.hasCaption ? "" : "contact-form-view-margins"} ${!props.hasShadow ? "contact-form-card-no-shadow" : ""}`}
                >
                    <div className="bulma-card-content background-colour-inherited">
                        <RenderHeader {...props} />
                        <div className={props.hasIcon ? "pt-5" : ""}>
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
                                                isDisabled={props.progress}
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
                                                isDisabled={props.progress}
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
                                                isDisabled={props.progress}
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
                                                isDisabled={props.progress}
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
                                                isDisabled={props.progress}
                                            />
                                        </Skeleton>
                                    </Animated>
                                </div>
                            </div>
                            <div className="bulma-content">
                                <Skeleton isLoading={props.isLoading} mode="Text" height={30}>
                                    <Notification text={props.consent} hasIcon />
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
    </section>
);

export const ContactFormView = (props: ContactFormViewProps): React.ReactElement => {
    return props.mode === "page" ? <PageContactFormView {...props} /> : <SectionContactFormView {...props} />;
};
