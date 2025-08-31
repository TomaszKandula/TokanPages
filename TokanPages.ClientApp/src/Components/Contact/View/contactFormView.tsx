import * as React from "react";
import { GET_ICONS_URL, GET_IMAGES_URL } from "../../../Api";
import { ImageDto, PresentationDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { Animated, Icon, ProgressBar, Skeleton, TextArea, TextField, Notification, CustomImage, Link } from "../../../Shared/Components";
import { ContactFormProps } from "../contactForm";
import "./contactFormView.css";
import { v4 as uuidv4 } from "uuid";

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

export const PageContactFormView = (props: ContactFormViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container">
            <div className="bulma-columns mx-4 my-6">
                <div className="bulma-column bulma-is-half p-0">

                {props.hasCaption ? (
                    <Animated dataAos="fade-down">
                        <Skeleton isLoading={props.isLoading} mode="Text" height={40}>
                            <p className="is-size-3	has-text-centered has-text-link pb-5">
                                {props.caption?.toUpperCase()}
                            </p>
                        </Skeleton>
                    </Animated>
                ) : null}
                <div
                    className={`bulma-card ${props.hasCaption ? "" : "contact-form-view-margins"} ${!props.hasShadow ? "contact-card-no-shadow" : ""}`}
                >
                    <div className="bulma-card-content background-colour-inherited">
                        {props.hasIcon ? (
                            <div className="is-flex is-flex-direction-column is-align-items-center">
                                <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                                    <Icon name="CardAccountMail" size={2.5} className="card-icon-colour" />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                    <p className="is-size-3 has-text-black">{props.caption}</p>
                                </Skeleton>
                            </div>
                        ) : null}
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
                <div className="bulma-column business-margins">

                    <div className="is-flex my-5">
                        <Skeleton isLoading={props.isLoading} mode="Circle" width={128} height={128}>
                            <figure className="bulma-image bulma-is-128x128">
                                <CustomImage
                                    base={GET_IMAGES_URL}
                                    source={props.presentation.image.link}
                                    title={props.presentation.image.title}
                                    alt={props.presentation.image.alt}
                                    className="bulma-is-rounded"
                                />
                            </figure>
                        </Skeleton>
                        <div className="bulma-content ml-4 is-flex is-flex-direction-column is-align-self-center is-gap-0.5">
                            <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24}>
                                <div className="is-size-4 has-text-weight-bold">{props.presentation.title}</div>
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24}>
                                <div className="is-size-5 has-text-weight-semibold has-text-link">
                                    {props.presentation.subtitle}
                                </div>
                            </Skeleton>
                            <Link
                                to={props.presentation.icon.href}
                                key={uuidv4()}
                                aria-label={props.presentation.icon.name}
                            >
                                <Skeleton isLoading={props.isLoading} mode="Rect" width={24} height={24}>
                                    <figure className="bulma-image bulma-is-24x24">
                                        <Icon name={props.presentation.icon.name} size={1.5} />
                                    </figure>
                                </Skeleton>
                            </Link>
                        </div>
                    </div>
                    <div className="bulma-content">
                        <Skeleton isLoading={props.isLoading} mode="Text" width={500} height={40}>
                            <p className="is-size-6">{props.presentation.description}</p>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} mode="Text" width={250} height={40} className="my-6">
                            <h2 className="is-size-3 my-6">{props.presentation.logos.title}</h2>
                        </Skeleton>
                        <div className="bulma-fixed-grid">
                            <div className="bulma-grid is-gap-7">
                                {props.presentation.logos.images.map((value: ImageDto, _index: number) => (
                                    <div
                                        className="bulma-cell is-flex is-justify-content-center is-align-self-center"
                                        key={uuidv4()}
                                    >
                                        <Skeleton
                                            isLoading={props.isLoading}
                                            mode="Rect"
                                            width={value.width}
                                            height={value.heigh}
                                        >
                                            <CustomImage
                                                base={GET_ICONS_URL}
                                                source={value.link}
                                                title={value.title}
                                                alt={value.alt}
                                                width={value.width}
                                                height={value.heigh}
                                            />
                                        </Skeleton>
                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </section>
);

export const SectionContactFormView = (props: ContactFormViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet">
            <div className="py-6">
                {props.hasCaption ? (
                    <Animated dataAos="fade-down">
                        <Skeleton isLoading={props.isLoading} mode="Text" height={40}>
                            <p className="is-size-3	has-text-centered has-text-link pb-5">
                                {props.caption?.toUpperCase()}
                            </p>
                        </Skeleton>
                    </Animated>
                ) : null}
                <div
                    className={`bulma-card ${props.hasCaption ? "" : "contact-form-view-margins"} ${!props.hasShadow ? "contact-card-no-shadow" : ""}`}
                >
                    <div className="bulma-card-content background-colour-inherited">
                        {props.hasIcon ? (
                            <div className="is-flex is-flex-direction-column is-align-items-center">
                                <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                                    <Icon name="CardAccountMail" size={2.5} className="card-icon-colour" />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                    <p className="is-size-3 has-text-black">{props.caption}</p>
                                </Skeleton>
                            </div>
                        ) : null}
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
}
