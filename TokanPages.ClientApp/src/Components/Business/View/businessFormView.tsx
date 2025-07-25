import * as React from "react";
import { GET_ICONS_URL, GET_IMAGES_URL } from "../../../Api";
import { ImageDto, OfferItemDto } from "../../../Api/Models";
import {
    Icon,
    ProgressBar,
    Skeleton,
    TextArea,
    TextField,
    Notification,
    CustomImage,
    Link,
} from "../../../Shared/Components";
import { BusinessFormViewProps, ServiceItemsProps, TechStackListProps } from "../Types";
import "./businessFormView.css";
import { v4 as uuidv4 } from "uuid";

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
    props.hasTechItems ? (
        <>
            <div className="bulma-content">
                <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24}>
                    <p className="is-size-5">{props.techLabel}</p>
                </Skeleton>
                {props.list.map((value: OfferItemDto, index: number) => (
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
            {props.list.map((value: OfferItemDto, index: number) => (
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
            <p className="is-size-3	has-text-centered has-text-link">{props.caption?.toUpperCase()}</p>
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
                <p className="is-size-3 has-text-black">{props.caption}</p>
            </Skeleton>
        </div>
    ) : (
        <></>
    );

export const BusinessFormView = (props: BusinessFormViewProps): React.ReactElement => (
    <section className={props.background}>
        <div className="bulma-container">
            <div className={`bulma-columns ${!props.className ? "p-6" : props.className}`}>
                <div className="bulma-column">
                    <RenderCaption {...props} />
                    <div className="bulma-card mx-4">
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
                                                onChange={props.descriptionHandler}
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
                                    hasTechItems={props.hasTechItems}
                                    techLabel={props.techLabel}
                                    list={props.techItems}
                                    handler={props.techHandler}
                                />
                                <ServiceItems
                                    isLoading={props.isLoading}
                                    isDisabled={props.progress}
                                    caption={props.pricing.caption}
                                    list={props.serviceItems}
                                    handler={props.serviceHandler}
                                />
                                <Skeleton isLoading={props.isLoading} mode="Rect" height={80}>
                                    <Notification text={props.pricing.disclaimer} />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Rect" height={40}>
                                    <ActiveButton {...props} />
                                </Skeleton>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="bulma-column px-6">
                    <div className="is-flex my-5">
                        <figure className="bulma-image bulma-is-128x128">
                            <CustomImage
                                base={GET_IMAGES_URL}
                                source={props.presentation.image.link}
                                title={props.presentation.image.title}
                                alt={props.presentation.image.alt}
                                className="bulma-is-rounded"
                            />
                        </figure>
                        <div className="bulma-content ml-4 is-flex is-flex-direction-column is-align-self-center is-gap-0.5">
                            <div className="is-size-4 has-text-weight-bold">{props.presentation.title}</div>
                            <div className="is-size-5 has-text-weight-semibold has-text-link">
                                {props.presentation.subtitle}
                            </div>
                            <Link
                                to={props.presentation.icon.href}
                                key={uuidv4()}
                                aria-label={props.presentation.icon.name}
                            >
                                <figure className="bulma-image bulma-is-24x24">
                                    <Icon name={props.presentation.icon.name} size={1.5} />
                                </figure>
                            </Link>
                        </div>
                    </div>
                    <div className="bulma-content">
                        <p className="is-size-6">{props.presentation.description}</p>
                        <h2 className="is-size-3 my-6">{props.presentation.logos.title}</h2>
                        <div className="bulma-fixed-grid">
                            <div className="bulma-grid is-gap-7">
                                {props.presentation.logos.images.map((value: ImageDto, _index: number) => (
                                    <div className="bulma-cell is-flex is-align-self-center" key={uuidv4()}>
                                        <CustomImage
                                            base={GET_ICONS_URL}
                                            source={value.link}
                                            title={value.title}
                                            alt={value.alt}
                                            width={value.width}
                                            height={value.heigh}
                                        />
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
