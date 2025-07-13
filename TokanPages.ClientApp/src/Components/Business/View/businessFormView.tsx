import * as React from "react";
import { DescriptionItemDto, PricingDto, ServiceItemDto, TechItemsDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent, ReactMouseEvent } from "../../../Shared/types";
import { Icon, ProgressBar, TextArea, TextField } from "../../../Shared/Components";
import { BusinessFormProps, ServiceItemCardProps, TechStackListProps } from "../Models";
import "./businessFormView.css";

interface BusinessFormViewProps extends ViewProperties, BusinessFormProps, FormProps {
    isMobile: boolean;
    caption: string;
    progress: boolean;
    buttonText: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    descriptionHandler: (event: ReactChangeTextEvent) => void;
    buttonHandler: () => void;
    techHandler: (event: ReactChangeEvent) => void;
    serviceHandler: (event: ReactMouseEvent) => void;
    serviceSelection: string[];
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
    techItems: TechItemsDto[];
    description: ExtendedDescriptionProps;
    pricing: PricingDto;
}

interface ExtendedDescriptionProps extends DescriptionItemDto {
    text: string;
}

const ActiveButton = (props: BusinessFormViewProps): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            disabled={props.progress}
            className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
        >
            {!props.progress ? props.buttonText : <ProgressBar size={20} />}
        </button>
    );
};

const TechStackList = (props: TechStackListProps): React.ReactElement => {
    return (
        <>
            {props.list.map((value: TechItemsDto, index: number) => (
                <div key={value.key} className="is-flex">
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
                    <p className="is-size-6 p-2">{value.value}</p>
                </div>
            ))}
        </>
    );
};

const ServiceItemCard = (props: ServiceItemCardProps) => {
    const isSelected = props.services.includes(props.value.id) ?? false;
    const style = isSelected ? "business-selected" : "business-unselected";
    const disabled = props.isDisabled ? "business-disabled" : "business-enabled";
    const className = `bulma-cell business-items ${style} ${disabled}`;

    return (
        <div id={props.value.id} data-disabled={props.isDisabled} className={className} onClick={props.handler}>
            <p className="is-size-6 business-item-text">{props.value.text}</p>
            <p className="is-size-6 has-text-weight-semibold business-item-price">{props.value.price}</p>
        </div>
    );
};

export const BusinessFormView = (props: BusinessFormViewProps): React.ReactElement => {
    const cardPadding = props.isMobile ? "px-3" : "px-6";

    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className={!props.className ? "py-6" : props.className}>
                    {props.hasCaption ? (
                        <p className="is-size-3	has-text-centered has-text-link">{props.caption?.toUpperCase()}</p>
                    ) : (
                        <></>
                    )}
                    <div className={cardPadding}>
                        <div className="bulma-card">
                            <div className="bulma-card-content">
                                {props.hasIcon ? (
                                    <div className="is-flex is-flex-direction-column is-align-items-center">
                                        <Icon name="BriefcaseVariant" size={3} className="has-text-link" />
                                        <p className="is-size-3 has-text-grey">{props.caption}</p>
                                    </div>
                                ) : (
                                    <></>
                                )}
                                <div className="mt-5">
                                    <div className="bulma-columns">
                                        <div className="bulma-column">
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
                                        </div>
                                    </div>
                                    <div className="bulma-columns">
                                        <div className="bulma-column">
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
                                        </div>
                                        <div className="bulma-column">
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
                                        </div>
                                    </div>
                                    <div className="bulma-columns">
                                        <div className="bulma-column">
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
                                        </div>
                                        <div className="bulma-column">
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
                                        </div>
                                    </div>
                                    <div className="bulma-columns">
                                        <div className="bulma-column">
                                            <TextArea
                                                required={props.description.required}
                                                isFixedSize
                                                uuid="description"
                                                isDisabled={props.progress}
                                                onChange={props.descriptionHandler}
                                                value={props.description.text}
                                                placeholder={props.description.label}
                                                rows={props.description.rows}
                                            />
                                        </div>
                                    </div>
                                    <div className="bulma-content">
                                        <p className="is-size-5 py-2">{props.techLabel}</p>
                                    </div>
                                    <div className="bulma-content">
                                        <TechStackList
                                            isDisabled={props.progress}
                                            list={props.techItems}
                                            handler={props.techHandler}
                                        />
                                    </div>
                                    <div className="bulma-content">
                                        <p className="is-size-5 py-2">{props.pricing.caption}</p>
                                    </div>
                                    <div className="bulma-grid bulma-is-col-min-10 is-gap-2.5">
                                        {props.pricing.services.map((value: ServiceItemDto, _index: number) => (
                                            <ServiceItemCard
                                                key={value.id}
                                                value={value}
                                                isDisabled={props.progress}
                                                handler={props.serviceHandler}
                                                services={props.serviceSelection}
                                            />
                                        ))}
                                    </div>
                                    <div className="bulma-notification is-flex is-align-items-center">
                                        <Icon name="Information" size={1} className="has-text-link" />
                                        <span className="is-size-6 p-3">{props.pricing.disclaimer}</span>
                                    </div>
                                    <ActiveButton {...props} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
