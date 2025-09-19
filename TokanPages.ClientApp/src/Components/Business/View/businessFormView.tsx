import * as React from "react";
import {
    Icon,
    ProgressBar,
    Skeleton,
    TextArea,
    TextField,
    Notification,
    PresentationView,
} from "../../../Shared/Components";
import { BusinessFormViewProps, OfferItemProps, ServiceItemsProps, TechStackListProps } from "../Types";
import "./businessFormView.css";

const ActiveButton = (props: BusinessFormViewProps): React.ReactElement => (
    <button
        type="submit"
        onClick={props.buttonHandler}
        disabled={props.progress}
        className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
    >
        {!props.progress ? props.buttonText : <ProgressBar size={20} />}
    </button>
);

const TechStackList = (props: TechStackListProps): React.ReactElement =>
    props.canDisplay ? (
        <>
            <div className="bulma-content">
                <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24}>
                    <p className="is-size-5">{props.caption}</p>
                </Skeleton>
                {props.items.map((value: OfferItemProps, index: number) => (
                    <div key={value.key} className="is-flex">
                        <Skeleton isLoading={props.isLoading} mode="Text" width={150} height={24}>
                            <div className="checkbox-wrapper-1 is-flex is-align-self-center">
                                <input
                                    type="checkbox"
                                    id={`${index}`}
                                    name={`tech-${index}`}
                                    disabled={props.isDisabled}
                                    onChange={props.handler}
                                    checked={value.isChecked}
                                    tabIndex={-1}
                                    className="substituted"
                                />
                                <label htmlFor={`${index}`} className="is-clickable"></label>
                            </div>
                            <p className="is-size-6 p-1">{value.value}</p>
                        </Skeleton>
                    </div>
                ))}
            </div>
        </>
    ) : (
        <></>
    );

const ServiceItems = (props: ServiceItemsProps): React.ReactElement => (
    <>
        <div className="bulma-content">
            <Skeleton isLoading={props.isLoading} mode="Text" width={300} height={24}>
                <p className="is-size-5">{props.caption}</p>
            </Skeleton>
            {props.items.map((value: OfferItemProps, index: number) => (
                <div key={value.key} className="is-flex">
                    <Skeleton isLoading={props.isLoading} mode="Rect" height={100}>
                        <div className="checkbox-wrapper-1 is-flex is-align-self-center">
                            <input
                                type="checkbox"
                                id={`${index}`}
                                name={`service-${index}`}
                                disabled={props.isDisabled}
                                onChange={props.handler}
                                checked={value.isChecked}
                                tabIndex={-1}
                                className="substituted"
                            />
                            <label htmlFor={`${index}`} className="is-clickable"></label>
                        </div>
                        <p className="is-size-6 p-1">{value.value}</p>
                    </Skeleton>
                </div>
            ))}
        </div>
    </>
);

const RenderCaption = (props: BusinessFormViewProps): React.ReactElement =>
    props.hasCaption ? (
        <Skeleton isLoading={props.isLoading} mode="Text" height={40}>
            <h2 className="is-size-3	has-text-centered has-text-link">{props.caption?.toUpperCase()}</h2>
        </Skeleton>
    ) : (
        <></>
    );

const RenderHeader = (props: BusinessFormViewProps): React.ReactElement =>
    props.hasIcon ? (
        <div className="is-flex is-flex-direction-column is-align-items-center">
            <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                <Icon name="Briefcase" size={2.5} className="card-icon-colour" />
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                <h2 className="is-size-3 has-text-black">{props.caption}</h2>
            </Skeleton>
        </div>
    ) : (
        <></>
    );

export const BusinessFormView = (props: BusinessFormViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container">
            <div className="bulma-columns mx-4 my-6">
                <div className="bulma-column bulma-is-half p-0">
                    <RenderCaption {...props} />
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <RenderHeader {...props} />
                            <div className="mt-5">
                                <div className="bulma-columns">
                                    <div className="bulma-column">
                                        <Skeleton isLoading={props.isLoading} mode="Rect">
                                            <TextField
                                                required
                                                uuid="company"
                                                isDisabled={props.progress}
                                                maxLength={255}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.companyText}
                                                placeholder={props.companyLabel}
                                            />
                                        </Skeleton>
                                    </div>
                                </div>
                                <div className="bulma-columns">
                                    <div className="bulma-column">
                                        <Skeleton isLoading={props.isLoading} mode="Rect">
                                            <TextField
                                                required
                                                uuid="firstName"
                                                isDisabled={props.progress}
                                                maxLength={255}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.firstNameText}
                                                placeholder={props.firstNameLabel}
                                            />
                                        </Skeleton>
                                    </div>
                                    <div className="bulma-column">
                                        <Skeleton isLoading={props.isLoading} mode="Rect">
                                            <TextField
                                                required
                                                uuid="lastName"
                                                isDisabled={props.progress}
                                                maxLength={255}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.lastNameText}
                                                placeholder={props.lastNameLabel}
                                            />
                                        </Skeleton>
                                    </div>
                                </div>
                                <div className="bulma-columns">
                                    <div className="bulma-column">
                                        <Skeleton isLoading={props.isLoading} mode="Rect">
                                            <TextField
                                                required
                                                uuid="email"
                                                isDisabled={props.progress}
                                                maxLength={255}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.emailText}
                                                placeholder={props.emailLabel}
                                            />
                                        </Skeleton>
                                    </div>
                                    <div className="bulma-column">
                                        <Skeleton isLoading={props.isLoading} mode="Rect">
                                            <TextField
                                                required
                                                uuid="phone"
                                                isDisabled={props.progress}
                                                maxLength={17}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.phoneText}
                                                placeholder={props.phoneLabel}
                                            />
                                        </Skeleton>
                                    </div>
                                </div>
                                <div className="bulma-columns">
                                    <div className="bulma-column">
                                        <Skeleton isLoading={props.isLoading} mode="Rect" height={300}>
                                            <TextArea
                                                required={props.description.required}
                                                uuid="description"
                                                isDisabled={props.progress}
                                                onChange={props.description.handler}
                                                value={props.description.text}
                                                placeholder={props.description.label}
                                                rows={props.description.rows}
                                            />
                                        </Skeleton>
                                    </div>
                                </div>
                                <TechStackList
                                    isLoading={props.isLoading}
                                    isDisabled={props.progress}
                                    canDisplay={props.technology.canDisplay}
                                    caption={props.technology.caption}
                                    items={props.technology.items}
                                    handler={props.technology.handler}
                                />
                                <ServiceItems
                                    isLoading={props.isLoading}
                                    isDisabled={props.progress}
                                    caption={props.pricing.caption}
                                    items={props.pricing.items}
                                    handler={props.pricing.handler}
                                />
                                <Skeleton isLoading={props.isLoading} mode="Rect" height={80}>
                                    <Notification text={props.pricing.disclaimer} hasIcon />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Rect" height={40}>
                                    <ActiveButton {...props} />
                                </Skeleton>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="bulma-column business-margins pt-0">
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
