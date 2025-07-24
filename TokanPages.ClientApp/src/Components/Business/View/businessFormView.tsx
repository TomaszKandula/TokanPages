import * as React from "react";
import { DescriptionItemDto, PricingDto, OfferItemDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { Icon, ProgressBar, Skeleton, TextArea, TextField } from "../../../Shared/Components";
import { BusinessFormProps, ServiceItemsProps, TechStackListProps } from "../Models";
import "./businessFormView.css";

interface BusinessFormViewProps extends ViewProperties, BusinessFormProps, FormProps {
    isMobile: boolean;
    caption: string;
    progress: boolean;
    buttonText: string;
    hasTechItems: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    descriptionHandler: (event: ReactChangeTextEvent) => void;
    buttonHandler: () => void;
    techHandler: (event: ReactChangeEvent) => void;
    serviceHandler: (event: ReactChangeEvent) => void;
}

interface FormProps {
    companyText: string;
    companyLabel: string;
    firstNameText: string;
    firstNameLabel: string;
    lastNameText: string;
    lastNameLabel: string;
    emailText: string;
    emailLabel: string;
    phoneText: string;
    phoneLabel: string;
    techLabel: string;
    techItems: OfferItemDto[];
    serviceItems: OfferItemDto[];
    description: ExtendedDescriptionProps;
    pricing: PricingDto;
}

interface ExtendedDescriptionProps extends DescriptionItemDto {
    text: string;
}

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

const TechStackList = (props: TechStackListProps): React.ReactElement => (
    props.hasTechItems ?
    <>
        <div className="bulma-content">
            <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24}>
                <p className="is-size-5 py-2">{props.techLabel}</p>
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
    </> : <></>
);

const ServiceItems = (props: ServiceItemsProps): React.ReactElement => (
    <>
        <div className="bulma-content">
            <Skeleton isLoading={props.isLoading} mode="Text" width={300} height={24}>
                <p className="is-size-5 py-2">{props.caption}</p>
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
                {/* <span>({value.price})</span> */}
            </Skeleton>
            </div>
        ))}
        </div>
    </>
);

const RenderCaption = (props: BusinessFormViewProps): React.ReactElement => (
    props.hasCaption ? (
        <Skeleton isLoading={props.isLoading} mode="Text" height={40}>
            <p className="is-size-3	has-text-centered has-text-link">{props.caption?.toUpperCase()}</p>
        </Skeleton>
    ) : (
        <></>
    )
);

const RenderHeader = (props: BusinessFormViewProps): React.ReactElement => (
    props.hasIcon ? (
        <div className="is-flex is-flex-direction-column is-align-items-center">
            <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                <Icon name="BriefcaseVariant" size={3} className="has-text-link" />
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                <p className="is-size-3 has-text-grey">{props.caption}</p>
            </Skeleton>
        </div>
    ) : (
        <></>
    )
);

const RendetTaxNotification = (props: BusinessFormViewProps): React.ReactElement => (
    <div className="bulma-notification is-flex is-align-items-center">
        <Icon name="Information" size={1} className="has-text-link" />
        <span className="is-size-6 p-2">{props.pricing.disclaimer}</span>
    </div>
);

export const BusinessFormView = (props: BusinessFormViewProps): React.ReactElement => (
        <section className={props.background}>
            <div className="bulma-container">
                <div className="bulma-columns">
                    <div className="bulma-column">
                        <div className={!props.className ? "py-6" : props.className}>
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
                                            <RendetTaxNotification {...props} />
                                        </Skeleton>
                                        <Skeleton isLoading={props.isLoading} mode="Rect" height={40}>
                                            <ActiveButton {...props} />
                                        </Skeleton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="bulma-column">
                        <div className={!props.className ? "py-6" : props.className}>
                            
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
